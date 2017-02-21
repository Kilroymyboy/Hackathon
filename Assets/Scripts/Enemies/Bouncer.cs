using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script determines the behaviour for the "Bouncer" enemy type. Note that it inherits additional fields and methods
from the Enemy base class.
*/
#endregion

public class Bouncer : Enemy
{
    void Start()
    {
        HP = 1;
        Collider.radius = 0.19f;
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    void FixedUpdate()
    {
        MovementPattern();
        BorderHitCheck(50);
        DestroyOutofBounds();
    }

    //Bounces the Enemy when it hits the "ground" (velocity.y = 0)
    protected override void MovementPattern()
    {
        Body.velocity = new Vector2(1 * Direction, Body.velocity.y);

        if (Body.velocity.y == 0)
            Body.AddForce(new Vector2(0, 300));
    }
}