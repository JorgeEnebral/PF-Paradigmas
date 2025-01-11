using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float tiltAmount = 10f; // Cantidad de inclinación máxima (en grados)
    public float smoothSpeed = 2f; // Velocidad de suavizado de la inclinación
    public float returnSpeed = 5f; // Velocidad con la que vuelve a la posición recta (sin giro)

    private float currentTilt = 0f; // Para interpolar la inclinación actual
    private Rigidbody carRB; // Referencia al Rigidbody del coche

    void Start()
    {
        // Obtener la referencia al Rigidbody del coche que es padre de la cámara
        carRB = transform.root.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Obtenemos el ángulo de giro a partir de la dirección de la velocidad del coche
        float steeringInput = carRB.velocity.x;

        // Calculamos la inclinación deseada en función del giro
        float targetTilt = steeringInput * tiltAmount;

        // Si no hay entrada de dirección (steeringInput 0), la cámara debe regresar rápido a la posición recta
        if (Mathf.Abs(steeringInput) < 0.1f) // Sin giro detectado
        {
            // Recupéralo rápido usando un valor de suavizado alto
            currentTilt = Mathf.Lerp(currentTilt, 0f, Time.deltaTime * returnSpeed);
        }
        else
        {
            // Suavizamos la transición de inclinación
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * smoothSpeed);
        }

        // Aplicamos la inclinación y mantenemos la rotación del coche
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x, // Mantén la rotación original en X (hacia adelante)
            transform.eulerAngles.y, // Mantén la rotación original en Y (hacia la dirección del coche)
            -currentTilt            // Aplica la inclinación en Z
        );
    }
}


