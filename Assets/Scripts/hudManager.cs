using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hudManager : MonoBehaviour {

    private string[] singleClause = new string[3];
    public Transform clause;

    //Make a queue of the prefab type
    //private Queue<T> clauses = new Queue<T>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void testClause()
    {
        // Take whatever is in the Clause array right now check it for format
        // and make it an obj then maybe queue it?
        Transform parent = GameObject.Find("Grid").transform;
        GameObject clause = (GameObject)Instantiate(Resources.Load("Clause"), parent);
        //GameObject clause = Instantiate(Resources.Load("Clause", typeof(GameObject))) as GameObject;
        //Instantiate(clause, parent.position, Quaternion.identity);
        //Resources.LoadAsync

    }

    public void clearClause()
    {
        for (int i = 0; i < 3; i++)
        {
            this.singleClause[i] = null;
        }
    }

    public void displayClause()
    {
        for (int i = 0; i < 3; i++)
        {
           print(this.singleClause[i] + " in index: " + i);
        }
    }

    public void addToClause(string word, int i)
    {
        this.singleClause[i] = word;
    }
}
