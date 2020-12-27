using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {
    public GameObject cardObject;
    public TouchScript touchScript;
    public CardScript subCard;
    public Vector3 newPosition;
    public bool canMove;
    public float speed = 1f;
    public float dragPowerX;
    public float dragPowerY;

    bool createdSubCard;

    // CALLED WHEN CARD IS SPAWNED
    void Start() {
        touchScript = GameObject.Find("Camera").GetComponent<TouchScript>();
        newPosition = transform.position;
        createdSubCard = false;
    }

    // CALLED EVERY TICK
    void Update() {
        if (touchScript.touched && !createdSubCard && canMove) {
            createdSubCard = true;
            GameObject sub = Instantiate(cardObject, new Vector3(0f, 0f, 5f), Quaternion.identity);
            sub.name = "Card";
            subCard = sub.GetComponent<CardScript>();
            subCard.canMove = false;
        }
    }

    // CALLED AT A FIXED TIME (every x second)
    void FixedUpdate() {
        if (transform.position.x < -1f && canMove && !touchScript.touching) {
            newPosition = new Vector3(-10f, 0f, 0f);
            newCardStepForward();
        } else if (transform.position.x > 1f && canMove && !touchScript.touching) {
            newPosition = new Vector3(10f, 0f, 0f);
            newCardStepForward();
        } else if (canMove) {
            newPosition = new Vector3(
                touchScript.correctedInputPosition.x * dragPowerX,
                touchScript.correctedInputPosition.y * dragPowerY,
                0f
            );
        }

        // Destroy object when reaching border
        if (transform.position.x < -9 || transform.position.x > 9) {
            Destroy(this.gameObject);
        }

        // Always update position and rotation with a Lerp for a smooth effect
        transform.position = Vector3.Lerp(transform.position, newPosition, speed);
        transform.rotation = Quaternion.Euler(0f, 0f, -transform.position.x);
    }

    // CALLED WHEN DISLIKED OR LIKED
    // DISABLE MOVEMENT AND CALL SUB CARD TO BE THE MAIN CARD
    private void newCardStepForward() {
        canMove = false;
        subCard.canMove = true;
        subCard.newPosition = Vector3.zero;
    }
}
