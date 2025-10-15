using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitPortrait : UIMaterialManager, IPointerEnterHandler, IPointerExitHandler
{
    public static GameObject selectCursor;
    public string unitKey;
    public static UnitPortrait _selected;
    public Button onClick;
    ToolTipAppearOnHover toolTipAppearOnHover;
    public Image unitPortrait;
    public static UnitPortrait selected
    {
        get => _selected;
        set
        {
            _selected = value;
            selectCursor.transform.position = _selected.transform.position;
        }
    }
    protected override void Start()
    {
        base.Start();
        unitPortrait.material = material;
        onClick = gameObject.AddComponent<Button>();
        onClick.onClick.AddListener(() => playerManager.playerManagers[1].spawner.spawnUnit(unitKey));
        
        toolTipAppearOnHover = gameObject.AddComponent<ToolTipAppearOnHover>();
        toolTipAppearOnHover.currencyType = "mushroom";
        toolTipAppearOnHover.cost = unitData.unitStats[unitKey]["cost"];
    }
    public void OnPointerEnter(PointerEventData eventdata)
    {
        playerManager.playerManagers[1].selectedUnit = unitKey;
        selected = this;
    }
    public void OnPointerExit(PointerEventData eventdata)
    {
        selected = this;
    }
}