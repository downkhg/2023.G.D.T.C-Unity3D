using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Item
    {
        public string m_strName;
        public int m_nRecovery;

        public Item(string name, int recovery)
        {
            m_nRecovery = recovery;
            m_strName = name;
        }
    }

    class Player
    {
        //변수(속성): 변경될수있는 값.
        public string m_strName;
        public int m_nAtk;
        public int m_nHp;

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

        public void UseItemSlot(Item item)
        {
            m_nHp += m_cItemSlot.m_nRecovery;
            m_cItemSlot = null;
        }
        
        public Player(string name, int atk, int hp)
        {
            m_nAtk = atk;
            m_nHp = hp;
            m_strName = name;
        }

        //함수(동작): 객체가 하는 행동의 알고리즘을 함수화 한것.
        public void Attack(Player target)
        {
            Random cRandom = new Random(); //? 
            int nRandom = 0;// cRandom.Next(0, 3); //1. 1// 2// 3//
            Console.WriteLine("Random:{0}", nRandom);
            if (nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
            {
                target.m_nHp = target.m_nHp - (this.m_nAtk + 10); // 100 - 10 = 90 //3. //3.
                Console.WriteLine("Ciritcal Attcka!");
            }
            else //3.
                target.m_nHp = target.m_nHp - this.m_nAtk; // 100 - 10 = 90
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
    }

    internal class RPG
    {
        public static void BattleMain()
        {
            Player player = new Player("player",10,100);
            Player monster = new Player("monster",10,100);

            while (true)
            {
                if (player.Death() == false) //플레이어가 살아있다면,
                {
                    Console.WriteLine("##### "+player.m_strName+"의 공격 #######");

                    Console.WriteLine(""+player.m_strName+"의 공격력:{0}", player.m_nAtk);
                    Console.WriteLine(""+monster.m_strName+"의 체력:{0}", monster.m_nHp);
                    if(monster.Death() == false) //if (monster.m_nHp <= 0)  break;
                        player.Attack(monster);
                    Console.WriteLine("남은 "+monster.m_strName+"의 체력:{0}", monster.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### "+monster.m_strName+" 승리! #####");
                    break;
                }
                if (!monster.Death()) //몬스터가 살아있다면,
                {
                    Console.WriteLine("##### "+monster.m_strName+"의 반격 #######");
                    Console.WriteLine(""+player.m_strName+"의 공격력:{0}", monster.m_nAtk);
                    Console.WriteLine(""+monster.m_strName+"의 체력:{0}", player.m_nHp);
                    if (!player.Death())
                        monster.Attack(player);
                    Console.WriteLine("남은 "+player.m_strName+"의 체력:{0}", player.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### "+player.m_strName+" 승리! #####");
                    break;
                }
            }
        }

        public static void Battle(Player player, Player monster)
        {
            while (true)
            {
                if (player.Death() == false) //플레이어가 살아있다면,
                {
                    Console.WriteLine("##### {0}의 공격 #######", player.m_strName);

                    Console.WriteLine(""+player.m_strName+"의 공격력:{0}", player.m_nAtk);
                    Console.WriteLine(""+monster.m_strName+"의 체력:{0}", monster.m_nHp);
                    if (monster.Death() == false) //if (monster.m_nHp <= 0)  break;
                        player.Attack(monster);
                    else
                    {
                       
                    }
                    Console.WriteLine("남은 "+monster.m_strName+"의 체력:{0}", monster.m_nHp);
                }
                else
                {
                    Console.WriteLine("#####"+ monster.m_strName +" 승리! #####");
                    break;
                }
                if (!monster.Death()) //monster.m_strName가 살아있다면,
                {
                    Console.WriteLine("##### "+monster.m_strName+"의 반격 #######");
                    Console.WriteLine(""+player.m_strName+"의 공격력:{0}", monster.m_nAtk);
                    Console.WriteLine(""+monster.m_strName+"의 체력:{0}", player.m_nHp);
                    if (!player.Death())
                        monster.Attack(player);
                    Console.WriteLine("남은 "+player.m_strName+"의 체력:{0}", player.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### "+player.m_strName+" 승리! #####");
                    Item item = monster.ReleaseItem();
                    player.SetItemSlot(item);
                    Console.WriteLine("{0}가 {1}을 쓰러뜨리고 {2}를 획득했다.", player.m_strName, monster.m_strName, item.m_strName);
                    break;
                }
            }
        }
    }
}
