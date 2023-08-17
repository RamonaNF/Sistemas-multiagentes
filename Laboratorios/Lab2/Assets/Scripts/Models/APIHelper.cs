using UnityEngine; //Para la clase JsonUtility
using System.Net;
using System.IO;

public static class APIHelper {

    public static Joke GetNewJoke() {
    
    	// Creamos el request -> GET
    	// Para POST: https://stackoverflow.com/questions/39246236/how-can-i-post-data-using-httpwebrequest
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://api.chucknorris.io/jokes/random");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse(); // string sin formato

        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd(); 

        return JsonUtility.FromJson<Joke>(json); // Convertir a JSON y llevarlo a nuestra clase (Joke)
    }
}

