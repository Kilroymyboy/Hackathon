using UnityEngine;
using System.Collections;
using System;

#region - Script Synopsis
/*
This script determines the behaviour for the "Torque" enemy type. Note that it inherits additional fields and methods
from the Enemy base class.
*/
#endregion

public class Torque : Enemy
{
	void Start()
	{
        HP = 2;
        Collider.radius = 0.19f;
	}

	void FixedUpdate()
	{
        MovementPattern();
        BorderHitCheck(80);
        DestroyOutofBounds();
    }

    //Has the effect of spinning the enemy and bouncing it. Torque is used to create motion.
    protected override void MovementPattern()
    {
        if (Body.velocity.y < 0)
            Body.AddTorque((float)Speed / 2 * Direction);

        else if (Body.velocity.y == 0)
            Body.AddForce(new Vector2(0, 100));
    }
}