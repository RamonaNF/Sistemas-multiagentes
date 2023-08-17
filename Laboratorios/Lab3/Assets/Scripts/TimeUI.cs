using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour {
    
    public TextMeshProUGUI timeText;
    
    private void OnEnable() { // Propio de MonoBehaviour
        // Action += Función
        // Relaciona su respuesta al evento especificado
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    // MonoBehaviour: Parte del ciclo de vida de un componente
    private void OnDisable() { 
        // Action -= Función
        // Desvincula respuesta al evento
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }
    
    private void UpdateTime() { // Actualiza el texto de la interfaz
        // Formato de doble dígito
        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute:00}";
    }
}
