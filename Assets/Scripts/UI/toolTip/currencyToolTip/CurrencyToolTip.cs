using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyToolTip : ToolTipBase
{
    public GameObject mushroomSprite;
    public GameObject crystalSprite;
    public TextMeshProUGUI costText;
    Dictionary<string, GameObject> currencyIcons = new();
    protected override void Start()
    {
        base.Start();
        currencyIcons.Add("mushroom", mushroomSprite);
        currencyIcons.Add("crystal", crystalSprite);
    }
    void setCurrencyIcon(string currencyType)
    {
        foreach (KeyValuePair<string, GameObject> kvp in currencyIcons)
        {
            bool isactive = kvp.Key == currencyType;
            kvp.Value.SetActive(isactive);
        }
    }
    public override void setPannel(ToolTipAppearOnHover data)
    {
        setCurrencyIcon(data.currencyType);
        costText.text = data.cost.ToString();
    }
}