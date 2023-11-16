using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//아이템의 요구사항
//장비템, 소모템 -> 능력치가 변화한다. -> 능력치 객체화한다.

namespace TextRPG
{
    class Status
    {
        public int nHP;
        public int nMP;
        public int nStr;
        public int nDef;

        public Status(int nHP = 0, int nMP = 0, int nStr = 0, int nDef = 0)
        {
            this.nHP = nHP;
            this.nMP = nMP;
            this.nStr = nStr;
            this.nDef = nDef;
        }

        public Status(Status status)
        {
            nHP = status.nHP;
            nMP = status.nMP;
            nStr = status.nStr;
            nDef = status.nDef;
        }

        public Status Add(Status b)
        {
            Status temp = new Status();
            temp.nHP = this.nHP + b.nHP;
            temp.nMP = this.nMP + b.nMP;
            temp.nStr = this.nStr + b.nStr;
            temp.nDef = this.nDef + b.nDef;
            return temp;
        }

        public static Status operator+(Status a, Status b)
        {
            Status temp = new Status();
            temp.nHP = a.nHP + b.nHP;
            temp.nMP = a.nMP + b.nMP;
            temp.nStr = a.nStr + b.nStr;
            temp.nDef = a.nDef + b.nDef;
            return temp;
        }

        public static Status operator -(Status a, Status b)
        {
            Status temp = new Status();
            temp.nHP = a.nHP - b.nHP;
            temp.nMP = a.nMP - b.nMP;
            temp.nStr = a.nStr - b.nStr;
            temp.nDef = a.nDef - b.nDef;
            return temp;
        }

        public void Display()
        {
            Console.WriteLine("HP{0}",nHP);
            Console.WriteLine("MP{0}", nMP);
            Console.WriteLine("Str{0}", nStr);
            Console.WriteLine("Def{0}", nDef);
        }
    }

    class Item
    {
        public enum E_ITEM_CATEGORY { CONSUMABLE, EQUMENT_WEAPON, EQUMENT_ARMOR, EQUMENT_ACC, ACTIVE }
        public E_ITEM_CATEGORY m_eCategory;
        public string m_strName;
        public Status m_sStatus;
        public int m_nPrice;

        public Item(E_ITEM_CATEGORY eCategory, string name, Status status, int price)
        {
            m_eCategory = eCategory;
            m_sStatus = new Status(status);
            m_strName = name;
            m_nPrice = price;
        }

        public Item(E_ITEM_CATEGORY eCategory, string name,int hp, int mp, int str, int def, int price)
        {
            m_eCategory = eCategory;
            m_sStatus = new Status(hp, mp, str, def);
            m_strName = name;
            m_nPrice = price;
        }

