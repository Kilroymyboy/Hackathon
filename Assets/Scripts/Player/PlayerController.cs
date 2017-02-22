using UnityEngine;
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

    //Used along with HorizontalMotion multiplier to create horizontal movement
    public static int MoveSpeed;

	void Start()
	{
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
        WalkMotion();
        JumpMotion();
    }

    //Used to detect player inputs and set parameters & players states for physics behaviour that occurs in FixedUpdate()
	void Update()
	{
        //Allow player movement only when not attacking
        if (PlayerState.Instance.Attack != Attack.Passive)
        {
            CheesyBody.velocity = new Vector2(0, 0.1f);
            HorizontalMotion = 0;
        }
        else
        {
            HorizontalMotion = Input.GetAxisRaw("Horizontal");

            if (HorizontalMotion != 0)
            {
                //float x = Mathf.Lerp(transform.position.x, Player.transform.position.x + 2, 0.02f * Time.deltaTime * 60);

                transform.localScale = new Vector3(HorizontalMotion, 1, 1);
                PlayerState.Instance.DirectionFacing = (DirectionFacing)HorizontalMotion;
            }

            if (Input.GetButtonDown("Jump"))
                JumpActivated = true;
        }

        if (CheesyBody.velocity.y == 0 && PlayerState.Instance.Attack == Attack.Passive)
            PlayerState.Instance.Vertical = Vertical.Grounded;

        Horizontal previousMotion = PlayerState.Instance.Horizontal;
        Horizontal currentMotion = PlayerState.Instance.Horizontal = (Horizontal)HorizontalMotion;

        //Fixes an error with the camera following the player incorrectly if quickly changing direction while at the furthest possible positions at each side of the screen.
        if ((int)previousMotion * (int)currentMotion == -1)
            PlayerState.Instance.Horizontal = Horizontal.Idle;
	}

    //Handles basic horizontal movement using physics-based velocity, called in FixedUpdate()
    private void WalkMotion()
    {
        CheesyBody.velocity = new Vector2(HorizontalMotion * MoveSpeed, CheesyBody.velocity.y);
    }

    //Handles player's vertical state and allows jumping only when grounded, using physics-based AddForce(), called in FixedUpdate()
    private void JumpMotion()
    {
        if (JumpActivated)
        {
            if (PlayerState.Instance.Vertical == Vertical.Grounded)
            {
                PlayerState.Instance.Vertical = Vertical.Airborne;
                CheesyBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();
            }
            JumpActivated = false;
        }
    }
}