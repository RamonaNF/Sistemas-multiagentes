using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    public float speed = 3;
    public Vector2 direction;
    
    void Update() {
    	transform.Translate( direction * speed * Time.deltaTime);
    }
}
