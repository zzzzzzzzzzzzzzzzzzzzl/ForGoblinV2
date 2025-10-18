using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    public Spawner spawner;
    public int team;
    public Material _material;
    public Material material
    {
        get => _material; set
        {
            _material = value;
            // setMaterial();
            UIMaterialManager.updateOverlays();
        }
    }
    [HideInInspector]

    public PlayerData playerData = new();
    [HideInInspector]

    public mushroomTimer timer;
    [HideInInspector]
    string _selectedUnit;
    public string selectedUnit
    {
        get => _selectedUnit; set
        {
            _selectedUnit = value;
            UpgradeUI.instance.setRows(_selectedUnit);
        }
    }
    public static Dictionary<int, playerManager> playerManagers = new();

    protected virtual void Awake()
    {
        material = _material;//just to trigger the get set
        playerManagers.Add(team, this);
        timer = gameObject.AddComponent<mushroomTimer>();
        timer.playerData = playerData;
        spawner.init( team);
        if (team == 1) initTeam1();
    }
    void initTeam1()
    {
        setUI();
    }
    protected virtual void Update()
    {

    }
    void setUI()
    {
        playerData.updateMushroomUI += () => { CurrencyUI.instance.updateMushroomCount(playerData.mushroom); };
        playerData.updateCrystalUI += () => { CurrencyUI.instance.updateCrystalJuice(playerData.crystal); };
        playerData.updateGatherRateUI += () => { CurrencyUI.instance.updateGatherRate(playerData.gatherRate); };

        playerInputActions = gameObject.AddComponent<PlayerInputActions>();
        playerInputActions.playerManager = this;

        CurrencyUI.instance.setUI(playerData.gatherRate, playerData.mushroom, playerData.crystal);

    }
}
