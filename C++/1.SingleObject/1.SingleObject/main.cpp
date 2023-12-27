#include <iostream>
//�����Ҵ�: ��Ÿ�� �߿� �޸𸮸� �����ϴ� ��.
//�����Ҵ�: ������ �Ҷ� �޸𸮰� �����ϴ� ��.(���������� ���������� ���������� �����ϴ� ��)
//-��������: �Լ��� ���� �ɶ� ������ �Ҵ�ȴ�.
//-��������: ���α׷��� ����ɶ� �����ǰ�, ����ɶ� ������ �����ȴ�.
//����: �����Ϸ����� �̷����� �ִٴ� ���� �˸��°�(������������)
//����: �����Ϸ����� �����Ѱ��� ���� ��ü�� ������ִ� ��(�޸𸮸� �����ϴµ� �ʿ��������� ��)
//������: �ҽ��ڵ� �����Ҷ�
//��Ÿ��: ���� ��
using namespace std;
class SingleObject
{
	static SingleObject* m_pInstance;
//public: �����ڰ� private�̸� �޸𸮸� ��ü�ܺο��� ���� �� �� ����.
	//������: ��ü�� �����ɶ� ȣ���ϴ� �Լ�.
	SingleObject() { cout << "SingleObject:" << this << endl; }
	int m_nData;
//public:
	//�Ҹ���: ��ü�� �ı��ɶ� ȣ���ϴ� �Լ�.
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
		//�Լ��� ���� ��������� ���ٸ� �Ҵ�����ʴ��� ���ٰ����ϴ�.
		cout << this << " SingleObject ShowMSG" << endl;//[" << &m_nData << "]:" << m_nData << endl;
	}
	void Release()
	{
		delete m_pInstance;
	}
};
//������� ������ ��ü�� �����Ǳ������� �����Ǿ���ϹǷ�,
//���α׷��� ����ɶ����� �޸𸮰� �����Ǿ���Ѵ�.(��������)
SingleObject* SingleObject::m_pInstance;

//�̱���: Ŭ������ �ν��Ͻ��� 1���̻� ���� �Ҽ����� Ŭ������ ����� ���.(������ ����,�������)
int main()
{
	//�����Ҵ�Ǵ��� �����Ѱ�?
	//�̱����� �ݵ�� ��ü�� 1�������ϹǷ�,
	//������ ���� ������ �����ϸ� �ȵȴ�. 
	//�׷��Ƿ�, private���� �����.
	//SingleObject cSingleObject[100];
	//�����ʹ� ����� ���� �޸𸮰� �Ҵ�Ǵ� ���� �ƴϹǷ�,
	//�������� �����ȴٰ��ص� ��ü�� ������� �ƴϴ�.
	SingleObject* pSingleObjectA = NULL;
	SingleObject* pArrSingleObjects[2] = {};
	//�����Ҵ��� �����Ѱ�?
	//�����Ҵ絵 ��ü�� �����Ҷ��� �����ڰ� public�̿��� �����ϴ�.
	//pSingleObjectA = new SingleObject();
	//��ü�� �������ε� �Ϲݸ���� �����ϴ°��� ��Ģ�����δ� �Ұ����ϳ�, �Լ��� ��ü ���������� �����Ҽ�������,
	//�޸𸮸� ����Ͽ� �����ϴµ��� ������ �����Ƿ� ������ �����ϴ�. ��, ��ü�� �Ϲݸ�������� ����Ϸ����Ѵٸ�, 
	//�Ϲ� ����� ��ü�� �����Ǳ����� ������ �Ұ����ϹǷ�, �����Ͽ����� ���Եȴ�.
	pSingleObjectA->ShowMessage(); 
	pSingleObjectA = SingleObject::GetInstance();
	//delete pSingleObjectA; //�Ҹ��ڰ� private�̾ƴϸ� ������ ���� ��ü�� ���� �� ���ɼ��� �ִ�.
	pSingleObjectA->ShowMessage();
	for (int i = 0; i < 2; i++)
		pArrSingleObjects[i] = SingleObject::GetInstance();

	cout << "pSingleObjectA:" << pSingleObjectA << endl;
	for (int i = 0; i < 2; i++)
	{
		cout << "pSingleObject[" << i << "]:" << pArrSingleObjects[i] << endl;
		pArrSingleObjects[i]->ShowMessage();
	}
	//�ν��Ͻ��� 1���̹Ƿ� ���� ������ �ҷ����ʿ�� ����.
	pSingleObjectA->Release();
}