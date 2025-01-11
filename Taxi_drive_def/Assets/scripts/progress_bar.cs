using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;  // La barra de progreso en la UI
    public float currentProgress; // El progreso actual (paciencia del pasajero)
 

    private bool isDecreasing = false; // Determina si la paciencia debería disminuir

    // Se debe llamar para empezar a disminuir la paciencia
    public void StartDecreasingPatience()
    {
        isDecreasing = true;
    }

    // Se debe llamar cuando el taxi llega al destino para resetear la barra de paciencia
    public void ResetPatience()
    {
        isDecreasing = false;
        currentProgress = 1f; // Resetear al máximo
        progressBar.value = currentProgress;
    }

    // Función para disminuir la paciencia con el paso del tiempo
    public void DecreasePatienceOverTime(float amount)
    {
        if (isDecreasing && currentProgress > 0f)
        {
            // Decrementamos el valor de la barra de paciencia con el paso del tiempo
            currentProgress -= amount;

            // Aseguramos que no baje de 0
            currentProgress = Mathf.Clamp(currentProgress, 0f, 1f);

            // Actualizamos el valor de la barra
            progressBar.value = currentProgress;
        }
    }

    void Update()
    {
        if (!isDecreasing)
        {
            progressBar.value = currentProgress;
        }
    }
}