using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCube : MonoBehaviour {

	////////////////////////////
	// Alle Variablen

	// Öffentlicher Boolean zum Überprüfen ob der Cube oben ist
	public bool upCube = false;

	// Öffentliche Intvariable für das Defenieren der Position des Cubes (0 = Boden; 1 = Decke; 2 = Vorne; 3 = Hinten; 4 = Links; 5 = Rechts)
	public int location = 0;

	// Die Geschwindigkeit des Cubes + die die Kraft die ausgeübt auf der Kugel
	public float speed = 1f;
	public float jumpForce = 0f;

	// Startposition, Endposition und Distanz
	private Vector3 startPos;
	private Vector3 endPos;
	private float distance = 1f;

	// Bool zum Überprüfen ob die Animation läuft
	private bool isBusy = false;

//-------------------------------------------------------

	////////////////////////////
	// Use this for initialization
	void Start () {

		startPos = transform.position;

	}

//-------------------------------------------------------

	////////////////////////////
	// Update is called once per frame
	void Update () {

	}

//-------------------------------------------------------

//////////////////////////////////////////////////////////////////////////////////////////
// ==============
// GAMEPLAY FUNKTIONEN
// ==============
//{///////////////////////////////////////////////////////////////////////////////////////

//-------------------------------------------------------

	////////////////////////////
	// Die Funktion wenn die rechte oder linke Maustaste gedrückt wird
	public void BlueCubeTrigger () {

		// Wird geprüft ob der Cube oben ist sprich schon einmal aktiviert wurde
		if (upCube != false & isBusy != true) {

			// Switchabfrage für die Position des Cubes + die korrekte Berechnung der neuen Position
			switch (location)
			{
			case 0:
				StartCoroutine (TopToGround ());
				break;

			case 1:
				StartCoroutine (GroundToTop ());
				break;

			case 2:
				StartCoroutine (BackToFront ());
				break;

			case 3:
				StartCoroutine (FrontToBack ());
				break;

			case 4:
				StartCoroutine (RightToLeft ());
				break;

			case 5:
				StartCoroutine (LeftToRight ());
				break;
			}

			upCube = false;

		}
	}
	
//-------------------------------------------------------

//} ENDE GAMEPLAY FUNKTIONEN

//////////////////////////////////////////////////////////////////////////////////////////
// ==============
// COLLISION / TRIGGER FUNKTIONEN
// ==============
//{///////////////////////////////////////////////////////////////////////////////////////

//-------------------------------------------------------

	////////////////////////////
	// Kollisionsabfrage ob die Kugel den aktivierten blauen Cube berührt
	void OnCollisionEnter (Collision col) {
			
		// Wird geprüft ob der Cube oben ist sprich schon einmal aktiviert wurde
		if (upCube != true) {

			// Switchabfrage für die Position des Cubes + die korrekte Berechnung der neuen Position
			switch (location)
			{
			case 0:
				StartCoroutine (GroundToTop ());
				col.rigidbody.AddForce (Vector3.up * jumpForce);
				break;

			case 1:
				StartCoroutine (TopToGround ());
				col.rigidbody.AddForce (-Vector3.up * jumpForce);
				break;

			case 2:
				StartCoroutine (FrontToBack ());
				col.rigidbody.AddForce (-Vector3.back * jumpForce);
				break;

			case 3:
				StartCoroutine (BackToFront ());
				col.rigidbody.AddForce (Vector3.back * jumpForce);
				break;

			case 4:
				StartCoroutine (LeftToRight ());
				col.rigidbody.AddForce (-Vector3.forward * jumpForce);
				break;

			case 5:
				StartCoroutine (RightToLeft ());
				col.rigidbody.AddForce (Vector3.forward * jumpForce);
				break;
			}

			upCube = true;

		}
	}

//-------------------------------------------------------

//} ENDE COLLISION / TRIGGER FUNKTIONEN

//////////////////////////////////////////////////////////////////////////////////////////
// ==============
// IENUMERATOR / ANIMATION
// ==============
//{///////////////////////////////////////////////////////////////////////////////////////

//-------------------------------------------------------

	////////////////////////////
	// Die Animation des Cubes von unten nach oben
	IEnumerator GroundToTop () {

		// Timer wird auf Null gesetzt und die Startposition und Endposition des Cubes wird ermittelt
		float timeSinceStarted = 0f;
		startPos = transform.position;
		endPos = transform.position + new Vector3 (0, distance, 0);

		// Der Timer wird gestartet
		while (timeSinceStarted <= 1f) {

			// Solange der Timer nicht zuende ist, ist der Cube "beschäftigt" und kann erstmal nicht weiter betätigt werden. Die Positionen vom Cube für die Animation werden berechnet und umgesetzt 
			isBusy = true;
			timeSinceStarted += Time.deltaTime * speed;
			transform.position = Vector3.Lerp (startPos, endPos, timeSinceStarted);
			yield return null;

		}

		// Nachdem der Timer zuende ist, ist der Cube wieder interagierbar
		isBusy = false;
		yield return new WaitForSeconds (0.1f);

	}

//-------------------------------------------------------

	////////////////////////////
	// Die Animation des Cubes von oben nach unten
	IEnumerator TopToGround () {

		float timeSinceStarted = 0f;
		startPos = transform.position;
		endPos = transform.position + new Vector3 (0, -distance, 0);

		while (timeSinceStarted <= 1f) {

			isBusy = true;
			timeSinceStarted += Time.deltaTime * speed;
			transform.position = Vector3.Lerp (startPos, endPos, timeSinceStarted);
			yield return null;

		}

		isBusy = false;
		yield return new WaitForSeconds (0.1f);

	}

//-------------------------------------------------------

	////////////////////////////
	// Die Animation des Cubes von vorne nach hinten
	IEnumerator FrontToBack () {

		float timeSinceStarted = 0f;
		startPos = transform.position;
		endPos = transform.position + new Vector3 (0, 0, -distance);

		while (timeSinceStarted <= 1f) {

			isBusy = true;
			timeSinceStarted += Time.deltaTime * speed;
			transform.position = Vector3.Lerp (startPos, endPos, timeSinceStarted);
			yield return null;

		}

		isBusy = false;
		yield return new WaitForSeconds (0.1f);

	}

//-------------------------------------------------------

	////////////////////////////
	// Die Animation des Cubes von hinten nach vorne
	IEnumerator BackToFront () {

		float timeSinceStarted = 0f;
		startPos = transform.position;
		endPos = transform.position + new Vector3 (0, 0, distance);

		while (timeSinceStarted <= 1f) {

			isBusy = true;
			timeSinceStarted += Time.deltaTime * speed;
			transform.position = Vector3.Lerp (startPos, endPos, timeSinceStarted);
			yield return null;

		}

		isBusy = false;
		yield return new WaitForSeconds (0.1f);

	}

//-------------------------------------------------------

	////////////////////////////
	// Die Animation des Cubes von links nach rechts
	IEnumerator LeftToRight () {

		float timeSinceStarted = 0f;
		startPos = transform.position;
		endPos = transform.position + new Vector3 (distance, 0, 0);

		while (timeSinceStarted <= 1f) {

			isBusy = true;
			timeSinceStarted += Time.deltaTime * speed;
			transform.position = Vector3.Lerp (startPos, endPos, timeSinceStarted);
			yield return null;

		}

		isBusy = false;
		yield return new WaitForSeconds (0.1f);

	}

//-------------------------------------------------------

	////////////////////////////
	// Die Animation des Cubes von rechts nach links
	IEnumerator RightToLeft () {

		float timeSinceStarted = 0f;
		startPos = transform.position;
		endPos = transform.position + new Vector3 (-distance, 0, 0);

		while (timeSinceStarted <= 1f) {

			isBusy = true;
			timeSinceStarted += Time.deltaTime * speed;
			transform.position = Vector3.Lerp (startPos, endPos, timeSinceStarted);
			yield return null;

		}

		isBusy = false;
		yield return new WaitForSeconds (0.1f);

	}

//-------------------------------------------------------

//} ENDE IENUMERATOR / ANIMATION

}