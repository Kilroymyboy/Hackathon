using UnityEngine;
using System.Collections;
using System;

#region - Script Synopsis
/*
This script gets attached to the Main Camera and controls the way that it behaves depending on the state that
it's in (enum of possible states at bottom of script) and the state of the player (determined in the PlayerController script). 
It essentially follows the player when its position is at 25% of the screen width (far left edge) or 75% of the screen width (far right edge).
If the player stops moving, the camera recenters on the player and stops recentering once the player is at exactly 50% of the screen width (middle of the screen).
*/
#endregion

public class CameraController : MonoBehaviour
{
    //Reference to the player GameObject
    public GameObject Player;

    //Stores an exclusive CameraState for moving the camera accordingly
    public CameraState CameraState;
    
	void Start()
	{
        CameraState = CameraState.Stationary;
	}

    //Handles camera motion based on where the player is on the screen and the current state the camera is in
    void LateUpdate()
    {
        float offset = Camera.main.orthographicSize * Camera.main.aspect / 4;
        Vector3 tempPos = Player.transform.position;
        Vector3 CheeseScreenPosition = Camera.main.WorldToViewportPoint(new Vector3(tempPos.x+4, tempPos.y, tempPos.z));

        if (CheeseScreenPosition.x < 0.34f || CheeseScreenPosition.x > 1.0f)
            CameraState = CameraState.Recentering;

        if (CameraState == CameraState.Recentering)
        {
            float x = Mathf.Lerp(transform.position.x, Player.transform.position.x+2, 0.02f * Time.deltaTime * 60);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            if (Math.Round(CheeseScreenPosition.x, 2) <= 0.67)
                CameraState = CameraState.Stationary;
        }
	}
}

public enum CameraState
{
    Stationary,
    Recentering
}