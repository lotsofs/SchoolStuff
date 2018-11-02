using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] SharedInt collectedCollectibles;
	[SerializeField] Collectible[] collectibles;
	[SerializeField] Text collectiblesText;
	[SerializeField] float hedgeLoweringSpeed;
	[SerializeField] float hedgeLoweringDistance;

	[SerializeField] Material defaultSkybox;
	[SerializeField] Material fancySkybox;
	[SerializeField] GameObject innerHedges;
	[SerializeField] Finish finish;

	const string PickupText = "Golden Spheres Collected: {0} out of {1}";
	const string PickupTextHalfway = "Golden Spheres Collected: {0} out of {1}. \nHalfway point reached,\nNew skybox awarded!";
	const string PickupTextAll = "All Golden Spheres Collected!!! \nHere's a little something!";
	const string WinText = "You beat the game! Well done!\nGolden Spheres collected: {0} out of {1}\nYou may now close the game.";
	int totalCollectibles;
	int halfCollectibles;
	Coroutine uiCoroutine;


	// Use this for initialization
	void Start () {
		foreach (Collectible collectible in collectibles) {
			collectible.OnPickup.AddListener(DisplayUI);
		}
		totalCollectibles = collectibles.Length;
		halfCollectibles = (int)Mathf.Ceil(totalCollectibles / 2);

		collectedCollectibles.Value = 0;

		finish.OnFinish.AddListener(Finish);
	}

	public void DisplayUI() {
		if (uiCoroutine != null) {
			StopCoroutine(uiCoroutine);
		}
		uiCoroutine = StartCoroutine(DisplayUIRoutine());
	}

	public void FancifySky() {
		RenderSettings.skybox = fancySkybox;
	}

	public void Finish() {
		collectiblesText.gameObject.SetActive(true);
		collectiblesText.text = string.Format(WinText, collectedCollectibles.Value, totalCollectibles);
	}

	IEnumerator HedgeLowering() {
		Vector3 hedgePosition = innerHedges.transform.position;
		Vector3 newHedgePosition = hedgePosition;
		newHedgePosition.y -= hedgeLoweringDistance;

		while (hedgePosition.y > newHedgePosition.y) {
			yield return null;
			hedgePosition.y -= Time.deltaTime * hedgeLoweringSpeed;
			innerHedges.transform.position = hedgePosition;
		}
	}

	IEnumerator DisplayUIRoutine() {
		collectiblesText.gameObject.SetActive(true);

		if (collectedCollectibles.Value == halfCollectibles) {
			collectiblesText.text = string.Format(PickupTextHalfway, collectedCollectibles.Value, totalCollectibles);
			FancifySky();
		}
		else if (collectedCollectibles.Value == totalCollectibles) {
			collectiblesText.text = PickupTextAll;
			StartCoroutine(HedgeLowering());
		}
		else {
			collectiblesText.text = string.Format(PickupText, collectedCollectibles.Value, totalCollectibles);
		}

		yield return new WaitForSeconds(2);
		collectiblesText.gameObject.SetActive(false);
		uiCoroutine = null;


	}

}
