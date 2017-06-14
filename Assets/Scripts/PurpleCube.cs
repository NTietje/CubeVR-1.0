using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------

public class PurpleCube : MonoBehaviour {

	////////////////////////////
	// Alle Variablen

	// Öffentliche Floatvariablen für die Rotationsdauer
	public float duration = 3;

	// Öffentliche Intvariable für das Defenieren der Rotationsrichtung des Cubes (0 = Boden; 1 = Decke; 2 = Vorne; 3 = Hinten)
	public int location = 0;

	// Öffentliches GameObject das den Mittelpunkt bestimmt worum gedreht wird
	public GameObject center;

	// Rotation
	private float rotation = 90;

	//Vektoren für From To deklarieren (initialiseren hier oben noch nicht möglich)
	private Vector3 frontToBack;
	private Vector3 backToFront;
	private Vector3 leftRotation;
	private Vector3 rightRotation;

	// Bool zum Überprüfen ob die Animation läuft
	private bool isBusy = false;

//-------------------------------------------------------

	////////////////////////////
	// Use this for initialization
	void Start()
	{
		frontToBack = new Vector3(1, 0, 0);
		backToFront = new Vector3(-1, 0, 0);
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
			switch (location)
			{
			case 0: // FrontToBack
				StartCoroutine(Animate(frontToBack));
				break;

			case 1: // BackToFront
				StartCoroutine(Animate(backToFront));
				break;

			case 2: // LeftToRight
				StartCoroutine(Animate(leftRotation));
				break;

			case 3: // RightToLeft
				StartCoroutine(Animate(rightRotation));
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
			switch (location)
			{
			case 0: // BackToFront
				StartCoroutine(Animate(backToFront));
				break;

			case 1: // FrontToBack
				StartCoroutine(Animate(frontToBack));
				break;

			case 2: // RightToLeft
				StartCoroutine(Animate(rightRotation));
				break;

			case 3: // LeftToRight
				StartCoroutine(Animate(leftRotation));
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

}