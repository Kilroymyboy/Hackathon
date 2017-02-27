using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    //Reference to player's RigidBody component
    Rigidbody2D BoxBody;

    void Start()
    {
        BoxBody = GetComponent<Rigidbody2D>();

    }

    //Calls methods that handle physics-based movement
    void FixedUpdate()
    {
    }

    //Used to detect player inputs and set parameters & players states for physics behaviour that occurs in FixedUpdate()
    void Update()
    {
    }

    //Handles basic horizontal movement using physics-based velocity, called in FixedUpdate()
    private void WalkMotion()
    {
    }

    //Handles player's vertical state and allows jumping only when grounded, using physics-based AddForce(), called in FixedUpdate()
    private void JumpMotion()
    {
    }

    void OnCollisionEnter2D(Collision collision)
    { 
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}

