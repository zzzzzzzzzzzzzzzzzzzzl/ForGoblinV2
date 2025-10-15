using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshPro text;
    float _damage;
    public float damage
    {
        get => _damage; set
        {
            _damage = value;
            init(_damage);
        }
    }
    public void init(float damage)
    {
        if (damage < 0) text.color = Color.red;
        else text.color = Color.green;
        string plus =damage<0? "+":"";
        text.text = $"{plus}{(int)damage}";
    }
    void fadeOut()
    {
        if (text.color.a < 0) Destroy(gameObject);
        text.color = new Color(text.color.r, text.color.b, text.color.g, text.color.a - .001f);
        transform.position += new Vector3(0, 0.001f);
    }
    void Update()
    {
        fadeOut();
        
    }
}