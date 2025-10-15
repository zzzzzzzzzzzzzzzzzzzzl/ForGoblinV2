using System.Collections;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public int maxMushrooms;
    public static GameObject[] mushroomArray;
    int startingMushroomCount=10;
    int spawnRate = 1;
    float spacing;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        mushroomArray = new GameObject[maxMushrooms];
        spacing = boxCollider2D.bounds.size.x / maxMushrooms;
        StartCoroutine(spawnCD());
        for (int i = 0; i < startingMushroomCount; i++)
        {
            newMushroom();
        }
    }
    void newMushroom()
    {
        int nullCount = mushroomArray.Count(i => i == null) - 1;
        if (nullCount == 0) return;

        int nullIdx = 0;
        int r = UnityEngine.Random.Range(0, nullCount);
        for (int i = 0; i < mushroomArray.Length; i++)
        {
            if (mushroomArray[i] == null)
            {
                if (nullIdx == r)
                {
                    mushroomArray[i] = Instantiate(ResourceLoader.mushroom["mushroom"], new Vector3(boxCollider2D.bounds.min.x + (spacing * i), -4.63f), new quaternion(), transform);
                    break;
                }
                nullIdx++;
            }
        }

    }
    IEnumerator spawnCD()
    {
        yield return new WaitForSeconds(spawnRate );
        // if (UnityEngine.Random.Range(0, 2) == 1)
        // {
        // }
            newMushroom();
        StartCoroutine(spawnCD());
    }
}