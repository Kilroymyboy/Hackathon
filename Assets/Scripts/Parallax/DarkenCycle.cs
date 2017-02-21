using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
When attached to a background GameObject, it will darken that object over a period of time determined by the cycleSpeed.
This is a minor enhancement to add to the effect of a "cloudy overcast" effect.
*/
#endregion

public class DarkenCycle : MonoBehaviour
{
    //Reference to the renderer for the sprite that goes through darkening cycles
    SpriteRenderer Renderer;

    //Stores the min/max brightness of the renderer as it cycles similar to the OvercastCycle script
    float minBrightness;
    float maxBrightness;

    //CycleSpeed of the darkening effect
    float cycleSpeed;

	void Start()
	{
        Renderer = GetComponent<SpriteRenderer>();
        minBrightness = 0.86f;
        maxBrightness = 1;
        cycleSpeed = 0.05f;
	}

    //PingPong used to create the cycle darkening/lightening effect
	void Update()
	{
        Renderer.color = Color.Lerp(
            new Color(maxBrightness, maxBrightness, maxBrightness, 1),
            new Color(minBrightness, minBrightness, minBrightness, 1),
            Mathf.PingPong(Time.time * cycleSpeed, maxBrightness)
            );
	}
}