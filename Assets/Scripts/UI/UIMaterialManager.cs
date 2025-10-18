using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class UIMaterialManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image overlay;
    public static Material material;
    public static List<Image> overlays = new();
    [SerializeField]
    public static void setMaterial(Material m)
    {
        material = m;
        updateOverlays();
        foreach (Entity e in Entity.Entites[1])
        {
            e.material = m;
        }

    }
    bool init = false;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        overlay.material = null;
    }
    protected virtual void Awake()
    {
        if (!init)
        {
            setMaterial(ResourceLoader.materials["UnitColor"]);

            // StartCoroutine(waitForMaterials());
            init = true;
        }
        overlays.Add(overlay);
        if (material != null) overlay.material = material;

    }
    protected virtual void Start()
    {

    }
    public static void updateOverlays()
    {
        foreach (Image o in overlays)
        {
            o.material = material;
        }
    }
}