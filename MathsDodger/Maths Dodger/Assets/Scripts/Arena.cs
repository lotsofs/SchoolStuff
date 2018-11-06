using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

	[SerializeField] Material[] materials;
	Renderer rend;
	[SerializeField] GameObject lightSource;
	[SerializeField] AudioSource musicSource;
	[SerializeField] AudioClip[] musics;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		Change();
	}

	public void Change() {
		
		float rand2 = UnityEngine.Random.value;
		rand2 *= musics.Length;
		rand2 = Mathf.Floor(rand2);
		musicSource.clip = musics[(int)rand2];
		musicSource.Play();
			
		float rand = UnityEngine.Random.value;
		rand *= materials.Length;
		rand = Mathf.Floor(rand);
		rend.material = materials[(int)rand];

		lightSource.transform.rotation = UnityEngine.Random.rotation;
	}
}
