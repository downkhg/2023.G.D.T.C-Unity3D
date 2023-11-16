/*
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
        }

        static void TextRPGMain()
        {
            Player player;
            Player monster;

            player = new Player("player", 10, 20);
            monster = new Player("slime", 10, 20);

            player.SetItemSlot(new Item("힐링포션(소)", 10));
            monster.SetItemSlot(new Item("힐링포션(소)",10));

            while (true)
            {
                string strInput;

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
