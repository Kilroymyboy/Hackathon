using UnityEngine;

//Remove DevU advert on play
public class Disable : MonoBehaviour
{
	void Start() 
	{
        gameObject.SetActive(false);
	}
}