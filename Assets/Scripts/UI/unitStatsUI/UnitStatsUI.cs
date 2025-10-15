using System.Collections.Generic;
using UnityEngine;

public class UnitStatsUI : ToolTipAppearOnHover
{
    public GameObject healthBar;
    public Dictionary<string, float> stats;
    public GameObject rowPrefab;
    Dictionary<string, UnitStatsUIRow> rows = new();
    static UnitStatsUI _selected;
    public static UnitStatsUI selected
    {
        get => _selected;
        set
        {
            _selected = value;
            toolTip.instance.showPannel(_selected, value != null);//new value not null// so last value null// selected is being set in the raycastHandler    
        }
    }
    public void init(Dictionary<string, float> stats)
    {
        this.stats = stats;
        pannelType = "unitStats";
    }
    public void updateHealth(float currentHealth, float baseHealth)
    {
        float xScale = currentHealth > 0 ? currentHealth / baseHealth : 0;
        healthBar.transform.localScale = new Vector3(xScale, healthBar.transform.localScale.y, 1);
        rows["health"].TMP.text = $"health:{currentHealth}";
    }
}
