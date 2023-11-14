using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Player
    {
        //변수(속성): 변경될수있는 값.
        public int m_nAtk;
        public int m_nHp;
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
            Player player = new Player();
            Player monster = new Player();

            while (true)
            {
                if (player.Death() == false) //플레이어가 살아있다면,
                {
                    Console.WriteLine("##### 플레이어의 공격 #######");

                    Console.WriteLine("플레이어의 공격력:{0}", player.m_nAtk);
                    Console.WriteLine("몬스터의 체력:{0}", monster.m_nHp);
                    if(monster.Death() == false) //if (monster.m_nHp <= 0)  break;
                        player.Attack(monster);
                    Console.WriteLine("남은 몬스터의 체력:{0}", monster.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### 몬스터 승리! #####");
                    break;
                }
                if (!monster.Death()) //몬스터가 살아있다면,
                {
                    Console.WriteLine("##### 몬스터의 반격 #######");
                    Console.WriteLine("플레이어의 공격력:{0}", monster.m_nAtk);
                    Console.WriteLine("몬스터의 체력:{0}", player.m_nHp);
                    if (!player.Death())
                        monster.Attack(player);
                    Console.WriteLine("남은 플레이어의 체력:{0}", player.m_nHp);
                }
                else
                {
                    Console.WriteLine("##### 플레이어 승리! #####");
                    break;
                }
            }
        }
    }
}
}
