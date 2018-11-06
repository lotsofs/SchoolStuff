using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	Dictionary<string, int> scoreList = new Dictionary<string, int>();

	Dictionary<int, string> inverseScoreList = new Dictionary<int, string>();

	[SerializeField] Text scoreText;

	int playCount = 1;

	// Use this for initialization
	void Start () {
		scoreList.Add("Steve", 54);
		scoreList.Add("John", 49);
		scoreList.Add("Mary", 119);
		scoreList.Add("Albert", 14);

		inverseScoreList.Add(54, "Steve");
		inverseScoreList.Add(49, "John");
		inverseScoreList.Add(119, "Mary");
		inverseScoreList.Add(14, "Albert");

		WriteScores();
	}

	void WriteScores() {
		List<int> scores = new List<int>();
		foreach (int value in scoreList.Values) {
			scores.Add(value);
		}
		scores.Sort();

		string scoreTable = string.Empty;
		for (int i = scores.Count - 1; i >= 0; i--) {
			scoreTable += string.Format("{0}: {1} - {2}\n",
				i + 1,
				inverseScoreList[scores[i]],
				scores[i]
			);
		}

		scoreText.text = scoreTable;
	}

	public void AddScore(int score) {
		if (!inverseScoreList.ContainsKey(score)) {
			scoreList.Add(Environment.UserName + playCount.ToString(), score);
			inverseScoreList.Add(score, Environment.UserName + playCount.ToString());
		}
		playCount++;
		WriteScores();
	}
}
