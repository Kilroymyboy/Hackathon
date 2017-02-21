using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
When attached to a background parent GameObject, it determines the position of the background relative to the camera.
Basically, as the camera moves, the background moves at a rate determined (in the inspector) by the Speed field. When background
elements that are further back move at a slower rate relative to closer elements it creates a classic "parallax" scrolling effect.
*/
#endregion

public class ParallaxController : MonoBehaviour
{
    //Stores speed of the parallax effect, set in inspector, lower for further away objects
    public float Speed;

    //Stores the autoscroll value for objects that scroll regardless of camera movement (ie: clouds)
    public float AutoScroll;

    //Stores the current camera position as well as the position of the background object relative to it
    //which creates the parallax effect
    Camera MainCamera;
    Vector3 ParallaxFollowCamera;

    //Stores the Autoscroll modified by deltaTime
    private float Scroll;

    //Stores the position of the gameObject at start so as to maintain any initial offsetting applied in the inspector
    private float OffSet;

	void Start()
	{
        MainCamera = Camera.main;
        ParallaxFollowCamera = transform.position;
        OffSet = transform.position.x;
	}

    
	void LateUpdate()
	{
        Scroll += AutoScroll * Time.deltaTime * 60;
        ParallaxFollowCamera.x = MainCamera.transform.position.x * Speed + Scroll + OffSet;
        transform.position = ParallaxFollowCamera;
	}
}