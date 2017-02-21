using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script handles general "World" behaviour, such as keeping the current Score and Level, as well as 
pausing the game and testing how the game runs at different framerates.
*/
#endregion

public class WorldManager : MonoBehaviour
{
    //Stores the Score displayed in the HUD
    public static int Score;

    //Stores the Level multiplier determined by the amount of enemy waves triggered
    public static int Level;

    //Returns a difficulty multiplier that increments every 3 levels
    public static int Difficulty
    {
        get
        {
            return Level / 3;
        }
    }

    //Stores whether or not pause has been engaged
    private bool PauseSwitch;

    //Reference to the player GameObject
    private GameObject CheeseHead;

    void Awake()
    {
        Level = 4;
    }

	void Start()
	{
        Application.targetFrameRate = 60; //Set the framerate for testing purposes
        QualitySettings.vSyncCount = 1; //0 for testing in Unity, 1 for final build

        CheeseHead = GameObject.Find("CheeseHead");
    }

    void Update()
    {
        GamePause();
    }

    //Pauses/unpauses the game using Time.timeScale and disabling/enabling the PlayerController script
    private void GamePause()
    {
        if (Input.GetButtonDown("Start"))
            PauseSwitch = !PauseSwitch;

        if (PauseSwitch && !GameOverManager.IsGameOver)
        {
            Time.timeScale = 0;
            CheeseHead.GetComponent<PlayerController>().enabled = CheeseHead.GetComponentInChildren<AttackController>().enabled = false;
        }
        else if (!PauseSwitch && !GameOverManager.IsGameOver)
        {
            Time.timeScale = 1;
            CheeseHead.GetComponent<PlayerController>().enabled = CheeseHead.GetComponentInChildren<AttackController>().enabled = true;
        }
    }
}