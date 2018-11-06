using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnswerField : MonoBehaviour {

	public Text text;
	[NonSerialized] public bool correct = false;
	[NonSerialized] public CharacterController player;

	public int Answer {
		set {
			text.text = value.ToString();
		}
	}

	private void Start() {
		Renderer renderer = this.GetComponent<Renderer>();
		Material material = renderer.material;
		Color color = UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
		material.color = color;
		text.color = color;
	}

	private void Update() {
		LookAtPlayer();
	}

	/// <summary>
	/// Point at the player
	/// </summary>
	/// <returns></returns>
	Vector3 LookAtPlayer() {
		Vector3 direction = this.transform.position - player.transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.LookRotation(direction);
		return direction;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			if (correct) {
				GameController.instance.Correct(int.Parse(text.text));
			}
			else {
				GameController.instance.Incorrect(int.Parse(text.text));
			}
		}
	}

}
