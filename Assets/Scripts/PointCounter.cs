using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointCounter : MonoBehaviour {
	Text text;
	int score = 0;
	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text>();
		text.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score;
	}

	public void increment() {
		score++;
	}

	public int clear() {
		int oldScore = score;
		score = 0;
		return oldScore;
	}
}
