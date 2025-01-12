using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject gameManagerPrefab;
    private void Awake()
    {
        // Asegurar que solo exista un GameManager (Singleton)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Instanciar el Prefab si no existe
        if (gameManagerPrefab != null && Instance == this)
        {
            Instantiate(gameManagerPrefab);
        }
    }
    public void MainMenuToGame()
    {
        Debug.Log("Entrando al juego");
        SceneManagerScript.LoadGameScene(); // Cargar la escena 0
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Saliendo al menú principal");
        SceneManagerScript.LoadMainMenu(); // Cargar la escena 2
    }
}
