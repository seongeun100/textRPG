namespace CSharpDay;

class Program
{
    static void Main(string[] args)
    {
        var gameLogic = new GameLogic();
        gameLogic.StartGame();
    }
}

class GameLogic
{
    private Player player;
    private bool isGameOver = false;

    public void StartGame()
    {
        Init();

        while (!isGameOver)
        {
            MainMenu();
        }
    }

    private void InputHandler()
    {
        Console.WriteLine("게임을 종료하시려면 ESC키를 눌러주세요.");
        var input = Console.ReadKey();
        if (input.Key == ConsoleKey.Escape)
        {
            isGameOver = true;
        }
    }

    private void Init()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신것을 환영합니다.\n이름을 입력하세요.");
            string? playerName = Console.ReadLine();

            if (string.IsNullOrEmpty(playerName))
            {
                Console.WriteLine("잘못된 이름입니다.");
                Thread.Sleep(1000);
                continue;
            }
            else
            {
                player = new Player(playerName);
                Console.WriteLine($"{player.name}님, 입장하셨습니다.");
                break;
            }
        }
        while (true)
        {
            Console.WriteLine("직업을 선택하세요. [1:전사 | 2:법사 | 3:궁수 | 4:도적 ]");
            bool isValid = int.TryParse(Console.ReadLine(), out int job);

            if (isValid && job >= 1 && job <= 4)
            {
                player.job = (Job)job;
                Console.WriteLine($"{player.job}를 선택하셨습니다.");
                break;
            }
            else
            {
                Console.WriteLine("올바른 직업을 선택해주세요.");
                continue;
            }
        }
        MainMenu();
    }
    private void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점");
        Console.WriteLine("\n0. 게임 종료");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        int.TryParse(Console.ReadLine(), out int index);

        switch (index)
        {
            case 1: Status(); break;
            case 2: Inventory(); break;
            case 3: Store(); break;
            case 0: InputHandler(); break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
                break;
        }
    }

    private void Status()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {player.level.ToString("D2")}");
            Console.WriteLine($"Chad {player.job}");
            Console.WriteLine($"공격력 : {player.totalAtk}");
            Console.WriteLine($"방어력 : {player.totalDef}");
            Console.WriteLine($"체 력 : {player.hp}");
            Console.WriteLine($"Gold : {player.gold} G\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요\n>>");

            bool isSelectedMenu = int.TryParse(Console.ReadLine(), out int index);
            if (!isSelectedMenu)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                Thread.Sleep(1000);
                continue;
            }
            if (index == 0)
            {
                MainMenu();
                return;
            }
            else
            {
                Console.WriteLine("올바른 행동을 입력해주세요.");
                Thread.Sleep(1000);
            }
        }
    }

    private void Inventory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < player.inventory.Count; i++)
            {
                Item item = player.inventory[i];
                bool isEquipped = player.equipItems.Contains(item);
                Console.WriteLine($"{player.inventory[i].ItemList(isEquipped)}");
            }
            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요\n>>");
            bool isSelectedMenu = int.TryParse(Console.ReadLine(), out int index);
            if (!isSelectedMenu)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                Thread.Sleep(1000);
                continue;
            }
            if (index == 0)
            {
                MainMenu();
                return;
            }
            else if (index == 1)
            {
                EquipItem();
            }
            else
            {
                Console.WriteLine("올바른 행동을 입력해주세요.");
                Thread.Sleep(1000);
            }
        }
    }
    private void EquipItem()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < player.inventory.Count; i++)
            {
                Item item = player.inventory[i];
                bool isEquipped = player.equipItems.Contains(item);
                Console.WriteLine($"{i + 1}. {player.inventory[i].ItemList(isEquipped)}");
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요\n>>");

            bool isSelectedMenu = int.TryParse(Console.ReadLine(), out int index);
            if (!isSelectedMenu)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                Thread.Sleep(1000);
                continue;
            }
            if (index == 0)
            {
                return;
            }
            else if (index >= 1 && index <= player.inventory.Count)
            {
                Item selected = player.inventory[index - 1];
                ItemType itemType = selected.type;

                if (player.equipItems.Contains(selected))
                {
                    player.equipItems.Remove(selected);
                    Console.WriteLine($"{selected.name}을(를) 장착 해제했습니다.");
                }
                else
                {
                    Item? equipped = null;

                    foreach (Item item in player.equipItems)
                    {
                        if (item.type == itemType)
                        {
                            equipped = item;
                            break;
                        }
                    }
                    if (equipped != null)
                    {
                        player.equipItems.Remove(equipped);
                        player.equipItems.Add(selected);
                        Console.WriteLine($"{equipped.name}을(를) 해제하고 {selected.name}을(를) 장착했습니다.");
                    }
                    else
                    {
                        player.equipItems.Add(selected);
                        Console.WriteLine($"{selected.name}을(를) 장착했습니다.");
                    }
                }
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("올바른 입력을 해주세요.");
                Thread.Sleep(1000);
            }
        }
    }
    private void Store()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("인벤토리\n필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");
            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < Item.storeItemList.Length; i++)
            {
                Item item = Item.storeItemList[i];
                bool isPurchased = player.inventory.Contains(item);

                Console.WriteLine(item.ItemInfo(isPurchased));
            }
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요\n>>");

            bool isSelectedMenu = int.TryParse(Console.ReadLine(), out int index);
            if (!isSelectedMenu)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                Thread.Sleep(1000);
                continue;
            }
            if (index == 0)
            {
                MainMenu();
                return;
            }
            else if (index == 1)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("상점 - 아이템 구매");
                    Console.WriteLine("인벤토리\n필요한 아이템을 얻을 수 있는 상점입니다.\n");

                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine($"{player.gold} G\n");
                    Console.WriteLine("[아이템 목록]\n");
                    for (int i = 0; i < Item.storeItemList.Length; i++)
                    {
                        Item item = Item.storeItemList[i];
                        bool isPurchased = player.inventory.Contains(item);

                        Console.WriteLine(item.ItemInfo(i + 1, isPurchased));
                    }
                    Console.WriteLine("\n0. 나가기");
                    Console.WriteLine("\n원하시는 행동을 입력해주세요\n>>");

                    bool isSelectedItem = int.TryParse(Console.ReadLine(), out int itemIndex);
                    if (!isSelectedItem)
                    {
                        Console.WriteLine("숫자를 입력해주세요.");
                        Thread.Sleep(1000);
                        continue;
                    }

                    if (itemIndex == 0)
                    {
                        break;
                    }
                    else if (itemIndex >= 1 && itemIndex <= Item.storeItemList.Length)
                    {
                        Item selectedItem = Item.storeItemList[itemIndex - 1];
                        if (player.inventory.Contains(selectedItem))
                        {
                            Console.WriteLine("이미 구매한 아이템입니다.");
                        }
                        else if (player.gold >= selectedItem.price)
                        {
                            player.gold -= selectedItem.price;
                            player.inventory.Add(selectedItem);
                            Console.WriteLine($"구매를 완료했습니다.");
                        }
                        else
                        {
                            Console.WriteLine("Gold가 부족합니다.");
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                    }
                }
            }
            else
            {
                Console.WriteLine("올바른 입력을 해주세요.");
                Thread.Sleep(1000);
            }
        }
    }
}

