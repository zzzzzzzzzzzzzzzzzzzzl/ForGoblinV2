using System;
using System.Collections;
using UnityEngine;

public class mushroomTimer : MonoBehaviour
{
    public float timer ;
    public PlayerData playerData;
    void Start()
    {
        StartCoroutine(gather());
    }
    void Update()
    {
        timer += Time.deltaTime;
    }
    IEnumerator gather()
    {
        timer = 0;
        yield return new WaitUntil(() => timer > playerData.gatherRate);
        playerData.mushroom++;
        StartCoroutine(gather());
    }
}