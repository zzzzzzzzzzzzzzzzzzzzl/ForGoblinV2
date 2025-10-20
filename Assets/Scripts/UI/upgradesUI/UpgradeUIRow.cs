using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeUIRow : UIMaterialManager, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public LoadingBar currentStats;
    public LoadingBar hoverIncrement;
    string unit;
    string stat;
    public Button button;
    public ToolTipAppearOnHover toolTipAppearOnHover;
    PlayerData playerData;
    protected override void Start()
    {
        base.Start();
        icon.material = material;
    }
    public void setRow(string unit, string stat)
    {
        playerData = playerManager.playerManagers[1].playerData;
        this.unit = unit;
        this.stat = stat;
        currentStats.barColor = Upgrades.statBarColors[stat];
        hoverIncrement.barColor = Upgrades.statBarColors[stat] * .5f;

        toolTipAppearOnHover.cost = playerData.Upgrades.getCost(unit, stat);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
                Debug.Log("click");
            if (playerData.makePurchase("crystal", playerData.Upgrades.getCost(unit, stat)))
            {
                Debug.Log("purchased");
                toolTipAppearOnHover.cost = playerData.Upgrades.getCost(unit, stat);
                playerData.Upgrades.upgradesPurchased[unit][stat]++;
                
                setBar(unit, stat);
                setIncrement(unit, stat);
                setRow(unit, stat);
            }
        });
        setBar(unit, stat);
        icon.sprite = ResourceLoader.statIcons[stat];
    }
    public void OnPointerEnter(PointerEventData eventdata)
    {
        setIncrement(unit, stat);
    }
    public void OnPointerExit(PointerEventData eventdata)
    {
        hoverIncrement.setBar(0);
    }

    void setBar(string unit, string stat)
    {
        if (Upgrades.statBarFillPercentage[stat] == -1)
        {
            currentStats.setBar(.2f + playerData.Upgrades.upgradesPurchased[unit][stat] * .1f);
        }
        else
        {
            currentStats.setBar((unitData.unitStats[unit][stat] + Upgrades.increment[stat](unitData.unitStats[unit][stat], playerData.Upgrades.upgradesPurchased[unit][stat])) * Upgrades.statBarFillPercentage[stat]);
        }
    }
    void setIncrement(string unit, string stat )
    {
        if (Upgrades.statBarFillPercentage[stat] == -1)
        {
            hoverIncrement.setBar(.2f + (playerData.Upgrades.upgradesPurchased[unit][stat]+1) * .1f);
        }
        else
        {
            hoverIncrement.setBar((unitData.unitStats[unit][stat] + Upgrades.increment[stat](unitData.unitStats[unit][stat], playerData.Upgrades.upgradesPurchased[unit][stat] + 1)) * Upgrades.statBarFillPercentage[stat]);
        }
    }

}