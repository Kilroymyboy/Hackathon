using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script determines the behaviour for the "Tweaker" enemy type. Note that it inherits additional fields and methods
from the Enemy base class.
*/
#endregion

public class Tweaker : Enemy
{
    //TweakOut gets switched on according to the Timer, creating sporadic movement in bursts
    float Timer;
    public bool TweakOut;

    void Start()
    {
        HP = 1;

        Collider.radius = 0.16f;
        Sprite.material.color = new Color(1, 0.8f, 0, 1);
    }

    void Update()
    {
        Timer += Time.deltaTime * 60;

        if (Timer > 30)
        {
            TweakOut = true;
            Timer = 0;
        }
    }

    void FixedUpdate()
    {
        MovementPattern();
        BorderHitCheck(50);
        DestroyOutofBounds();
    }

    //Partially randomized values creates an erratic movement pattern
    protected override void MovementPattern()
    {
        if (TweakOut)
        {
            float randomX = Random.Range(-29f, 29f);
            float randomY = Random.Range(-120f, 120f);

            Body.AddForce(new Vector2(randomX * Speed, randomY * Speed));
            TweakOut = false;
        }
    }
}