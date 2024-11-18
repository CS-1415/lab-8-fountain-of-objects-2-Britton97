using System;

public class Map
{
    private int playerRow;
    private int playerColumn;
    private int rows;
    private int columns;
    private Room_Abs[,] rooms;
    private string lastRoomName;
    private Player player;

    public Map(int rows, int columns, Player _player)
    {
        this.rows = rows;
        this.columns = columns;
        playerRow = rows / 2;
        playerColumn = columns / 2;
        rooms = new Room_Abs[rows, columns];
        PopulateRooms();
        lastRoomName = string.Empty;
        player = _player;
    }

    public void PopulateRooms()
    {
        FountainOfObjectsRoom fountainOfObjectsRoom = new FountainOfObjectsRoom(2, 2);
        int randomXPos = new Random().Next(1, rows);
        int randomYPos = new Random().Next(1, columns);
        EntranceRoom entranceRoom = new EntranceRoom(fountainOfObjectsRoom.fountainOfObjectsEvent,0, 0);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i == 0 && j == 0)
                {
                    rooms[i, j] = entranceRoom;
                    entranceRoom.SetPosition(i, j);
                }
                else if (i == randomXPos && j == randomYPos)
                {
                    rooms[i, j] = fountainOfObjectsRoom;
                    fountainOfObjectsRoom.SetPosition(i, j);
                }
                else
                {
                    //random between 0 and 1
                    //if 0, create a battle room
                    //if 1, create an empty room
                    int roomType = new Random().Next(0, 2);
                    if (roomType == 0)
                    {
                        rooms[i, j] = new BattleRoom(i, j);
                    }
                    else
                    {
                        rooms[i, j] = new EmptyRoom(i, j);
                    }
                }
            }
        }
    }

    public void EnterRoom(int row, int column)
    {
        Room_Abs room = rooms[row, column];
        lastRoomName = room.Name;
        room.OnPlayerEnter(player);
    }

    public void PrintGrid()
    {
        Console.Clear();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i == playerRow && j == playerColumn)
                {
                    Console.Write("■ ");
                }
                else
                {
                    Console.Write("□ ");
                }
            }
            Console.WriteLine();
        }
        //Console.WriteLine($"You entered the {lastRoomName}");
    }

    public void MovePlayer(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (playerRow > 0) playerRow--;
                break;
            case ConsoleKey.DownArrow:
                if (playerRow < rows - 1) playerRow++;
                break;
            case ConsoleKey.LeftArrow:
                if (playerColumn > 0) playerColumn--;
                break;
            case ConsoleKey.RightArrow:
                if (playerColumn < columns - 1) playerColumn++;
                break;
        }
        //EnterRoom(playerRow, playerColumn);
    }

    public void Start()
    {
        PrintGrid();
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            MovePlayer(key);
            PrintGrid();
            EnterRoom(playerRow, playerColumn);
        }
    }
}