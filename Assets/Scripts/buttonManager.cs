using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//If you wanna change scenes for different levels
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour {

    hudManager hud = new hudManager();
    Button[] children;

    // Use this for initialization
    void Start () {
       
        children = GameObject.Find("Canvas").transform.GetComponentsInChildren<Button>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClearClick()
    {
        // Clear the sent clauses
        hud.clearClause();
        for(int i = 0; i < children.Length; i++)
        {
            children[i].interactable = true;
        }
    }

    public void onPlayClick()
    {
        // Some AI shit right here
    }

    public void onTestClick()
    {
        hud.testClause();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].interactable = true;
        }
    }

    public void onIfClick()
    {
        hud.addToClause("If", 0);
        children[3].interactable = false;
    }

    public void onElseClick()
    {
        hud.addToClause("Else", 0);
        children[4].interactable = false;
    }

    public void onWhileClick()
    {
        hud.addToClause("While", 0);
        children[5].interactable = false;
    }

    public void onStairsClick()
    {
        hud.addToClause("Stairs", 1);
        children[6].interactable = false;
    }

    public void onRockClick()
    {
        hud.addToClause("Rock", 1);
        children[7].interactable = false;
    }

    public void onTreeClick()
    {
        hud.addToClause("Tree", 1);
        children[8].interactable = false;
    }

    public void onPushClick()
    {
        hud.addToClause("Push", 2);
        children[9].interactable = false;
    }

    public void onChopClick()
    {
        hud.addToClause("Chop", 2);
        children[10].interactable = false;
    }

    public void onClimbClick()
    {
        hud.addToClause("Climb", 2);
        children[11].interactable = false;
    }


    public void onNextLevelClick()
    {
        //Go to the next level 
        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % 6;
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);

        //Haven't tested this yet
        Text title = GameObject.Find("Canvas").transform.GetComponentInChildren<Text>();
        title.text = "LEVEL " + nextLevel;

        // Clear things if necessary
        for (int i = 0; i < children.Length; i++)
        {
            children[i].interactable = true;
        }

        // Call some sort of clear list from hud

    }
}
