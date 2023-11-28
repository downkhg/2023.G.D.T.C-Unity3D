#include <stdio.h>
#include "Operator.h"

void VirtualMain()
{
	COp* pOperator;
	int nDataA;
	int nDataB;
	char cInput;
	//�μ��� ���ϰ� �߰��� ���ڷ� �����ڸ� �޴´�.
	printf("Input sample(1+1):");
	scanf("%d%c%d", &nDataA, &cInput, &nDataB);
	//���� �����ڿ� ���� �Լ������Ϳ� �Ҵ�Ǵ� �Լ��� �޶������Ѵ�.
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
	//�Լ������Ϳ� �Ű������� �����ϰ� ���ϰ��� ����Ѵ�.
	printf("= %d\n", pOperator->Operator(nDataA, nDataB));
	delete pOperator;
}

//��Ģ���� �Լ��� �����Ѵ�.
int Add(int a, int b) { return a + b; }
int Sub(int a, int b) { return a - b; }
int Multi(int a, int b) { return a * b; }
int Divide(int a, int b) { return a / b; }
//��Ģ���� ���� �����
//��Ģ�����Լ��� ����� �μ��� �����ڸ� �Է¹ް�, �Է¹��� �����ڿ� ���� �Լ������Ϳ� �Ҵ�Ǵ� �Լ��� ���� �Ҵ��ϰ�, �μ��� �Լ������͸� �̿��Ͽ� ����Ѵ�.

void TestMain()
{
	int (*pfnOprater)(int, int);//�Լ������͸� Ȱ���Ͽ� ��Ģ���꿡 �ش��ϴ� �Լ��� �޴´�.
	int nDataA;
	int nDataB;
	char cInput;
	//�μ��� ���ϰ� �߰��� ���ڷ� �����ڸ� �޴´�.
	printf("Input sample(1+1):");
	scanf("%d%c%d", &nDataA, &cInput, &nDataB);
	//���� �����ڿ� ���� �Լ������Ϳ� �Ҵ�Ǵ� �Լ��� �޶������Ѵ�.
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
	//�Լ������Ϳ� �Ű������� �����ϰ� ���ϰ��� ����Ѵ�.
	printf("= %d\n", pfnOprater(nDataA, nDataB));
}

void main()
{
	//
	//TestMain();
	VirtualMain();
}