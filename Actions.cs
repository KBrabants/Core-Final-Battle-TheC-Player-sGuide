using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Battle
{
    public interface ICharacterAction
    {
        /// <summary>
        /// Returns flase if turn should be over.
        /// </summary>
        /// <returns></returns>
        public bool CompleteAction(Encounter encounter, Entity Caller);

    }

    public class DoNothing : ICharacterAction
    {
        public override string ToString() {
            return "Do Nothing";
        }
        public bool CompleteAction(Encounter _, Entity c)
        {
            return false;
        }
    }
    public class BoneCrush : ICharacterAction
    {
        public override string ToString()
        {
            return "Bone Crush";
        }
        public bool CompleteAction(Encounter encounter, Entity C)
        {
            Entity Target = HICharacterAction.GetEnemyTarget(encounter, C);

            Target.TakeDamage(2);

            return false;
        }
    }
    public class GreatSwordSwing : ICharacterAction
    {
        public override string ToString()
        {
            return "Great Sword Swing";
        }
        public bool CompleteAction(Encounter encounter, Entity C)
        {

            Entity Target = HICharacterAction.GetEnemyTarget(encounter, C);

            Target.TakeDamage(10);

            return false;
        }
    }
    public class Punch : ICharacterAction
    {
        public override string ToString()
        {
            return "Punch";
        }
        public bool CompleteAction(Encounter encounter, Entity C)
        {
            Entity Target = HICharacterAction.GetEnemyTarget(encounter, C);

            Target.TakeDamage(5);

            return false;
        }
    }
    public class UseItem : ICharacterAction
    {
        public override string ToString()
        {
            return "Use Item";
        }
        public bool CompleteAction(Encounter encounter, Entity C)
        {
            return true;
        }
    }
    public class PreSetActions
    {
        public static ICharacterAction[] PlayerActions = new ICharacterAction[] {new DoNothing(), new Punch(), new UseItem()};
        public static ICharacterAction[] Skeleton = new ICharacterAction[] { new DoNothing(), new BoneCrush() };
        public static ICharacterAction[] Knight = new ICharacterAction[] { new DoNothing(), new GreatSwordSwing() };
        public static ICharacterAction[] UncodedOne = new ICharacterAction[] { new DoNothing(), new GreatSwordSwing(), new Punch(), new BoneCrush() };
    }
    public class HICharacterAction
    {
        public static Entity GetEnemyTarget(Encounter encounter, Entity Caller)
        {
            foreach (Party party in encounter.EncounterParties!)
            {
                if (party == Caller.party) { continue; }


                if (Caller.RandomActions)
                {
                    return Encounter.ChooseRandomTarget(party);
                }
                else
                {
                    Console.WriteLine("Choose Your Target: ");

                    for(int i = 0; i < party.PartyMembers.Count; i++)
                    {
                        Console.WriteLine($"{i} - {party.PartyMembers![i].Name}");
                    }

                    int temp = PlayerInputSystem.GetValidNumber(0, party.count, "Select A Valid Target");

                    return party.PartyMembers![temp];

                }
            }
            // This should never happen
            return new Entity();
        }
    }
}
