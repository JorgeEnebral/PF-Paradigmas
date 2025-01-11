using UnityEngine;

public class PoliceCarFactory : MonoBehaviour
{
    public GameObject policeCarPrefab; // Prefab del coche de policía
    public CarController taxi; // Referencia al script del taxi
    public float speedThreshold = 20f; // Velocidad límite para activar el coche de policía
    private bool isPoliceSpawned = false; // Bandera para evitar múltiples apariciones
    private GameObject spawnedPoliceCar; // Referencia al coche de policía instanciado
    public PoliceAlertController alertController;
    // Coordenadas fijas para el punto de aparición
    private Vector3 policeSpawnPosition = new Vector3(9.59f, 0.14f, -13.2f);

    void Update()
    {
        // Verifica si la velocidad del taxi supera el umbral
        if (taxi.GetSpeed() > speedThreshold && !isPoliceSpawned)
        {
            SpawnPoliceCar(); // Spawn del coche de policía
        }
    }

    // Método para hacer aparecer el coche de policía
    void SpawnPoliceCar()
    {
        // Instanciamos el coche de policía en la posición fija
        spawnedPoliceCar = Instantiate(policeCarPrefab, policeSpawnPosition, Quaternion.identity);
        if (alertController != null)
        {
            alertController.ShowAlert("¡Ha aparecido un coche de policía!");
        }
        // Asegúrate de que el coche de policía tenga el componente de persecución
        var policeChase = spawnedPoliceCar.GetComponent<PoliceCarController>();
        if (policeChase != null)
        {
            policeChase.player = taxi.transform; // El objetivo es el taxi
        }

        isPoliceSpawned = true; // Impide que el coche de policía se genere más de una vez
    }

    // Método para eliminar el coche de policía cuando el taxi termine el viaje
    public void RemovePoliceCar()
    {
        if (spawnedPoliceCar != null)
        {
            Destroy(spawnedPoliceCar); // Destruye el coche de policía
            isPoliceSpawned = false; // Permite que el coche de policía se pueda re-aparecer si es necesario
        }
    }
}

