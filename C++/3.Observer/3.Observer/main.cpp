#include <iostream>
#include <vector>
using namespace std;
//다형성: 부모객체에서 자식객체의 메모리에 접근 하고 싶을때 사용함.
//동적바인딩: 함수의 기능이 참조(동적할당)하는 함수에 따라 달라진다.
//가상함수는 함수포인터처럼 작동하여 콜백구조를 만들때 많이 활용한다.
//델리게이트, 델리게이트체인, 람다
//다음과 같이 모델링할때는 전략자패턴에 가까우나.
//커멘더의 명령에 따라 작동하는것은 비슷하므로 옵져버라고 말할수있다.
class Unit
{
public:
	int x;
	int y;
	//추상클래스: 순수함수를 1개이상 가진 클래스. 객체화가 불가능하다.
	//순수가상함수: 자식에서 반드시 오버라이드해야만 한다.
	virtual void Attack(Unit* target) = 0
	{
		cout << typeid(*this).name() << "Attack" << endl;
	}
	virtual void Move(int x, int y)
	{
		cout << typeid(*this).name() << "Move:" << x << "," << y << endl;
	}
	virtual void SkillA(Unit* target)
	{
		cout << "SkillA" << endl;
	}
	virtual void SkillB(Unit* target)
	{
		cout << "SkillB" << endl;
	}
	virtual void SkillC(Unit* target)
	{
		cout << "SkillC" << endl;
	}
};
//추상클래스 유닛을 마린과 메딕으로 실체화한다.
//실체화: 추상클래스를 객체화가 가능한 클래스를 정의한다.
class Marin : public Unit
{
	void Attack(Unit* unit) override
	{
		Unit::Attack(unit);
	}

	void SkillA(Unit* unit) override
	{
		cout << "Active StillPack" << endl;
	}
};

class Medic : public Unit
{
	void Attack(Unit* unit) override
	{
		Move(unit->x, unit->y);
	}

	void SkillA(Unit* unit) override
	{
		cout << "Active Hill" << endl;
	}

	void SkillB(Unit* unit) override
	{
		cout << "Recovery" << endl;
	}
};

class Commader
{
	//다형성을 활용하여 자식객체에서 오버라이딩된 함수에 접근할 수 있다.
	vector<Unit*> group;
public:
	enum E_COMMAND { ATK, MOV };

	void AddGroup(Unit* unit)
	{
		group.push_back(unit);
	}

	bool RemvoeGroup(Unit* unit)
	{
		vector<Unit*>::iterator it;
		for (it = group.begin(); it != group.end(); it++)
		{
			if (&unit == &(*it))
				break;
		}
		if (it != group.end())
		{
			group.erase(it);
			return true;
		}
		return false;
	}
	//부대에 명령을 내리는 기능
	void Command(E_COMMAND command, Unit* target)
	{
		switch (command)
		{
		case E_COMMAND::ATK:
			for (int i = 0; i < group.size(); i++)
				group[i]->Attack(target);
			break;
		case E_COMMAND::MOV:
			for (int i = 0; i < group.size(); i++)
				group[i]->Move(10, 10);
			break;
		default:
			break;
		}
	}
};

void main()
{
	Commader cCommder;
	//Unit units[10];//추상클래스는 객체화 할 수 없다.
	Marin marins[10];
	Medic medics[10];

	for (int i = 0; i < 10; i++)
		cCommder.AddGroup(&marins[i]);

	for (int i = 0; i < 10; i++)
		cCommder.AddGroup(&medics[i]);

	cCommder.Command(Commader::E_COMMAND::ATK, &marins[0]);
}