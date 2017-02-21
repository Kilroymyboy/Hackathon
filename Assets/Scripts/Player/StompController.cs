using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script simply destroys an enemy that's been "stomped" on, which is determined by a collision between the "Stomper" GameObject's 
collider colliding with that of an enemy.
*/
#endregion

public class StompController : MonoBehaviour
{
    //Detects when "Stomper" GameObject collides with an enemy. Awards points, and effects the enemy accordingly
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(coll.gameObject);

            Rigidbody2D cheeseHead = GameObject.Find("CheeseHead").GetComponent<Rigidbody2D>();
            cheeseHead.velocity = new Vector2(cheeseHead.velocity.x, 0);
            cheeseHead.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        
            GetComponent<AudioSource>().Play();

            WorldManager.Score += 300;
        }
    }
}