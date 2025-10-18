using UnityEngine;
using UnityEngine.UI;

public class materialSelector : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform layoutGroup;

    void Start()
    {
        foreach(Material m in ResourceLoader.materials.Values)
        {
            GameObject row = Instantiate(rowPrefab, layoutGroup);
            row.GetComponent<materialSelectorRow>().updateMaterial(m);
        }
    }
}
