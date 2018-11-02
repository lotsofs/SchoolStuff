using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour {

	[SerializeField] SharedInt collected;

	[HideInInspector] public UnityEvent OnPickup;

	private void OnTriggerEnter(Collider other) {
		collected.Value++;
		Destroy(this.gameObject);
		OnPickup.Invoke();
	}

}
