﻿using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script is responsible for determining player movement, including walking and jumping.
SpookedCheck() slows down the player when he gets "spooked" by colliding with a Ghost enemy.
*/
#endregion

public class PlayerController : MonoBehaviour
{
    //Reference to player's RigidBody component
    Rigidbody2D CheesyBody;

    //Used as a mutliplier along with MoveSpeed to create horizontal movement for the player
    float HorizontalMotion;

    //Stores if jump button is pressed in Update loop, then acts on it with a physics event in the FixedUpdate loop
    bool JumpActivated;
    bool JumpOver;

    // tells the player where to move
    float moveTo;

    // gets start pos
    Vector2 startPos;

    //Used along with HorizontalMotion multiplier to create horizontal movement
    public static int MoveSpeed;

    // used for runnin into walls
    RaycastHit2D hit;

    private CommandRunner cmd;
    private int i = 0;
    private System.Collections.Generic.List<string[]> str;

    private string[] strArr = {"IF Nothing Walk", "IF Nothing Walk", "IF Box Push", "IF Nothing Walk", "IF Nothing Walk", "IF Nothing Walk", "IF Box Push", "IF Nothing Walk", "IF Nothing Walk", "IF Nothing Walk", "IF Nothing Walk", "IF Stairs Climb", "IF Stairs Climb", "IF Stairs Climb", "IF Nothing Walk" };
    void Start()
    {
         
        cmd = new CommandRunner(strArr);
        str = cmd.getCommands();

        HorizontalMotion = 0;
        MoveSpeed = 3;

        CheesyBody = GetComponent<Rigidbody2D>();

        PlayerState.Instance.Horizontal = Horizontal.Idle;
        PlayerState.Instance.Vertical = Vertical.Airborne;
        PlayerState.Instance.DirectionFacing = DirectionFacing.Right;
        PlayerState.Instance.Attack = Attack.Passive;
    }

    //Calls methods that handle physics-based movement
    void FixedUpdate()
    {

        if (i < strArr.Length && PlayerState.Instance.Horizontal == Horizontal.Idle && PlayerState.Instance.Vertical == Vertical.Grounded && PlayerState.Instance.Attack == Attack.Passive)
        {
            cmd.callCommand(str[i]);
            i++;
        }
        WalkMotion();
        JumpMotion();
        
    }

    //Used to detect player inputs and set parameters & players states for physics behaviour that occurs in FixedUpdate()
	void Update()
	{


        if (CheesyBody.velocity.y == 0 && PlayerState.Instance.Attack == Attack.Passive)
            PlayerState.Instance.Vertical = Vertical.Grounded;

        //Horizontal previousMotion = PlayerState.Instance.Horizontal;
        //Horizontal currentMotion = PlayerState.Instance.Horizontal = (Horizontal)HorizontalMotion;

        //Fixes an error with the camera following the player incorrectly if quickly changing direction while at the furthest possible positions at each side of the screen.
        //if ((int)previousMotion * (int)currentMotion == -1)  
    }

    //Handles basic horizontal movement using physics-based velocity, called in FixedUpdate()
    private void WalkMotion()
    {
        float prevPos = transform.position.x;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2((transform.position.x + 0.3f), transform.position.y), new Vector2(1, 0));


        if (PlayerState.Instance.Horizontal == Horizontal.MovingRight)
        {
            float x = Mathf.Lerp(transform.position.x, moveTo, 0.05f * Time.deltaTime * 60);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        float currPos = transform.position.x;

        if (System.Math.Round(currPos - prevPos, 3) < 0.002)
        {
            PlayerState.Instance.Horizontal = Horizontal.Idle;
        }
        
        if (PlayerState.Instance.Vertical != Vertical.Airborne && hit.collider.tag != "Fist" && hit.distance == 0.0f)
        {
            transform.position = startPos;

            PlayerState.Instance.Horizontal = Horizontal.Idle;
        }

        //CheesyBody.velocity = new Vector2(HorizontalMotion * MoveSpeed, CheesyBody.velocity.y);
    }

    private void WalkMotion2()
    {
        startPos = transform.position;
        if (HorizontalMotion == 0 && PlayerState.Instance.Horizontal != Horizontal.MovingRight && PlayerState.Instance.Vertical != Vertical.Airborne)
        {
            PlayerState.Instance.DirectionFacing = (DirectionFacing)1.0f;
            PlayerState.Instance.Horizontal = Horizontal.MovingRight;
            moveTo = transform.position.x + 1.01f;
        }

        //CheesyBody.velocity = new Vector2(HorizontalMotion * MoveSpeed, CheesyBody.velocity.y);
    }

    //Handles player's vertical state and allows jumping only when grounded, using physics-based AddForce(), called in FixedUpdate()
    private void JumpMotion()
    {

        if (JumpActivated)
        {
            if (PlayerState.Instance.Vertical == Vertical.Grounded)
            {
                PlayerState.Instance.Vertical = Vertical.Airborne;
                CheesyBody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();
            }

            if (transform.position.y - startPos.y > .9)
            {
                JumpOver = true;
                JumpActivated = false;
            }
        }
        if (JumpOver)
        {
            float x = Mathf.Lerp(transform.position.x, moveTo, 0.05f * Time.deltaTime * 60);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }


    private void JumpMotion2()
    {
        if (PlayerState.Instance.Vertical != Vertical.Airborne && PlayerState.Instance.Horizontal != Horizontal.MovingRight)
        {
            JumpActivated = true;
            moveTo = transform.position.x + 1.0f;
            JumpOver = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }
}