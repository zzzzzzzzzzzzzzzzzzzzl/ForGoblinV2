using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;

public class CurrencyUI : UIMaterialManager
{
    public TextMeshProUGUI gatherRate;
    public TextMeshProUGUI mushroomCount;
    public TextMeshProUGUI crystalJuice;
    public LoadingBar mushroomBar;
    public static CurrencyUI instance;
    public Button upgradeGatherRate;
    ToolTipAppearOnHover gatherRatePriceOnHover;
    PlayerData playerData;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public void setUI(float gatherRate, int mushroomCount, int crystalJuice)
    {
        updateMushroomCount(mushroomCount);
        updateCrystalJuice(crystalJuice);
        upgradeGatherRate.onClick.AddListener(buyGatherRateOnClick);
        playerData = playerManager.playerManagers[1].playerData;


        gatherRatePriceOnHover = upgradeGatherRate.gameObject.AddComponent<ToolTipAppearOnHover>();
        gatherRatePriceOnHover.currencyType = "crystal";
        gatherRatePriceOnHover.cost = playerData.Upgrades.getCost("gatherRate", "gatherRate");
        updateGatherRate(gatherRate);
    }
    public void updateGatherRate(float gatherRate)
    {
        this.gatherRate.text = $"GATHER RATE {Math.Round(playerData.gatherRate,2)}";
        gatherRatePriceOnHover.cost = playerData.Upgrades.getCost("gatherRate", "gatherRate");
    }
    public void updateMushroomCount(int mushroomCount)
    {
        this.mushroomCount.text = $"MUSHROOM {math.floor(mushroomCount)}";
    }
    public void updateCrystalJuice(int crystalJuice)
    {
        this.crystalJuice.text = $"CRYSTAL {math.floor(crystalJuice)}";
    }
    void buyGatherRateOnClick()
    {
        if (playerData.makePurchase("crystal", playerData.Upgrades.getCost("gatherRate", "gatherRate")))
        {
            playerData.Upgrades.upgradesPurchased["gatherRate"]["gatherRate"]++;
            playerData.gatherRate = 1f/playerData.Upgrades.upgradesPurchased["gatherRate"]["gatherRate"];
        }
    }
    void Update()
    {
        mushroomBar.setBar( playerManager.playerManagers[1].timer.timer/ playerManager.playerManagers[1].playerData.gatherRate);
    }
}