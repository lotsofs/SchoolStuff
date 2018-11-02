using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour {

	[SerializeField] float spinSpeed;

	// Update is called once per frame
	void Update () {
		transform.Rotate(0, Time.deltaTime * spinSpeed, 0);
	}
}
