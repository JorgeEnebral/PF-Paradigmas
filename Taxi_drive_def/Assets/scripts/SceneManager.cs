using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public static void LoadDefeatScene()
    {
        SceneManager.LoadScene(2);
    }
}
