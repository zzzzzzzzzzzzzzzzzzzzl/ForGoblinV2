using UnityEngine;

public class UIPoint : MonoBehaviour
{
    public static UIPoint point;
    void Awake()
    {
        point = this;
    }
    public void setSize(Vector2 size)
    {
        point.GetComponent<RectTransform>().sizeDelta = size * 1.5f; 
    }
}
