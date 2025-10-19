using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class cleanStaticData
{
    public static void clear()
    {
        UIMaterialManager.overlays = new();
        playerManager.playerManagers = new();
        Entity.Entites = new()//team represented by int
    {
        { -1,new()},
        { 0,new()},
        { 1,new()}
    };
    Entity.entitiesDict = new();
    }
}
