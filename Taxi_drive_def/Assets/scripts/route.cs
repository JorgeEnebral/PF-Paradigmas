//using UnityEngine;
//using UnityEngine.AI;

//public class RouteGenerator : MonoBehaviour
//{
//    public Vector3 roadAreaMin = new Vector3(-81.2f, 1f, -75.5f); // Coordenada mínima de la zona (esquina inferior)
//    public Vector3 roadAreaMax = new Vector3(103.6f, 1f, 137.6f); // Coordenada máxima de la zona (esquina superior)
//    public GameObject minimapRoute; // Objeto que representa la ruta en el minimapa

//    // Prefabs para los bloques de inicio (recogida) y destino
//    public GameObject pickupMarkerPrefab;
//    public GameObject destinationMarkerPrefab;

//    public Vector3[] routePoints; // Puntos para la ruta: [0] inicio, [1] destino
//    private LineRenderer lineRenderer;
//    private bool isPickingUp = true; // Determina si es momento de recoger el pasajero o transportarlo

//    private GameObject pickupMarker; // Bloque visual para la recogida del pasajero
//    private GameObject destinationMarker; // Bloque visual para el destino

//    public string minimapLayerName = "MinimapLayer"; // Nombre de la capa para el minimapa

//    void Start()
//    {
//        // Añadimos el componente LineRenderer si no lo tiene
//        lineRenderer = minimapRoute.GetComponent<LineRenderer>();
//        if (lineRenderer == null)
//            lineRenderer = minimapRoute.AddComponent<LineRenderer>();

//        lineRenderer.startWidth = 2f; // Grosor de la línea
//        lineRenderer.endWidth = 2f;
//        lineRenderer.positionCount = 2; // Solo 2 puntos (inicio y destino)

//        // Aseguramos que el LineRenderer esté en la capa del minimapa
//        minimapRoute.layer = LayerMask.NameToLayer(minimapLayerName);

//        // Llamamos a GenerateRoute para crear la primera ruta
//        GenerateRoute();
//    }

//    void Update()
//    {
//        // Si el coche llega a la posición de destino, comenzamos la siguiente ruta
//        if (Vector3.Distance(transform.position, routePoints[1]) < 5f)
//        {
//            isPickingUp = false;  // Ya no estamos recogiendo, ahora estamos transportando al pasajero
//            GenerateRoute(); // Generamos una nueva ruta
//        }

//        // Cambiar el color de la ruta según si estamos recogiendo al pasajero o transportándolo
//        if (isPickingUp)
//        {
//            lineRenderer.startColor = Color.green;
//            lineRenderer.endColor = Color.green;
//        }
//        else
//        {
//            lineRenderer.startColor = Color.blue;
//            lineRenderer.endColor = Color.blue;
//        }
//    }

//    void GenerateRoute()
//    {
//        routePoints = new Vector3[2]; // Dos puntos, el de inicio y el de destino

//        // Generamos el punto de inicio (donde el pasajero es recogido)
//        routePoints[0] = GetRandomPointInArea();

//        // Generamos el punto de destino (donde transportamos al pasajero)
//        routePoints[1] = GetRandomPointInArea();

//        // Si ya existe un bloque de recogida o destino, los destruimos
//        if (pickupMarker != null) Destroy(pickupMarker);
//        if (destinationMarker != null) Destroy(destinationMarker);

//        // Creamos un marcador verde para la recogida
//        pickupMarker = Instantiate(pickupMarkerPrefab, routePoints[0], Quaternion.identity);
//        pickupMarker.layer = LayerMask.NameToLayer(minimapLayerName); // Poner en la capa del minimapa

//        // Creamos un marcador rojo para el destino
//        destinationMarker = Instantiate(destinationMarkerPrefab, routePoints[1], Quaternion.identity);
//        destinationMarker.layer = LayerMask.NameToLayer(minimapLayerName); // Poner en la capa del minimapa

//        // Dibujamos la ruta en el minimapa
//        UpdateMinimapRoute(); // Actualizamos la ruta en el minimapa con LineRenderer
//    }

