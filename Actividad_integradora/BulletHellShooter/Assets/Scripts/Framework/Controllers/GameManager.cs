using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject boss;
    public GameObject spaceship;
    
    public Image playerImg;
    public Image bossImg;
    public Image spaceshipImg;
    
    private float timer;
    private float start;
    private float duration;
    
    private Vector4 color;
    private int step;
    
    public static bool Moving { get; set; }
    public static bool Move { get; set; }
    
    void Start() {
    	timer = 0;
    	start = 1;
    	duration = 10;
    	
    	step = 0;
    	Moving = false;
    	Move = false;
    
        player.SetActive(false);
        boss.SetActive(false);
        spaceship.SetActive(false);
    }

    void Update() {
        if(Moving == false) {
    	    timer += Time.deltaTime;
    	}
    	
    	switch(step) {
            case 0:
            	if(timer >= start) { 
            	    Moving = true;
		    player.SetActive(true);
		    changeColor(playerImg,1f);
		    
		    step += 1;
		    timer = 0;
		}
		break;
		
	    case 1:
            	if(timer >= duration && Move == false && Moving == false) {
		    Move = true;
		} else if(timer >= duration && Move == true && Moving == true) {
		    Move = false;
		    Moving = false;
		    
		    player.SetActive(false);
		    changeColor(playerImg,0.22f);
		    
		    step += 1;
		    timer = 0;
		}
		break;
		
	    case 2:
            	if(timer >= start) {
		    Moving = true;
		    boss.SetActive(true);
	            changeColor(bossImg,1f);
	            
	            step += 1;
	            timer = 0;
		}
		break;
		
	    case 3:
            	if(timer >= duration && Move == false && Moving == false) {
		    Move = true;
		} else if(timer >= duration && Move == true && Moving == true) {
		    Move = false;
		    Moving = false;
		    
		    boss.SetActive(false);
		    changeColor(bossImg,0.22f);
		    
		    step += 1;
		    timer = 0;
		}
		break;
		
	    case 4:
            	if(timer >= start) {
		    spaceship.SetActive(true);
		    changeColor(spaceshipImg,1f);
		    
		    step += 1;
	            timer = 0;
	            
	            Move = true;
		}
		break;
		
	    case 5:
            	if(timer >= duration) {
            	    Move = false;
		    step += 1;
	            timer = 0;
		}
		break;
		
	    case 6:
            	if(timer >= duration) {
		    spaceship.SetActive(false);
		    changeColor(spaceshipImg,0.22f);
		    step += 1;
		    timer = 0;
		}
		break;
		
	    case 7:
	        if(timer >= start) {
		    //Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
		}
		break;
            
            default:
                break;
        }
    }
    
    void changeColor(Image img, float a) {
        color = img.color;
	color.w = a;
	img.color = color;
    }
}
