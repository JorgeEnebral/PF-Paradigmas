using UnityEngine;
using TMPro;  // Usar TextMeshPro

public class GameScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia a la UI TextMeshPro que mostrará la puntuación
    public int totalScore = 0; // Puntuación total
    public ProgressBarController progressBarController; // Necesitamos acceder a la barra de progreso para obtener la paciencia del pasajero

    void Start()
    {
        // Mostrar la puntuación al iniciar el juego
        UpdateScoreText();
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        // Configurar anclaje a la esquina superior izquierda
        rectTransform.anchorMin = new Vector2(0f, 1f); // Esquina superior izquierda
        rectTransform.anchorMax = new Vector2(0f, 1f); // Igual a anchorMin
        rectTransform.pivot = new Vector2(0f, 1f); // Pivote en la parte superior izquierda

        // Ajustamos el texto para que sea más ancho
        rectTransform.anchoredPosition = new Vector2(10f, -10f); // Un pequeño margen de la esquina
        rectTransform.sizeDelta = new Vector2(400f, 100f); // Más espacio en el ancho (Ajusta este valor según sea necesario)

        // Configurar color del texto
        scoreText.color = new Color32(169, 169, 169, 255); // Gris oscuro
        scoreText.enableWordWrapping = true; 
    }

    public void CompleteJourney(float patience)
    {
        // Calculamos la puntuación por el viaje completado
        int journeyPoints = 200; // 200 puntos por cada viaje completado
        Debug.Log(patience);
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
