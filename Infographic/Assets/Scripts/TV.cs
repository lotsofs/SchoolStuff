using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour {

	[SerializeField] TVSequence _blackScreen;
	[SerializeField] TVSequence[] _tvSequences;
	[SerializeField] GameObject _screen;

	[SerializeField] AudioSource _speakers;

	Renderer _screenRenderer;

	private void Start() {
		_screenRenderer = _screen.GetComponent<Renderer>();
	}

	public void StartShow() {
		StartCoroutine(PlayShow());
	}

	public IEnumerator PlayShow() {
		foreach (TVSequence sequence in _tvSequences) {
			_screenRenderer.material = sequence.material;
			_speakers.clip = sequence.audio;
			_speakers.Play();
			while (_speakers.isPlaying) {
				yield return null;
			}
		}
		_screenRenderer.material = _blackScreen.material;
	}

}
