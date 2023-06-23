using ConsoleApp1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Program : MonoBehaviour
{

    void Start()
    {
        List<TextAsset> list = Resources.LoadAll<TextAsset>("data").ToList();
        string json = list.First().text;
        Debug.Log(json);

        ConsoleMonFactory consoleMonFactory = new ConsoleMonFactory();
        List<ConsoleMon> fighters = consoleMonFactory.LoadJson(json);
        Arena arena = new Arena();
        Roster rosterA = new Roster("Player", new ConsoleMon[] { fighters[0], fighters[2] });
        Roster rosterB = new Roster("AI", new ConsoleMon[] { fighters[1], fighters[3] });
        arena.Fight(rosterA, rosterB);

    }
}
