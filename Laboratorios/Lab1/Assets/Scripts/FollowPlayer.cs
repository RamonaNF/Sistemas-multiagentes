
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    
    public GameObject player; // Jugador que se va a seguir
    // Distancia respecto al jugador
    private Vector3 offset = new Vector3(-0.6f, 4, -7.9f); 
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Invocado después del update  
    void LateUpdate() { // Para que la cámara se mueva después del jugador
    	// Para que la cámara no quede en el centro del auto
        transform.position = player.transform.position + offset;
    }
}

