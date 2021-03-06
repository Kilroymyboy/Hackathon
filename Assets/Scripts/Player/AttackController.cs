﻿using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script is responsible for determining the movement of the attacking "Fist"
during punching or projectile attacks, as well as damaging the enemy when the fist collides with it.
*/
#endregion

public class AttackController : MonoBehaviour
{
    //Reference to the player GameObject
    public GameObject Player;

    //Stores player's fist position for start and end of punch motion
    float StartingPunchPosition;
    float EndingPunchPosition;
    
    //Stores the upper-limit counter used to pause the fist at full extention
    //depending if punching (shorter pause) or throwing a projectile (longer pause)
    int MaxPause;

    //Stores the position of the player's fist when attacking
    float PunchMotion;

    //Used with MaxPause to handle pausing of the attack state
    float AttackPause;

    //Multiplier used in conjuction with Time.deltaTime to create punch motion via Lerping
    float Accumulator;

    bool Recoiling;

    void Start()
    {
        PunchMotion = Mathf.Infinity;
        AttackPause = 0.5f;
        Accumulator = 0.02f;
    }

	void Update()
	{

        //Handles the rest of the attack determined in the if...else if() above, playing out the attack according to the initial conditions set
        if (PlayerState.Instance.Attack == Attack.Punch)
        {
            Accumulator += Time.deltaTime;

            if (PunchMotion == EndingPunchPosition)
            {
                AttackPause += Time.deltaTime * 60;
            }

            if (AttackPause > MaxPause)
            {
                AttackPause = 1;
                Accumulator = 0.02f;
                Recoiling = true;
            }

            //Reverses the attack animation when recoiling, and resets the attack state to passive when completed
            if (!Recoiling)
            {
                PunchMotion = Mathf.Lerp(StartingPunchPosition, EndingPunchPosition, Accumulator * 7);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Accumulator * 5);
            }
            else
            {
                PunchMotion = Mathf.Lerp(EndingPunchPosition, StartingPunchPosition, Accumulator * 5);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.6f, 0.5f, 1), Accumulator * 4);

                if (transform.position.x == StartingPunchPosition)
                {
                    Recoiling = false;
                    PlayerState.Instance.Attack = Attack.Passive;
                    Accumulator = 0.02f;
                }
            }

            transform.position = new Vector3(PunchMotion, Player.transform.position.y, transform.position.z);

            GetComponent<SpriteRenderer>().enabled = true;
        }
        else //When not attacking, disables visiblity of the "Fist"
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
	}

    private void punchMotion2()
    {

        if (PlayerState.Instance.Attack == Attack.Passive)
        {
            PlayerState.Instance.Attack = Attack.Punch;
            StartingPunchPosition = Player.transform.position.x;
            EndingPunchPosition = StartingPunchPosition + (int)PlayerState.Instance.DirectionFacing * 0.7f;

            MaxPause = 10;
            GetComponents<AudioSource>()[0].Play();
        }
    }

    //Detects collision between player's fist and enemy, awards points and adds force to the enemy accordingly
    void OnCollisionEnter2D(Collision2D coll)
    {
         if (coll != null)
        {
          //  float distance = Mathf.Abs(coll.point.x - transform.position.x);
            Rigidbody2D rigBod = coll.rigidbody;

            if (coll.collider.tag == "Box")
            {
                if (PlayerState.Instance.Attack == Attack.Punch)
                {
                    rigBod.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    coll.rigidbody.AddForce(new Vector2(2.0f, 0.0f), ForceMode2D.Impulse);
                }
              
            }
            else if (coll.collider.tag == "Tree")
            {

                if (PlayerState.Instance.Attack == Attack.Punch)
                {
                    rigBod.constraints = RigidbodyConstraints2D.None;// | RigidbodyConstraints2D.FreezeRotation;
                    coll.collider.enabled = false;
                    coll.rigidbody.AddForce(new Vector2(10.0f, 0.0f), ForceMode2D.Impulse);
                    rigBod.rotation = -70;
                   
                }
            }
        }
    }
}