using System.Drawing;
public class BattleSystem
{
    //make a constructor that takes in player and monster
    private Player player;
    private Monster monster;
    private Color healthyColor = Color.Green; // Starting color
    private Color endColor = Color.Red; // Ending color
    private ConsoleTextPrinter colorLerper = new ConsoleTextPrinter();
    public BattleSystem(Player player, Monster monster)
    {
        //set the player and monster
        this.player = player;
        this.monster = monster;
    }

    public void StartBattle()
    {
        Console.Clear();
        List<string> battleLog = new List<string>();
        bool battleEnded = false;
        bool playerTurn = true;
        while (!battleEnded)
        {
            if(battleEnded) { break; }
            Console.Clear();
            BattleHeader();
            if (battleLog.Count > 0)
            {
                //foreach (string log in battleLog) but only the last 5 logs
                for (int i = Math.Max(0, battleLog.Count - 5); i < battleLog.Count; i++)
                {
                    Console.WriteLine(battleLog[i]);
                }
            }
            if (player.Health <= 0)
            {
                Console.WriteLine("You died!");
                battleEnded = true;
            }
            else if (monster.Health <= 0)
            {
                if(monster.weapon.MaxDamage > player.weapon.MaxDamage)
                {
                    Console.WriteLine($"You won the battle, but the {monster.Name} had a better weapon!\r\nYou equip it's {monster.weapon.Name} and discard your {player.weapon.Name}");
                    player.weapon = monster.weapon;
                }
                Console.WriteLine("\nYou won the battle! (Press any key to continue)");
                battleEnded = true;
            }

            if(battleEnded) { break; }
            Wait(1000);
            if (playerTurn)
            {
                string attackInfo = player.OnAttack(monster);
                Console.WriteLine(attackInfo);
                battleLog.Add(attackInfo);
                playerTurn = false;
            }
            else
            {
                string attackInfo = monster.OnAttack(player);
                Console.WriteLine(attackInfo);
                battleLog.Add(attackInfo);
                playerTurn = true;
            }
        }
    }

    public static void Wait(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

    public void BattleHeader()
    {
        Color playerColor = colorLerper.LerpColor(endColor, healthyColor, 0, player.MaxHealth, player.Health);
        Color monsterColor = colorLerper.LerpColor(endColor, healthyColor, 0, monster.MaxHealth, monster.Health);
        Console.Write($"{player.Name} AC({player.ArmorClass}): ");
        colorLerper.PrintColoredText($"{player.Health}", playerColor);
        Console.Write(" HP ------ ");
        Console.Write($"{monster.Name} AC({monster.ArmorClass}): ");
        colorLerper.PrintColoredText($"{monster.Health}", monsterColor);
        Console.WriteLine(" HP");
    }
}