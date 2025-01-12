using UnityEngine;
using TMPro; // Usamos TextMeshPro para el texto

public class PoliceAlertController : MonoBehaviour
{
    public TextMeshProUGUI alertText; // Referencia al TextMeshPro en la UI
    private float alertDuration = 2f; // Duraci�n del mensaje en segundos
    private float alertTimeRemaining = 0f; // Tiempo restante para que desaparezca el mensaje
    private bool isAlertActive = false; // Si el mensaje est� activo o no

    void Start()
    {
        RectTransform rectTransform = alertText.GetComponent<RectTransform>();
        alertText.color = new Color32(169, 169, 169, 255);
        alertText.enableWordWrapping = false;
        alertText.overflowMode = TextOverflowModes.Overflow;
    }

    void Update()
    {
        if (isAlertActive)
        {
            alertTimeRemaining -= Time.deltaTime; 
            if (alertTimeRemaining <= 0)
            {
                HideAlert(); // Oculta el mensaje despu�s de 1 segundo
            }
        }
    }

    public void ShowAlert(string message)
    {
        alertText.text = message; // Muestra el mensaje
        alertText.gameObject.SetActive(true); // Aseg�rate de que el objeto est� visible
        isAlertActive = true;
        alertTimeRemaining = alertDuration; // Reinicia el contador del mensaje
    }

    public void HideAlert()
    {
        alertText.gameObject.SetActive(false); // Oculta el objeto de texto
        isAlertActive = false;
    }
}

