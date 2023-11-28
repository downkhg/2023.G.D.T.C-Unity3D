#include <iostream>
#include <stdlib.h> //메모리 동적할당 헤더
#include <crtdbg.h> //메모리 누수 탐지 헤더
//#include "State.h"

using namespace std;

//클래스의 선언: 이러한 이름의 클래스가 있다는 것을 컴파일러 에게 알리는 것.
class Context;
class State;
class StateOne;
class StateTow;
class StateThree;


//클래스의 정의: 클래스의 멤버를 정의하는 것.
class State
{
    friend class Context;
public:
    //클래스함수의 선언: 클래스의 함수의 형태를 선언하는 것.
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
    ///※그러나 friend는 사용하지않도록 권장함.
    //다음과 같이 friend클래스를 활용하여
    //SetState를 외부에서는 사용하지 못하지만,
    //각 State객체에서는 접근가능하도록 할수도 있다.
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

//클래스함수의 정의: 클래스에 선언된 함수들을 정의한다.
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
	_CrtSetBreakAlloc(159); //메모리 누수시 번호를 넣으면 할당하는 위치에 브레이크 포인트를 건다.
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF); //메모리 누수 검사 

	Context cContext;
	//성성자에서 기본 StateOne을 할당하거나,
	//성성시 State를 설정할수있도록 생성자를 정의함.
	//소멸자에는 생성된 객체를 소멸시키도록 만듦.

	///※그러나 friend는 사용하지않도록 권장함.
	//friend키워드를 이용하여 Context만 사용가능하게 만들 수도 있다.
	//다음인터페이스는 사용하기 불편함
	//cContext.SetState(new StateOne());
	//Context::GoNext()시 다른 생성된 객체가 삭제되도록,
	//Contest::SetState()에서 기존에 할당된 객체를 삭제하게 만듦.
	cContext.GoNext();
	cContext.GoNext();
	cContext.GoNext();
}