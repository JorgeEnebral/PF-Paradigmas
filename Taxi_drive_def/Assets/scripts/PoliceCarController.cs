using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Para cambiar la escena

public class PoliceCarController : MonoBehaviour
{
    public Transform player; // Referencia al taxi que el coche de polic�a seguir�
    public float catchDistance = 3f; // Distancia m�nima para atrapar al taxi
    public string gameOverMenuSceneName = "GameOverMenu"; // Nombre de la escena del men� al atrapar al jugador
   
    private NavMeshAgent agent; // Componente NavMeshAgent para la navegaci�n

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent

        if (player == null)
        {
            Debug.LogError("No se ha asignado un objetivo (taxi) para el coche de polic�a.");
        }

        

        // Configuraci�n inicial del NavMeshAgent
        agent.speed = 8f; // Velocidad del coche de polic�a
        agent.acceleration = 10f; // Aceleraci�n para maniobras suaves
    }

    void Update()
    {
        if (player != null)
        {
            // Hacer que el coche de polic�a siga al taxi
            agent.SetDestination(player.position);

            // Verificar si el coche de polic�a est� lo suficientemente cerca del taxi
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= catchDistance)
            {
                EndGame(); // Termina el juego si est� a una distancia de atrapar
            }
        }
    }

    private void EndGame()
    {
        Destroy(gameObject);
        Debug.Log("�Te atrap� el coche de polic�a!");
        // Cambiar a la escena del men� Game Over
        SceneManagerScript.LoadDefeatScene();
    }


}


