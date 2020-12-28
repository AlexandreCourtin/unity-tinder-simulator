using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {

	public GameObject cardObject;
	public Vector2 inputPosition;
	public Vector2 originalInputPosition;
	public Vector2 correctedInputPosition;
	public bool touched;
	public bool touching;

	// CALLED WHEN APP IS STARTING
	void Start() {
		touched = false;
		touching = false;

		// INSTANTIATE FIRST CARD
		GameObject sub = Instantiate(cardObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
		sub.name = "Card";
		CardScript cardScript = sub.GetComponent<CardScript>();
		cardScript.canMove = true;
	}

	// CALLED EVERY TICK
	void Update() {
		// TOUCH SYSTEM
		touched = false;

		if (touch() && !touching) {
			touching = true;
			touched = true;

			// GET INITAL POSITION WHEN FIRST TOUCHING SCREEN
			originalInputPosition = inputPosition;
		} else if (!touch() && touching) {
			touching = false;
		}
		
		// CORRECT POSITION FROM INITAL TOUCH POSITION
		if (touching) {
			correctedInputPosition = new Vector2(
				inputPosition.x - originalInputPosition.x,
				inputPosition.y - originalInputPosition.y
			);
		} else {
			correctedInputPosition = Vector2.zero;
		}
	}

	// TOUCHING IS EITHER REAL TOUCH ON MOBILE OR CLICK ON PC
	private bool touch() {
		if (Input.GetMouseButton(0)) {
			inputPosition = Input.mousePosition;
			return true;
		} else if (Input.touchCount > 0) {
			inputPosition = Input.GetTouch(0).position;
			return true;
		}
		return false;
	}
}
