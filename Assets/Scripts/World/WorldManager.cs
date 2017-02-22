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
    //Stores the Level multiplier determined by the amount of enemy waves triggered
    public static int Level;

    //Reference to the player GameObject
    private GameObject player;

	void Start()
	{
        Application.targetFrameRate = 60; //Set the framerate for testing purposes
        QualitySettings.vSyncCount = 0; //0 for testing in Unity, 1 for final build


        //TODO: change to object string to player object name
        player = GameObject.Find("CheeseHead");
    }

    void Update()
    {
        
    }
}