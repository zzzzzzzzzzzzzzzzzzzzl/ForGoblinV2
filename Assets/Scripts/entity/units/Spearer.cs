using System;
using System.Collections;
using UnityEngine;

public class Spearer : Unit
{
    protected override void Start()
    {
        base.Start();
    }
    protected override Action attack()
    {
        return () =>
        {
            foreach (Entity e in findUnitsInrange(transform.position, stats["range"], -team))
            {
                e.healthChange(-stats["attack"] * nextAttackMulti);
            }
        };
    }
}
