using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {
	
	public GameObject cardObject;
	public TouchScript touchScript;
	public CardScript subCard;
	public Vector3 newPosition;
	public Color[] faceColorDictionnary;
	public Color[] hatColorDictionnary;
	public GameObject[] hatObjectDictionnary;
	public GameObject[] beardObjectDictionnary;
	public SpriteRenderer faceSprite;
	public bool canMove;
	public float speed = 1f;
	public float dragPowerX;
	public float dragPowerY;

	bool createdSubCard;

	// NAMES THAT WILL BE CHOSEN WHEN PICKING RANDOM NAME
	string[] nameDictionnary = {
		"Alex", "Andy", "Eva", "Francis", "Andréa", "Marion", "Guilhem", "Yvette",
		"Romain", "Charly", "Nicolas", "Camille", "Gabriel", "Guy", "Jean", "Malvic",
		"Claude", "Delphine", "Laura", "Laure", "Eliott", "Melba", "Aïka", "Lara",
		"Léa"
	};

	// CALLED WHEN CARD IS SPAWNED
	void Start() {
		touchScript = GameObject.Find("Camera").GetComponent<TouchScript>();
		newPosition = transform.position;
		createdSubCard = false;

		// RANDOM NAME ATTRIBUTION
		int randName = Random.Range(0, nameDictionnary.Length);
		GetComponentInChildren<TextMesh>().text = nameDictionnary[randName];

		// RANDOM FACE COLOR
		int randFaceColor = Random.Range(0, faceColorDictionnary.Length);
		faceSprite.color = faceColorDictionnary[randFaceColor];

		// RANDOM HAT OBJECT
		int randhatObject = Random.Range(0, hatObjectDictionnary.Length);
		for (int i = 0 ; i < hatObjectDictionnary.Length ; i++) {
			if (i == randhatObject) {
				hatObjectDictionnary[i].gameObject.active = true;
			} else {
				hatObjectDictionnary[i].gameObject.active = false;
			}
		}

		// RANDOM BEARD OBJECT
		int randbeardObject = Random.Range(0, beardObjectDictionnary.Length);
		for (int i = 0 ; i < beardObjectDictionnary.Length ; i++) {
			if (i == randbeardObject) {
				beardObjectDictionnary[i].gameObject.active = true;
			} else {
				beardObjectDictionnary[i].gameObject.active = false;
			}
		}

		// RANDOM HAT COLOR
		int randHatColor = Random.Range(0, hatColorDictionnary.Length);
		SpriteRenderer[] hatRenderers = hatObjectDictionnary[randhatObject].GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer hatRenderer in hatRenderers) {
			hatRenderer.color = hatColorDictionnary[randHatColor];
		}

		// RANDOM BEARD COLOR
		int randBeardColor = Random.Range(0, hatColorDictionnary.Length);
		SpriteRenderer[] beardRenderers = beardObjectDictionnary[randbeardObject].GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer beardRenderer in beardRenderers) {
			beardRenderer.color = hatColorDictionnary[randBeardColor];
		}
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
		// DESTROY OBJECT WHEN REACHING BORDER
		if (transform.position.x < -5f || transform.position.x > 5f) {
			Destroy(this.gameObject);
		}
		// SWIPE LEFT
		else if (transform.position.x < -1f && canMove && !touchScript.touching) {
			newPosition = new Vector3(-6f, 0f, 0f);
			newCardStepForward();
		}
		// SWIPE RIGHT
		else if (transform.position.x > 1f && canMove && !touchScript.touching) {
			newPosition = new Vector3(6f, 0f, 0f);
			newCardStepForward();
		}
		// UPDATE POSITION WITH TOUCHSCRIPT
		else if (canMove) {
			newPosition = new Vector3(
				touchScript.correctedInputPosition.x * dragPowerX,
				touchScript.correctedInputPosition.y * dragPowerY,
				0f
			);
		}

		// ALWAYS UPDATE POSITION AND ROTATION WITH A LERP FOR A SMOOTH EFFECT
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