public class Player
{
    public string name;
    public Job job;

    public int level;
    public int hp;
    public int atk;
    public int def;
    public int gold;
    public string description;

    public List<Item> inventory = new List<Item>();
    public List<Item> equipItems = new List<Item>();

    public int totalAtk
    {
        get
        {
            int total = atk;
            foreach (Item item in equipItems)
            {
                total += item.atk;
            }
            return total;
        }
    }
    public int totalDef
    {
        get
        {
            int total = def;
            foreach (Item item in equipItems)
            {
                total += item.def;
            }
            return total;
        }
    }

    public Player(string name)
    {
        this.name = name;

        this.level = 1;
        this.atk = 10;
        this.def = 5;
        this.hp = 100;
        this.gold = 15000;
    }
}

public class Item
{
    public string name;
    public string description;
    public ItemType type;
    public int atk;
    public int def;
    public int price;

    public Item(string name, ItemType type, int atk, int def, string description, int price)
    {
        this.name = name;
        this.type = type;
        this.atk = atk;
        this.def = def;
        this.description = description;
        this.price = price;
    }
    public static Item[] storeItemList = new Item[]
    {
        new Item("수련자 갑옷", ItemType.Armor, 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
        new Item("무쇠갑옷", ItemType.Armor, 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
        new Item("스파르타의 갑옷", ItemType.Armor,  0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
        new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
        new Item("청동 도끼", ItemType.Weapon, 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
        new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000)
    };

    public string ItemInfo(bool purchased)
    {
        string stat = atk > 0 ? $"공격력 +{atk}" : $"방어력 +{def}";
        string priceText = purchased ? "구매완료" : $"{price} G";
        return $"- {name} | {stat} | {description} | {priceText}";
    }

    public string ItemInfo(int index, bool purchased)
    {
        string stat = atk > 0 ? $"공격력 +{atk}" : $"방어력 +{def}";
        string priceText = purchased ? "구매완료" : $"{price} G";
        return $"- {index} {name} | {stat} | {description} | {priceText}";
    }
    public string ItemList()
    {
        string stat = atk > 0 ? $"공격력 +{atk}" : $"방어력 +{def}";
        return $"- {name} | {stat} | {description}";
    }

    public string ItemList(bool isEquipped)
    {
        string equipTag = isEquipped ? "[E]" : " ";
        string stat = atk > 0 ? $"공격력 +{atk}" : $"방어력 +{def}";
        return $"- {equipTag}{name} | {stat} | {description}";
    }

}

public enum ItemType
{
    Weapon,
    Armor
}

public enum Job
{
    Warrior = 1,
    Wizzard,
    Archor,
    Rogue
}