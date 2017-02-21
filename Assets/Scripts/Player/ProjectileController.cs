using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
When a projectile is "fired" (via the AttackController script) a projectile GameObject is created with this script attached.
This script simply determines the movement and collision behaviour of the projectile, as well as its lifecycle.
*/
#endregion

public class ProjectileController : MonoBehaviour
{
    //Stores the speed and direction the projectile is travelling
    int Direction;
    int Speed;

    //Used to automatically destroy a projectile after a set amount of time has elapsed
    float Timer;

	void Start()
	{
        Direction = (int)PlayerState.Instance.DirectionFacing;
        transform.position = GameObject.Find("Fist").transform.position - new Vector3(0.3f, 0, 0) * Direction;
        Speed = 12;
	}

    //Moves, rotates and auto-destroys the projectile
	void Update()
	{
        transform.position = new Vector2(transform.position.x + Time.deltaTime * Speed * Direction, transform.position.y);
        transform.Rotate(0, 0, 6 * Direction * -1 * Time.deltaTime * 60);
        Timer += Time.deltaTime * 60;

        if (Timer > 120)
        {
            GameObject.Destroy(gameObject);
        }
	}

    //Detects collision between the projectile and enemy, Awards points, and effects the enemy accordingly
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(gameObject);

            Rigidbody2D enemy = coll.gameObject.GetComponent<Rigidbody2D>();
            enemy.velocity = new Vector2(0, 0);

            enemy.AddForce(new Vector2((float)PlayerState.Instance.DirectionFacing * 11, 14), ForceMode2D.Impulse);
            enemy.GetComponent<Enemy>().DoDamage(1);

            WorldManager.Score += 125;
        }
    }
}