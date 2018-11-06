using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] SharedInt health;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -2) {
			GameController.instance.PlayPainfulSound();
			health.Value /= 2;
			transform.position = Vector3.zero;
		}
	}
}
