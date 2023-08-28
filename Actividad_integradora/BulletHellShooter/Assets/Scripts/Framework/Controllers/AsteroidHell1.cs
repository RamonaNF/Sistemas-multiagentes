using System.Collections.Generic;
using UnityEngine;
using System;

public class AsteroidHell1 : MonoBehaviour {
    public float secondsPerMove;
    private float timer;
    
    public int asteroids = 5;
    public float startAngle = 0, endAngle = 180;
    public GameObject asteroidPrefab;
    
    List<GameObject> instances = new List<GameObject>();
    
    void Start() {
        timer = secondsPerMove;
    }
    
    void Update() {
    	if(timer <= 0) {
    	    OnFire();
    	    timer = secondsPerMove;
    	    
    	    startAngle += 10;
    	    endAngle += 10;
    	}
    	
    	timer -= Time.deltaTime;
    	
    	for(int i = instances.Count - 1; i >= 0; i--) {
            if (instances[i].transform.position.x >= 12.4  ||
            instances[i].transform.position.x <= -12.4  ||
            instances[i].transform.position.y >= 6.4  ||
            instances[i].transform.position.y <= -4.4) {
            	GameObject obj = instances[i];
                instances.RemoveAt(i);
                Destroy(obj);
            }
        }
    }
    
    void CreateAsteroid(Vector2 dir) {
        GameObject asteroidInstance = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
    	BulletMovement bulletMovementInstance = asteroidInstance.GetComponent<BulletMovement>();
    	    
    	bulletMovementInstance.direction = dir;
    	instances.Add(asteroidInstance);
    }

    void OnFire() {
        float angleStep = (endAngle - startAngle) / asteroids;
        float angle = startAngle;
        
        Vector2 pos = new Vector2();
        Vector2 dir1 = new Vector2();
    	
        for(int i = 0; i < asteroids; i++) {
            pos.x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
    	    pos.y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);
    	    dir1 = (pos - (Vector2)transform.position).normalized;
    	    CreateAsteroid(dir1);
    	    
    	    pos.x = transform.position.x + Mathf.Sin(((angle - 20) * Mathf.PI) / 180);
    	    pos.y = transform.position.y + Mathf.Cos(((angle - 20) * Mathf.PI) / 180);
    	    dir1 = (pos - (Vector2)transform.position).normalized;
    	    CreateAsteroid(dir1);
    	    
    	    angle += angleStep;
    	}
    }
    
    void OnDisable() { 
        for(int i = instances.Count - 1; i >= 0; i--) {
            GameObject obj = instances[i];
            instances.RemoveAt(i);
            Destroy(obj);
        }
    }
}
