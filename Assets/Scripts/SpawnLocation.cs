using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour {

	public GameObject preFab;
	GameObject instance;
	GameObject instance2;
	bool isAlive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawn() {

		if (isAlive == false) {

			instance = (GameObject)Instantiate(preFab, transform.position, Quaternion.identity);
			isAlive = true;
			Destroy (instance2);

		} else {

			instance2 = (GameObject)Instantiate(preFab, transform.position, Quaternion.identity);
			Destroy (instance);
			isAlive = false;

		}
	}
}
