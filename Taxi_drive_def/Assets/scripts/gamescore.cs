using UnityEngine;
using TMPro;  // Usar TextMeshPro

public class GameScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia a la UI TextMeshPro que mostrará la puntuación
    private int totalScore = 0; // Puntuación total
    public ProgressBarController progressBarController; // Necesitamos acceder a la barra de progreso para obtener la paciencia del pasajero

    void Start()
    {
        // Mostrar la puntuación al iniciar el juego
        UpdateScoreText();
    }

    public void CompleteJourney(float patience)
    {
        // Calculamos la puntuación por el viaje completado
        int journeyPoints = 200; // 200 puntos por cada viaje completado
        int patiencePoints = (int)(patience * 100); // Paciencia del pasajero multiplicada por 100

        // Sumar los puntos al total
        totalScore += journeyPoints + patiencePoints;

        // Mostrar la puntuación actualizada
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Actualiza el texto de la puntuación en pantalla
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + totalScore.ToString();
        }
    }
}
