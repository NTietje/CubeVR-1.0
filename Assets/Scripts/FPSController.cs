using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------

public class FPSController : MonoBehaviour {

	////////////////////////////
	// Alle Variablen

	// Öffentliche Floatvariable für Geschwindigkeit und Mausempfindlichkeit
	public float speed = 2f;
	public float sensitivity = 2f;
	public float distanceToCube = 5f;
	CharacterController player;

	// Öffentliches GameObject zum Definieren welche Kamera die Augen sind
	public GameObject eyes;
	GameObject mainCamera;

	// Floatvariable für Vorne-/Rückwehrs- und Links-/Rechtsbewegung
	float moveFB;
	float moveLR;

	// Floatvariable für X und Y Rotation
	float rotX;
	float rotY;

//-------------------------------------------------------

	////////////////////////////
	// Use this for initialization
	void Start () {

		player = GetComponent<CharacterController> ();
		mainCamera = GameObject.FindWithTag ("MainCamera");

	}

//-------------------------------------------------------

	////////////////////////////
	// Update is called once per frame
	void Update () {

		// Berechnung von den Bewegungen mit der Geschwindigkeitsvariable
		moveFB = Input.GetAxis ("Vertical") * speed;
		moveLR = Input.GetAxis ("Horizontal") * speed;

		// Berechnung von der Rotation mit der Mausempfindlichkeitsvariable
		rotX = Input.GetAxis ("Mouse X") * sensitivity;
		rotY = Input.GetAxis ("Mouse Y") * sensitivity;

		// Zusammenführung der Berechnungen von moveLR und moveFB in ein Vector3
		Vector3 movement = new Vector3 (moveLR, 0, moveFB);

		// Zusammenführung der Berechnungen von den Rotationsvariablen. X Rotation auf Boby und Kamera und Y Rotation nur auf Kamera. Y ist negiert damit die Bewegung mit der Maus nicht invertiert ist 
		transform.Rotate (0, rotX, 0);
		eyes.transform.Rotate (-rotY, 0, 0);

		movement = transform.rotation * movement;
		player.Move (movement * Time.deltaTime);

		// Checkt nach ob die linke Maustaste gedrückt wird
		if (Input.GetMouseButtonDown (0)) {

			// Der Mittelpunkt des Bildschirms wird berechnet
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			// Ein Ray wird von der Bildmitte verschickt
			Ray ray = mainCamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;

			// Wird geprüft ob der Ray ein Objekt trifft
			if (Physics.Raycast (ray, out hit, distanceToCube)) {
				
				// Wird geprüft ob der Cube die Farbe rot hat
				if (hit.collider.GetComponent<RedCube> () != null) {
				
					RedCube rc = hit.collider.GetComponent<RedCube> ();
					rc.LeftTrigger ();

				}

					// Wird geprüft ob der Cube die Farbe lila hat
					else if (hit.collider.GetComponent<PurpleCube> () != null) {

							PurpleCube pc = hit.collider.GetComponent<PurpleCube> ();
							pc.LeftTrigger ();
				
					}

						// Wird geprüft ob der Cube die Farbe blau hat
						else if (hit.collider.GetComponent<BlueCube> () != null) {
								
								BlueCube bc = hit.collider.GetComponent<BlueCube> ();
								bc.BlueCubeTrigger ();
				
						}
					
							// Wir geprüft ob der Cube ein Spawnknopf ist
							else if (hit.collider.GetComponent<SpawnButton> () != null) {
									
									SpawnButton sb = hit.collider.GetComponent<SpawnButton> ();
									sb.SpawnButtonPress ();

							}
			}
		}

		// Checkt nach ob die rechte Maustaste gedrückt wird
		if (Input.GetMouseButtonDown (1)) {

			// Der Mittelpunkt des Bildschirms wird berechnet
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			// Ein Ray wird von der Bildmitte verschickt
			Ray ray = mainCamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;

			// Wird geprüft ob der Ray ein Objekt trifft
			if (Physics.Raycast (ray, out hit, distanceToCube)) {

				// Wird geprüft ob der Cube die Farbe rot hat
				if (hit.collider.GetComponent<RedCube> () != null) {

					RedCube rc = hit.collider.GetComponent<RedCube> ();
					rc.RightTrigger ();

				}

					// Wird geprüft ob der Cube die Farbe lila hat
					else if (hit.collider.GetComponent<PurpleCube> () != null) {

							PurpleCube pc = hit.collider.GetComponent<PurpleCube> ();
							pc.RightTrigger ();

					}

						// Wird geprüft ob der Cube die Farbe blau hat
						else if (hit.collider.GetComponent<BlueCube> () != null) {

								BlueCube bc = hit.collider.GetComponent<BlueCube> ();
								bc.BlueCubeTrigger ();

						}

							// Wir geprüft ob der Cube ein Spawnknopf ist
							else if (hit.collider.GetComponent<SpawnButton> () != null) {

									SpawnButton sb = hit.collider.GetComponent<SpawnButton> ();
									sb.SpawnButtonPress ();

							}
			}
		}
	}
}