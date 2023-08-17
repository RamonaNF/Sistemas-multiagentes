using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFigure : MonoBehaviour {
    public float timeToMove;
    
    public float initialX;
    public float initialY;
    public float initialZ;
    
    public float targetX;
    public float targetY;
    public float targetZ;
    
    private void OnEnable() { // Propio de MonoBehaviour
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    private void OnDisable() { 
        TimeManager.OnMinuteChanged -= TimeCheck;
    }
    
    private void TimeCheck() {
        if(TimeManager.Minute % 10 == 0) {
            StartCoroutine(Move());
        }
    
    }
    
    private IEnumerator Move() {
        transform.position = new Vector3(initialX,initialY,initialZ);
        Vector3 targetPos = new Vector3(targetX,targetY,targetZ);

        Vector3 currentPos = transform.position;

	float timeElapsed = 0;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            transform.Rotate(0f, 0f, 10f, Space.World);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

    }
}
