using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	[SerializeField] Camera _camera;
	[SerializeField] LayerMask _layerMask;
	[SerializeField] float _useDistance;

	// Update is called once per frame
	void Update () {
		Use();
	}

	/// <summary>
	/// Upon pressing E the object player is looking at is used. Also it's hardcoded.
	/// </summary>
	void Use() {
		if (Input.GetKeyDown(KeyCode.E)) {
			RaycastHit hit;
			if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward), out hit, _useDistance, _layerMask)) {
				IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
				if (interactable != null) {
					interactable.Interact();
				}
				else {
					Debug.Log("Can't use that " + hit.transform);
				}
			}
			else {
				Debug.Log("Nothing used");
			}
		}
	}
}
