using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatToolTip : ToolTipBase
{

    public GameObject rowPrefab;
    public GameObject layout;
    protected override void Start()
    {
        base.Start();
        InitPannel();
    }

    public Dictionary<string, UnitStatToolTipRow> rows = new();
    public Dictionary<string, GameObject> background = new();

    public override void setPannel(ToolTipAppearOnHover data)
    {
        foreach (string s in rows.Keys)
        {
            rows[s].gameObject.SetActive(false);
            background[s].gameObject.SetActive(false);
        }
        if (data is UnitStatsUI unitStatsData)
        {
            foreach (KeyValuePair<string, float> kvp in unitStatsData.stats)
            {
            if (kvp.Key != "slowDuration" && kvp.Key != "forageRange" && kvp.Key != "summonAttackSpeed" && kvp.Key != "summonSpeed"&& kvp.Key != "cost")
                {
                    rows[kvp.Key].statValue = kvp.Value;
                    rows[kvp.Key].gameObject.SetActive(kvp.Value > 0);
                    background[kvp.Key].gameObject.SetActive(kvp.Value > 0);

                }
            }
        }
    }
    public void InitPannel()
    {
        background.Add("t", top);
        overlays.Add(top.GetComponent<Image>());
        foreach (string stat in unitData.statNames)
        {
            if (stat != "slowDuration" && stat != "forageRange" && stat != "summonAttackSpeed" && stat != "summonSpeed"&& stat != "cost")
            {
                UnitStatToolTipRow row = Instantiate(rowPrefab, layout.transform).GetComponent<UnitStatToolTipRow>();
                rows.Add(stat, row);
                row.name = stat;
                row.statValue = 0;
                row.icon.sprite = ResourceLoader.statIcons[stat];
                background.Add(stat, Instantiate(middle));
                overlays.Add(background[stat].GetComponent<Image>());
            }
        }
        background.Add("b", bottom);
        overlays.Add(bottom.GetComponent<Image>());
        updateOverlays();

        Destroy(middle);

        foreach (string s in background.Keys)
        {
            background[s].transform.parent = backgroundLayout.transform;
            background[s].transform.localScale = new Vector3(1, 1, 1);
        }
        gameObject.SetActive(false);
    }
    public GameObject backgroundLayout;
    public GameObject top;
    public GameObject middle;
    public GameObject bottom;
    public void setBackGround()
    {

    }
}