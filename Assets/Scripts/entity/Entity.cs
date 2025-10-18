using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
// using Unity.Mathematics;
// public abstract class EntityStateBase
// {
//     public abstract void update(Entity e);
// }
public class Entity : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Animator animator;
    public Dictionary<string, float> stats;
    [HideInInspector]
    public static Dictionary<Type, List<Entity>> entitiesDict = new();
    [HideInInspector]
    public int team;
    [HideInInspector]
    public Material material
    {
        get => spriteRenderer.material; set
        {
            spriteRenderer.material = value;
        }
    }
    public SpriteRenderer spriteRenderer;
    private float _health;
    protected StateManager stateManager;
    [HideInInspector]
    public UnitStatsUI unitStatsUI;
    protected Dictionary<string, Action> stateActions = new();
    public string entityType;
    public static Dictionary<int, List<Entity>> Entites = new()//team represented by int
    {
        { -1,new()},
        { 0,new()},
        { 1,new()}
    };
    public virtual float health
    {
        get => _health;
        set
        {
            _health = value;
            if (health <= 0 && stateManager.state != "dead")
            {
                stateManager.state = "dead";
                Entites[team].Remove(this);
                die();
            }
        }
    }
    public void healthChange(float change)
    {
        if (change < 0)
        {
            change = change + stats["armour"] < 0 ? change + stats["armour"] : -1;
        }
        if (change + health > stats["health"])
        {
            health = stats["health"];
            change = 0;
        }
        DamageText damageText = Instantiate(ResourceLoader.UIPrefabs["DamageText"], transform.position + new Vector3(0, 1, 0), new quaternion(), transform).GetComponent<DamageText>();
        damageText.damage = change;
        health += change;
        unitStatsUI.updateHealth(health, stats["health"]);
    }
    protected virtual void Update()
    {
        // if(boxCollider.is)
    }
    protected virtual void die()
    {
        animator.Play("die");
    }
    protected virtual void Start()
    {
        try
        {
            if (entityType != "Imp")
            {
                stats = new Dictionary<string, float>(unitData.unitStats[entityType]);
            }
            health = stats["health"];
        }
        catch
        {
            Debug.LogError($"{entityType} invalid key for unitStats ");
        }


        material = playerManager.playerManagers[team].material;

        if (boxCollider == null) boxCollider = GetComponentInChildren<BoxCollider2D>();
        if (boxCollider == null) boxCollider = GetComponent<BoxCollider2D>();
        if (entityType == "spawner") Debug.Log(boxCollider);

        stateManager = gameObject.AddComponent<StateManager>();
        if (entityType != "Spawner") spriteRenderer.sortingOrder = UnityEngine.Random.Range(0, 10);
    }
    protected virtual void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}

