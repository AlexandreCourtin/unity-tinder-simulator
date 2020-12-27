using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public float maxX = 1f;
	public float maxY = 1f;
	public float speed = 1f;

	Vector3 currentCamPosition;
	
	void Start() {
		currentCamPosition = transform.position;

		StartCoroutine(ChangePosition());
	}

	void FixedUpdate() {
		transform.position = Vector3.Lerp(transform.position, currentCamPosition, speed);
	}

	IEnumerator ChangePosition() {
		while (true) {
			currentCamPosition.x = Random.Range(-maxX, maxX);
			currentCamPosition.y = Random.Range(-maxY, maxY);
			yield return new WaitForSeconds(.1f);
		}
	}
}
