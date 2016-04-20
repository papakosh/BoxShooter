﻿using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
	public float zMaxRange = 25.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn

	private float nextSpawnTime;

	// Use this for initialization
	void Start ()
	{
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime) {
			// Spawn the game object through function below
			MakeThingToSpawn ();

			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}	
	}

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

		GameObject[] spawnBeacon = GameObject.FindGameObjectsWithTag ("SpawnBeacon");

		// determine which object to spawn
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		//Debug.Log ("object to spawn number = " + objectToSpawn);
		if (objectToSpawn == 0 && spawnBeacon.Length > 0) {
			
			int spawnBeaconChosen = Random.Range (0, 2); 
			//Debug.Log ("ground enemy chosen")
			if (spawnBeaconChosen == 0) {
				spawnPosition.x = spawnBeacon[0].transform.position.x;
				spawnPosition.y = spawnBeacon[0].transform.position.y;
				spawnPosition.z = spawnBeacon[0].transform.position.z;
			} else {
				spawnPosition.x = spawnBeacon[1].transform.position.x;
				spawnPosition.y = spawnBeacon[1].transform.position.y;
				spawnPosition.z = spawnBeacon[1].transform.position.z;
			}
		} else {
			// get a random position between the specified ranges
			spawnPosition.x = Random.Range (xMinRange, xMaxRange);
			spawnPosition.y = Random.Range (yMinRange, yMaxRange);
			spawnPosition.z = Random.Range (zMinRange, zMaxRange);
		}


		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;
	}
}
