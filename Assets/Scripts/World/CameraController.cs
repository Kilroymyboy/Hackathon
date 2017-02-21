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
    public GameObject CheeseHead;

    //Stores an exclusive CameraState for moving the camera accordingly
    public CameraState CameraState;
    
	void Start()
	{
        CameraState = CameraState.Stationary;
	}

    //Handles camera motion based on where the player is on the screen and the current state the camera is in
    void LateUpdate()
    {
        float offset = Camera.main.orthographicSize * Camera.main.aspect / 2;
        Vector3 CheeseScreenPosition = Camera.main.WorldToViewportPoint(CheeseHead.transform.position);

        if (CheeseScreenPosition.x < 0.25f || CheeseScreenPosition.x > 0.75f)
            CameraState = CameraState.Following;

        if (CameraState == CameraState.Following && PlayerState.Instance.Horizontal == Horizontal.Idle)
            CameraState = CameraState.Recentering;
        else if (CameraState == CameraState.Following)
            transform.position = new Vector3(CheeseHead.transform.position.x - offset * (int)PlayerState.Instance.DirectionFacing, transform.position.y, transform.position.z);

        if (CameraState == CameraState.Recentering)
        {
            float x = Mathf.Lerp(transform.position.x, CheeseHead.transform.position.x, 0.02f * Time.deltaTime * 60);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            if (Math.Round(CheeseScreenPosition.x, 1) == 0.5f)
                CameraState = CameraState.Stationary;
        }
	}
}

public enum CameraState
{
    Stationary,
    Following,
    Recentering
}