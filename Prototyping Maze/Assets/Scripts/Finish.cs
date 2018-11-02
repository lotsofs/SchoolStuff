using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour {

	[SerializeField] GameObject teleporter;
	[HideInInspector] public UnityEvent OnFinish;

	private void OnTriggerEnter(Collider other) {
		other.transform.position = teleporter.transform.position;
		OnFinish.Invoke();
	}
}
