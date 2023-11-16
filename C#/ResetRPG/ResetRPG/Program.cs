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

            player = new Player("player", 10, 100);
            monster = new Player("slime", 10, 100);

            player.Attack(monster);

            monster.Attack(player);
        }
    }
}
