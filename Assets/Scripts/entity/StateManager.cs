using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class StateManager : MonoBehaviour

{

    public Dictionary<string, Action> stateActions = new();

    public string state;

    public bool locked = false;

    public void waitForAction(IEnumerator action)

    {
        StartCoroutine(lockAction(action));

    }

    IEnumerator lockAction(IEnumerator action)

    {

        locked = true;

        yield return action;

        locked = false;

    }

    public void stateAction()

    {

        if (!locked) stateActions[state]();

    }

}
