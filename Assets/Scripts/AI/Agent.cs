using UnityEngine;

public class Agent : playerManager
{
    public aiType aitype;
    public Material mat;
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
    public void setAi()
    {
        aitype = aiType.selected;
        material = aitype.material;

        spawner.material = material;
        spawner.spriteRenderer.material = material;
        playerData.Upgrades = new(aitype.baseUpgrades);
    }
}
