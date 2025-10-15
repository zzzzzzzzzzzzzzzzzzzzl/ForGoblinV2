using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
  public static List<string> units = new List<string> { "Magi", "Macer", "Swordsman", "Spearer", "Shielder", "Archer", "Summoner", "Imp", "Healer" };
  public static Dictionary<string, GameObject> unitPrefabs = new();
  public static Dictionary<string, GameObject> UIPrefabs = new();
  public static Dictionary<string, GameObject> mushroom = new();
  public static Dictionary<string, GameObject> fx = new();
  public static Dictionary<string, Sprite> unitPortraits = new();
  public static Dictionary<string, Sprite> statIcons = new();
  public static Dictionary<string, Sprite> currencyIcons = new();
  public static Dictionary<string, Material> materials = new();

  public static bool ResourcesLoaded = false;
  void Awake()
  {
   if(!ResourcesLoaded){ unitPrefabs = loadFolder<GameObject>("units");
    fx = loadFolder<GameObject>("fx");
    UIPrefabs = loadFolder<GameObject>("UIprefabs");
    mushroom = loadFolder<GameObject>("mushroom");
    unitPortraits = loadFolder<Sprite>("unitPortraits");
    statIcons = loadFolder<Sprite>("StatIcons");
    materials = loadFolder<Material>("Materials");}

    ResourcesLoaded = true;
  }

  Dictionary<string, T> loadFolder<T>(string folder)where T:Object
  {
    Dictionary<string, T> d = new();
    foreach (T g in Resources.LoadAll<T>(folder))
    {
      d[g.name] = g;
    }
    return d;
  }
}