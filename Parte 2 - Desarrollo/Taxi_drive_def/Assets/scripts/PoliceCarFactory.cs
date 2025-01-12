using UnityEngine;

public class PoliceCarFactory : MonoBehaviour
{
    public GameObject policeCarPrefab; // Prefab del coche de polic�a
    public CarController taxi; // Referencia al script del taxi
    public float speedThreshold = 20f; // Velocidad l�mite para activar el coche de polic�a
    private bool isPoliceSpawned = false; // Bandera para evitar m�ltiples apariciones
    private GameObject spawnedPoliceCar; // Referencia al coche de polic�a instanciado
    public PoliceAlertController alertController;
    // Coordenadas fijas para el punto de aparici�n
    private Vector3 policeSpawnPosition = new Vector3(9.59f, 0.14f, -13.2f);

    void Update()
    {
        // Verifica si la velocidad del taxi supera el umbral
        if (taxi.GetSpeed() > speedThreshold && !isPoliceSpawned)
        {
            SpawnPoliceCar(); // Spawn del coche de polic�a
        }
    }

    // M�todo para hacer aparecer el coche de polic�a
    void SpawnPoliceCar()
    {
        // Instanciamos el coche de polic�a en la posici�n fija
        spawnedPoliceCar = Instantiate(policeCarPrefab, policeSpawnPosition, Quaternion.identity);
        if (alertController != null)
        {
            alertController.ShowAlert("�Ha aparecido un coche de polic�a!");
        }
        // Aseg�rate de que el coche de polic�a tenga el componente de persecuci�n
        var policeChase = spawnedPoliceCar.GetComponent<PoliceCarController>();
        if (policeChase != null)
        {
            policeChase.player = taxi.transform; // El objetivo es el taxi
        }

        isPoliceSpawned = true; // Impide que el coche de polic�a se genere m�s de una vez
    }

    // M�todo para eliminar el coche de polic�a cuando el taxi termine el viaje
    public void RemovePoliceCar()
    {
        if (spawnedPoliceCar != null)
        {
            Destroy(spawnedPoliceCar); // Destruye el coche de polic�a
            isPoliceSpawned = false; // Permite que el coche de polic�a se pueda re-aparecer si es necesario
        }
    }
}

