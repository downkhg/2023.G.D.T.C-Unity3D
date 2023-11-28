#pragma once

#include <stdio.h>
//�������̽�: ����� ����������, �����Լ��� ������ Ŭ����.
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