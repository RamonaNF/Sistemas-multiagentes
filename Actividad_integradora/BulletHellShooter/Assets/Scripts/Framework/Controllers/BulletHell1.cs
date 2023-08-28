using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletHell1 : MonoBehaviour {
    private float secondsPerShot;
    private float timer;
    
    public int bullets = 5;
    public float startAngle = 0, endAngle = 360;
    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    
    List<GameObject> instances = new List<GameObject>();
    
    void Start() {
        secondsPerShot = .2f;
        timer = secondsPerShot;
    }
    
    void Update() {
    	if(timer <= 0) {
    	    OnFire();
    	    timer = secondsPerShot;
    	    
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
                
                BulletsUI.Bullets -= 1;
                BulletsUI.OnBulletsChanged?.Invoke();
            }
        }
    }
    
    void CreateBullets(Vector2 dir1, Vector2 dir2) {
        GameObject bulletInstance = Instantiate(bullet1Prefab, transform.position, Quaternion.identity);
    	BulletMovement bulletMovementInstance = bulletInstance.GetComponent<BulletMovement>();
    	
    	bulletMovementInstance.direction = dir1;
    	instances.Add(bulletInstance);
    	
    	bulletInstance = Instantiate(bullet2Prefab, transform.position, Quaternion.identity);
    	bulletMovementInstance = bulletInstance.GetComponent<BulletMovement>();
    	
    	bulletMovementInstance.direction = dir2;
    	instances.Add(bulletInstance);
    	
    	BulletsUI.Bullets += 2;
    	BulletsUI.OnBulletsChanged?.Invoke();
    }

    void OnFire() {
        float angleStep = (endAngle - startAngle) / bullets;
        float angle = startAngle;
        
        Vector2 pos = new Vector2();
        
        Vector2 dir1 = new Vector2();
        Vector2 dir2 = new Vector2();
    	
        for(int i = 0; i < bullets; i++) {
            // 2 per shot (angle - 20)
    	    pos.x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
    	    pos.y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);
    	    dir1 = (pos - (Vector2)transform.position).normalized;
    	    
    	    pos.x = transform.position.x + Mathf.Sin(((angle-20) * -Mathf.PI) / 180);
    	    pos.y = transform.position.y + Mathf.Cos(((angle-20) * -Mathf.PI) / 180);
    	    dir2 = (pos - (Vector2)transform.position).normalized;
    	    
    	    CreateBullets(dir1, dir2);
    	    
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
