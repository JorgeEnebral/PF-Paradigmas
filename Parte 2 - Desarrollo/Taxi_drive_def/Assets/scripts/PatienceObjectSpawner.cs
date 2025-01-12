using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PatienceObjectSpawner : MonoBehaviour
{
    public GameObject patienceObjectPrefab; // Prefab del objeto que suma paciencia
    public Transform taxi; // Referencia al taxi
    public ProgressBarController progressBarController; // Barra de paciencia
    public RouteGenerator routeGenerator; // Para usar su método de generación de posiciones
    public List<GameObject> activeObjects = new List<GameObject>(); // Lista de objetos activos
    private bool isJourneyActive = false; // Control del estado del viaje
    private float interactionDistance = 5f; // Distancia para recoger un objeto
    public int maxObjects = 4; // Número máximo de objetos simultáneos

    void Update()
    {
        // Comprobamos si el viaje ha comenzado
        if (routeGenerator.journeyStarted)
        {
            // Si el viaje es nuevo y aún no hemos generado los objetos
            if (!isJourneyActive)
            {
                isJourneyActive = true;
                SpawnObjects(); // Generamos los objetos
            }

            // Verificamos las distancias con cada objeto activo
            CheckForCollection();
        }
        else
        {
            // Si el viaje ha terminado, eliminamos todos los objetos activos
            if (isJourneyActive)
            {
                isJourneyActive = false;
                ClearActiveObjects();
            }
        }
    }

    void SpawnObjects()
    {
        // Limpiamos cualquier objeto previo (por seguridad)
        ClearActiveObjects();

        // Generamos los objetos de paciencia en posiciones aleatorias
        for (int i = 0; i < maxObjects; i++)
        {
            Vector3 spawnPosition = routeGenerator.GetRandomPointInArea();
            spawnPosition.y = 1.0f;
            GameObject newObject = Instantiate(patienceObjectPrefab, spawnPosition, Quaternion.identity);
            activeObjects.Add(newObject); // Añadimos el objeto a la lista de objetos activos
        }
    }

    void CheckForCollection()
    {
        for (int i = activeObjects.Count - 1; i >= 0; i--) // Iteramos de atrás hacia adelante
        {
            GameObject obj = activeObjects[i];

            if (obj == null) continue; // Si el objeto ya no existe, pasamos al siguiente

            float distance = Vector3.Distance(taxi.position, obj.transform.position);

            if (distance <= interactionDistance)
            {
                CollectPatienceObject(obj); // Recogemos el objeto
            }
        }
    }

    void CollectPatienceObject(GameObject obj)
    {
        // Aumentamos la paciencia del pasajero
        progressBarController.IncreasePatience(0.3f);

        // Eliminamos el objeto de la escena y de la lista
        activeObjects.Remove(obj);
        Destroy(obj);
    }

    void ClearActiveObjects()
    {
        // Eliminamos y destruimos todos los objetos activos
        foreach (GameObject obj in activeObjects)
        {
            if (obj != null) Destroy(obj);
        }
        activeObjects.Clear(); // Limpiamos la lista
    }
}

