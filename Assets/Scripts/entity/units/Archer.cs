
using System;
using System.Collections;
using UnityEngine;

public class Archer : Unit
{
    protected override void Start()
    {
        base.Start();
    }
    IEnumerator slow(Entity target)
    {
        target.stats["speed"] *= stats["slowMulti"];
        yield return new WaitForSeconds(stats["slowDuration"]);
        target.stats["speed"] /= stats["slowMulti"];
    }
    protected override Action attack()
    {
        return () =>
        {
            if (target != null)
            {
                StartCoroutine(slow(target));
                target.healthChange(-stats["attack"] * nextAttackMulti);
                soundManager.instance.playSound(soundManager.instance.attackSounds[UnityEngine.Random.Range(0, soundManager.instance.attackSounds.Count)], 1);
            }
        };
    }

}