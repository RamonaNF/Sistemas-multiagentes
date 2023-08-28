using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletHell3 : MonoBehaviour {
    private float secondsPerShot;
    private float timer;
    
    
    public int bullets = 5;
    public float startAngle = 0, endAngle = 360;
    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    private bool prefab;
    
    List<GameObject> instances = new List<GameObject>();
    
    void Start() {
        prefab = true;
        secondsPerShot = 0.5f;
        timer = secondsPerShot;
    }
    
    void Update() {
    	if(timer <= 0) {
    	    OnFire();
    	    timer = secondsPerShot;
    	    prefab = !prefab;
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
                
                BulletsUI.Bullets -= 1;
                BulletsUI.OnBulletsChanged?.Invoke();
            }
        }
    }
    
    void CreateBullet(Vector2 dir, float speed) {
        GameObject bulletInstance;
        
        if(prefab) {
            bulletInstance = Instantiate(bullet1Prefab, transform.position, Quaternion.identity);
        } else {
            bulletInstance = Instantiate(bullet2Prefab, transform.position, Quaternion.identity);
        }
        
    	BulletMovement bulletMovementInstance = bulletInstance.GetComponent<BulletMovement>();
    	    
    	bulletMovementInstance.speed = speed;
    	bulletMovementInstance.direction = dir;
    	instances.Add(bulletInstance);
    	
    	BulletsUI.Bullets += 1;
    	BulletsUI.OnBulletsChanged?.Invoke();
    }

    void OnFire() {
        float angleStep = (endAngle - startAngle) / bullets;
        float angle = startAngle;
        Vector2 pos = new Vector2();
        Vector2 dir = new Vector2();
    	
        for(int i = 0; i < bullets; i++) {
    	    pos.x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
    	    pos.y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);
    		
    	    dir = (pos - (Vector2)transform.position).normalized;
    	    
    	    if(GameManager.Move == true) {
    	        CreateBullet(dir, 3 + Mathf.Sin(5 * ((angle*Mathf.PI) / 180)));
    	    } else {
    	        CreateBullet(dir, 3 + Mathf.Sin(8 * ((angle*Mathf.PI) / 180)));
    	    }
    	    
    	    angle += angleStep;
    	}
    }
    
    void OnDisable() { 
        for(int i = instances.Count - 1; i >= 0; i--) {
            GameObject obj = instances[i];
            instances.RemoveAt(i);
            Destroy(obj);
                
            BulletsUI.Bullets -= 1;
            BulletsUI.OnBulletsChanged?.Invoke();
        }
    }
}
