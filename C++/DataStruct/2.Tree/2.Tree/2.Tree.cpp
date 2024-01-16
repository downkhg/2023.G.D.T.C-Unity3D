﻿/*##################################
이진트리(C언어 수업용)
파일명: BinaryTree.cpp
작성자 : 김홍규(downkhg@gmail.com)
마지막수정날짜 : 2022.03.04
버전 : 1.01
###################################*/
#include <stdio.h>

using namespace std;

struct SNode {
	int nData;
	SNode* pLeft;
	SNode* pRight;
};

SNode* CreateNode(int data)
{
	SNode* pTemp = new SNode;
	pTemp->nData = data;
	pTemp->pLeft = NULL;
	pTemp->pRight = NULL;
	return pTemp;
};
bool MakeLeft(SNode* pParent, SNode* pChilde)
{
	if (pParent == NULL)
		return false;
	pParent->pLeft = pChilde;
	return true;
};
bool MakeRight(SNode* pParent, SNode* pChilde)
{
	if (pParent == NULL)
		return false;
	pParent->pRight = pChilde;
	return true;
};

void Traverse(SNode* pNode)
{
	if (!pNode) return;
	printf("%d\n", pNode->nData); //전위
	Traverse(pNode->pLeft);
	//printf("%d\n", pNode->nData); //중위
	Traverse(pNode->pRight);
	//printf("%d\n", pNode->nData); //후위
}

void Print(SNode* pSeed)
{
	Traverse(pSeed);
}

void main()
{
	SNode* pSeed = NULL;

	SNode* pParent = CreateNode(10);
	SNode* pLeft = CreateNode(20);
	SNode* pRight = CreateNode(30);
	SNode* pD = CreateNode(40);
	SNode* pE = CreateNode(50);

	MakeLeft(pParent, pLeft);//가희,지원
	MakeRight(pParent, pRight);//정규,승현

	MakeLeft(pLeft, pD);//훈배,성준
	MakeRight(pLeft, pE);//민규,창현

	pSeed = pParent;

	Print(pSeed);
}