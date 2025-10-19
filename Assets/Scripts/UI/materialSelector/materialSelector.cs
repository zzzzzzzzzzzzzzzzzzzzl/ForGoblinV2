using UnityEngine;

public class materialSelector : UIMaterialManager
{
    public GameObject rowPrefab;
    public Transform layoutGroup;

    protected override void Start()
    {
        base.Start();
        foreach(Material m in ResourceLoader.materials.Values)
        {
            GameObject row = Instantiate(rowPrefab, layoutGroup);
            row.GetComponent<materialSelectorRow>().updateMaterial(m);
        }
    }
}
