using Final_Battle;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;


Console.Write("What is your name mighty programmer: ");

string? PlayerName = null;
while(PlayerName == null)
    PlayerName = Console.ReadLine();


Console.Write($"\n{PlayerName} encounters a Skeleton!!\n");

// Player fights a skeleton

TrueProgrammer player = new(PlayerName);

TheUncodedOne Assistant = new TheUncodedOne();


Party Heros = new Party(player, Assistant);
Party Villans = new Party(new TheUncodedOne(), new TheUncodedOne());

Encounter encounter = new(Heros, Villans);  encounter.Play();

Thread.Sleep(3000);
Console.Clear();

// Player fights 2 skeletons, but a knight has joined his party :D!

#region Console Text
Console.Write($"\n{PlayerName} encounters two Skeletons!!\n");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write($"\n Don't worry, I am here to help you I am a Knight!\n");
Console.ForegroundColor= ConsoleColor.White;
#endregion

player = new(PlayerName);

Heros = new Party(player, new Knight());
Villans = new Party(new Skeleton(), new Skeleton());

encounter = new(Heros, Villans); encounter.Play();

Thread.Sleep(3000);
Console.Clear();



//Uncoded One Encounter
#region Console Text
Console.Write($"\n{PlayerName} encounters the UncodedOne!!\n");

Console.ForegroundColor = ConsoleColor.Red;
Console.Write($"\n Why don't you entertain me {PlayerName}.\n");
Console.ForegroundColor = ConsoleColor.White;
#endregion

player = new(PlayerName);

Heros = new Party(player, new Knight());
Villans = new Party(new Skeleton(), new TheUncodedOne());

encounter = new(Heros, Villans); encounter.Play();

public class Party
{
    public List<Entity> PartyMembers { get; set; }
    public int count { get; private set; } = 0;
    /// <summary>
    ///
    /// </summary>
    /// <param name="partyMembers">There should be at least one party member</param>
    public Party(params Entity[] partyMembers) { 
        PartyMembers = partyMembers.ToList();

        for(int i = 0; i < PartyMembers.Count; i++)
        {
            PartyMembers[i].party = this;
        }

        count = partyMembers.Length;
    }

    public void PlayPartyTurns(Encounter encounter)
    {
        foreach (Entity entity in PartyMembers!)
        {
            Console.WriteLine("---------------------------------");
            entity.TakeTurn(encounter);
        }
    }

    public void ResetTurns()
    {
        for (int i = 0; i < PartyMembers!.Count; i++)
        {
            PartyMembers[i].HasTurn = true;
        }
    }
}
public class Encounter
{
    public Party[] EncounterParties { get; set; }
    public Encounter(params Party[] parties)
    {
       EncounterParties = parties;
    }

    public void Play()
    {
        bool Battling = true;
        while (Battling)
        {
            foreach (Party party in EncounterParties!)
            {
                party.ResetTurns();
            }

            foreach (Party party in EncounterParties!)
            {
                party.PlayPartyTurns(this);
                Console.WriteLine();

                if (CheckBattleField())
                {
                   Battling = false;
                    break;
                }
            }


        }

        Console.WriteLine("The battle has concluded.");
    }

    public bool CheckBattleField()
    {
        foreach (Party party in EncounterParties!)
        {
            if(party.PartyMembers.Count == 0)
                return true;
        }

        return false;
    }

    public static Entity ChooseRandomTarget(Party party)
    {
        Entity entity = new Entity();
        Random rt = new Random();

        int i = rt.Next(0, party.count);

        entity = party.PartyMembers![i];

        return entity;
    }
}
