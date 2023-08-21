using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permite moverse a través del mapa y buscar llegar al objetivo
public class GeneticPathFinder : MonoBehaviour {
    public float creatureSpeed;
    public float pathMultiplier; // Optimización de genes: Para que el algoritmo no se estanque
    int pathIndex = 0;
    public DNA dna; // Conjunto de genes
    public bool hasFinished = false; // Se ha llegado a la meta
    bool hasBeenInitialized = false;
    Vector2 target;
    Vector2 nextPoint;
    
    public float fitness {
        get {
            float dist = Vector2.Distance(transform.position,target);
            if(dist == 0) {
                dist = 0.0001f;
            }
        return 60/dist;
        }
    }

    public void InitCreature(DNA newDna, Vector2 _target) {
        dna = newDna;
        target = _target;
        nextPoint = transform.position;
        hasBeenInitialized = true;    
    }
    
    private void Update() {
        if(hasBeenInitialized && !hasFinished){
            if(pathIndex == dna.genes.Count || Vector2.Distance(transform.position,target)<0.5f) {
                hasFinished = true;
            }
            if((Vector2)transform.position == nextPoint) {
                nextPoint = (Vector2)transform.position + dna.genes[pathIndex] * pathMultiplier;
                pathIndex++;
            } else {
                transform.position = Vector2.MoveTowards(transform.position,nextPoint,creatureSpeed*Time.deltaTime);
            }
        }
    }
    
    /* private void Start() {
        InitCreature(new DNA(),Vector2.zero);
    } */
}