//    // Función que genera una posición aleatoria dentro del área de la carretera
//    Vector3 GetRandomPointInArea()
//    {
//        float x = Random.Range(roadAreaMin.x, roadAreaMax.x);
//        float z = Random.Range(roadAreaMin.z, roadAreaMax.z);  // La componente Y se mantiene constante para trabajar en 2D

//        Vector3 randomPos = new Vector3(x, 1.5f, z); // Posición en 3D (y = 1.5f, altura típica de la carretera)

//        // Validamos que la posición está en un área válida del NavMesh
//        NavMeshHit hit;
//        if (NavMesh.SamplePosition(randomPos, out hit, 2.0f, NavMesh.AllAreas))
//        {
//            return hit.position; // Si es válido, devolvemos la posición en el NavMesh
//        }
//        else
//        {
//            // Si no encontramos un punto válido, probamos de nuevo
//            return GetRandomPointInArea();
//        }
//    }

//    // Función para actualizar la ruta visualizada en el minimapa
//    void UpdateMinimapRoute()
//    {
//        if (lineRenderer == null)
//        {
//            Debug.LogWarning("No se encontró el componente LineRenderer en minimapRoute");
//            return;
//        }

//        // Aseguramos que el LineRenderer esté en la capa del minimapa
//        lineRenderer.gameObject.layer = LayerMask.NameToLayer(minimapLayerName);

//        // Establecemos la cantidad de puntos del LineRenderer
//        lineRenderer.positionCount = routePoints.Length;

//        // Asignamos las posiciones de los puntos generados al LineRenderer
//        lineRenderer.SetPosition(0, routePoints[0]);
//        lineRenderer.SetPosition(1, routePoints[1]);
//    }
//}

using UnityEngine;
using UnityEngine.AI;

public class RouteGenerator : MonoBehaviour
{
    public Vector3 roadAreaMin = new Vector3(-81.2f, 1f, -75.5f); // Coordenada mínima de la zona (esquina inferior)
    public Vector3 roadAreaMax = new Vector3(103.6f, 1f, 137.6f); // Coordenada máxima de la zona (esquina superior)
    public GameObject minimapRoute; // Objeto que representa la ruta en el minimapa

    // Prefabs para los bloques de inicio (recogida) y destino
    public GameObject pickupMarkerPrefab;
    public GameObject destinationMarkerPrefab;

    public Vector3[] routePoints; // Puntos para la ruta: [0] inicio, [1] destino
    private LineRenderer lineRenderer;
    private Material lineMaterial;
    public bool isPickingUp = true; // Determina si es momento de recoger el pasajero o transportarlo
    public bool journeyStarted = false;

    private GameObject pickupMarker; // Bloque visual para la recogida del pasajero
    private GameObject destinationMarker; // Bloque visual para el destino

    public string minimapLayerName = "MinimapLayer"; // Nombre de la capa para el minimapa
    public GameObject taxi;

    public ProgressBarController progressBarController;
    private float timeSpentInJourney = 0f;

    public GameScoreController gameScoreController;

    void Start()
    {
        // Añadimos el componente LineRenderer si no lo tiene
        lineRenderer = minimapRoute.GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = minimapRoute.AddComponent<LineRenderer>();

        lineMaterial = new Material(Shader.Find("Standard"));
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 2f; // Grosor de la línea
        lineRenderer.endWidth = 2f;
        lineRenderer.positionCount = 2; // Solo 2 puntos (inicio y destino)

        // Aseguramos que el LineRenderer esté en la capa del minimapa
        minimapRoute.layer = LayerMask.NameToLayer(minimapLayerName);

        // Llamamos a GenerateRoute para crear la primera ruta
        GenerateRoute();

        progressBarController.ResetPatience();

    }

