﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Program
    {
        static int Add(int a, int b)//3.1: 10,20
        {
            int c = a + b; //3.2: 30 = 10 + 20;
            return c;//3.3: 30
        }
        //변수: 데이터이터를 저장하는 공간.
        //데이터타입을 테스트하기위해서 코드를 작성.
        //인트형 변수를 만들고, 값을 0으로 초기화한다.
        //실수형 변수를 만들고, 1과 4의 값을 나누어 저장한다.
        //각변수의 값을 확인하기위하여 콘솔에 출력한다.
        static void ValMain()
        {

        }

        //1.Main함수: 프로그램이 실행시에 호출되는 함수
        static void Main(string[] args) //1.
        {
            //Console.WriteLine("Hello World!");//2.
            //Console.WriteLine("Add:" + Add(10, 20));//3
            //ValMain();
            //PlayerAttackMonsterMain();
            //PlayerAttackCritcalMain();
            //PlayerAttackWihleMain();
            //BattleMain();
            //RPG.BattleMain();
            RPGGameMain();
        }//4
        //플레이어가 몬스터를 (공격)한다.
        //플레이어가 몬스터를 몬스터의 hp가 감소 한다. -> 얼만큼 감소되는가? -> 플레이어의 데미지만큼
        //변수(데이터):몬스터의 hp, 플레이어의 공격력
        //알고리즘: 몬스터의 hp - 플레이어의 공격력 = 몬스터의 hp
        //공격력은 10이고 몬스터의 hp는 100이다.
        //바뀐 데이터의 값을 알기위하여 변수의 값을 출력한다.
        static void PlayerAttackMonsterMain()
        {
            int nMonsterHP = 100;//100
            int nPlayerAtk = 10; //10
            Console.WriteLine("플레이어의 공격력:{0}", nPlayerAtk);
            Console.WriteLine("몬스터의 체력:{0}", nMonsterHP);
            nMonsterHP = nMonsterHP - nPlayerAtk; // 100 - 10 = 90
            Console.WriteLine("남은 몬스터의 체력:{0}",nMonsterHP);
        }
        //플레이어가 공격하는데 (일정확률로 추가데미지)를 준다.
        //일정확률? 50% //확률: 준비한 숫자에서 특정값이 나올 경우의 수.
        static void PlayerAttackCritcalMain()
        {
            int nMonsterHP = 100;//100
            int nPlayerAtk = 10; //10
            Console.WriteLine("플레이어의 공격력:{0}", nPlayerAtk);
            Console.WriteLine("몬스터의 체력:{0}", nMonsterHP);
            //
            Random cRandom = new Random(); //? 
            int nRandom = cRandom.Next(0, 3); //1. 1// 2// 3//
            Console.WriteLine("Random:{0}",nRandom);
            if(nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
            {
                nMonsterHP = nMonsterHP - (nPlayerAtk+100); // 100 - 10 = 90 //3. //3.
                Console.WriteLine("Ciritcal Attcka!");
            }
            else //3.
                nMonsterHP = nMonsterHP - nPlayerAtk; // 100 - 10 = 90
            Console.WriteLine("남은 몬스터의 체력:{0}", nMonsterHP);
        }
        //공격을 몬스터가 사망할때까지 반복한다.
        //몬스터 사망: 몬스터 체력이 0이되었을때
        static void PlayerAttackWihleMain()
        {
            int nMonsterHP = 50;//100 //2. 100 
            int nPlayerAtk = 15; //10 //3. 10
            bool isLoop = true; 
            while (isLoop) //1 //9
            {
                //if (nMonsterHP <= 0) //4. 100 == 0 :F //10. 90 == 0 :F
                //    break; 
                if (nMonsterHP >= 0) //종료조건의 반대되는 조건으로 동작의 조건을 설정한다.
                {
                    Console.WriteLine("플레이어의 공격력:{0}", nPlayerAtk); //10 //5. 10
                    Console.WriteLine("몬스터의 체력:{0}", nMonsterHP); //100 //6. 100
                    nMonsterHP = nMonsterHP - nPlayerAtk; // 100 - 10 = 90 //7.
                    Console.WriteLine("남은 몬스터의 체력:{0}", nMonsterHP); //90 //8. 90
                }
                else
                    isLoop = false;
            }
        }
        //플레이어가 몬스터를 공격한다.
        //몬스터는 플레이어를 반격한다.
        //반격: 공격을 받고나서 공격한 대상을 공격한다.
        //플레이어의 hp, 몬스터의 공격력
        static void BattleMain()
        {
            int nPlayerAtk = 10; //10
            int nPlayerHP = 100;//100

            int nMonsterHP = 100;//100
            int nMonsterAtk = 10; //10

            while (true)
            {
                if (nPlayerHP > 0) //플레이어가 살아있다면,
                {
                    Console.WriteLine("##### 플레이어의 공격 #######");

                    Console.WriteLine("플레이어의 공격력:{0}", nPlayerAtk);
                    Console.WriteLine("몬스터의 체력:{0}", nMonsterHP);

                    if (nMonsterHP <= 0)
                        break;
                    Random cRandom = new Random(); //? 
                    int nRandom = 0;// cRandom.Next(0, 3); //1. 1// 2// 3//
                    Console.WriteLine("Random:{0}", nRandom);
                    if (nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
                    {
                        nMonsterHP = nMonsterHP - (nPlayerAtk + 10); // 100 - 10 = 90 //3. //3.
                        Console.WriteLine("Ciritcal Attcka!");
                    }
                    else //3.
                        nMonsterHP = nMonsterHP - nPlayerAtk; // 100 - 10 = 90
                    Console.WriteLine("남은 몬스터의 체력:{0}", nMonsterHP);
                }
                else
                {
                    Console.WriteLine("##### 몬스터 승리! #####");
                    break;
                }
                if (nMonsterHP > 0) //몬스터가 살아있다면,
                {
                    Console.WriteLine("##### 몬스터의 반격 #######");
                    Console.WriteLine("플레이어의 공격력:{0}", nMonsterAtk);
                    Console.WriteLine("몬스터의 체력:{0}", nPlayerHP);

                    if (nPlayerHP <= 0)
                        break;

                    Random cRandom = new Random(); //? 
                    int nRandom = 1;// cRandom.Next(0, 3); //1. 1// 2// 3//
                    Console.WriteLine("Random:{0}", nRandom);
                    if (nRandom == 1) //2. 1 == 1:T //2 == 1 : F //3 == 1 : F
                    {
                        nPlayerHP = nPlayerHP - (nMonsterAtk + 100); // 100 - 10 = 90 //3. //3.
                        Console.WriteLine("Ciritcal Attcka!");
                    }
                    else //3.
                        nPlayerHP = nPlayerHP - nMonsterAtk; // 100 - 10 = 90
                    Console.WriteLine("남은 플레이어의 체력:{0}", nPlayerHP);
                }
                else
                {
                    Console.WriteLine("##### 플레이어 승리! #####");
                    break;
                }
            }
        }

        static void RPGGameMain()
        {
            Player player = new Player("player",10,100);

            Player slime = new Player("slime",20,50);
            Player zombie = new Player("zombie",20,100);
            Player skeleton = new Player("skeleton",30,200);

            slime.SetItemSlot(new Item("힐링포션(소)", 10));
            zombie.SetItemSlot(new Item("힐링포션(중)", 50));
            skeleton.SetItemSlot(new Item("힐링포션(대)", 100));

            string strSelectFiled = "";

            Console.Write("장소이름을 입력하세요.(숲,무덤,던전)");
            strSelectFiled = Console.ReadLine();

            Console.WriteLine("{0}에 들어갔습니다.",strSelectFiled);
            switch(strSelectFiled)
            {
                case "숲":
                    RPG.Battle(player, slime);
                    break;
                case "무덤":
                    RPG.Battle(player, zombie);
                    break;
                case "던전":
                    RPG.Battle(player, skeleton);
                    break;
            }  
        }
    }
}
