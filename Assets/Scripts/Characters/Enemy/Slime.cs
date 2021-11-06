// Elijah Nieves
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Slime : Enemy
{
    private bool isCoroutineExecuting = false;
    int seconds = 0;
    private int attackDelay = 1;       //how long the slime will wait between jumps toward the player.
    private int attackLength = 2;       //how many seconds the slime will attack before the delay

    // Use FixedUpdate() for physics
    protected override void FixedUpdate()
    {
        StartCoroutine(Wait(attackDelay, SlimeMove));
        
        /*StartCoroutine(Time());
        while (seconds < attackLength)
        {
            Movement(direction);
        }

        if (seconds >= (attackLength + attackDelay))
        {
            seconds = 0;
        }*/
    }

    void SlimeMove()
    {
        Movement(direction);
    }

    IEnumerator Wait(float delay, System.Action passedFunction)
    {

        if (isCoroutineExecuting)
        {
            yield break;
        }

        isCoroutineExecuting = true;

        yield return new WaitForSecondsRealtime(delay);      //wait for the duration of the attack delay

        passedFunction();       // currently trying to figure out how to make this run for 2 seconds, then wait for 1 second, and repeat
                                // all solultions I have tried online do not function correctly. Tried using Time, did not work.

        isCoroutineExecuting = false;
    }

    /*IEnumerator Time()
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }

        isCoroutineExecuting = true;

        while (true)
        {
            timeCount();

            yield return new WaitForSecondsRealtime(1);
        }
    }

    void timeCount()
    {
        seconds += 1;
    }*/
}
