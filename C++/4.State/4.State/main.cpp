#include <iostream>
#include <stdlib.h> //�޸� �����Ҵ� ���
#include <crtdbg.h> //�޸� ���� Ž�� ���
//#include "State.h"

using namespace std;

//Ŭ������ ����: �̷��� �̸��� Ŭ������ �ִٴ� ���� �����Ϸ� ���� �˸��� ��.
class Context;
class State;
class StateOne;
class StateTow;
class StateThree;


//Ŭ������ ����: Ŭ������ ����� �����ϴ� ��.
class State
{
    friend class Context;
public:
    //Ŭ�����Լ��� ����: Ŭ������ �Լ��� ���¸� �����ϴ� ��.
    virtual void GoNext(Context* context) = 0;
};


class StateThree : public State
{
public:
    void GoNext(Context* context) override;
};

class StateTow : public State
{
public:
    void GoNext(Context* context) override;
};


class StateOne : public State
{
public:
    void GoNext(Context* context) override;
};

class Context
{
    State* m_pState = NULL;
    ///�ر׷��� friend�� ��������ʵ��� ������.
    //������ ���� friendŬ������ Ȱ���Ͽ�
    //SetState�� �ܺο����� ������� ��������,
    //�� State��ü������ ���ٰ����ϵ��� �Ҽ��� �ִ�.
    friend class StateOne;
    friend class StateTow;
    friend class StateThree;

    void SetState(State* newState);
public:
    Context();
    Context(State* curState);
    ~Context();
    void GoNext();
};

//Ŭ�����Լ��� ����: Ŭ������ ����� �Լ����� �����Ѵ�.
void StateThree::GoNext(Context* context)
{
    context->SetState(new StateOne);
}

void StateTow::GoNext(Context* context)
{
    context->SetState(new StateThree);
}

void StateOne::GoNext(Context* context)
{
    context->SetState(new StateTow);
}

Context::Context()
{
    m_pState = new StateOne();
}

Context::Context(State* state)
{
    m_pState = state;
}

Context::~Context()
{
    delete m_pState;
}

void Context::SetState(State* newState)
{
    if (m_pState) delete m_pState;
    std::cout << "SetState:" << typeid(*newState).name() << std::endl;
    m_pState = newState;
}

void Context::GoNext()
{
    m_pState->GoNext(this);
}


void main()
{
	_CrtSetBreakAlloc(159); //�޸� ������ ��ȣ�� ������ �Ҵ��ϴ� ��ġ�� �극��ũ ����Ʈ�� �Ǵ�.
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF); //�޸� ���� �˻� 

	Context cContext;
	//�����ڿ��� �⺻ StateOne�� �Ҵ��ϰų�,
	//������ State�� �����Ҽ��ֵ��� �����ڸ� ������.
	//�Ҹ��ڿ��� ������ ��ü�� �Ҹ��Ű���� ����.

	///�ر׷��� friend�� ��������ʵ��� ������.
	//friendŰ���带 �̿��Ͽ� Context�� ��밡���ϰ� ���� ���� �ִ�.
	//�����������̽��� ����ϱ� ������
	//cContext.SetState(new StateOne());
	//Context::GoNext()�� �ٸ� ������ ��ü�� �����ǵ���,
	//Contest::SetState()���� ������ �Ҵ�� ��ü�� �����ϰ� ����.
	cContext.GoNext();
	cContext.GoNext();
	cContext.GoNext();
}