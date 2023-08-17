
// Librerías básicas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour { // Clase : Hereda
    
    // Cámaras
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey; // Tecla para cambio entre cámaras
    
    public float speed = 5.0f;
    public float forwardInput;
    public float turnSpeed = 0.0f;
    public float horizontalInput;
    
    // Multiplayer
    public string inputId;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
    	forwardInput = Input.GetAxis("Vertical" + inputId);
    	horizontalInput = Input.GetAxis("Horizontal" + inputId);
    
    	transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput); // transform.Translate(0, 0, 1);
    	// Time.deltaTime tiempo transcurrido entre cada llamada al update (frame)
        
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        
        if(Input.GetKeyDown(switchKey)) { // Cambio entre cámaras
	    mainCamera.enabled = !mainCamera.enabled;
	    hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}

