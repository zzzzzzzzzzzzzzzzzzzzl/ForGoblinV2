using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Magi : Unit
{
    protected override void Start()
    {
        base.Start();

    }
    protected override Action attack()
    {
        return () =>
    {
        foreach (Entity e in findUnitsInrange(targetPos, stats["splash"], -team))
        {
            e.healthChange(-stats["attack"] * nextAttackMulti);
        }
        GameObject fx = Instantiate(ResourceLoader.fx["destroy"], targetPos + new Vector3(0, .5f, 0), new quaternion());
        fx.transform.localScale *= stats["splash"];
        soundManager.instance.playSound(soundManager.instance.magiExplosion, 1);
    };
    }
}
