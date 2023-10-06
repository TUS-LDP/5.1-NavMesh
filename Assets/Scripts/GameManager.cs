using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public bool partollingEnabled;

    // Demonstration code using events:
    //      Delegates: https://unity3d.com/learn/tutorials/topics/scripting/delegates
    //      Events: https://unity3d.com/learn/tutorials/topics/scripting/events

    // Declare a delegate template
    public delegate void TogglePatrolAction();

    // Create an event of the above delegate type
    public event TogglePatrolAction OnTogglePatrol;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        // Set patrollingEnabled to true by default
        partollingEnabled = true;
    }

    public void TogglePatrolling()
    {
        this.partollingEnabled = !this.partollingEnabled;

        if (OnTogglePatrol != null)
        {
            OnTogglePatrol();
        }
    }
 
}
