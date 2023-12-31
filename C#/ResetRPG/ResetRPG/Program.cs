﻿/*
문제: 초급자들은 코드를 만들어지는 과정을 이해하지 못하면 복잡한 코드를 이해하기 어렵다.

목표: 초급자들이 이해하기 쉬운 방법으로 수업진행.

방법:
1. 플레이어 클래스만 가져다쓴다.
2. 플레이어와 몬스터를 싸우게 만든다.
3. 몬스터를 쓰러뜨리면 아이템을 획득하게 한다.
4. 몬스터와 전투에서 활용 가능 하도록 만든다
5. 아이템의 구매와 판매를 구현한다 -> NPC가 필요하다.
-NPC의 상점목록을 구현하기위해 인벤토리 필요.
->인벤토리 아이템들을 여러개 저장하는 곳 -> 배열,리스트
->NPC -> Player추가 해야한다.
-상점목록에서 구매하면 인벤토리에 아이템이 삭제되지않는다.
-거래기능은 아이템이 삭제되고 구매한 대상에게 아이템을 준다.
6.렙업요소 구현하기.
7.장비를 구현하고싶다.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace ResetRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextRPGMain();
            //TestStoreMain();
        }
       
        enum E_ITEM { HPPOSTION_S,HPPOSTION_M,HPPOSTION_L, WOOD_WEAPON, WOOD_ARMOR, WOOD_RING }
        static void TextRPGMain()
        {
            Player player;
            Player monster;

            player = new Player("player", 100, 20, 10, 0);
            monster = new Player("slime", 100, 20, 10, 0);

            
            DataManager dataManager = new DataManager();

            //dataManager.Load();
            dataManager.Init();
            dataManager.Save();

            Player npc = new Player("store", 100, 20, 10, 0);

            dataManager.SetPlayerAllData(npc);

            Item getItem = null;
            getItem = dataManager.GetItem(DataManager.E_ITEM.HPPOSTION_S);
            player.SetItemSlot(getItem);
            monster.SetItemSlot(getItem);
            getItem = dataManager.GetItem(DataManager.E_ITEM.WOOD_WEAPON);
            player.SetIventoryItem(getItem);
            getItem = dataManager.GetItem(DataManager.E_ITEM.WOOD_ARMOR);
            player.SetIventoryItem(getItem);
            getItem = dataManager.GetItem(DataManager.E_ITEM.WOOD_RING);
            player.SetIventoryItem(getItem);

            string strSelectFiled = "";
            bool isLoop = true;

            while (isLoop)
            {
                Console.WriteLine("게임을 종료하려면 '나가기' 입력하세요!");
                Console.Write("장소이름을 입력하세요.(상점, 장비함, 필드)");
                strSelectFiled = Console.ReadLine();

                Console.WriteLine("{0}에 들어갔습니다.", strSelectFiled);
                switch (strSelectFiled)
                {
                    case "상점":
                        Store(player, npc);
                        break;
                    case "장비함":
                        Iventory(player);
                        break;
                    case "필드":
                        Battle(player, monster);
                        break;
                    case "나가기":
                        Console.WriteLine("게임을 종료합니다.");
                        break;
                }
            }
        }

        static void Iventory(Player player)
        {
            Console.WriteLine("그만두려면 '-1'이나 '나가기' 입력하세요!");
            player.DisplayIventory("의 인벤토리(사용할 아이템의 번호를 입력해주세요.)");
            string strInputText = Console.ReadLine();
            if (strInputText == "나가기") return;
            int nSelectIdx = int.Parse(strInputText);
            if (nSelectIdx == -1) return;
            Item selectItem = player.GetIvenventoryItemIdx(nSelectIdx);
            if (selectItem != null) 
                selectItem.Use(player); 
            player.Display("의 장비함(장비를 해제하려면 장비함를 입력해주세요.)");
            strInputText = Console.ReadLine();
            if (strInputText == "나가기") return;
            nSelectIdx = int.Parse(strInputText);
            if (nSelectIdx == -1) return;
            if (nSelectIdx < (int)Player.E_EQUMENT_TYPE.MAX)
                player.m_llistEqument[nSelectIdx].Use(player);   
        }

        static void Store(Player player, Player npc)
        {
            npc.DisplayIventory("의 상점 목록(선택할 아이템의 번호를 입력하세요");
            string strInputText = Console.ReadLine();
            int nSelectIdx = int.Parse(strInputText);
            player.StoreBuy(npc, nSelectIdx);
            player.DisplayIventory("의 인벤토리");
        }

        static void Battle(Player player, Player monster)
        {
            string strInput;
            while (true)
            {
                Console.WriteLine("행동을 선택하세요!");
                strInput = Console.ReadLine();
                if (strInput == "공격")
                {
                    player.Display("가 공격했다!");
                    player.Attack(monster);
                }
                else
                {
                    player.UseItemSlot();
                    player.Display("가 아이템을 사용했다!");
                }

                monster.Display("이 피해를 입었다!");
                if (monster.Death())
                {
                    monster.Display("이 패배했다.");
                    Item item = monster.ReleaseItem();
                    player.SetItemSlot(item);
                    player.Display("가 아이템을 획득했다!");
                    break;
                }

                monster.Display("이 공격했다!");
                monster.Attack(player);
                player.Display("가 피해를 입었다!");
                if (player.Death())
                {
                    player.Display("가 패배했다.");
                    break;
                }
            }
        }
    }
}
