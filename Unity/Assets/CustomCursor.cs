using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {
	public GameObject[] sprites;
	// Use this for initialization
	void Start () {
		ReadyToGrab ();
	}

	public void Point() {
		foreach (var sprite in sprites) {
			sprite.SetActive(false);
		}
		sprites [0].SetActive(true);
	}

	public void ReadyToGrab() {
		foreach (var sprite in sprites) {
			sprite.SetActive(false);
		}
		sprites [1].SetActive(true);
	}

	public void Grab() {
		foreach (var sprite in sprites) {
			sprite.SetActive(false);
		}
		sprites [2].SetActive(true);
	}

	// Update is called once per frame
	void Update () {
		if (!Director.Instance.Hero.hold) {
			var posVec = Input.mousePosition;
			posVec.z = transform.position.z - Camera.main.transform.position.z;
			var target = Camera.main.ScreenToWorldPoint (posVec);

			transform.position = target;
		}
		Cursor.visible = false;
	}
}
