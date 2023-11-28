#pragma once

#include <stdio.h>
//인터페이스: 멤버를 가질수없고, 가상함수만 가지는 클래스.
__interface Op
//class Op
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