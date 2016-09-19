using UnityEngine;
using System.Collections;

public class SoundsDatabase : MonoBehaviour {
	public AudioSource musicSource;
	public AudioSource audioSource;
	public AudioClip eatSound;
	public AudioClip eatSound2;
	public AudioClip eatSound3;

	public AudioClip grabSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(AudioClip audioClip, float volumeScale = 1) {
		audioSource.PlayOneShot(audioClip, volumeScale);
	}
}
