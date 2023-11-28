#pragma once

#include <stdio.h>

class Op
{
public:
	virtual int Operator(int a, int b) = 0;
};

class Add :public Op
{
public:
	int Operator(int a, int b) { return a + b; };
};

class Sub :public  Op
{
public:
	int Operator(int a, int b) { return a - b; };
};

class Multi :public  Op
{
public:
	int Operator(int a, int b) { return a * b; };
};

class Div :public  Op
{
public:
	int Operator(int a, int b) { return a / b; };
};

void VirtualMain();