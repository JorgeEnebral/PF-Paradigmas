using UnityEngine;

public class AddMeshColliders : MonoBehaviour
{
    void Start()
    {
        // Encuentra todos los objetos en la escena
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            // Comprueba que el objeto tenga un Renderer (para asegurarse de que es visible) 
            // y que no tenga ya un Collider
            if (obj.GetComponent<Renderer>() != null && obj.GetComponent<Collider>() == null)
            {
                // Intenta añadir un Mesh Collider
                MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
                    meshCollider.convex = false; // Usar mallas normales para estáticos
                }
                else
                {
                    Debug.LogWarning($"El objeto '{obj.name}' no tiene un MeshFilter para generar un MeshCollider.");
                }
            }
        }

        Debug.Log("Mesh Colliders añadidos a todos los objetos aplicables.");
    }
}
