#include <iostream>
//동적할당: 런타임 중에 메모리를 생성하는 것.
//정적할당: 컴파일 할때 메모리가 생성하는 것.(컴파일전에 지역변수와 전역변수를 정의하는 것)
//-지역변수: 함수가 선언 될때 변수가 할당된다.
//-전역변수: 프로그램이 실행될때 생성되고, 종료될때 변수가 삭제된다.
//선언: 컴파일러에게 이런것이 있다는 것을 알리는것(존제하지않음)
//정의: 컴파일러에게 선언한것을 실제 형체를 만들어주는 것(메모리를 생성하는데 필요한정보를 줌)
//컴파일: 소스코드 빌드할때
//런타임: 실행 중
using namespace std;
class SingleObject
{
	static SingleObject* m_pInstance;
//public: 생성자가 private이면 메모리를 객체외부에서 생성 할 수 없다.
	//생성자: 객체가 생성될때 호출하는 함수.
	SingleObject() { cout << "SingleObject:" << this << endl; }
	int m_nData;
//public:
	//소멸자: 객체가 파괴될때 호출하는 함수.
	~SingleObject() { cout << "~SingleObject:" << this << endl; }
public:
	static SingleObject* GetInstance()
	{
		cout << "GetInstance Start:" << endl;
		if (m_pInstance == NULL)
			m_pInstance = new SingleObject();
		cout << "GetInstance End:" << endl;
		return m_pInstance;
	}
	void ShowMessage()
	{
		//함수는 내부 멤버변수가 없다면 할당되지않더라도 접근가능하다.
		cout << this << " SingleObject ShowMSG" << endl;//[" << &m_nData << "]:" << m_nData << endl;
	}
	void Release()
	{
		delete m_pInstance;
	}
};
//정적멤버 변수는 객체가 생성되기전부터 생성되어야하므로,
//프로그램이 종료될때까지 메모리가 유지되어야한다.(전역변수)
SingleObject* SingleObject::m_pInstance;

//싱글톤: 클래스의 인스턴스가 1개이상 존재 할수없는 클래스를 만드는 기법.(생성자 은닉,정적멤버)
int main()
{
	//정적할당되는지 가능한가?
	//싱글톤은 반드시 객체가 1개여야하므로,
	//다음과 같은 생성이 가능하면 안된다. 
	//그러므로, private으로 만든다.
	//SingleObject cSingleObject[100];
	//포인터는 대상을 직접 메모리가 할당되는 것이 아니므로,
	//여러개가 생성된다고해도 객체가 생긴것은 아니다.
	SingleObject* pSingleObjectA = NULL;
	SingleObject* pArrSingleObjects[2] = {};
	//동적할당은 가능한가?
	//동적할당도 객체를 생성할때는 생성자가 public이여만 가능하다.
	//pSingleObjectA = new SingleObject();
	//객체가 생성전인데 일반멤버에 접근하는것은 원칙적으로는 불가능하나, 함수는 객체 생성전부터 존재할수있으며,
	//메모리를 사용하여 접근하는데는 지장이 없으므로 실행이 가능하다. 단, 객체가 일반멤버변수를 출력하려고한다면, 
	//일반 멤버는 객체가 생성되기전에 접근이 불가능하므로, 컴파일오류가 나게된다.
	pSingleObjectA->ShowMessage(); 
	pSingleObjectA = SingleObject::GetInstance();
	//delete pSingleObjectA; //소멸자가 private이아니면 다음과 같이 객체를 삭제 할 가능성이 있다.
	pSingleObjectA->ShowMessage();
	for (int i = 0; i < 2; i++)
		pArrSingleObjects[i] = SingleObject::GetInstance();

	cout << "pSingleObjectA:" << pSingleObjectA << endl;
	for (int i = 0; i < 2; i++)
	{
		cout << "pSingleObject[" << i << "]:" << pArrSingleObjects[i] << endl;
		pArrSingleObjects[i]->ShowMessage();
	}
	//인스턴스가 1개이므로 굳이 여러번 불러줄필요는 없다.
	pSingleObjectA->Release();
}