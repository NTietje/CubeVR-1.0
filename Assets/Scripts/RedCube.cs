using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------

public class RedCube : MonoBehaviour
{

    ////////////////////////////
    // Alle Variablen

    // Öffentliche Floatvariablen für das maximale Ziehen und Drücken des Cubes + die Geschwindigkeit + die Kraft die ausgeübt auf der Kugel
    public float maxPull = 0;
    public float maxPush = 0;
    public float speed = 1f;
    public float force = 0f;

    // Öffentliche Intvariable für das Defenieren der Position des Cubes (0 = Boden; 1 = Decke; 2 = Vorne; 3 = Hinten; 4 = Links; 5 = Rechts)
    public int location = 0;

    // Bool zum Prüfen ob der Cube allgemein eine Kollision mit Objekten haben soll
    public bool haveCollision = false;

    // Startposition, Endposition und Distanz
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance = 1f;

    //Vektoren für From To deklarieren (initialiseren hier oben noch nicht möglich)
    private Vector3 groundToTop;
    private Vector3 topToGround;
    private Vector3 frontToBack;
    private Vector3 backToFront;
    private Vector3 leftToRight;
    private Vector3 rightToLeft;


    // Bool zum Überprüfen ob die Animation läuft und ob die Kollision aktiv oder inaktiv ist
    private bool isBusy = false;
    private bool collisionOn = false;

    //-------------------------------------------------------

    ////////////////////////////
    // Use this for initialization
    void Start()
    {

        startPos = transform.position;

        // initialiserie Vektoren
        groundToTop = new Vector3(0, distance, 0);
        topToGround = new Vector3(0, -distance, 0);
        frontToBack = new Vector3(0, 0, -distance);
        backToFront = new Vector3(0, 0, distance);
        leftToRight = new Vector3(distance, 0, 0);
        rightToLeft = new Vector3(-distance, 0, 0);

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
        if (maxPull != 0 & isBusy != true)
        {

            // Die Kollision mit der Kugel ist nun aktiv
            collisionOn = true;

            // Switchabfrage für die Position des Cubes + die korrekte Berechnung der neuen Position
            switch (location)
            {
                case 0: // GroundToTop
                    StartCoroutine(Animate(groundToTop));
                    break;

                case 1: // TopToGround
                    StartCoroutine(Animate(topToGround));
                    break;

                case 2: // FrontToBack
                    StartCoroutine(Animate(frontToBack));
                    break;

                case 3: // BackToFront
                    StartCoroutine(Animate(backToFront));
                    break;

                case 4: // LeftToRight
                    StartCoroutine(Animate(leftToRight));
                    break;

                case 5: // RightToLeft
                    StartCoroutine(Animate(rightToLeft));
                    break;
            }

            --maxPull;
            ++maxPush;

        }
    }

    //-------------------------------------------------------

    ////////////////////////////
    // Die Funktion wenn die rechte Maustaste gedrückt wird
    public void RightTrigger()
    {

        // Wird geprüft ob der maximale Drückwert ungleich Null ist
        if (maxPush != 0 & isBusy != true)
        {

            // Switchabfrage für die Position des Cubes + die korrekte Berechnung der neuen Position
            switch (location)
            {
                case 0: // TopToGround
                    StartCoroutine(Animate(topToGround));
                    break;

                case 1: // GroundToTop
                    StartCoroutine(Animate(groundToTop));
                    break;

                case 2: // BackToFront
                    StartCoroutine(Animate(backToFront));
                    break;

                case 3: // FrontToBack
                    StartCoroutine(Animate(frontToBack));
                    break;

                case 4: // RightToLeft
                    StartCoroutine(Animate(rightToLeft));
                    break;

                case 5: // LeftToRight
                    StartCoroutine(Animate(leftToRight));
                    break;
            }

            ++maxPull;
            --maxPush;

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
    // Die Kollisionsabfrage mit der Kugel
    void OnCollisionEnter(Collision col)
    {

        // Wird geprüft ob der Cube überhaupt eine Kollision haben darf und ob diese aktiv ist
        if (collisionOn != false && haveCollision != false)
        {

            // Switchabfrage für die Position des Cubes
            switch (location)
            {
                case 0:
                    col.rigidbody.AddForce(Vector3.up * force);
                    break;

                case 1:
                    col.rigidbody.AddForce(-Vector3.up * force);
                    break;

                case 2:
                    col.rigidbody.AddForce(-Vector3.back * force);
                    break;

                case 3:
                    col.rigidbody.AddForce(Vector3.back * force);
                    break;

                case 4:
                    col.rigidbody.AddForce(-Vector3.left * force);
                    break;

                case 5:
                    col.rigidbody.AddForce(Vector3.left * force);
                    break;
            }
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
    // Die Animation des Cubes von unten nach oben, der Vektor fromTo gibt an, wo die Animation anfängt/ wo sie aufhört
    IEnumerator Animate(Vector3 fromTo)
    {
        // Timer wird auf Null gesetzt und die Startposition und Endposition des Cubes wird ermittelt
        float timeSinceStarted = 0f;
        startPos = transform.position;
        endPos = transform.position + fromTo;

        // Der Timer wird gestartet
        while (timeSinceStarted <= 1f)
        {

            // Solange der Timer nicht zuende ist, ist der Cube "beschäftigt" und kann erstmal nicht weiter betätigt werden. Die Positionen vom Cube für die Animation werden berechnet und umgesetzt 
            isBusy = true;
            timeSinceStarted += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, endPos, timeSinceStarted);
            yield return null;

        }

        // Nachdem der Timer zuende ist, ist der Cube wieder interagierbar und die Kollision ist deaktiviert
        isBusy = false;
        yield return new WaitForSeconds(0.1f);
        collisionOn = false;

    }


    //} ENDE IENUMERATOR / ANIMATION
}