using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hudManager : MonoBehaviour {

    private string[] singleClause = new string[3]; 

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
        Transform parent = GameObject.Find("Grid").transform;
        GameObject clause = (GameObject)Instantiate(Resources.Load("Clause"), parent);

        if (singleClause[0].Equals("If"))
        {

        }
        else if (singleClause[0].Equals("Else"))
        {

        }
        else if (singleClause[0].Equals("While"))
        {

        }
        else
        {
            print("This is invalid input: " + singleClause[0]);
        }
        // Take whatever is in the Clause array right now check it for format
        // and make it an obj then maybe queue it?
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
