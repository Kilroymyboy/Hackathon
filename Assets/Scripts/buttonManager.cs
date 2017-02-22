using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If you wanna change scenes for different levels
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClearClick()
    {

    }

    public void onPlayClick()
    {

    }

    public void onTestClick()
    {

    }

    public void onIfClick()
    {

    }

    public void onElseClick()
    {

    }

    public void onWhileClick()
    {

    }

    public void onStairsClick()
    {

    }

    public void onRockClick()
    {

    }

    public void onTreeClick()
    {

    }

    public void onPushClick()
    {

    }

    public void onChopClick()
    {

    }

    public void onClimbClick()
    {

    }


    public void onNextLevelClick()
    {
        //If you wanna change scenes for different levels
        SceneManager.LoadScene("testScene", LoadSceneMode.Single);
    }
}
