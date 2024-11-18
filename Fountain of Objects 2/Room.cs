using Events;

public abstract class Room_Abs
{
    private string name = string.Empty;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int rowPosition;
    public int RowPosition
    {
        get { return rowPosition; }
        set { rowPosition = value; }
    }

    private int columnPosition;
    public int ColumnPosition
    {
        get { return columnPosition; }
        set { columnPosition = value; }
    }

    private bool alreadyVisited = false;
    public bool AlreadyVisited
    {
        get { return alreadyVisited; }
        set
        {
            if (value == true && alreadyVisited == false)
            {
                alreadyVisited = value;
            }
        }
    }

    //constructor
    public Room_Abs(int _rowPostion, int _columnPosition, string _name = "Default Room")
    {
        Name = _name;
        RowPosition = _rowPostion;
        ColumnPosition = _columnPosition;
    }

    public abstract void OnPlayerEnter(Player player);

}

public class EmptyRoom : Room_Abs
{
    public EmptyRoom(int rowPosition, int columnPosition, string name = "Empty Room") : base(rowPosition, columnPosition, name) { }
    public override void OnPlayerEnter(Player player)
    {
        Console.WriteLine("You entered an empty room");
    }
}

public class BattleRoom : Room_Abs
{
    private Monster monster = default!;
    public Monster Monster
    {
        get { return monster; }
        set { monster = value; }
    }

    private BattleEvent battleEvent = default!;

    public BattleRoom(int rowPosition, int columnPosition, string name = "Battle Room") : base(rowPosition, columnPosition, name)
    {
        //randomly generate a monster
        //random number between 1 and 5
        int weaponType = new Random().Next(1, 6);
        switch (weaponType)
        {
            case 1:
                Monster = new Monster("Goblin", 10, 0, new Fist());
                break;
            case 2:
                Monster = new Monster("Ugly Goblin", 15, 2, new Sword());
                break;
            case 3:
                Monster = new Monster("Mean Goblin", 25, 3, new Spear());
                break;
            case 4:
                Monster = new Monster("Orc", 25, 2, new GreatSword());
                break;
            case 5:
                Monster = new Monster("Ugly Orc", 30, 1, new Stick());
                break;
            default:
                Monster = new Monster("Baby Goblin", 5, 1, new Fist());
                break;
        }
        battleEvent = new BattleEvent();
    }

    public override void OnPlayerEnter(Player player)
    {
        //Console.WriteLine($"You entered a room with a {Monster.Name}");
        if (AlreadyVisited)
        {
            Console.WriteLine($"You entered {Name}");
            return;
        }
        battleEvent.StartInteraction(player, Monster);
        if (battleEvent.monster?.Health <= 0)
        {
            Name = "Battle Room (Cleared)";
            AlreadyVisited = true;
        }
    }
}

public class FountainOfObjectsRoom : Room_Abs
{
    public FountainOfObjectsEvent fountainOfObjectsEvent = default!;
    public FountainOfObjectsRoom(int rowPosition, int columnPosition, string name = "Fountain of Objects") : base(rowPosition, columnPosition, name)
    {
        fountainOfObjectsEvent = new FountainOfObjectsEvent();
    }

    public void SetPosition(int row, int column)
    {
        RowPosition = row;
        ColumnPosition = column;
    }
    public override void OnPlayerEnter(Player player)
    {
        if (fountainOfObjectsEvent.activated)
        {
            Console.WriteLine("You enter the Fountain of Objects Room\r\nYou have already activated the fountain");
        }
        else
        {
            fountainOfObjectsEvent.StartInteraction(player, "Fountain of Objects");
        }
    }
}

public class EntranceRoom : Room_Abs
{
    FountainOfObjectsEvent fountain = default!;
    private EntranceEvent entranceEvent = default!;
    public EntranceRoom(FountainOfObjectsEvent _fountain, int rowPosition, int columnPosition, string name = "Entrance Room") : base(rowPosition, columnPosition, name)
    {
        fountain = _fountain;
        entranceEvent = new EntranceEvent();
    }

    public void SetPosition(int row, int column)
    {
        RowPosition = row;
        ColumnPosition = column;
    }

    public override void OnPlayerEnter(Player player)
    {
        if (fountain.activated)
        {
            Console.WriteLine("You can leave the duneon now that you have activated the fountain");
            entranceEvent.StartInteraction(player, fountain);
        }
        else
        {
            Console.WriteLine("You cannot leave until you activate the fountain");
        }
    }
}