    void Update()
    {
        Vector3 taxiPosition = taxi.transform.position;
        // Si el coche llega a la posición de inicio, comenzamos la siguiente ruta
        if (!journeyStarted && Vector3.Distance(taxiPosition, routePoints[0]) < 5f)
        {
            // El taxi ha llegado al pasajero, ahora comienza el viaje
            journeyStarted = true;
            isPickingUp = false;
            progressBarController.StartDecreasingPatience();
            // Cambiar la ruta al destino final (mostrado en verde)
            lineMaterial.color = Color.green;
        }

        // Cuando lleguemos al destino final
        if (journeyStarted && Vector3.Distance(taxiPosition, routePoints[1]) < 5f)
        {
            // El taxi llegó al destino final
            Debug.Log("Taxi ha llegado a destino.");
            Destroy(destinationMarker); // Eliminar marcador de destino
            progressBarController.ResetPatience();
            float patienceRemaining = progressBarController.progressBar.value;
            gameScoreController.CompleteJourney(patienceRemaining);
            GenerateRoute(); // Crear la siguiente ruta (recogida + destino)
        }

        // Comportamiento del taxi dependiendo de la etapa del viaje
        if (isPickingUp && !journeyStarted)
        {
            // Ruta de recogida en color azul
            lineMaterial.color = Color.blue;
            lineRenderer.SetPosition(0, taxiPosition); // El taxi al principio
            lineRenderer.SetPosition(1, routePoints[0]); // La posición de recogida
        }

        if (journeyStarted)
        {
            // Aumenta el tiempo en que el pasajero está viajando (en segundos)
            timeSpentInJourney += Time.deltaTime;
            lineRenderer.SetPosition(0, taxiPosition); // El taxi al principio
            lineRenderer.SetPosition(1, routePoints[1]); // La posición de recogida

            // Asegura que la barra de paciencia siga decreciendo, basada en el tiempo
            if (progressBarController.progressBar.value > 0f)
            {
                // En lugar de calcular la distancia, solo se reduce la paciencia basado en el tiempo
                float patienceDecayRate = 0.03f;  // 0.1 de paciencia por segundo (puedes ajustar esta velocidad)
                progressBarController.DecreasePatienceOverTime(patienceDecayRate * Time.deltaTime);
            }
        }

    }

    void GenerateRoute()
    {
        routePoints = new Vector3[2]; // Dos puntos, el de inicio y el de destino

        // Generamos el punto de inicio (donde el pasajero es recogido)
        routePoints[0] = GetRandomPointInArea();

        // Generamos el punto de destino (donde transportamos al pasajero)
        routePoints[1] = GetRandomPointInArea();

        // Si ya existe un bloque de recogida o destino, los destruimos
        if (pickupMarker != null) Destroy(pickupMarker);
        if (destinationMarker != null) Destroy(destinationMarker);

        // Creamos un marcador verde para la recogida
        pickupMarker = Instantiate(pickupMarkerPrefab, routePoints[0], Quaternion.identity);
        pickupMarker.layer = LayerMask.NameToLayer(minimapLayerName); // Poner en la capa del minimapa

        // Creamos un marcador rojo para el destino
        destinationMarker = Instantiate(destinationMarkerPrefab, routePoints[1], Quaternion.identity);
        destinationMarker.layer = LayerMask.NameToLayer(minimapLayerName); // Poner en la capa del minimapa

        // Dibujamos la ruta en el minimapa
        //UpdateMinimapRoute(); // Actualizamos la ruta en el minimapa con LineRenderer

        // Resetear estados de viaje
        isPickingUp = true; // Empezamos con la recogida
        journeyStarted = false; // El viaje no ha comenzado aún
    }

    // Función que genera una posición aleatoria dentro del área de la carretera
    Vector3 GetRandomPointInArea()
    {
        float x = Random.Range(roadAreaMin.x, roadAreaMax.x);
        float z = Random.Range(roadAreaMin.z, roadAreaMax.z);  // La componente Y se mantiene constante para trabajar en 2D

        Vector3 randomPos = new Vector3(x, 1.5f, z); // Posición en 3D (y = 1.5f, altura típica de la carretera)

        // Validamos que la posición está en un área válida del NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 2.0f, NavMesh.AllAreas))
        {
            return hit.position; // Si es válido, devolvemos la posición en el NavMesh
        }
        else
        {
            // Si no encontramos un punto válido, probamos de nuevo
            return GetRandomPointInArea();
        }
    }
}

