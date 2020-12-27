using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public TouchScript touchScript;
    public bool canMove = false;
    public float speed = 1f;
    public float dragPowerX;
    public float dragPowerY;

    void FixedUpdate() {
        if (canMove) {
            Vector2 newPosition = new Vector2(
                touchScript.correctedInputPosition.x * dragPowerX,
                touchScript.correctedInputPosition.y * dragPowerY
            );
            transform.position = Vector2.Lerp(transform.position, newPosition, speed);
            transform.rotation = Quaternion.Euler(0f, 0f, -transform.position.x);
        }
    }
}
