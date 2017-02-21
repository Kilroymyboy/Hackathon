using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script is responsible for displaying the Score and Level as text that scales depending on the size of the screen.
*/
#endregion

public class HeadsUpDisplay : MonoBehaviour
{
    //Stores the font size (which scales to the size of the screen)
    private int FontSize;

    //The fontstyle for the score/level displayed in the upper-left of the screen
    public GUIStyle FontStyle = new GUIStyle();


    void Start()
    {
        FontStyle.font = Resources.Load<Font>("Fonts/trebucbd");
    }

    //Displays the "HUD" as a GUI overlay
    void OnGUI()
    {
        ScaleFontSize();

        GUI.Label(new Rect(Screen.width / 85f, Screen.height / 58, Screen.width, Screen.height),
            string.Format("SCORE: {0}", WorldManager.Score.ToString()), FontStyle
            );

        GUI.Label(new Rect(Screen.width / 85f, Screen.height / 20, Screen.width, Screen.height),
            string.Format("LEVEL: {0}", (WorldManager.Level - 4).ToString()), FontStyle
            );
    }

    //Scales the font relative to the size of the screen
    private void ScaleFontSize()
    {
        FontSize = Screen.width / 64;
        FontStyle.fontSize = FontSize;
    }
}