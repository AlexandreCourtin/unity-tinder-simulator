using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {

    public Vector2 inputPosition;
    public Vector2 originalInputPosition;
    public Vector2 correctedInputPosition;
    public bool touched;
    public bool touching;

    void Start() {
        touched = false;
        touching = false;
    }

    void Update() {
        // TOUCH SYSTEM
		touched = false;

		if (touch() && !touching) {
			touching = true;
			touched = true;
		}
		else if (!touch() && touching) {
			touching = false;
		}
        
        // CARD SYSTEM
        if (touched) {
            originalInputPosition = inputPosition;
        }

        if (touching) {
            correctedInputPosition = new Vector2(
                inputPosition.x - originalInputPosition.x,
                inputPosition.y - originalInputPosition.y
            );
        } else {
            correctedInputPosition = Vector2.zero;
        }
    }

    private bool touch() {
		if (Input.GetMouseButton(0)) {
            inputPosition = Input.mousePosition;
			return true;
		} else {
            for (int i = 0; i < Input.touchCount; i++) {
                inputPosition = Input.GetTouch(0).position;
                return true;
            }
        }
		return false;
	}
}
