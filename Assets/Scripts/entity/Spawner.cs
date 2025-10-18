using Unity.Mathematics;
using UnityEngine;

public class Spawner : Entity
{
    public Transform spawnPoint;
    public void init(int team)
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.team = team;
        Entites[team].Add(this);
    }
    protected override void Start()
    {
        base.Start();
        
        unitStatsUI = Instantiate(ResourceLoader.UIPrefabs["healthBar"], transform.position + new Vector3(0, spriteRenderer.bounds.size.y + .2f, 0), new quaternion(), transform).GetComponent<UnitStatsUI>();
        unitStatsUI.init(stats);

    }
    public void spawnUnit(string key)
    {
        if (playerManager.playerManagers[team].playerData.makePurchase("mushroom", Mathf.CeilToInt(unitData.unitStats[key]["cost"])))
        {
            animator.SetTrigger("spawn");
            GameObject newUnit = Instantiate(ResourceLoader.unitPrefabs[key], spawnPoint.transform.position, new quaternion(), transform);
            Unit u = newUnit.GetComponent<Unit>();
            u.team = team;
            u.material = material;
        }
    }
    protected override void die()
    {
        soundManager.instance.playSound(soundManager.instance.lose, 1);
    }
}
