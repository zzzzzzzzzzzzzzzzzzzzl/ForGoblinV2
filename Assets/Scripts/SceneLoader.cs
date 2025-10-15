using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void loadScene()
    {
        SceneManager.LoadScene("game");
    }
}