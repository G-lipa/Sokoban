using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

internal class Program
{
    //地图文件夹[*****注意*****]
    public static string? mapFilePath;
    //基本属性
    public static int[,]? Map;
    public static int _4x, _4y;
    public static int XLength, YLength;
    public static int boxNumber;

    private static void Main(string[] args)
    {
        // 获取地图文件夹的路径
        mapFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + "\\Map";
        Console.WriteLine($"[MAP] {mapFilePath}");
        // return;

        for (; ; )
        {
            Console.WriteLine("[1] 游戏\n[2] 创建地图\n[3] 控制介绍\n[B] 退出");
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
                case '1':
                    Sokoban();
                    break;
                case '2':
                    GetMapString();
                    break;
                case '3':
                    DisplayInfo();
                    break;
                case 'B':
                case 'b':
                    return;
            }
            Console.Clear();
        }
    }

    //推箱子
    public static void Sokoban()
    {
    restart:
        Console.ForegroundColor = ConsoleColor.White;
        // Console.Clear();

        GetMap();

        if (Map == null) return;

        for (; ; )
        {
            //清屏
            Console.Clear();

            //打印地图 - //0空气，1墙壁，2箱位，3箱子，4玩家，5箱位上的箱子，6箱位上的玩家
            for (int i = 0; i < XLength; i++)
            {
                for (int j = 0; j < YLength; j++)
                {
                    switch (Map[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                        case 5:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 6:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                    }
                    Console.Write("  ");
                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }

            //判断是否通关
            int count = 0;
            for (int i = 0; i < XLength; i++)
            {
                for (int j = 0; j < YLength; j++)
                {
                    if (Map[i, j] == 5)
                        count++;
                    if (boxNumber == count)
                    {
                        Console.WriteLine("通关啦！");
                        Console.ReadKey(true);
                        return;
                    }
                }
            }

            //操作 - 控制方向
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (Map[_4x - 1, _4y] == 3 || Map[_4x - 1, _4y] == 5)
                    {
                        if (!(Map[_4x - 2, _4y] == 3 || Map[_4x - 2, _4y] == 1 || Map[_4x - 2, _4y] == 5))
                        {
                            //移动箱子
                            Map[_4x - 1, _4y] -= 3;
                            Map[_4x - 2, _4y] += 3;
                            //玩家移动
                            Map[_4x - 1, _4y] += 4;
                            Map[_4x, _4y] -= 4;
                            _4x--;
                        }
                    }
                    else if (Map[_4x - 1, _4y] != 1)
                    {
                        //玩家移动
                        Map[_4x - 1, _4y] += 4;
                        Map[_4x, _4y] -= 4;
                        _4x--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (Map[_4x + 1, _4y] == 3 || Map[_4x + 1, _4y] == 5)
                    {
                        if (!(Map[_4x + 2, _4y] == 3 || Map[_4x + 2, _4y] == 1 || Map[_4x + 2, _4y] == 5))
                        {
                            //移动箱子
                            Map[_4x + 1, _4y] -= 3;
                            Map[_4x + 2, _4y] += 3;
                            //玩家移动
                            Map[_4x + 1, _4y] += 4;
                            Map[_4x, _4y] -= 4;
                            _4x++;
                        }
                    }
                    else if (Map[_4x + 1, _4y] != 1)
                    {
                        //玩家移动
                        Map[_4x + 1, _4y] += 4;
                        Map[_4x, _4y] -= 4;
                        _4x++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    if (Map[_4x, _4y - 1] == 3 || Map[_4x, _4y - 1] == 5)
                    {
                        if (!(Map[_4x, _4y - 2] == 3 || Map[_4x, _4y - 2] == 1 || Map[_4x, _4y - 2] == 5))
                        {
                            //移动箱子
                            Map[_4x, _4y - 1] -= 3;
                            Map[_4x, _4y - 2] += 3;
                            //玩家移动
                            Map[_4x, _4y - 1] += 4;
                            Map[_4x, _4y] -= 4;
                            _4y--;
                        }
                    }
                    else if (Map[_4x, _4y - 1] != 1)
                    {
                        //玩家移动
                        Map[_4x, _4y - 1] += 4;
                        Map[_4x, _4y] -= 4;
                        _4y--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (Map[_4x, _4y + 1] == 3 || Map[_4x, _4y + 1] == 5)
                    {
                        if (!(Map[_4x, _4y + 2] == 3 || Map[_4x, _4y + 2] == 1 || Map[_4x, _4y + 2] == 5))
                        {
                            //移动箱子
                            Map[_4x, _4y + 1] -= 3;
                            Map[_4x, _4y + 2] += 3;
                            //玩家移动
                            Map[_4x, _4y + 1] += 4;
                            Map[_4x, _4y] -= 4;
                            _4y++;
                        }
                    }
                    else if (Map[_4x, _4y + 1] != 1)
                    {
                        //玩家移动
                        Map[_4x, _4y + 1] += 4;
                        Map[_4x, _4y] -= 4;
                        _4y++;
                    }
                    break;
                case ConsoleKey.B:
                    goto restart;
            }
        }
    }

    //选择地图
    private static void GetMap()
    {
        if (mapFilePath == null)
            return;
        string[] allMapFile = Directory.GetFiles(mapFilePath);  //获取所有地图
        if (allMapFile.Length == 0) //默认地图
        {
            // 0空气，1墙壁，2箱位，3箱子，4玩家，5箱位上的箱子，6箱位上的玩家
            Map = new int[7, 10] {
            { 0,1,1,1,1,1,1,1,0,0 },
            { 0,1,0,0,0,0,0,1,1,1 },
            { 1,1,3,1,1,1,0,0,0,1 },
            { 1,0,4,0,3,0,0,3,0,1 },
            { 1,0,2,2,1,0,3,0,1,1 },
            { 1,1,2,2,1,0,0,0,1,0 },
            { 0,1,1,1,1,1,1,1,1,0 }};

            boxNumber = 4;
            XLength = Map.GetLength(0);
            YLength = Map.GetLength(1);
            _4x = 3;
            _4y = 2;
            return;
        }

        string? map;
        string? input;
        do
        {
            Console.Clear();
            Console.WriteLine(" >> 选择 关卡 <<\n[ Enter ] 完成\n");
            for (int i = 1; i <= allMapFile.Length; i++)
                Console.WriteLine($"[ {i} ]");
            input = Console.ReadLine();
            if (input == "b" || input == "B") return;
            if (!int.TryParse(input, out int mapNumber))
                continue;
            if (mapNumber > 0 && mapNumber <= allMapFile.Length)
            {
                using (StreamReader reader = new StreamReader(allMapFile[mapNumber - 1]))
                {
                    if (reader.ReadLine() == "<XLG2005002540>")
                    {
                        map = reader.ReadLine() ?? "0111100010010011001111000021103304110100211111111";
                        if (!int.TryParse(reader.ReadLine(), out XLength))
                            continue;
                        if (!int.TryParse(reader.ReadLine(), out YLength))
                            continue;
                        if (!int.TryParse(reader.ReadLine(), out _4x))
                            continue;
                        if (!int.TryParse(reader.ReadLine(), out _4y))
                            continue;
                        if (!int.TryParse(reader.ReadLine(), out boxNumber))
                            continue;
                        break;
                    }
                }
            }
        } while (true);
        Map = new int[XLength, YLength];
        for (int i = 0, count = 0; i < XLength; i++)
            for (int j = 0; j < YLength; j++, count++)
            {
                Map[i, j] = map[count] - 48;
            }
    }

    //创建地图
    public static void GetMapString()
    {
        int user_x = 0, user_y = 0;
        int rowNumber, columnNumber;
        int[,] map;
        int boxNumber = 0;
        int x_4 = 0, y_4 = 0;
        StringBuilder mapStyle = new StringBuilder();

        while (true)
        {
            Console.Clear();
            Console.Write("行数:");
            if (!int.TryParse(Console.ReadLine(), out rowNumber))
            {
                continue;
            }
            Console.Write("列数:");
            if (!int.TryParse(Console.ReadLine(), out columnNumber))
            {
                continue;
            }
            map = new int[rowNumber, columnNumber];
            break;
        }

        while (true)
        {
            //清屏
            Console.Clear();

            //打印地图 - //0空气，1墙壁，2箱位，3箱子，4玩家，5箱位上的箱子，6箱位上的玩家
            Console.WriteLine("[0]空气 [1]墙壁 [2]箱位 [3]箱子 [4]玩家 \n[5]箱位上的箱子 [6]箱位上的玩家\n[B] 退出\n");
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    if (i == user_y && j == user_x)
                    {
                        Console.Write("□");
                    }
                    switch (map[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                        case 5:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 6:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                    }
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '0':
                    map[user_y, user_x] = 0;
                    break;
                case '1':
                    map[user_y, user_x] = 1;
                    break;
                case '2':
                    map[user_y, user_x] = 2;
                    break;
                case '3':
                    map[user_y, user_x] = 3;
                    break;
                case '4':
                    map[user_y, user_x] = 4;
                    break;
                case '5':
                    map[user_y, user_x] = 5;
                    break;
                case '6':
                    map[user_y, user_x] = 6;
                    break;
                case '\b':
                    if (user_x - 1 == -1 && user_y - 1 != -1)
                    {
                        user_y--;
                        user_x = columnNumber - 1;
                    }
                    else
                    {
                        user_x--;
                    }
                    map[user_y, user_x] = 0;
                    continue;
                case 'b':
                case 'B':
                    return;
            }
            if (user_x + 1 == columnNumber)
            {
                user_y++;
                user_x = 0;
            }
            else
            {
                user_x++;
            }
            if (user_y == rowNumber && user_x == 0)
            {
                break;
            }
        }

        Console.Clear();

        //格式化 - 0空气，1墙壁，2箱位，3箱子，4玩家，5箱位上的箱子，6箱位上的玩家
        for (int i = 0; i < rowNumber; i++)
            for (int j = 0; j < columnNumber; j++)
            {
                mapStyle.Append(map[i, j]);
                //记录玩家位置
                if (map[i, j] == 4 || map[i, j] == 6)
                {
                    y_4 = i;
                    x_4 = j;
                }
                //记录箱子个数
                if (map[i, j] == 3 || map[i, j] == 5)
                    boxNumber++;
            }
        if (y_4 == 0 && x_4 == 0)
            return;
        //地图数据
        string mapData = $"<XLG2005002540>\n{mapStyle}\n{rowNumber}\n{columnNumber}\n{y_4}\n{x_4}\n{boxNumber}";
        //将地图数据写入文件
        if (mapFilePath == null)
            return;
        int mapNumber = Directory.GetFiles(mapFilePath).Length + 1;
        string filePath = mapFilePath + "\\" + mapNumber + ".XLG";
        File.WriteAllText(filePath, mapData);
        // 调试代码
        Console.WriteLine(filePath);
        Console.ReadKey();
    }

    //展示介绍
    public static void DisplayInfo()
    {
        Console.Clear();
        Console.WriteLine("[ W/↑ ]\t\t上\n[ S/↓ ]\t\t下\n[ A/← ]\t\t左\n[ D/→ ]\t\t右\n[ Enter ]\t确认\n[ B ]\t\t退出/重来");
        Console.ReadKey(true);
    }
}