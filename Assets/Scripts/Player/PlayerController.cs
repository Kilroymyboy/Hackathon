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
    bool JumpOver;

    // tells the player where to move
    float moveTo;

    // gets start pos
    Vector2 startPos;

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
        /*
        RaycastHit2D hit = Physics2D.Raycast(new Vector2((transform.position.x + 0.3f), transform.position.y), new Vector2(1, 0));
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            Rigidbody2D rigBod = hit.rigidbody;

            if (hit.collider.tag == "Box")
            {

                //bool push = true;

                if (PlayerState.Instance.Attack == Attack.Punch)
                {
                    rigBod.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    rigBod.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                }
                print(distance);
            }
            else if (hit.collider.tag == "Tree")
            {

                if (PlayerState.Instance.Attack == Attack.Punch)
                {
                    rigBod.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                    rigBod.rotation = -60;
                    //Destroy(rigBod);
                }
                else
                {
                    rigBod.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
                print(distance);

            }
        }
        */
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

            if (HorizontalMotion != 0 && PlayerState.Instance.Horizontal != Horizontal.MovingRight)
            {
                PlayerState.Instance.DirectionFacing = (DirectionFacing)HorizontalMotion;
                PlayerState.Instance.Horizontal = Horizontal.MovingRight;
                moveTo = transform.position.x + 1;
            }

            if (Input.GetButtonDown("Jump"))
            {
                JumpActivated = true;
                moveTo = transform.position.x + 1;
                JumpOver = false;
            }

        }

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
                CheesyBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();
                startPos = transform.position;
            }

            if (transform.position.y - startPos.y > .7)
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

    public void OnCollisionEnter2D(Collision collision)
    {
        if(collision.gameObject.tag == "")
        {

        }
    }
}