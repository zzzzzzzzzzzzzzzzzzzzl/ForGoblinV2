using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIMaterialManager : MonoBehaviour
{
    public Image overlay;
    public static Material material;
    public static List<Image> overlays = new();
    [SerializeField]
    public static void setMaterial()
    {
        Debug.Log(ResourceLoader.materials.Count);
        material = ResourceLoader.materials["UnitColor"];
    }
    IEnumerator waitForMaterials()
    {
        yield return new WaitUntil(() => ResourceLoader.materials.Count > 0);
        setMaterial();
        updateOverlays();
    }
    bool init = false;
    protected virtual void Start()
    {
        if (!init)
        {
            StartCoroutine(waitForMaterials());
            init = true;
        }
        Debug.Log(material);
        overlays.Add(overlay);
        if (material != null) overlay.material = material;

    }
    public static void updateOverlays()
    {
        foreach (Image o in overlays)
        {
            o.material = material;
        }
    }
}