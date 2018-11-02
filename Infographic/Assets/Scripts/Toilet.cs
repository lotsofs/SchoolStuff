using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour, IInteractable {

	[SerializeField] AudioSource _audioSource;

	public void Interact() {
		if (!_audioSource.isPlaying) {
			_audioSource.Play();
		}
	}
}
