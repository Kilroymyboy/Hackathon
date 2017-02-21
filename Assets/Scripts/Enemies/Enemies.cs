using UnityEngine;
using System.Collections;
using System;

#region - Script Synopsis
/*
This script forms an OOP (Object Oriented Programming) basis for Enemy creation.
The generic Enemy<T> class, at instantiation, creates an enemy GameObject and attaches any script of type
Enemy that you so choose. This instantiation is handled in the EnemyFactory script.
*/
#endregion

//Generic Enemy "shell" that instantiates the Enemy script in the EnemiesFactory script and attaches it 
//to a GameObject. References for both of these objects are in GameObject and ScriptComponent, respectively
public class Enemy<T> where T : Enemy
{
    //References to the Enemy GameObject and attached ScriptComponent
    public GameObject GameObject;
    public T ScriptComponent;

    public Enemy(string name)
    {
        GameObject = new GameObject(name);
        ScriptComponent = GameObject.AddComponent<T>();
    }
}

//Base class that all specific Enemy types (Bouncer, Ghost, Tweaker, etc) inherit from
public abstract class Enemy : MonoBehaviour
{
    //Stores Enemy's health value, set in each inheriting enemy's script
    protected int HP;

    //Stores references to the enemy's RigidBody, SpriteRenderer and Collider components respectively
    public Rigidbody2D Body;
    public SpriteRenderer Sprite;
    public CircleCollider2D Collider;

    //Stores enemy's speed and direction
    public int Speed;
    public int Direction;

    //Abstract method, requiring unique movement behaviour for each inheriting Enemy type
    protected abstract void MovementPattern();

    void Awake()
    {
        //Adds components common to all enemy types
        Body = gameObject.AddComponent<Rigidbody2D>();
        Sprite = gameObject.AddComponent<SpriteRenderer>();
        Collider = gameObject.AddComponent<CircleCollider2D>();

        //Sets "eyeball" sprite common to all enemy types
        Sprite.sprite = Resources.Load<Sprite>("EyeBall");
        Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("EyeBall");
    }

    //Enemy speed, direction and position determined by this pseudo-constructor just after instantiation in EnemiesFactory script
    public void Initialize(int speed, int direction, Vector3 position)
    {
        Speed = speed;
        Direction = direction;
        transform.position = position;
    }

    //Initialize overload for enemies that don't require a specific direction at instantiation
    public void Initialize(int speed, Vector3 position)
    {
        Speed = speed;
        transform.position = position;
    }

    //Called whenever the player damages the enemy
    public void DoDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            GameObject.Find("Stomper").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }  
    }

    //Checks if the enemy hit the horizontal border of the screen, and pushes it back into play
    protected void BorderHitCheck(float force)
    {
        force *= Speed;
        Vector3 enemyPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (enemyPosition.x < 0f)
        {
            Body.velocity = new Vector2(0, Body.velocity.y);
            Body.AddForce(new Vector2(force, 0));
            Direction = 1;
        }
        else if (enemyPosition.x > 1f)
        {
            Body.velocity = new Vector2(0, Body.velocity.y);
            Body.AddForce(new Vector2(force * -1, 0));
            Direction = -1;
        }
    }

    //Destroys enemies if they have veered far out of the visible screen space
    protected void DestroyOutofBounds()
    {
        if (transform.position.y < -6 || transform.position.y > 30)
            GameObject.Destroy(gameObject);
    }


    //Determines if the Enemy collided with the player and effects the player GameObject accordingly
    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Rigidbody2D CheeseBody = coll.gameObject.GetComponent<Rigidbody2D>();
            CheeseBody.velocity = new Vector2(0, 0);
            CheeseBody.AddForce(new Vector2(0, 300));

            coll.gameObject.GetComponent<PlayerController>().enabled = false;
            coll.gameObject.GetComponent<Collider2D>().enabled = false;

            foreach (Transform child in coll.gameObject.transform)
                Destroy(child.gameObject);

            GameOverManager.IsGameOver = true;
        }
    }
}