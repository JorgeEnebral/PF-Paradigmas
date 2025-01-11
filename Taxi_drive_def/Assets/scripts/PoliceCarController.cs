using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    public Transform target; // El taxi que el coche de policía seguirá
    private NavMeshAgent agent; // Componente NavMeshAgent para la navegación

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent

        if (target == null)
        {
            Debug.LogError("No se ha asignado un objetivo (taxi) para el coche de policía.");
        }

        // Establecer un poco de velocidad inicial si deseas personalizar
        agent.speed = 8f; // Velocidad del coche de policía
        agent.acceleration = 10f; // Aceleración para maniobrar mejor
    }

    void Update()
    {
        if (target != null)
        {
            // Hacer que el coche de policía siga al taxi
            agent.SetDestination(target.position);
        }
    }
}

