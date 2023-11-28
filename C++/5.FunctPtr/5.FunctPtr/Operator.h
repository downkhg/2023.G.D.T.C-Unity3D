#pragma once

#include <stdio.h>
//인터페이스: 멤버를 가질수없고, 가상함수만 가지는 클래스.
__interface COp
//class Op
{
public:
	virtual int Operator(int a, int b) = 0;
};

class CAdd :public COp
{
public:
	int Operator(int a, int b) { return a + b; };
};

class CSub :public  COp
{
public:
	int Operator(int a, int b) { return a - b; };
};

class CMulti :public  COp
{
public:
	int Operator(int a, int b) { return a * b; };
};

class CDiv :public  COp
{
public:
	int Operator(int a, int b) { return a / b; };
};

void VirtualMain();