using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    // Demonstration code using events:
    //      Delegates: https://unity3d.com/learn/tutorials/topics/scripting/delegates
    //      Events: https://unity3d.com/learn/tutorials/topics/scripting/events

    // Declare a delegate. A delegate is like a function template which defines a function signature, 
    // that is, what the function returns, how many arguments it takes and what are the types of those
    // arguments
    // The delegate below is called TogglePatrolAction and defines a function that returns nothing (void)
    // and takes in no arguments. We will use this delegate when creating a custom unity event.
    // The TogglePatrolAction delegate is like a variable that can store a function that matches the 
    // delegates signature, indeed, it can hold a list of functions that match the delegates signature.
    public delegate void TogglePatrolAction();

    // Next we create an event of the above delegate type. You can call the event what you like but they 
    // are typically called On...., I'm calling the event OnTogglePatrol.
    // We will be able to assign (aka subscribe) functions to the event (See the Patrol.cs script)
    // I am making the event static so that it can be accessed (and subscribed to) even before a GameManager
    // instance is created.
    public static event TogglePatrolAction OnTogglePatrol;

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
    }

    public void TogglePatrolling()
    {
        // Make sure the event is not null, that is, make sure at least one function has
        // subscribed to it
        if (OnTogglePatrol != null)
        {
            // Call the event, this is essentially the same as calling every function
            // that has subscribed to it.
            OnTogglePatrol();
        }
    }
 
}
