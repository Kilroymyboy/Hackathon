using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script is an example of a Singleton Pattern which instantiates a single instance of itself
once the "getter" is accessed elsewhere in code. The purpose of this script is to create a GameObject
at runtime that holds the various possible player states, determined in the enums at the bottom of this file.
Using a Singleton Pattern here allows easy global access to its static instance, which in turn gives access to the possible states,
which are publically visible in the inspector since they themselves are not static.
*/
#endregion

public class PlayerState : MonoBehaviour
{
    //Singleton instance, globally accessible for setting player's state
    private static PlayerState _instance;
    public static PlayerState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("PlayerState").AddComponent<PlayerState>();

            return _instance;
        }
    }

    //Stores exclusive states for Horizontal, Vertical, Direction and Attack states
    public Horizontal Horizontal;
    public Vertical Vertical;
    public DirectionFacing DirectionFacing;
    public Attack Attack;
}

public enum Horizontal
{
    Idle = 0,
    MovingLeft = -1,
    MovingRight = 1
}

public enum Vertical
{
    Grounded,
    Airborne
}

public enum DirectionFacing
{
    Left = -1,
    Right = 1
}

public enum Attack
{
    Passive,
    Punch,
    Projectile
}
