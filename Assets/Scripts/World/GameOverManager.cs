using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
When the player gets hit by an enemy, it sets the IsGameOver field to true. This then makes the "Game Over"
banner visible and enables the GameStartManager script in the Restart GameObject, allowing the game to be restarted.
*/
#endregion

public class GameOverManager : MonoBehaviour
{
    //Reference to the "GameOver" splash image attached to the GameOver GameObject
    SpriteRenderer GameOverRenderer;

    //Holds the state for Game Over, used in other scripts for end-game specific operations
    public static bool IsGameOver;

    void Start()
    {
        GameOverRenderer = GetComponent<SpriteRenderer>();
        IsGameOver = false;
    }

    //If Game Over, fades in the splash image and enables the ability to restart via the GameStartManager
    //attached to Restart GameObject
    void Update()
    {
        if (IsGameOver)
        {
            GameOverRenderer.color = Color.Lerp(GameOverRenderer.color, new Color(1, 1, 1, 0.8f), 0.5f * Time.deltaTime);

            GameStartManager reStart = GetComponentInChildren<GameStartManager>();
            reStart.enabled = true;
        }
    }
}