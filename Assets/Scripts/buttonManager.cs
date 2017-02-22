using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If you wanna change scenes for different levels
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour {

    hudManager hud = new hudManager();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClearClick()
    {
        // Clear the sent clauses
        hud.clearClause();
    }

    public void onPlayClick()
    {
        // Some AI shit right here
    }

    public void onTestClick()
    {
        hud.testClause();
    }

    public void onIfClick()
    {
        hud.addToClause("If", 0);
    }

    public void onElseClick()
    {
        hud.addToClause("Else", 0);
    }

    public void onWhileClick()
    {
        hud.addToClause("While", 0);
    }

    public void onStairsClick()
    {
        hud.addToClause("Stairs", 1);
    }

    public void onRockClick()
    {
        hud.addToClause("Rock", 1);
    }

    public void onTreeClick()
    {
        hud.addToClause("Tree", 1);
    }

    public void onPushClick()
    {
        hud.addToClause("Push", 2);
    }

    public void onChopClick()
    {
        hud.addToClause("Chop", 2);
    }

    public void onClimbClick()
    {
        hud.addToClause("Climb", 2);
    }


    public void onNextLevelClick()
    {
        //Go to the next level 
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}
