using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudManager : MonoBehaviour {

    private string[] singleClause = new string[3];
    GameObject clause;

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
        clause = (GameObject)Instantiate(Resources.Load("Clause"), parent);

        Text txt = clause.GetComponentInChildren<Text>();

        for (int i = 0; !(singleClause[i].Equals(null)) && i < 4; i++)
        {
            if (singleClause[0].Equals("If"))
            {
                txt.text = singleClause[0] + " " + singleClause[1] + "\n" + "then " + singleClause[2];
            }
            else if (singleClause[0].Equals("Else"))
            {
                txt.text = singleClause[0] + " " + singleClause[1] + "\n" + "then " + singleClause[2];
            }
            else if (singleClause[0].Equals("While"))
            {
                txt.text = singleClause[0] + " " + singleClause[1] + "\n" + "do " + singleClause[2];
            }
            else
            {
                print("This is invalid input: " + singleClause[0]);
            }
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


    public void deleteClause()
    {
        //GameObject clone = (GameObject)Instantiate(clause, transform.position, Quaternion.identity);
        Destroy((GameObject)Instantiate(clause, transform.position, Quaternion.identity));
    }
}
