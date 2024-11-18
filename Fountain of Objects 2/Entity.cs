public abstract class Entity_Abs
{
    private string name = string.Empty;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    private int armorClass;
    public int ArmorClass
    {
        get { return armorClass; }
        set { armorClass = value; }
    }

    public Weapon_Abs weapon;
    public int AttackDamage
    {
        get { return weapon.Damage; }
    }

    //constructor
    public Entity_Abs( Weapon_Abs _weapon, string _name = "Default", int _health = 100, int _armorClass = 5)
    {
        Name = _name;
        Health = _health;
        MaxHealth = 100;
        weapon = _weapon;
        ArmorClass = _armorClass;
    }


    public void OnTakeDamage(int damage)
    {
        if (damage > ArmorClass)
        { //if the damage is greater than the armor class, subtract the armor class from the damage
            damage -= ArmorClass;
        }
        else
        {
            damage = 0;
        }

        Health -= damage;
        if (Health <= 0)
        {
            OnDeath();
        }
    }
    public abstract void OnDeath();
    public string OnAttack(Entity_Abs target)
    {
        //Console.WriteLine($"{Name} attacks {target.Name} for {AttackDamage} damage!");
        int skillCheck = new Random().Next(1, 21);
        //if skillCheck is higher or equal to the target's armor class, the attack hits
        if (skillCheck >= target.ArmorClass)
        {
            int damage = weapon.Damage;
            target.OnTakeDamage(damage);
            string attackInfo = $"{Name} attacks {target.Name} with {weapon.Name} for {damage} damage!\r\n- {Name} rolls ({skillCheck}) and bypasses {target.Name}'s AC({target.ArmorClass})";
            return attackInfo;
        }
        else
        {
            string attackInfo = $"{Name} attacks {target.Name} with {weapon.Name} and misses!\r\n- {Name} rolls ({skillCheck}) and cannot overcome {target.Name}'s AC({target.ArmorClass})";
            return attackInfo;
        }
        //target.OnTakeDamage(AttackDamage);
        //string attackInfo = $"{Name} attacks {target.Name} for {AttackDamage} damage!";
        //return attackInfo;
    }
}

//player entity class, but on death it will print a message and quit the application
public class Player : Entity_Abs
{
    public Player(string name, int health, int armorClass, Weapon_Abs weapon) : base(weapon, name, health, armorClass){}
    public override void OnDeath()
    {
        Console.WriteLine("You died!");
        Environment.Exit(0);
    }
}

//monster entity class, but on death it will print a message and say you won the battle
public class Monster : Entity_Abs
{
    public Monster(string name, int health, int armorClass, Weapon_Abs weapon) : base(weapon, name, health, armorClass){}
    public override void OnDeath()
    {
        Console.WriteLine("You won the battle!");
    }
}
