using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// X: rango visible de -11 a 11
// Y: rango visible de -3 a 5

public class BossController : MonoBehaviour {
    
    public float type;
    private float frequency = 1f;
    private float amplitude = .5f;
    
    Vector3 pos = new Vector3 ();
    Vector3 auxPos = new Vector3 ();
    
    void Start() {
        pos = transform.position;
    }

    void Update() {
    	auxPos = pos;
    	
    	if(type == 1){ // Up N Down: Player
    	    
    	    if(GameManager.Move == false) {
    	        if(pos.x < -6.32) { //-7.49) { // Acomodo inicial
    	            auxPos.x += .05f;
    	            pos = auxPos;
    	        } else { // Flotando
    	            GameManager.Moving = false;
    	            auxPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
    	        }
    	    
    	        transform.position = auxPos;
    	    } else { // Salida
    	    	if(pos.x < 13.49) {
    	            auxPos.x += .05f;
    	            pos = auxPos;
    	            transform.position = auxPos;
    	        } else { // Fin
    	            GameManager.Moving = true;
    	        }
    	    }
    	    
        } else if(type == 2) { // Rotate: Spaceship
            transform.rotation *= Quaternion.Euler(0f,0f,0.8f);
            
        } else if(type == 3) { // Move: Boss
            if(GameManager.Move == false) {
                if(pos.x > 6.13) { //9.03){ // Acomodo inicial
                    auxPos.x -= .05f;
    	            pos = auxPos;
    	            
    	            transform.position = auxPos;
    	        } else { // No hay movimiento, únicamente su animación
    	            GameManager.Moving = false;
    	        }
    	    } else { // Salida
    	         if(pos.x > -12.52){
    	            auxPos.x -= .05f;
    	            pos = auxPos;
    	            
    	            transform.position = auxPos;
    	         } else { // Fin
    	             GameManager.Moving = true;
    	         }
    	    }
        }
    }
}
