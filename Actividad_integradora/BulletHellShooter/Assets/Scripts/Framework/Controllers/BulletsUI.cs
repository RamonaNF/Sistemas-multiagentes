using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BulletsUI : MonoBehaviour {
    // Action: permite lanzar eventos al juego
    public static Action OnBulletsChanged;
    
    public TextMeshProUGUI bulletsText;
    public static int Bullets { get; set; }
    
    void Start() {
        Bullets = 0;
    }
    
    // OnEnable|OnDisable: Parte del ciclo de vida de un componente
    //                     Propios de MonoBehaviour
    
    private void OnEnable() { 
        // Action += Función
        // Relaciona su respuesta al evento especificado
        OnBulletsChanged += UpdateBullets;
    }
    
    private void OnDisable() { 
        // Action -= Función
        // Desvincula respuesta al evento
        OnBulletsChanged -= UpdateBullets;
    }
    
    // Actualiza el texto de la interfaz
    private void UpdateBullets() { 
        bulletsText.text = $"Bullets: {Bullets.ToString()}";
    }
}