        public void Use(Player target)
        {
            switch(m_eCategory)
            {
                case E_ITEM_CATEGORY.CONSUMABLE:
                    target.Consumable(m_sStatus);
                    break;
                case E_ITEM_CATEGORY.EQUMENT_WEAPON:
                    if (target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.WEAPON] == null)
                    {
                        target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.WEAPON] = this;
                        target.m_sStatus += this.m_sStatus;
                    }
                    else
                    {
                        target.SetIventoryItem(target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.WEAPON]);
                        target.m_sStatus -= this.m_sStatus;
                        target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.WEAPON] = null;
                    }
                    break;
                case E_ITEM_CATEGORY.EQUMENT_ARMOR:
                    if (target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ARMOR] == null)
                    {
                        target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ARMOR] = this;
                        target.m_sStatus += this.m_sStatus;
                    }
                    else
                    {
                        target.SetIventoryItem(target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ARMOR]);
                        target.m_sStatus -= this.m_sStatus;
                        target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ARMOR] = null;
                    }
                    break;
                case E_ITEM_CATEGORY.EQUMENT_ACC:
                    if (target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ACC] == null)
                    {
                        target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ACC] = this;
                        target.m_sStatus += this.m_sStatus;
                    }
                    else
                    {
                        target.SetIventoryItem(target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ACC]);
                        target.m_sStatus -= this.m_sStatus;
                        target.m_llistEqument[(int)Player.E_EQUMENT_TYPE.ACC] = null;
                    }
                    break;
                case E_ITEM_CATEGORY.ACTIVE:
                    break;
            }
        }
    }

    class Player
    {
        //변수(속성): 변경될수있는 값.
        public string m_strName;
        public Status m_sStatus;
        public int m_nHp;
        public int m_nMp;

        public int m_nGold;

        public enum E_EQUMENT_TYPE { WEAPON, ARMOR, ACC, MAX }
        public List<Item> m_llistEqument = new List<Item>((int)E_EQUMENT_TYPE.MAX);

        public void SetEqumentType(Item item)
        {
            item.Use(this);
        }

        public void Consumable(Status status)
        {
            m_nHp += status.nHP;
            m_nMp += status.nMP;
        }

        public void SetEqument(Status status)
        {
            m_sStatus += status;
        }

        public void ReleaseEqument(Player target)
        {
            target.m_sStatus -= m_sStatus;
        }

        public List<Item> m_listIventory = new List<Item>();

        public void SetIventoryItem(Item item)
        {
            m_listIventory.Add(item);
        }

        public Item GetIvenventoryItemIdx(int idx)
        {
            return m_listIventory[idx];
        }

        public void RemoveIvemtoryItem(Item item)
        {
            m_listIventory.Remove(item);
        }

        public void UseIventoryItem(int idx)
        {
            Item item = m_listIventory[idx];
            if (item != null)
                item.Use(this);
            m_listIventory.Remove(item);
        }

        public bool StoreBuy(Player store, int selectIdx)
        {
            Item item = store.GetIvenventoryItemIdx(selectIdx);
            if (m_nGold >= item.m_nPrice)
            {
                this.SetIventoryItem(item);
                m_nGold -= item.m_nPrice;
                Console.WriteLine("{0}의 거래가 하여 {1}을 소모했습니다!", item.m_strName, item.m_nPrice);
                return true;
            }
            Console.WriteLine("소지금이 부족합니다!");
            return false;
        }

        public bool Sell(Player target, int selectIdx)
        {
            Item item = this.GetIvenventoryItemIdx(selectIdx);
            if(target.m_nGold >= item.m_nPrice)
            {
                target.SetIventoryItem(item);
                target.m_nGold -= item.m_nPrice;
                this.RemoveIvemtoryItem(item);
                this.m_nGold += item.m_nPrice;
                Console.WriteLine("{0}의 거래가 되어 {1}을 얻었습니다!", item.m_strName, item.m_nPrice);
                return true;
            }
            Console.WriteLine("{0}의 소지금이 부족합니다!", target.m_strName);
            return false;
        }

        public Item m_cItemSlot;

        public void SetItemSlot(Item item)
        {
            m_cItemSlot = item;
        }

        public Item ReleaseItem()
        {
            Item temp = m_cItemSlot;
            m_cItemSlot = null;
            return temp;
        }

        public void UseItemSlot()
        {
            if (m_cItemSlot != null)
            {
                Console.WriteLine("{0} 사용", m_cItemSlot );
                m_sStatus += m_cItemSlot.m_sStatus;
                m_cItemSlot = null;
            }
            else
            {
                Console.WriteLine("아이템이 없습니다.");
            }
        }

       
        public Player(string name,int hp,int mp, int str, int def, int gold = 999999999)
        {
            m_sStatus = new Status(hp, mp, str, def);
            m_nHp = hp;
            m_nMp = mp;
            m_strName = name;
            m_nGold = gold;

            for(int i = 0;  i < m_llistEqument.Capacity; i++)
            {
                m_llistEqument.Add(null);
            }
        }

        public void Demeged(int demage)
        {
            this.m_sStatus.nHP -= demage; 
        }

        //함수(동작): 객체가 하는 행동의 알고리즘을 함수화 한것.
        public void Attack(Player target)
        {
            Random cRandom = new Random(); //? 
            int nRandom = 0;// cRandom.Next(0, 3); //1. 1// 2// 3//
            float fDamage = m_sStatus.nStr;
            Console.WriteLine("Random:{0}", nRandom);
            if (nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
            {
                fDamage = fDamage*1.5f;
                Console.WriteLine("Ciritcal Attcka!");
            }
            target.Demeged((int)fDamage);
        }
        //인터페이스(접근방식): 인간은 값을 일일히 확인하여 사고하는데 익숙하지않다. 이를 함수화하여 제공하면 이를 인터페이스라고 부른다. 
        //죽었다는것은 행동으로 보기 어려우나, 인간의 사고과정에 맞춰서 생각하 쉽게 만든다.
        public bool Death()
        {
            if (m_nHp > 0)
                return false;
            else
                return true;
        }

        public void Display(string msg = "")
        {
            Console.WriteLine("# {0} {1} #", m_strName, msg);
            m_sStatus.Display();
            if(m_cItemSlot != null)
                Console.WriteLine("아이템슬롯:{0}", m_cItemSlot.m_strName);
            else
                Console.WriteLine("아이템슬롯: 비었음");
            Console.WriteLine("# 장비함 #", m_strName, msg);
            for(E_EQUMENT_TYPE e = E_EQUMENT_TYPE.WEAPON; e < E_EQUMENT_TYPE.MAX; e++)
            {
                if(m_llistEqument[(int)e] != null)
                    Console.WriteLine("{0}:{1}", e.ToString(), m_llistEqument[(int)e].m_strName);
                else
                    Console.WriteLine("{0}:비었음", e.ToString());
            }
            foreach (var c in m_strName) Console.Write("#");
            foreach (var c in msg) Console.Write("#");
            Console.WriteLine();
        }

        public void DisplayIventory(string msg = "")
        {
            Console.WriteLine("# {0} {1} #", m_strName, msg);
            for (int i = 0; i < m_listIventory.Count; i++)
            {
                Console.WriteLine("[{0}]:{1}", i, m_listIventory[i].m_strName);
            }
            Console.WriteLine("Gold:{0}", m_nGold);
        }
    }

    internal class RPG
    {
       
    }
}