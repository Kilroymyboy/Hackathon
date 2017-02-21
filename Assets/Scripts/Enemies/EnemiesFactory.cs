using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script's Awake() method creates a collider in the middle of the screen which acts as a trigger.
When that trigger is activated, it will fire the event tied into the OnTriggerEnter2D() method below.
That method simply instantiates random enemies (the number is determined by the current difficulty level)
within the switch() statement.
*/
#endregion

public class EnemiesFactory : MonoBehaviour
{
    //Creates a collider that triggers Enemy spawning
    void Awake()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.offset = new Vector2(0, 5);
        collider.size = new Vector2(0.5f, 9.5f);
    }


    //When the collider is triggered it spawns a new wave of Enemies determined by the current "level" multiplier
    //which increments with each triggered spawn. Also, destroys the trigger collider to avoid possible re-triggering
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(GetComponent<Collider2D>());
            WorldManager.Level++;

            for (int i = 0; i < WorldManager.Difficulty; i++)
            {
                int randomInstance = Random.Range(0, 6);
                float randomX = transform.position.x + (6 * (Random.Range(0, 2) * 2 - 1));
                float randomY = Random.Range(4, 8);

                switch (randomInstance)
                {
                    case 0:
                        Enemy<Gigantor> giantGeorge = new Enemy<Gigantor>("GiantGeorge");
                        giantGeorge.ScriptComponent.Initialize(speed: 1, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 1:
                        Enemy<Tweaker> tweakyTim = new Enemy<Tweaker>("TweakyTim");
                        tweakyTim.ScriptComponent.Initialize(speed: 4, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 2:
                        Enemy<Lush> lushyLinda = new Enemy<Lush>("LushyLinda");
                        lushyLinda.ScriptComponent.Initialize(speed: Random.Range(6, 18), position: new Vector3(randomX, randomY, 1));
                        break;

                    case 3:
                        Enemy<Bouncer> bouncyBill = new Enemy<Bouncer>("BouncyBill");
                        bouncyBill.ScriptComponent.Initialize(speed: 4, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 4:
                        Enemy<Torque> torqyTom = new Enemy<Torque>("TorqyTom");
                        torqyTom.ScriptComponent.Initialize(speed: 3, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 5:
                        Enemy<Ghost> ghostlyGayle = new Enemy<Ghost>("GhostlyGayle");
                        ghostlyGayle.ScriptComponent.Initialize(speed: 2, position: new Vector3(randomX, randomY, 1));
                        break;
                }
            }
        }
    }
}