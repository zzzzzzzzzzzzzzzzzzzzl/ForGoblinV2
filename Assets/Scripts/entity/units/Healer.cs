using System.Collections;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Healer : Unit
{
    Unit healTarget;
    float healTimer = 0;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        healTimer += Time.deltaTime;
    }
    protected override void setStateActions()
    {
        stateManager.stateActions.Add("walk", () => walk());
        stateManager.stateActions.Add("idle", () => { });
        stateManager.stateActions.Add("dead", () => { stateManager.locked = true; });
        stateManager.stateActions.Add("heal", () => stateManager.waitForAction(triggerHeal()));
    }
    protected override void setState()
    {
        checkForTarget();
        healTarget = checkForHealTarget();
        stateManager.state = target != null ? "idle" : "walk";
        stateManager.state = healTarget != null ? "heal" : stateManager.state;
    }
    Unit checkForHealTarget()
    {//finds the lowest unit positioned in front of the healer
        Unit infront = Entites[team].OfType<Unit>().Where(i => i.transform.position.x * team > transform.position.x * team).Where(i => i.health < i.stats["health"]).OrderBy(i => i.health).FirstOrDefault();
        return Entites[team].OfType<Unit>().Where(i => i.transform.position.x * team > transform.position.x * team && (i.transform.position.x - stats["healRange"]) * team < transform.position.x * team).Where(i => i.health < i.stats["health"]).OrderBy(i => i.health).FirstOrDefault();
    }
    IEnumerator triggerHeal()
    {
        animator.Play("heal");
        healTimer = 0;
        yield return new WaitUntil(() => healTimer > stats["healSpeed"]);
        if (healTarget != null)
        {
            healTarget.healthChange(stats["healAmount"]);
            Instantiate(ResourceLoader.fx["heal"], healTarget.transform.position, new quaternion(), transform.parent);
            soundManager.instance.playSound(soundManager.instance.healerHeal, 1);
        }
    }
}