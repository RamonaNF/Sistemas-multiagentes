using System;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    // Action: permite lanzar eventos al juego
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    
    // Cualquiera puede acceder a su valor,
    // pero solo se puede modificar en TimeManager
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 0.5f; // .5s reales = 1min juego
    private float timer; // Intervalo de tiempo

    void Start() { // Called before the first frame update
        Minute = 45;
        Hour = 11;
        timer = minuteToRealTime;
    }

    void Update() { // Called once per frame
        timer -= Time.deltaTime;
        
        // El número de fotogramas equivale al minuteToRealTime
        if(timer <= 0) {
            Minute++;
            OnMinuteChanged?.Invoke();
            
            if(Minute >= 60) {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            } 

            timer = minuteToRealTime;
        }
    }
}
