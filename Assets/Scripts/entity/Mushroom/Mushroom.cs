using System.Collections;
using UnityEngine;

public class Mushroom : Entity
{
    protected override void Awake()
    {
        base.Awake();
        Entites[0].Add(this);
    }
}