using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

	[NonSerialized] public Vector3 direction;
	float age = 0;
	[SerializeField] float lifeSpan = 4;
	[SerializeField] float projectileSpeed = 14;

	[SerializeField] SharedInt health;

	void Update () {
		age += Time.deltaTime;
		if (age >= lifeSpan) {
			Destroy(this.gameObject);
		}

		this.transform.position += direction * Time.deltaTime * projectileSpeed;
	}

	private void OnTriggerEnter(Collider other) {
		GameController.instance.PlayPainfulSound();
		if (other.gameObject.tag == "player") {
			Destroy(this.gameObject);
			health.Value--;
		}
	}

}
