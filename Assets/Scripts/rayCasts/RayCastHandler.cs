using System.Linq;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{

    public static BoxCollider2D hitCollider;
    void getRayCasts()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
        RaycastHit2D target = hits.FirstOrDefault();


        if (target.collider is BoxCollider2D boxCollider && boxCollider.transform.GetComponent<Entity>() is Entity e && e != null)
        {           
            UnitStatsUI.selected = e.unitStatsUI;
        }
        else if(UnitStatsUI.selected!=null)
        {
            UnitStatsUI.selected = null;
        }
    }
    void Update()
    {
        getRayCasts();
    }
}