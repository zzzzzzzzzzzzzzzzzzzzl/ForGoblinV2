using System.Collections.Generic;
using UnityEngine;

public class VerticalGameObjectLayout : MonoBehaviour
{
    public float rowSize;
    int childCountChange = 0;
    public void updateLayout()
    {
        int idx = 0;
        foreach (Transform child in transform)
        {
            child.position = transform.position + new Vector3(0,- idx * rowSize, 0);
            idx++;
        }
    }
    void Update()
    {
        if (transform.childCount > childCountChange)
        {
            childCountChange = transform.childCount;
            updateLayout();
        }
    }

}