using UnityEngine;
using Pathfinding.Serialization.JsonFx;

public class Sketch : MonoBehaviour {
	public GameObject myPrefab;

	string _WebsiteURL = "http://ldad621.azurewebsites.net/tables/ass3?zumo-api-version=2.0.0"; /*ldad621- azure website- table from swagger uploaded */

	void Start () {

		string jsonResponse = Request.GET(_WebsiteURL);

		//Just in case something went wrong with the request we check the reponse and exit if there is no response.
		if (string.IsNullOrEmpty(jsonResponse))
		{
			return;
		}

		//We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
		Assignment[] Ass3 = JsonReader.Deserialize<Assignment[]>(jsonResponse);

		int totalCubes = Ass3.Length; /*ldad621- definition for length not working at first */ 
		int totalDistance = 5;
		int i = 0;

		/*ldad621- for loop begins here -  */
		foreach (Assignment ass3 in Ass3)
		{
			Debug.Log("This list name is: " + ass3.CardName); /*ldd621 extracting the data from Table "ASS3"- shown at bottom of game screen */
			float perc = i / (float)totalCubes;
			i++;
			float x = perc * totalDistance;
			float y = 5.0f; /*ldad621- position of the cube in game mode on unity */
			float z = 0.0f;
			GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
			newCube.GetComponent<myCubeScript>().setSize((1.0f - perc) * 2);
			newCube.GetComponent<myCubeScript>().ratateSpeed = perc; /*ldad621- creates boxes for each row in my excel table */
			newCube.GetComponentInChildren<TextMesh>().text = ass3.CardName; /*ldad621- Extracting data from cardname */
		}
	}

	void Update () {

	}
} 