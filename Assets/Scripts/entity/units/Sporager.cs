using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class sporager : Unit
{

    Vector3 targetPosition;
    protected override void Start()
    {
        base.Start();
        getNextPos();
    }
    protected override void setStateActions()
    {
        stateManager.stateActions.Add("walk", () => walk());
        stateManager.stateActions.Add("idle", () => stateManager.waitForAction(idle()));
        stateManager.stateActions.Add("dead", () => { stateManager.locked = true; });
    }
    protected override void setState()
    {
        stateManager.state = "walk";
        getNextPos();
    }
    IEnumerator idle()
    {
        animator.Play("idle");
        yield return new WaitForSeconds(1.5f);
    }

    void getNextPos()
    {
        Mushroom m =Entites[0].Where(e => e is Mushroom).OrderBy(e => (transform.position - e.transform.position).sqrMagnitude).FirstOrDefault() as Mushroom;
        Debug.Log("try get next pos");
        
        //list of mushrooms in range
        //now filter them so they are behind enemy
    }
    void get()
    {
    }

}