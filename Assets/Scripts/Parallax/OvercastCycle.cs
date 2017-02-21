using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This is a variation of the same principle behind the DarkenCycle script. This changes the opacity of the sky background GameObject
over a period of time, revealing the dark blue color behind the sprite. This adds to the "cloudy overcast" effect, but in a slightly different way.
*/
#endregion

public class OvercastCycle : MonoBehaviour
{
    //Reference to the renderer for the sprite that goes through overcast cycles of the background sky
    SpriteRenderer Renderer;

    //Stores the min/max opacity of the renderer as it cycles similar to the DarkenCycle script
    float minOpacity;
    float maxOpacity;

    //CycleSpeed of the opacity effect
    float cycleSpeed;

	void Start()
	{
        Renderer = GetComponent<SpriteRenderer>();
        minOpacity = 0.6f;
        maxOpacity = 1;
        cycleSpeed = 0.05f;
	}

    //PingPong used to create the cycle effect of sky darkening
    void Update()
	{
        Renderer.color = Color.Lerp(
            new Color(1,1,1, maxOpacity),
            new Color(1,1,1, minOpacity),
            Mathf.PingPong(Time.time * cycleSpeed, maxOpacity)
            );
	}
}