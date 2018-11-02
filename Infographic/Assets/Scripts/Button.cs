using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable {

	[SerializeField] Vector3 _pressPosition;
	[SerializeField] Vector3 _unpressPosition;
	[SerializeField] float _movementTime;
	[SerializeField] float _waitTime;

	[SerializeField] UnityEvent _buttonAction;
	[SerializeField] bool _interactEnabled = true;

	float _speed;

	public void Start() {
		float distance = Vector3.Distance(_pressPosition, _unpressPosition);
		transform.localPosition = _unpressPosition;
		_speed = distance / _movementTime;
	}

	public void Interact() {
		if (_interactEnabled == false) {
			Debug.Log("Button not enabled");
			return;
		}
		StartCoroutine(ButtonPress());
		Debug.Log("Button pressed");
	}

	IEnumerator ButtonPress() {
		_interactEnabled = false;
		_buttonAction.Invoke();
		float time = 0;
		while (time < _movementTime) {
			time += Time.deltaTime;
			float step = _speed * Time.deltaTime;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, _pressPosition, step);
			yield return null;
		}
		transform.localPosition = _pressPosition;
		yield return new WaitForSeconds(_waitTime);
		StartCoroutine(ButtonUnpress());
	}

	IEnumerator ButtonUnpress() {
		float time = 0;
		while (time < _movementTime) {
			time += Time.deltaTime;
			float step = _speed * Time.deltaTime;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, _unpressPosition, step);
			yield return null;
		}
		transform.localPosition = _unpressPosition;
		_interactEnabled = true;
	}
}
