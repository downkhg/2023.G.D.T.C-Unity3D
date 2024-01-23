#pragma once
/*##################################
����Ʈ��(������)
���ϸ�: BinaryTreeClass.cpp
�ۼ��� : ��ȫ��(downkhg@gmail.com)
������������¥ : 2017.09.10
���� : 1.01
###################################*/
#include <stdio.h>

using namespace std;

class CBinaryTree
{
public:
	struct SNode {
		int nData;
		SNode* pLeft;
		SNode* pRight;
	};

	CBinaryTree()
	{
		m_pSeed = NULL;
	}

	SNode* CreateNode(int data)
	{
		SNode* pTemp = new SNode;
		pTemp->nData = data;
		pTemp->pLeft = NULL;
		pTemp->pRight = NULL;
		return pTemp;
	};
	bool MakeLeft(SNode* pPerant, SNode* pChilde)
	{
		if (pPerant == NULL)
			return false;
		pPerant->pLeft = pChilde;
		return true;
	};
	bool MakeRight(SNode* pPerant, SNode* pChilde)
	{
		if (pPerant == NULL)
			return false;
		pPerant->pRight = pChilde;
		return true;
	};

	void Print()
	{
		Traverse(m_pSeed);
	}

	SNode* GetSeed()
	{
		return m_pSeed;
	}

	void SetSeed(SNode* pNode)
	{
		m_pSeed = pNode;
	}

private:
	SNode* m_pSeed;

	void Traverse(SNode* pNode)
	{
		if (!pNode) return;
		//printf("%d\n", pNode->nData); //����
		Traverse(pNode->pLeft);
		//printf("%d\n", pNode->nData); //����
		Traverse(pNode->pRight);
		printf("%d\n", pNode->nData); //����
	}
};

void main()
{
	CBinaryTree cBinaryTree;

	CBinaryTree::SNode* pPrant = cBinaryTree.CreateNode(10);
	CBinaryTree::SNode* pLeft = cBinaryTree.CreateNode(20);
	CBinaryTree::SNode* pRight = cBinaryTree.CreateNode(30);
	CBinaryTree::SNode* pD = cBinaryTree.CreateNode(40);
	CBinaryTree::SNode* pE = cBinaryTree.CreateNode(50);

	cBinaryTree.MakeLeft(pPrant, pLeft);
	cBinaryTree.MakeRight(pPrant, pRight);

	cBinaryTree.MakeLeft(pLeft, pD);
	cBinaryTree.MakeRight(pLeft, pE);

	cBinaryTree.SetSeed(pPrant);

	cBinaryTree.Print();
}