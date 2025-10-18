using UnityEngine;

public class GiaManager : playerManager
{
    protected override void Awake()
    {
        // material = _material;//just to trigger the get set
        playerManagers.Add(0, this);
    }
}