using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UnitPortraitLoader : MonoBehaviour
{
    public Dictionary<string, UnitPortrait> unitPortraits = new();
    public GameObject unitPortraitPrefab;
    public GameObject selectCursor;
    public static UnitPortraitLoader instance;
    void Awake()
    {
        instance = this;
        UnitPortrait.selectCursor = selectCursor;
        foreach (Sprite sprite in ResourceLoader.unitPortraits.Values)
        {
            UnitPortrait u = Instantiate(unitPortraitPrefab, transform).GetComponent<UnitPortrait>();
            unitPortraits.Add(sprite.name, u);
            u.unitPortrait.sprite = sprite;
            u.name = sprite.name;
            u.unitKey = sprite.name;
        }
        UnitPortrait.selected = unitPortraits.Values.ToList()[0];
    }
}