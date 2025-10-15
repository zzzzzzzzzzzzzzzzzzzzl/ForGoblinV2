using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeUI : UIMaterialManager
{
    Dictionary<string, UpgradeUIRow> rows = new();
    public GameObject layout;
    public GameObject rowPrefab;
    public static UpgradeUI instance;

    protected override void Start()
    {
        base.Start();
        instance = this;
        foreach (string stat in unitData.statNames)
        {
            rows.Add(stat, Instantiate(rowPrefab, layout.transform).GetComponent<UpgradeUIRow>());
            rows[stat].gameObject.SetActive(false);
        }
    }
    public void setRows(string unitName)
    {
        foreach (UpgradeUIRow r in rows.Values)
        {
            r.gameObject.SetActive(false);
        }
        List<string> filteredStats = unitData.unitStats[unitName].Where(i => Upgrades.includeStats[unitName].Contains(i.Key)).Select(i => i.Key).ToList();
        foreach (string stat in filteredStats)
        {
            rows[stat].gameObject.SetActive(true);
            rows[stat].setRow(unitName, stat);
        }
    }
}