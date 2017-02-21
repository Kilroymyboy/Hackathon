using UnityEngine;
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
    public GameObject CheeseHead;

    //Reference to the projectile prefab
    public GameObject Projectile;

    //Stores player's fist position for start and end of punch motion
    float StartingPunchPosition;
    float EndingPunchPosition;
    
    //Stores the upper-limit counter used to pause the fist at full extention
    //depending if punching (shorter pause) or throwing a projectile (longer pause)
    int MaxPause;

    //Stores the position of the player's fist when attacking
    float PunchMotion;

    //Stores the force value multiplier of the attack, used when striking the enemy
    float AttackForce;

    //Used with MaxPause to handle pausing of the attack state
    float AttackPause;

    //Multiplier used in conjuction with Time.deltaTime to create punch motion via Lerping
    float Accumulator;

    bool Recoiling;

    void Start()
    {
        PunchMotion = Mathf.Infinity;
        AttackForce = 5;
        AttackPause = 1;
        Accumulator = 0.02f;
    }

	void Update()
	{
        //Ternary which enables visibility of the "background" fist when using a projectile attack
        GetComponentsInChildren<SpriteRenderer>()[1].enabled = (PlayerState.Instance.Attack == Attack.Projectile) ? true : false;

        //Handles initial conditions and state keeping of the punch attack (also prevents interrupting/cancelling the attack)
        if (Input.GetButtonDown("Punch") && PlayerState.Instance.Attack == Attack.Passive)
        {
            PlayerState.Instance.Attack = Attack.Punch;
            StartingPunchPosition = CheeseHead.transform.position.x;
            EndingPunchPosition = StartingPunchPosition + (int)PlayerState.Instance.DirectionFacing * 0.7f;

            MaxPause = 10;
            GetComponents<AudioSource>()[0].Play();
        }
        //Similar to the above punch attack handling, except in the case of peforming a projectile attack
        else if (Input.GetButtonDown("Projectile") && PlayerState.Instance.Attack == Attack.Passive && GameObject.Find("Projectile(Clone)") == null)
        {
            PlayerState.Instance.Attack = Attack.Projectile;
            StartingPunchPosition = CheeseHead.transform.position.x;
            EndingPunchPosition = StartingPunchPosition + (int)PlayerState.Instance.DirectionFacing * 0.5f;

            MaxPause = 20;
            GetComponents<AudioSource>()[1].Play();
        }

        //Handles the rest of the attack determined in the if...else if() above, playing out the attack according to the initial conditions set
        if (PlayerState.Instance.Attack == Attack.Punch || PlayerState.Instance.Attack == Attack.Projectile)
        {
            Accumulator += Time.deltaTime;

            if (PunchMotion == EndingPunchPosition)
            {
                AttackPause += Time.deltaTime * 60;
            }

            if (AttackPause > MaxPause)
            {
                if (PlayerState.Instance.Attack == Attack.Projectile)
                    GameObject.Instantiate(Projectile);

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

            transform.position = new Vector3(PunchMotion, CheeseHead.transform.position.y, transform.position.z);

            GetComponent<SpriteRenderer>().enabled = true;
        }
        else //When not attacking, disables visiblity of the "Fist"
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
	}

    //Detects collision between player's fist and enemy, awards points and adds force to the enemy accordingly
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Rigidbody2D enemy = coll.gameObject.GetComponent<Rigidbody2D>();
            enemy.velocity = new Vector2(0, 0);
            enemy.AddForce(new Vector2((float)PlayerState.Instance.DirectionFacing * AttackForce, AttackForce), ForceMode2D.Impulse);

            enemy.GetComponent<Enemy>().DoDamage(2);
            WorldManager.Score += 200;
        }   
    }
}