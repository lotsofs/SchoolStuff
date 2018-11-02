using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable {

	public void Interact() {
		Debug.Log("Quitting :)");
		Application.Quit();
	}

}
