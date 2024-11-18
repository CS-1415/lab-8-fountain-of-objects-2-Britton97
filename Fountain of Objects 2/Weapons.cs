public abstract class Weapon_Abs
{

    public enum DiceType
    {
        D4,
        D6,
        D8,
        D10,
        D12,
        D20
    }
    private string name = string.Empty;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private DiceType diceType;
    public int Damage
    {
        get
        {
            //switch statement to determine the damage based on the dice type
            //then randomly generate a number between 1 and the max value of the dice
            //return the value
            switch (diceType)
            {
                case DiceType.D4:
                    return new Random().Next(1, 5) + DamageModifier;
                case DiceType.D6:
                    return new Random().Next(1, 7) + DamageModifier;
                case DiceType.D8:
                    return new Random().Next(1, 9) + DamageModifier;
                case DiceType.D10:
                    return new Random().Next(1, 11) + DamageModifier;
                case DiceType.D12:
                    return new Random().Next(1, 13) + DamageModifier;
                case DiceType.D20:
                    return new Random().Next(1, 21) + DamageModifier;
                default:
                    return 0;
            }
        }
    }

    public int MaxDamage
    {
        get
        {
            switch (diceType)
            {
                case DiceType.D4:
                    return 4 + DamageModifier;
                case DiceType.D6:
                    return 6 + DamageModifier;
                case DiceType.D8:
                    return 8 + DamageModifier;
                case DiceType.D10:
                    return 10 + DamageModifier;
                case DiceType.D12:
                    return 12 + DamageModifier;
                case DiceType.D20:
                    return 20 + DamageModifier;
                default:
                    return 0;
            }
        }
    }

    private int damageModifier;
    public int DamageModifier
    {
        get { return damageModifier; }
        set { damageModifier = value; }
    }

    public Weapon_Abs(string _name = "Default", DiceType _diceType = DiceType.D6)
    {
        Name = _name;
        diceType = _diceType;
    }

    public string DisplayWeaponStats()
    {
        string damageRange = "";
        switch (diceType)
        {
            case DiceType.D4:
                damageRange = "1-4";
                break;
            case DiceType.D6:
                damageRange = "1-6";
                break;
            case DiceType.D8:
                damageRange = "1-8";
                break;
            case DiceType.D10:
                damageRange = "1-10";
                break;
            case DiceType.D12:
                damageRange = "1-12";
                break;
            case DiceType.D20:
                damageRange = "1-20";
                break;
            default:
                break;
        }
        string weaponStats = $"{Name} - {diceType} Damage";
        if(damageModifier != 0)
        {
            weaponStats = $"{Name}({diceType}): {damageRange} + {DamageModifier} Damage";
        }
        else if(damageModifier == 0)
        {
            weaponStats = $"{Name}({diceType}): {damageRange} Damage";
        }
        return weaponStats;
    }
}

public class Sword : Weapon_Abs
{
    public Sword(string _name = "Sword", DiceType _diceType = DiceType.D6) : base(_name, _diceType)
    {
    }
}

public class Spear : Weapon_Abs
{
    public Spear(string _name = "Spear", DiceType _diceType = DiceType.D8) : base(_name, _diceType)
    {
    }
}

public class GreatSword : Weapon_Abs
{
    public GreatSword(string _name = "Great Sword", DiceType _diceType = DiceType.D12) : base(_name, _diceType)
    {
    }
}

public class Fist : Weapon_Abs
{
    public Fist(string _name = "Fist", DiceType _diceType = DiceType.D4) : base(_name, _diceType)
    {
    }
}

public class Stick : Weapon_Abs
{
    public Stick(string _name = "Stick", DiceType _diceType = DiceType.D4) : base(_name, _diceType)
    {
        DamageModifier = 1;
    }
}