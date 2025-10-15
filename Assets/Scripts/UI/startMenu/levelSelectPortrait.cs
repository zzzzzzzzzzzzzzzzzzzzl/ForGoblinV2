using UnityEngine;
using UnityEngine.UI;

public class levelSelectPortrait :UIMaterialManager 
{
    public Image image;
    public Button button;
    protected override void Start()
    {
        base.Start();
        button = gameObject.AddComponent<Button>();
    }
}