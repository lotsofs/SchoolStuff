using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {

	[SerializeField] Vector3 _openPosition;
	[SerializeField] Vector3 _closedPosition;
	[SerializeField] float _movementTime;
	[SerializeField] bool _interactEnabled = true;
	float _speed;

	public void Start() {
		float distance = Vector3.Distance(_openPosition, _closedPosition);
		transform.localPosition = _closedPosition;
		_speed = distance / _movementTime;
	}

	public void Interact() {
		if (_interactEnabled == false) {
			Debug.Log("Door not enabled");
			return;
		}
		StartCoroutine(DoorOpen());
	}

	IEnumerator DoorOpen() {
		_interactEnabled = false;
		float time = 0;
		while (time < _movementTime) {
			time += Time.deltaTime;
			float step = _speed * Time.deltaTime;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, _openPosition, step);
			yield return null;
		}
		transform.localPosition = _openPosition;
	}
}
