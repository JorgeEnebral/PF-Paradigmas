using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Para cambiar la escena

public class PoliceCarController : MonoBehaviour
{
    public Transform player; // Referencia al taxi que el coche de policía seguirá
    public float catchDistance = 3f; // Distancia mínima para atrapar al taxi
    public string gameOverMenuSceneName = "GameOverMenu"; // Nombre de la escena del menú al atrapar al jugador
   
    private NavMeshAgent agent; // Componente NavMeshAgent para la navegación

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent

        if (player == null)
        {
            Debug.LogError("No se ha asignado un objetivo (taxi) para el coche de policía.");
        }

        

        // Configuración inicial del NavMeshAgent
        agent.speed = 8f; // Velocidad del coche de policía
        agent.acceleration = 10f; // Aceleración para maniobras suaves
    }

    void Update()
    {
        if (player != null)
        {
            // Hacer que el coche de policía siga al taxi
            agent.SetDestination(player.position);

            // Verificar si el coche de policía está lo suficientemente cerca del taxi
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= catchDistance)
            {
                EndGame(); // Termina el juego si está a una distancia de atrapar
            }
        }
    }

    private void EndGame()
    {
        Destroy(gameObject);
        Debug.Log("¡Te atrapó el coche de policía!");
        // Cambiar a la escena del menú Game Over
        SceneManagerScript.LoadDefeatScene();
    }


}


