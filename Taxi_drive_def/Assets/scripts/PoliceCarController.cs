using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    public Transform target; // El taxi que el coche de polic�a seguir�
    private NavMeshAgent agent; // Componente NavMeshAgent para la navegaci�n

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent

        if (target == null)
        {
            Debug.LogError("No se ha asignado un objetivo (taxi) para el coche de polic�a.");
        }

        // Establecer un poco de velocidad inicial si deseas personalizar
        agent.speed = 8f; // Velocidad del coche de polic�a
        agent.acceleration = 10f; // Aceleraci�n para maniobrar mejor
    }

    void Update()
    {
        if (target != null)
        {
            // Hacer que el coche de polic�a siga al taxi
            agent.SetDestination(target.position);
        }
    }
}

