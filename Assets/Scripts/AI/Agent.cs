using UnityEngine;

public class Agent : playerManager
{
    float timePassed = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 3)
        {
            timePassed = 0;
            spawner.spawnUnit("Swordsman");
        }
    }
    protected override void init()
    {
        Debug.Log(aiType.selected);
        material = aiType.selected.material;

        spawner.material = material;
        spawner.spriteRenderer.material = material;
        playerData.Upgrades = new(aiType.selected.baseUpgrades);
    }
}
