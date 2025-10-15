using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image bar;
    public Image backGround;
    public Color barColor;
    public Color backGroundColor;
    void Start()
    {
        backGround.color = backGroundColor;
        bar.color = barColor;
    }
    public void setBar(float p)
    {
        bar.rectTransform.anchoredPosition = new Vector2(backGround.rectTransform.rect.width + (p-2)*backGround.rectTransform.rect.width, 0);
    }
}