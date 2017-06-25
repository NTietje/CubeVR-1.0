using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------

public class PurpleCube : MonoBehaviour {

	////////////////////////////
	// Alle Variablen

	// Öffentliche Floatvariablen für die Rotationsdauer
	public float duration = 3;

	// Öffentliche Intvariable für das Defenieren der Rotationsachse des Cubes (0 = Z-Achse 1 = X-Achse)
	public int rotationAxis = 0;

	// Öffentliches GameObject das den Mittelpunkt bestimmt worum gedreht wird
	public GameObject center;

	// Rotation
	private float rotation = 90;

	// Vektoren für From To deklarieren (initialiseren hier oben noch nicht möglich)
	private Vector3 frontRotation;
	private Vector3 backRotation;
	private Vector3 leftRotation;
	private Vector3 rightRotation;

	// Bool zum Überprüfen ob die Animation läuft
	private bool isBusy = false;

	// Liste mit allen Cubes die Mitrotieren
	public List <GameObject> interactCubes;

	// Zugriff auf die anderen Scripte
	CubeLocation _cubeLocation;

//-------------------------------------------------------

	////////////////////////////
	// Use this for initialization
	void Start()
	{
		// Alle Children vom lila Cube werden aussortiert, damit nur die die interagierbar sind in der Liste bleiben
		foreach (Transform child in transform)
		{

			if (child.gameObject.tag == "PurpleConnected")
			{

				interactCubes.Add (child.gameObject);

			}

		}

		frontRotation = new Vector3(1, 0, 0);
		backRotation = new Vector3(-1, 0, 0);
		leftRotation = new Vector3(0, 0, 1);
		rightRotation = new Vector3(0, 0, -1);

	}

//-------------------------------------------------------

	////////////////////////////
	// Update is called once per frame
	void Update()
	{

	}

//-------------------------------------------------------

//////////////////////////////////////////////////////////////////////////////////////////
// ==============
// GAMEPLAY FUNKTIONEN
// ==============
//{///////////////////////////////////////////////////////////////////////////////////////

//-------------------------------------------------------

	////////////////////////////
	// Die Funktion wenn die linke Maustaste gedrückt wird
	public void LeftTrigger()
	{

		// Wird geprüft ob der maximale Ziehwert ungleich Null ist
		if (isBusy != true)
		{

			// Switchabfrage für die Position des Cubes + die korrekte Berechnung der neuen Position
			switch (rotationAxis)
			{
			case 0: // FrontRotation
				StartCoroutine (Animate (frontRotation));
				break;

			case 1: // LeftRotation
				StartCoroutine(Animate(leftRotation));
				rotationChangerLeft();
				break;
			}
		}
	}

//-------------------------------------------------------

	////////////////////////////
	// Die Funktion wenn die rechte Maustaste gedrückt wird
	public void RightTrigger()
	{

		// Wird geprüft ob der maximale Drückwert ungleich Null ist
		if (isBusy != true)
		{

			// Switchabfrage für die Position des Cubes + die korrekte Berechnung der neuen Position
			switch (rotationAxis)
			{
			case 0: // BackRotation
				StartCoroutine(Animate(backRotation));
				break;

			case 1: // RightRotation
				StartCoroutine(Animate(rightRotation));
				rotationChangerRight();
				break;
			}
		}
	}

//-------------------------------------------------------

//} ENDE GAMEPLAY FUNKTIONEN

//////////////////////////////////////////////////////////////////////////////////////////
// ==============
// IENUMERATOR / ANIMATION
// ==============
//{///////////////////////////////////////////////////////////////////////////////////////

//-------------------------------------------------------

	////////////////////////////
	// Die Animation der Rotation des Cubes
	IEnumerator Animate(Vector3 fromTo)
	{
		// Timer wird auf Null gesetzt und die Rotationsrate wird berechnet
		float timeSinceStarted = 0f;
		float rate = rotation / duration;

		// Der Timer wird gestartet
		while (timeSinceStarted <= rotation)
		{

			// Solange der Timer nicht zuende ist, ist der Cube "beschäftigt" und kann erstmal nicht weiter betätigt werden. Die Rotation vom Cube für die Animation werden berechnet und umgesetzt 
			isBusy = true;
			timeSinceStarted += Time.deltaTime * rate;
			transform.RotateAround (center.transform.position, fromTo, Time.deltaTime * rate);
			yield return null;

		}

		// Die minimale Differenz wird noch hinzugefügt damit es genau 90 Grad gedreht wurde
		transform.RotateAround (center.transform.position, fromTo, rotation-timeSinceStarted);

		// Nachdem der Timer zuende ist, ist der Cube wieder interagierbar
		isBusy = false;
		yield return new WaitForSeconds(0.1f);

	}

//-------------------------------------------------------

//} ENDE IENUMERATOR / ANIMATION

//////////////////////////////////////////////////////////////////////////////////////////
// ==============
// LOCATIONSÄNDERUNGSFUNKTIONEN
// ==============
//{///////////////////////////////////////////////////////////////////////////////////////

//-------------------------------------------------------

	////////////////////////////
	// Locationänderung wenn nach Links gedreht wird
	public void rotationChangerLeft()
	{

		// Alle Objekte in der Liste werden durchgegangen
		foreach (GameObject loc in interactCubes)
		{

			// Zugriff auf das Script
			_cubeLocation = loc.GetComponentInChildren<CubeLocation> ();

			// Switchabfrage um neue Location zu ermitteln
			switch (_cubeLocation.location)
			{
			case 0:
				_cubeLocation.location = 5;
				break;

			case 1:
				_cubeLocation.location = 4;
				break;

			case 4:
				_cubeLocation.location = 0;
				break;

			case 5:
				_cubeLocation.location = 1;
				break;
			}
		}
	}

//-------------------------------------------------------

	////////////////////////////
	// Locationänderung wenn nach Links gedreht wird
	public void rotationChangerRight()
	{

		foreach (GameObject loc in interactCubes)
		{

			_cubeLocation = loc.GetComponentInChildren<CubeLocation> ();

			switch (_cubeLocation.location)
			{
			case 0:
				_cubeLocation.location = 4;
				break;

			case 1:
				_cubeLocation.location = 5;
				break;

			case 4:
				_cubeLocation.location = 1;
				break;

			case 5:
				_cubeLocation.location = 0;
				break;
			}
		}
	}

//-------------------------------------------------------

	// Weitere 2 Funktionen kommen noch ...

//} ENDE LOCATIONSÄNDERUNGSFUNKTIONEN

}