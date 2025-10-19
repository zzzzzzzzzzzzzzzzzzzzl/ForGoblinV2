using UnityEngine.UI;
using UnityEngine;
using UnityEditor.UI;

public class materialSelectorRow : UIMaterialManager
{
    public Material m;
    public Image image;
    public Button button;

    public void updateMaterial(Material m)
    {
        this.m = m;
        image.material = m;
        button.onClick.AddListener(() =>
        {
            Debug.Log(m.name);
            setMaterial(m);
            foreach(Entity e in Entity.Entites[1])
            {
                e.material = m;
            }
        });

    }
}
