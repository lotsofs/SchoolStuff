using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] AudioSource audioWrong;
	[SerializeField] AudioSource audioCorrect;
	[SerializeField] AudioSource audioPain;

	[SerializeField] Arena arena;
	[SerializeField] GameObject ui;

	[SerializeField] Text equationText;
	[SerializeField] Text feedbackText;
	[SerializeField] Text hpText;

	[SerializeField] float ringSize;
	[SerializeField] Enemy enemy;
	[SerializeField] CharacterController player;

	[SerializeField] SharedInt health;
	[SerializeField] int defaultHealth = 10;

	[SerializeField] AnswerField answerField;
	List<AnswerField> answerFields;

	int difficultyLevel = 10;
	[SerializeField] ScoreKeeper sk;
	public static GameController instance;

	List<Enemy> enemies = new List<Enemy>();

	void Start () {
		instance = this;
		difficultyLevel = 10;

		health.Value = defaultHealth;

		feedbackText.text = "Welcome!";
		feedbackText.color = Color.blue;

		SpawnEnemies(10);
		GenerateQuestion();
	}

	public void PlayPainfulSound() {
		audioPain.Play();
	}

	private void Update() {
		hpText.text = health.Value.ToString() + " HP";

		if (health.Value <= 0) {
			ResetGame();
		}

	}

	void ResetGame() {
		for (int i = answerFields.Count - 1; i >= 0; i--) {
			Destroy(answerFields[i].gameObject);
			answerFields.RemoveAt(i);
		}

		feedbackText.text = "you died. But you did really well!";
		feedbackText.color = Color.yellow;
		health.Value = defaultHealth;
		player.transform.position = Vector3.zero;

		sk.AddScore(difficultyLevel);
		difficultyLevel = 10;

		KillEnemies();
		SpawnEnemies(11);

		arena.Change();
		GenerateQuestion();
	}

	void GenerateQuestion() {
		Equation eq = new Equation(difficultyLevel);

		string equation = string.Empty;
		equation += eq.values[0].ToString();
		for (int i = 1; i < eq.values.Count; i++) {
			equation += " + ";
			equation += eq.values[i].ToString();
		}
		equation += " = ? ";
		equationText.text = equation;

		CreateAnswerFields(eq);
	}

	/// <summary>
	/// Spawns the answer fields on the lfoor
	/// </summary>
	/// <param name="eq"></param>
	public void CreateAnswerFields(Equation eq) {
		answerFields = new List<AnswerField>();

		Vector2 location = Random.insideUnitCircle;
		location *= ringSize - 4;
		Vector3 location3D = new Vector3(location.x, -1, location.y);
		AnswerField correctAnswerField = Instantiate(answerField, location3D, Quaternion.identity);
		correctAnswerField.Answer = eq.answers[0];
		correctAnswerField.correct = true;
		correctAnswerField.player = player;
		answerFields.Add(correctAnswerField);

		for (int i = 1; i < eq.answers.Count; i++) {
			location = Random.insideUnitCircle;
			location *= ringSize - 4;
			location3D = new Vector3(location.x, -1, location.y);
			AnswerField incorrectAnswerField = Instantiate(answerField, location3D, Quaternion.identity);
			incorrectAnswerField.Answer = eq.answers[i];
			incorrectAnswerField.player = player;

			answerFields.Add(incorrectAnswerField);
		}
	}

	public void Correct(int providedAnswer) {
		audioCorrect.Play();
		float random = UnityEngine.Random.value;
		random *= 10;
		int randomI = Mathf.RoundToInt(random);
		// Motivational messages
		switch (randomI) {
			default:
				feedbackText.text = "Correct!";
				break;
			case 1:
				feedbackText.text = "Correct. You are doing awesome. Keep it up!";
				break;
			case 2:
				feedbackText.text = "WOOOOO YEAH!!";
				break;
			case 3:
				feedbackText.text = "Correct. You are good at this!";
				break;
			case 4:
				feedbackText.text = "Correct. Soon you will be master!";
				break;
		}

		float spawnEnemies = UnityEngine.Random.value;
		if (spawnEnemies < 0.1f) {
			SpawnEnemies(2);
		}
		else if (spawnEnemies > 0.9f) {
			KillEnemies(true);
		}

		difficultyLevel += 10;
		feedbackText.color = Color.green;
		health.Value++;
		for (int i = answerFields.Count - 1; i >= 0; i--) {
			Destroy(answerFields[i].gameObject);
			answerFields.RemoveAt(i);
		}
		GenerateQuestion();
		ui.transform.Rotate(Vector3.up, UnityEngine.Random.value * 360);
	}

	public void Incorrect(int providedAnswer) {
		audioWrong.Play();

		difficultyLevel -= 7;

		string feedback = string.Empty;
		feedback += "Incorrect answer :( You were off by ";
		int difference = int.Parse(answerFields[0].text.text);
		difference = Mathf.Abs(difference - providedAnswer);
		feedback += difference.ToString();
		feedbackText.text = feedback;
		feedbackText.color = Color.red;
		for (int i = answerFields.Count - 1; i >= 0; i--) {
			Destroy(answerFields[i].gameObject);
			answerFields.RemoveAt(i);
		}
		GenerateQuestion();
		ui.transform.Rotate(Vector3.up, UnityEngine.Random.value * 360);

	}

	/// <summary>
	/// Spawn enemies around the ring
	/// </summary>
	/// <param name="amount"></param>
	void SpawnEnemies(int amount) {
		for (int i = 0; i < amount; i++) {
			Vector2 location = Random.insideUnitCircle;
			location = location.normalized;
			location *= ringSize;
			Vector3 location3D = new Vector3(location.x, 0, location.y);
			Enemy newEnemy = Instantiate(enemy, location3D, Quaternion.identity);
			newEnemy.player = player;
			enemies.Add(newEnemy);
		}
	}

	void KillEnemies(bool onlyOne = false) {
		for (int i = enemies.Count - 1; i >= 0; i--) {
			Destroy(enemies[i].gameObject);
			enemies.RemoveAt(i);
			if (onlyOne) {
				break;
			}
		}
	}
}
