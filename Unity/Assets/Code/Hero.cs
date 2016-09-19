using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	public GameObject[] upperMouthSprites;
	public GameObject[] lowerMouthSprites;

	public GameObject lowerMouth;
	public Transform upperMouthPoint;
	public Transform cursorHelpPoint;

	Vector3 holdPos;
	Vector3 holdMousePos;
	public bool hold;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hold) {
			var posVec = Input.mousePosition;
			posVec.z = transform.position.z - Camera.main.transform.position.z;
			var target = Camera.main.ScreenToWorldPoint(posVec);

			transform.position = holdPos+target-holdMousePos;
			//transform.position = new Vector3 (target.x-0.2f, target.y-1.5f, transform.position.z);
			Director.Instance.CustomCursor.gameObject.transform.position = new Vector3(cursorHelpPoint.position.x, cursorHelpPoint.position.y,Director.Instance.CustomCursor.gameObject.transform.position.z);
		}
		transform.position = new Vector3(Mathf.Clamp(transform.position.x,-10,-4),Mathf.Clamp(transform.position.y,-10,0),transform.position.z);
		var rotationSpeed = 0.5f;
		var rotation = -new Vector3(0,0,50*rotationSpeed)*(transform.position.x+2)-new Vector3(0,0,220*rotationSpeed);
		rotation = new Vector3(rotation.x,rotation.y,Mathf.Clamp(rotation.z,0,60));

		foreach (var sprite in upperMouthSprites) {
			sprite.SetActive(false);
		}
		foreach (var sprite in lowerMouthSprites) {
			sprite.SetActive(false);
		}

		if (rotation.z >= 0 && rotation.z < 2) {
			upperMouthSprites[0].SetActive(true);
			lowerMouthSprites[0].SetActive(true);
		}
		if (rotation.z >= 2 && rotation.z < 20) {
			upperMouthSprites[1].SetActive(true);
			lowerMouthSprites[1].SetActive(true);
		}
		if (rotation.z >= 20 && rotation.z <= 60) {
			upperMouthSprites[2].SetActive(true);
			lowerMouthSprites[2].SetActive(true);
		}

		if (!hold && rotation.z > 0) {
			transform.position += new Vector3(2/rotationSpeed,0,0)*Time.deltaTime;
		}
		transform.eulerAngles = rotation;
		//transform.eulerAngles += new Vector3(0,0,100)*Time.deltaTime;
		lowerMouth.transform.position = transform.position;
		lowerMouth.transform.eulerAngles = -transform.eulerAngles;

		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel(0);
		}
	}

	void OnMouseEnter() {
		if (!hold) {
			Director.Instance.CustomCursor.ReadyToGrab ();
		}
	}

	void OnMouseExit() {
		if (!hold) {
			Director.Instance.CustomCursor.Point ();
		}
	}

	void OnMouseDown() {
		Director.Instance.CustomCursor.Grab();
		//Director.Instance.CustomCursor.gameObject.transform.parent = transform;
		Director.Instance.Sounds.PlaySound(Director.Instance.Sounds.grabSound);
		holdPos = transform.position;

		var posVec = Input.mousePosition;
		posVec.z = transform.position.z - Camera.main.transform.position.z;
		holdMousePos = Camera.main.ScreenToWorldPoint(posVec);
		cursorHelpPoint.transform.position = Camera.main.ScreenToWorldPoint(posVec);

		hold = true;
	}

	void OnMouseUp() {
		//Director.Instance.CustomCursor.gameObject.transform.parent = transform.parent;
		Director.Instance.CustomCursor.ReadyToGrab();
		hold = false;
	}
}
