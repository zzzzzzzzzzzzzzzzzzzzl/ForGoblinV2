using System.Collections;
using System.Linq;
using System.Text;
using UnityEditor.Rendering;
using UnityEngine;

public class sporager : Unit
{
    float maxWalkPos;
    float minWalkPos;
    Vector3 walkTarget;
    public int mushroomCount;
    Entity nearestMushroom;
    Spawner spawner;
    protected override void Start()
    {
        base.Start();
        spawner = Entites[team].Find(i => i is Spawner) as Spawner;
        minWalkPos = spawner.transform.position.x;
    }
    protected override void setStateActions()
    {
        stateManager.stateActions.Add("walk", () => walk());
        stateManager.stateActions.Add("idle", () => stateManager.waitForAction(idle()));
        stateManager.stateActions.Add("dead", () => { stateManager.locked = true; });

    }
    IEnumerator idle()
    {
        animator.Play("idle");
        yield return new WaitForSeconds(1.5f);

    }
    int dir;
    protected override void walk()
    {
        
        transform.position += new Vector3(dir * stats["speed"] * Time.deltaTime, 0, 0);
    }
    bool goToWalkTarget()
    {
        if (Mathf.Abs(transform.position.x - walkTarget.x) < .2f)
        {
            walkTarget = findWalkTarget();
            dir = transform.position.x > walkTarget.x ? -1 : 1;
            animator.Play("pivot");
            if (nearestMushroom != null)
            {
                pickUpMushroom();
            }
            dropOff();
            return true;
        }
        return false;
    }
    protected override void setState()
    {
        goToWalkTarget();
    }
    Vector3 findWalkTarget()
    {
        nearestMushroom = findNearestValidMushroom();
        maxWalkPos = findNearestEntity(team).transform.position.x;
        if (mushroomCount >= stats["capacity"])
        {
            return new Vector3(minWalkPos, transform.position.y, transform.position.z);
        }
        if (nearestMushroom != null)
        {
            return nearestMushroom.transform.position;
        }
        else
        {
            if (Random.Range(0, 2) == 0) stateManager.state = "idle";
            return new Vector3(Random.Range(minWalkPos, maxWalkPos), transform.position.y, transform.position.z);
        }
    }
    void dropOff()
    {
        if ((transform.position - spawner.transform.position).sqrMagnitude < 1)
        {
            mushroomCount = 0;
        }
    }
    Entity findNearestValidMushroom()
    {
        Entity e = Entites[0].Where(i => (i.transform.position - transform.position).sqrMagnitude < stats["forageRange"])
        .Where(i => i.transform.position.x * team < maxWalkPos * team)
        .OrderBy(i => (i.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
        return e;
    }
    void pickUpMushroom()
    {
        if ((transform.position - nearestMushroom.transform.position).sqrMagnitude < .2f)
        {
            animator.Play("shovel");
            nearestMushroom.health = -100;
            mushroomCount++;
        }
    }
}