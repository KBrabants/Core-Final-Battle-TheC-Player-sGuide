using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Final_Battle
{
    public class Entity
    {
        public string? Name { get; set; }
        public bool HasTurn { get; set; } = true;
        public bool RandomActions { get; set; } = false;
        public int Health { get; private set; } = 30;
        public Party party { get; set; } = new Party();
        public ICharacterAction[] Actions { get; set; } = new ICharacterAction[0];
        public void TakeTurn(Encounter encounter)
        {
            if (RandomActions)
            {
                DoRandomAction(encounter);
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"What would you like {Name} to do: ");
            Console.ForegroundColor= ConsoleColor.White;
            while (HasTurn)
            {
                HasTurn = DoPlayerAction(encounter);
            }


            HasTurn = false;
        }

        public bool DoPlayerAction(Encounter encounter)
        {
            int selection = -1;

            for (int i = 0; i < Actions.Length; i++)
            {
                Console.WriteLine($"{i} - {Actions[i]}");
            }

            selection = PlayerInputSystem.GetValidNumber(0, Actions.Length, $"{Name} dosen't know that one...");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Name} used {Actions[selection]}\n");
            Console.ForegroundColor= ConsoleColor.White;
            return Actions[selection].CompleteAction(encounter, this);
        }

        public bool DoRandomAction(Encounter encounter)
        {
            Console.WriteLine($"{Name}'s turn...");
            Thread.Sleep(1000);
            int selection = -1;
            Random random = new Random();
            selection = random.Next(0, Actions.Length);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Name} used {Actions[selection]}\n");
            Console.ForegroundColor = ConsoleColor.White;
            return Actions[selection].CompleteAction(encounter, this);
        }

        public void TakeDamage(int value)
        {
            Health = Health - value;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{Name} lost {value} Health, now has {Health} Health");
            Console.ForegroundColor = ConsoleColor.White;

            if (Health < 1)
            {
                party.PartyMembers.Remove(this);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{Name} has died!!!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void SetHealth(int value)
        {
            Health = value;
        }
    }

    public class Skeleton : Entity
    {
        public Skeleton() {
            Name = "Skeleton";
            RandomActions = true;
            Actions = PreSetActions.Skeleton;
        }
    }

    public class Knight : Entity
    {
        public Knight()
        {
            Name = "Knight";
            Actions = PreSetActions.Knight;
        }

    }

    public class TrueProgrammer : Entity
    {
        public TrueProgrammer(string name)
        {
            Name = name;
            Actions = PreSetActions.PlayerActions;
        }
    }

    public class TheUncodedOne : Entity
    {
        public TheUncodedOne()
        {
            Name = "Uncoded One";
            RandomActions = true;
            Actions = PreSetActions.UncodedOne;
            SetHealth(60);
        }
    }
}
