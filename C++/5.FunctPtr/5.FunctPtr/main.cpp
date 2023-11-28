#include <stdio.h>
#include "Operator.h"

void VirtualMain()
{
	COp* pOperator;
	int nDataA;
	int nDataB;
	char cInput;
	//두수를 더하고 중간에 문자로 연산자를 받는다.
	printf("Input sample(1+1):");
	scanf("%d%c%d", &nDataA, &cInput, &nDataB);
	//받은 연산자에 따라 함수포인터에 할당되는 함수가 달라지게한다.
	switch (cInput)
	{
	case '+':
		pOperator = new CAdd();
		break;
	case '-':
		pOperator = new CSub();
		break;
	case '*':
		pOperator = new CMulti();
		break;
	case '/':
		pOperator = new CDiv();
		break;
	default:
		break;
	}
	//함수포인터에 매개변수를 지정하고 리턴값을 출력한다.
	printf("= %d\n", pOperator->Operator(nDataA, nDataB));
	delete pOperator;
}

//사칙연산 함수를 정의한다.
int Add(int a, int b) { return a + b; }
int Sub(int a, int b) { return a - b; }
int Multi(int a, int b) { return a * b; }
int Divide(int a, int b) { return a / b; }
//사칙연산 계산기 만들기
//사칙연산함수를 만들어 두수와 연산자를 입력받고, 입력받은 연산자에 따라 함수포인터에 할당되는 함수의 값을 할당하고, 두수를 함수포인터를 이용하여 계산한다.

void TestMain()
{
	int (*pfnOprater)(int, int);//함수포인터를 활용하여 사칙연산에 해당하는 함수를 받는다.
	int nDataA;
	int nDataB;
	char cInput;
	//두수를 더하고 중간에 문자로 연산자를 받는다.
	printf("Input sample(1+1):");
	scanf("%d%c%d", &nDataA, &cInput, &nDataB);
	//받은 연산자에 따라 함수포인터에 할당되는 함수가 달라지게한다.
	switch (cInput)
	{
	case '+':
		pfnOprater = Add;
		break;
	case '-':
		pfnOprater = Sub;
		break;
	case '*':
		pfnOprater = Multi;
		break;
	case '/':
		pfnOprater = Divide;
		break;
	default:
		break;
	}
	//함수포인터에 매개변수를 지정하고 리턴값을 출력한다.
	printf("= %d\n", pfnOprater(nDataA, nDataB));
}

void main()
{
	//
	//TestMain();
	VirtualMain();
}