using System.Collections.Generic;
using UnityEngine;

public class toolTip : MonoBehaviour
{
    public static toolTip instance;
    public Canvas canvas;
    RectTransform r;
    Vector2 offset = new Vector2(100, 100);
    public CurrencyToolTip cost;
    public UnitStatToolTip unitStats;
    public Dictionary<string, ToolTipBase> pannels = new();
    public ToolTipBase currentPannel;
    void Start()
    {
        pannels.Add("cost", cost);
        pannels.Add("unitStats", unitStats);
        instance = this;
        r = GetComponent<RectTransform>();
    }
    void followCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePos, canvas.worldCamera, out Vector2 pos);
        r.anchoredPosition = pos + offset;
    }
    void Update()
    {
        followCursor();
    }
    public void showPannel(ToolTipAppearOnHover toolTipAppearOnHover, bool setActive)
    {
        gameObject.SetActive(setActive);
        if (toolTipAppearOnHover != null)
        {
            pannels[toolTipAppearOnHover.pannelType].gameObject.SetActive(setActive);
            if (setActive) pannels[toolTipAppearOnHover.pannelType].setPannel(toolTipAppearOnHover);
        }
    }
}

