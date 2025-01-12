using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float tiltAmount = 10f; // Cantidad de inclinaci�n m�xima (en grados)
    public float smoothSpeed = 2f; // Velocidad de suavizado de la inclinaci�n
    public float returnSpeed = 5f; // Velocidad con la que vuelve a la posici�n recta (sin giro)

    private float currentTilt = 0f; // Para interpolar la inclinaci�n actual
    private Rigidbody carRB; // Referencia al Rigidbody del coche

    void Start()
    {
        // Obtener la referencia al Rigidbody del coche que es padre de la c�mara
        carRB = transform.root.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Obtenemos el �ngulo de giro a partir de la direcci�n de la velocidad del coche
        float steeringInput = carRB.velocity.x;

        // Calculamos la inclinaci�n deseada en funci�n del giro
        float targetTilt = steeringInput * tiltAmount;

        // Si no hay entrada de direcci�n (steeringInput 0), la c�mara debe regresar r�pido a la posici�n recta
        if (Mathf.Abs(steeringInput) < 0.1f) // Sin giro detectado
        {
            // Recup�ralo r�pido usando un valor de suavizado alto
            currentTilt = Mathf.Lerp(currentTilt, 0f, Time.deltaTime * returnSpeed);
        }
        else
        {
            // Suavizamos la transici�n de inclinaci�n
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * smoothSpeed);
        }

        // Aplicamos la inclinaci�n y mantenemos la rotaci�n del coche
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x, // Mant�n la rotaci�n original en X (hacia adelante)
            transform.eulerAngles.y, // Mant�n la rotaci�n original en Y (hacia la direcci�n del coche)
            -currentTilt            // Aplica la inclinaci�n en Z
        );
    }
}


