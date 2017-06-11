using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnFood : MonoBehaviour {

	// Food Prefab
	public GameObject foodPrefab;

	// Borders
	public Transform borderTop;
	public Transform borderBottom;
	public Transform borderLeft;
	public Transform borderRight;

	// Use this for initialization
	void Start () {
		Spawn();
	}

	// Spawn food
	public void Spawn () {
		Spawn(null);
	}

	public void Spawn(Snake snake) {
		int y = 0;
		int x = 0;
		do {
			// y position between top and bottom border
			y = (int) Random.Range(borderBottom.position.y, borderTop.position.y);

			// x position between left and right border
			x = (int) Random.Range(borderLeft.position.x, borderRight.position.x);
		} while(snake != null && snake.collidesWith(x, y));

		// instantiate the food at (x,y)
		Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
	}
}
