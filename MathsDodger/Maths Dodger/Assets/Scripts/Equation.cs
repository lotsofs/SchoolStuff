using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation {

	public enum Types {
		Plus,
		Minus,
		Multiply,
		Divide
	}

	public List<int> answers;
	public List<int> values;
	public Types type;

	public Equation(int difficulty) {
		type = Types.Plus;
		values = new List<int> {
			(int)(Random.value * difficulty) ,
			(int)(Random.value * difficulty) ,
		};
		answers = new List<int>();
		int correctAnswer = 0;
		foreach (int value in values) {
			correctAnswer += value;
		}
		answers.Add(correctAnswer);

		for (int i = 0; i < 5; i++) {
			float number = Random.value;
			number *= difficulty * 2;
			number -= difficulty;
			int numberI = correctAnswer + Mathf.RoundToInt(number);
			if (!answers.Contains(numberI)) {
				answers.Add(numberI);
			}
		}

		//switch (difficulty) {
		//	default:
		//	case 1:
		//		GenerateAddQuestion();
		//		break;
		//}
	}

	void GenerateAddQuestion() {
		type = Types.Plus;
		values = new List<int> {
			(int)(Random.value * 10) ,
			(int)(Random.value * 10) ,
		};
		answers = new List<int>();
		int correctAnswer = 0;
		foreach (int value in values) {
			correctAnswer += value;
		}
		answers.Add(correctAnswer);

		if (Mathf.RoundToInt(Random.value) == 1) {
			answers.Add(correctAnswer + 1);
			if (Mathf.RoundToInt(Random.value) == 1) {
				answers.Add(correctAnswer - 1);
			}
			else {
				answers.Add(correctAnswer + 2);
			}
		}
		else {
			answers.Add(correctAnswer - 1);
			if (Mathf.RoundToInt(Random.value) == 1) {
				answers.Add(correctAnswer - 2);
			}
			else {
				answers.Add(correctAnswer - 3);
			}
		}
	}


}
