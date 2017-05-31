using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------

public class SpawnButton : MonoBehaviour {

	////////////////////////////
	// Alle Variablen

	public SpawnLocation _SpawnLocation;

//-------------------------------------------------------

	////////////////////////////
	// Use this for initialization
	void Start () {

	}

//-------------------------------------------------------

	////////////////////////////
	// Update is called once per frame
	void Update () {

	}

//-------------------------------------------------------

	////////////////////////////
	// Funktion die die Funktion Spawn im anderen Script auslöst
	public void SpawnButtonPress () {
	
		_SpawnLocation.Spawn ();

	}
}
