using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Summoner : Unit
{
    float summonTimer = 0;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        summonTimer += Time.deltaTime;
    }
    protected override void setStateActions()
    {
        base.setStateActions();
        stateManager.stateActions.Add("summon", () => stateManager.waitForAction(triggerSummon()));
    }
    protected override void setState()
    {
        base.setState();
        if (summonTimer>stats["summonCD"] && target == null) stateManager.state = "summon";
    }
    IEnumerator triggerSummon()
    {
        summonTimer = 0;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(.8f);
        GameObject newUnit = Instantiate(ResourceLoader.unitPrefabs["Imp"], transform.position, new quaternion(), transform.parent);
        Unit imp = newUnit.GetComponent<Unit>();
        imp.team = team;
        imp.stats = getImpStats();
        Instantiate(ResourceLoader.fx["magic"], transform.position, new quaternion(), transform.parent);
        soundManager.instance.playSound(soundManager.instance.summonerSummon, 1);
    }
    Dictionary<string, float> getImpStats()
    {
        Dictionary<string, float> impStats = new()
        {
        {"health", stats["summonHealth"]},
        {"speed", stats["summonSpeed"]},
        {"attackSpeed", stats["summonAttackSpeed"]},
        {"attack", stats["summonAttack"]},
        {"range", 1},
        { "crit", 0},
        { "armour", 0},
        };
        return impStats;

    }
}