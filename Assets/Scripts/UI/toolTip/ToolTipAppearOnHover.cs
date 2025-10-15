using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipAppearOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float _cost;
    public string currencyType;
    public string pannelType="cost";
    public Unit unit;
    public float cost
    {
        get => _cost;
        set
        {
            _cost = value;
            if (toolTip.instance != null) toolTip.instance.cost.costText.text = _cost.ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        toolTip.instance.showPannel(this,true);
        // toolTip.instance.cost.costText.text = _cost.ToString();
    }
    public void OnPointerExit(PointerEventData eventdata)
    {
        toolTip.instance.showPannel(this,false);
    }
}