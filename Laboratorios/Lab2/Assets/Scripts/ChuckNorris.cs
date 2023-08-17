using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Text MeshPro

public class ChuckNorris : MonoBehaviour {
    
    public TextMeshProUGUI jokeText;
    
    public void NewJoke() {
    	Joke j = APIHelper.GetNewJoke();
    	jokeText.text = j.value;
    }
}

