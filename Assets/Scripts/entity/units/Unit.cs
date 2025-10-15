using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
public class Unit : Entity
{
    protected Entity target;
    protected float nextAttackMulti = 1;
    float attackTimer = 0;
    protected Vector3 targetPos;
    protected override void Start()
    {
        base.Start();
        if (team == -1)
        {
            spriteRenderer.flipX = true;
        }
        stateManager.state = "walk";
        Entites[team].Add(this);
        setStateActions();
        unitStatsUI = Instantiate(ResourceLoader.UIPrefabs["healthBar"], transform.position + new Vector3(0, spriteRenderer.bounds.size.y + .2f, 0), new quaternion(), transform).GetComponent<UnitStatsUI>();
        unitStatsUI.init(stats);

    }
    protected virtual void setStateActions()
    {
        stateManager.stateActions.Add("walk", () => walk());
        stateManager.stateActions.Add("attack", () => { if (target != null) { rollCrit(); stateManager.waitForAction(attackWrapper(attack())); } });
        stateManager.stateActions.Add("idle", () => { });
        stateManager.stateActions.Add("dead", () => { stateManager.locked = true; });
    }
    protected override void Update()
    {
        base.Update();
        if (stateManager.state != "dead" && !stateManager.locked)
        {
            setState();
        }
        stateManager.stateAction();
        attackTimer += Time.deltaTime;
    }
    protected void checkForTarget()
    {
        Entity nearestEntity = findNearestEntity(team);
        target = (nearestEntity != null && (nearestEntity.transform.position - transform.position).sqrMagnitude < stats["range"]) ? nearestEntity : null;
        if (target) targetPos = target.transform.position;
    }
    protected virtual void setState()
    {
        checkForTarget();
        stateManager.state = target != null ? "attack" : "walk";
    }
    protected Entity findNearestEntity(int team)
    {
        return Entites[-team].OrderBy(i => (i.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
    }
    protected List<Entity> findUnitsInrange(Vector3 pos, float range, int team)
    {
        return Entites[team].Where(i => (i.transform.position - pos).sqrMagnitude < range).ToList();
    }
    protected virtual void walk()
    {
        transform.position += new Vector3(.1f, 0, 0) * Time.deltaTime * team * 3 * stats["speed"];
    }
    void rollCrit()
    {
        if (stats["crit"] > 0) nextAttackMulti = UnityEngine.Random.Range(0f, 100f) < stats["crit"] ? 2 : 1;
    }
    protected virtual Action attack()
    {
        return () =>
        {
            if (target != null)
            {
                target.healthChange(-stats["attack"] * nextAttackMulti);
                soundManager.instance.playSound(soundManager.instance.attackSounds[UnityEngine.Random.Range(0, soundManager.instance.attackSounds.Count)], 1);
            }
        };
    }
    IEnumerator attackWrapper(Action action)
    {
        attackTimer = 0;
        animator.speed = 1 / stats["attackSpeed"];
        animator.SetTrigger("attack");
        yield return new WaitUntil(()=>attackTimer>stats["attackSpeed"]);
        action();
        animator.speed = 1;
    }
    protected override void die()
    {
        soundManager.instance.playSound(soundManager.instance.death, 1f);
        animator.Play("die");
    }
}
