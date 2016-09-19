using UnityEngine;
using System.Collections;

public class LevelCode : MonoBehaviour {
	public GameObject projectile;
	public float score;
	public GameObject barPivot;
	int maxScore = 5;
	float startSpawnRate = 2f;
	float spawnRate = 2f;
	bool isInmaxScoreLoop;
	// Use this for initialization
	void Start () {
		StartCoroutine (spawnLoop());
		spawnRate = startSpawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		barPivot.transform.localScale = new Vector3 (Mathf.Clamp(score/(float)maxScore,0,1), barPivot.transform.localScale.y, barPivot.transform.localScale.z);
		if (score >= maxScore && !isInmaxScoreLoop) {
			StartCoroutine(maxScoreLoop());
		}
	}

	IEnumerator maxScoreLoop() {
		isInmaxScoreLoop = true;
		spawnRate = 0.5f;
		while (Director.Instance.Sounds.musicSource.pitch<2) {
			Director.Instance.Sounds.musicSource.pitch += 1.5f * Time.deltaTime;
			yield return null;
		}
		Director.Instance.Sounds.musicSource.pitch = 2;
		Director.Instance.Sounds.PlaySound(Director.Instance.Sounds.eatSound3,1f);
		yield return new WaitForSeconds(10);
		spawnRate = startSpawnRate;
		while (Director.Instance.Sounds.musicSource.pitch>1) {
			Director.Instance.Sounds.musicSource.pitch -= 1.5f * Time.deltaTime;
			yield return null;
		}
		Director.Instance.Sounds.musicSource.pitch = 1;
		score = 0;
		isInmaxScoreLoop = false;
	}

	IEnumerator spawnLoop() {
		while (true) {
			GameObject.Instantiate (projectile,new Vector3(12,-4+2*(Random.Range(0,10)/10f),0),transform.rotation);
			yield return new WaitForSeconds(spawnRate);
		}
	}
}
