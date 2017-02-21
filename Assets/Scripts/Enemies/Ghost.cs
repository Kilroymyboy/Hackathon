using UnityEngine;
using System.Collections;
using System;

#region - Script Synopsis
/*
This script determines the behaviour for the "Ghost" enemy type. Note that it inherits additional fields and methods
from the Enemy base class.
*/
#endregion

public class Ghost : Enemy
{
    void Start()
    {
        HP = 1;

        Collider.radius = 0.16f;
        Body.isKinematic = true;

        Sprite.sortingOrder = 1;
        Sprite.material.color = new Color(1, 1, 1, 0.5f);
        transform.localScale = new Vector3(1.8f, 1.8f, 1);
    }

    void Update()
    {
        MovementPattern();
        DestroyOutofBounds();
    }

    //Simply moves the Ghost enemy towards the player target
    protected override void MovementPattern()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            GameObject.Find("CheeseHead").transform.position,
            Speed * Time.deltaTime
            );
    }

    //Unique event to the Ghost enemy, causes player to be "spooked", reducing it's MoveSpeed temporarily
    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerController.IsSpooked = true;
            PlayerController.MoveSpeed -= 1;
            GameObject.Destroy(gameObject);
        }
    }
}