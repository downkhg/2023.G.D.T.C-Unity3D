/*##################################
�����带 �̿��� WinAPI����(������)
���ϸ�: WinAppDemo.cpp
�ۼ���: ��ȫ�� (downkhg@gmail.com)
������������¥: 2020.12.14
����: 1.2 (�޼��� �ּҰ� ��� �� ���� �ܰ躰ȭ)
###################################*/

#include <iostream>
#include <Windows.h>
#include <process.h>
#include <string>
#include <queue>

using namespace std;

enum WM_MSG { CREATE, COMMOND, PAINT, DESTROY, MAX };
string strMSG[MAX] = { "CREATE","COMMOND","PAINT","DESTROY" };

bool g_bLoop = true;
queue<int> g_queMsg;

void TestQueueMain()
{
	g_queMsg.push(WM_MSG::CREATE);
	g_queMsg.push(WM_MSG::COMMOND);
	g_queMsg.push(WM_MSG::PAINT);
	g_queMsg.push(WM_MSG::DESTROY);
	printf("Queue Count: %d\n", g_queMsg.size());
	while(g_queMsg.empty() == false)
	{
		int nMsg = g_queMsg.front();
		printf("MsgCount[%d]:%s\n", g_queMsg.size(), strMSG[nMsg].c_str());
		g_queMsg.pop();
	}
	printf("Queue Count: %d\n", g_queMsg.size());
}

//arg�� ���� �ܺ��� �����Ͱ��� �������ִ�.
unsigned int WINAPI WndProc(void* arg)
{
	cout << "arg:" << arg << endl;
	int* pData = (int*)arg;

	while (g_bLoop)
	{
		switch (*pData)
		{
		case CREATE:
			cout << "�ʱ�ȭ" << endl;
			*pData = COMMOND;
			break;
		case COMMOND:
			cout << "����� �Է��ϼ���." << endl;
			for (int i = 0; i < MAX; i++)
				cout << i << ":" << strMSG[i];
			cout << endl;
			break;
		case PAINT:
			cout << "ȭ�鿡 �׸��ϴ�." << endl;
			break;
		case DESTROY:
			cout << "���α׷��� �����մϴ�." << endl;
			g_bLoop = false;
			break;
		default:
			break;
		}
		Sleep(2000);
	}
	return 0;
}

int WinAPIDemoMain()
{
	HANDLE hThread = NULL;
	DWORD dwThreadID = NULL;

	int nMSG = CREATE;
	cout << "Msg:" << &nMSG << endl;
	//���μ���: ���α׷��� ���� �⺻���� ó���� ����ϴ� �帧����(���η���), ū�帧����.
	//������: ���μ��� ���ο� ó���� �ϴ� �帧����(�ݺ����� ���ÿ� ó������), ū�帧 ������ ���� �帧.
	//������� ���α׷� ���ο��� ó���ϴ� ������ �����Ҽ� �־���ϹǷ�, 
	//�� ���α׷� ���� �Լ��� ȣ���Ͽ� ����Ѵ�.
	//�ݹ��Լ�: ���μ��������� ȣ�������ʰ�, �ܺο��� ȣ���ϵ��� �ϴ� �Լ�.
	hThread = (HANDLE)_beginthreadex(NULL, 0,
		WndProc,
		(void*)&nMSG, 0,
		(unsigned int*)dwThreadID);

	while (g_bLoop)
	{
		scanf_s("%d", &nMSG);
	}

	return 0;
}

//�Է��� �����鼭 ȭ�鿡 �޼����� �ʿ��� ����� ó���ϴ� ���α׷�.
//ť�� Ȱ���Ͽ� �޼����� ť�� �װ�, �����忡�� ť���� �����͸� 1���������� ó���ϴ� ���α׷����� �����.
//1. ť�� ���������� ���� ó���ϱ�.
//2. ť�� main�� ���������� ���� ������ ������ �����ϵ��� ó���ϱ�.
int main()
{
	TestQueueMain();
	//WinAPIDemoMain();
}