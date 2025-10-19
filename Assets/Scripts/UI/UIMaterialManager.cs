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


    }
   public static  bool init = false;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (UIPoint.point != null)
        {
            UIPoint.point.gameObject.SetActive(true);
            UIPoint.point.transform.position = overlay.transform.position;
            UIPoint.point.setSize(overlay.rectTransform.sizeDelta);
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (UIPoint.point != null)
        {
            UIPoint.point.gameObject.SetActive(false);
        }
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
            // o.material = material;
        }
    }
}