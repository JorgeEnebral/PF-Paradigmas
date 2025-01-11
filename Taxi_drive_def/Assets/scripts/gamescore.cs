using UnityEngine;
using TMPro;  // Usar TextMeshPro

public class GameScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia a la UI TextMeshPro que mostrar� la puntuaci�n
    public int totalScore = 0; // Puntuaci�n total
    public ProgressBarController progressBarController; // Necesitamos acceder a la barra de progreso para obtener la paciencia del pasajero

    void Start()
    {
        // Mostrar la puntuaci�n al iniciar el juego
        UpdateScoreText();
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        // Configurar anclaje a la esquina superior izquierda
        rectTransform.anchorMin = new Vector2(0f, 1f); // Esquina superior izquierda
        rectTransform.anchorMax = new Vector2(0f, 1f); // Igual a anchorMin
        rectTransform.pivot = new Vector2(0f, 1f); // Pivote en la parte superior izquierda

        // Ajustamos el texto para que sea m�s ancho
        rectTransform.anchoredPosition = new Vector2(10f, -10f); // Un peque�o margen de la esquina
        rectTransform.sizeDelta = new Vector2(400f, 100f); // M�s espacio en el ancho (Ajusta este valor seg�n sea necesario)

        // Configurar color del texto
        scoreText.color = new Color32(169, 169, 169, 255); // Gris oscuro
        scoreText.enableWordWrapping = true; 
    }

    public void CompleteJourney(float patience)
    {
        // Calculamos la puntuaci�n por el viaje completado
        int journeyPoints = 200; // 200 puntos por cada viaje completado
        Debug.Log(patience);
        int patiencePoints = (int)(patience * 100); // Paciencia del pasajero multiplicada por 100

        // Sumar los puntos al total
        totalScore += journeyPoints + patiencePoints;

        // Mostrar la puntuaci�n actualizada
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Actualiza el texto de la puntuaci�n en pantalla
        if (scoreText != null)
        {
            scoreText.text = "Puntuaci�n: " + totalScore.ToString();
        }
    }
}
