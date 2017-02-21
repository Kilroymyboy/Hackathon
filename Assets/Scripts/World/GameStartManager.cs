using UnityEngine;
using UnityEngine.SceneManagement;

#region - Script Synopsis
/*
This script allows the game to be restarted, and shows a "Press Enter" message when it's Game Over.
*/
#endregion

public class GameStartManager : MonoBehaviour
{
    //Timer used for blinking behaviour of the "Press Enter" image
    float LoopTimer;

    //Enables/disables the "Press Enter" image to restart the game, reloading the "MainScene" scene
    void Update()
    {
        LoopTimer += Time.deltaTime * 60;

        if (LoopTimer > 30)
        {
            SpriteRenderer pressEnterRenderer = GetComponent<SpriteRenderer>();
            pressEnterRenderer.enabled = !pressEnterRenderer.enabled;
            LoopTimer = 0;
        }

        if (Input.GetButtonDown("Start"))
        {
            WorldManager.Score = 0;
            SceneManager.LoadScene("MainScene");
        }
    }
}