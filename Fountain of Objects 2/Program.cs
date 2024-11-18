using System;
using System.Drawing;
using Events;

class Program
{
    static void Main(string[] args)
    {
        //BattleEvent battleEvent = new BattleEvent();
        //battleEvent.StartInteraction(new Player("Knight", 100, 10, new GreatSword()), new Monster("Goblin",25, 5, new Stick()));
        Player player = new Player("Knight", 100, 10, new Sword());
        Map map = new Map(3,3, player);
        map.Start();
    }
}