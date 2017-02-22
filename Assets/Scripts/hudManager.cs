using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudManager : MonoBehaviour {

    private string[] singleClause = new string[3];
    // List of clauses is a cvs
    public List<GameObject> cvs = new List<GameObject>();
    private int index = 0;

    // Use this for initialization
    void Start () {
    }

    public void testClause()
    {
        Transform parent = GameObject.Find("Grid").transform;
        GameObject clause = (GameObject)Instantiate(Resources.Load("Clause"), parent);

        Text txt = clause.GetComponentInChildren<Text>();

        if (!(singleClause[0].Equals(null) || singleClause[1].Equals(null) || singleClause[2].Equals(null)))
        {
            if (singleClause[0].Equals("If") || singleClause[0].Equals("Else"))
            {
                txt.text = singleClause[0] + " " + singleClause[1] + "\n" + "then " + singleClause[2];
            } else
            {
                txt.text = singleClause[0] + " " + singleClause[1] + "\n" + "do " + singleClause[2];
            }
            
        }

        print("Before adding to list" + cvs.Count);
        cvs.Add((GameObject)clause);
        index = cvs.Count;
        print("After adding to list" + cvs.Count);
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


    public void deleteClause(GameObject checking)
    {
        //GameObject clone = (GameObject)Instantiate(clause, transform.position, Quaternion.identity);
        //Destroy((GameObject)Instantiate(clause, transform.position, Quaternion.identity));
        // print("Goes here! hud");


        print("IN delete clause" + index);
        for (int i = 0; i < this.cvs.Count; i++)
        {
            print("This is the checking list before deletion: " + checking);
            print("This is the cvs list before deletion: " + cvs);
            if (checking.Equals(cvs[i]))
            {
                Destroy(cvs[i]);
                print("This is the checking list before deletion: " + checking);
                print("This is the cvs list before deletion: " + cvs);
            }
        }
    }
}
