namespace Events
{
    public abstract class Event_Abs<T1, T2>
    {
        #region Key Mapping
        public Dictionary<ConsoleKey, string> keyMap = new Dictionary<ConsoleKey, string>
        {
            { ConsoleKey.UpArrow, "up" },
            { ConsoleKey.DownArrow, "down" },
            { ConsoleKey.Enter, "enter" }
        };
        #endregion
        #region Properties
        protected List<string> eventOptions = new List<string>();
        public abstract List<string> EventOptions { get; }
        private string name = "Event";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
        #region Display Functions
        // Display the options and let the user select one
        public string DisplayOptions()
        {
            int selectedIndex = 0;
            bool optionSelected = false;

            while (!optionSelected)
            {
                Console.Clear();
                DisplayMenu(selectedIndex); // Show the menu with the current selection

                var key = Console.ReadKey(intercept: true);
                string input = ReadInput(key);

                // Move selection based on arrow keys or select based on Enter key
                if (input == "up")
                {
                    selectedIndex = (selectedIndex == 0) ? EventOptions.Count - 1 : selectedIndex - 1;
                }
                else if (input == "down")
                {
                    selectedIndex = (selectedIndex == EventOptions.Count - 1) ? 0 : selectedIndex + 1;
                }
                else if (input == "enter")
                {
                    optionSelected = true;
                }
            }

            return EventOptions[selectedIndex]; // Return the selected option
        }

        // Display the options with the '>' indicator at the selected index
        private void DisplayMenu(int selectedIndex)
        {
            Console.WriteLine($"{Name}: What will you do?");
            for (int i = 0; i < EventOptions.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine($"> {EventOptions[i]}");
                }
                else
                {
                    Console.WriteLine($"  {EventOptions[i]}");
                }
            }
        }
        #endregion
        #region Read Input
        // Read input based on keyMap
        public string ReadInput(ConsoleKeyInfo keyPushed)
        {
            if (keyMap.TryGetValue(keyPushed.Key, out string? mappedValue) && mappedValue != null)
            {
                return mappedValue;
            }
            return "";
        }
        #endregion
        public abstract void StartInteraction(T1 t1, T2 t2);
        public abstract bool Interact();
    }

    public class BattleEvent : Event_Abs<Player, Monster>
    {
        // Define specific options for BattleEvent
        //private List<string> eventOptions = new List<string>();
        public override List<string> EventOptions => eventOptions;
        private Player? player = null;
        public Monster? monster = null;
        public override void StartInteraction(Player t1, Monster t2)
        {
            player = t1;
            monster = t2;
            Name = $"Battle with {monster.Name}";
            if (monster != null)
            {
                eventOptions.Insert(0, $"Fight {monster.Name}");
                eventOptions.Insert(1, "Run");
            }
            Interact();
        }
        public override bool Interact()
        {
            if (player == null || monster == null) { return false; }
            //Console.WriteLine("Battle Event");
            string selectedOption = DisplayOptions();

            Console.Clear();
            //Console.WriteLine($"You selected: {selectedOption}");

            if (selectedOption == $"Fight {monster.Name}")
            {
                //Console.WriteLine("You chose to fight the enemy.");
                BattleSystem battleSystem = new BattleSystem(player, monster);
                battleSystem.StartBattle();
            }
            else if (selectedOption == "Run")
            {
                Console.WriteLine("You chose to run away.");
            }

            return true;
        }
    }

    public class FountainOfObjectsEvent : Event_Abs<Player, string>
    {
        public override List<string> EventOptions => eventOptions;
        private Player? player = null;
        public bool activated = false;
        public override void StartInteraction(Player t1, string t2)
        {
            player = t1;
            Name = "Fountain of Objects";
            eventOptions.Insert(0, "Activate Fountain");
            eventOptions.Insert(1, "Leave");
            Interact();
        }
        public override bool Interact()
        {
            if (player == null) { return false; }
            //Console.WriteLine("Fountain of Objects Event");
            string selectedOption = DisplayOptions();

            Console.Clear();
            //Console.WriteLine($"You selected: {selectedOption}");

            if (selectedOption == "Activate Fountain")
            {
                Console.WriteLine("You activate the Fountain.");
                activated = true;
            }
            else if (selectedOption == "Leave")
            {
                Console.WriteLine("You leave the Fountain alone.");
            }

            return true;
        }
    }

    public class EntranceEvent : Event_Abs<Player, FountainOfObjectsEvent>
    {
        public override List<string> EventOptions => eventOptions;
        private Player? player = null;
        public FountainOfObjectsEvent? fountain = null;
        public override void StartInteraction(Player t1, FountainOfObjectsEvent t2)
        {
            player = t1;
            fountain = t2;
            Name = "Entrance";
            eventOptions.Insert(0, "Leave");
            Interact();
        }
        public override bool Interact()
        {
            if (player == null || fountain == null) { return false; }
            //Console.WriteLine("Entrance Event");
            string selectedOption = DisplayOptions();

            Console.Clear();
            //Console.WriteLine($"You selected: {selectedOption}");
            if (selectedOption == "Leave")
            {
                if (fountain.activated)
                {
                    Console.WriteLine("You leave the dungeon. You won. Tudah!");
                    Console.WriteLine("Congratulations! You activated the Fountain of Objects and left the dungeon.");
                    //quit the game
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("You cannont leave until you activate the Fountain of Objects.");
                }
            }

            return true;
        }
    }
}