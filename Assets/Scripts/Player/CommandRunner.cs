using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CommandRunner : MonoBehaviour {

    private List<string[]> commands = new List<string[]>();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public CommandRunner(string[] strAry)
    {
        parseCommand(strAry);

    }
    public List<string[]> getCommands()
    {
        return commands;
    }

    private void parseCommand(string[] str)
    {

        Regex[] ifStats = {new Regex("IF Box Push") , new Regex("IF Nothing Walk"),
            new Regex("IF Tree Chop"), new Regex("IF Stairs Climb") };
        for (int i = 0; i < str.Length; i++)
        {
            for (int j = 0; j < ifStats.Length; j++)
            { 
                if (ifStats[j].Match(str[i]).Success)
                {
                    if (j == 0)
                    {
                        
                        commands.Add(new string[] { "Fist", "punchMotion2" });

                    }
                    else if (j == 1)
                    {
                        commands.Add(new string[] { "Player", "WalkMotion2" });
                    }
                    else if (j == 2)
                    {
                        commands.Add(new string[] { "Fist", "punchMotion2" });
                    }
                    else
                    {
                        commands.Add(new string[] { "Player", "JumpMotion2" });
                    }
                }
            }
        }

    }

    internal void callCommand(string[] str)
    {

        if(PlayerState.Instance.Horizontal == Horizontal.Idle)
        {

            GameObject.FindGameObjectWithTag(str[0]).SendMessage(str[1]);

        }
            
    }

 

    private void whileHandler(string[] test2)
    {
        throw new NotImplementedException();
    }
}
