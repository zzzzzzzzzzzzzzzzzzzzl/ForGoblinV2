using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatToolTipRow : UIMaterialManager
{
    public Image icon;
    float _statValue;

    public float statValue
    {
        get => _statValue; set
        {
            _statValue = value;
            setText();
        }
    }
    string _name;
    public new string name { get => _name; set { _name = value; } }
    public TextMeshProUGUI text;
    protected override void Start()
    {
        base.Start();
        icon.material = material;
    }
    public void setText()
    {
        text.text = $"{name.ToUpper()} : {statValue}";
    }
}