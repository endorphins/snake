using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {
	// current movement direction, default is right
	Vector2 dir = Vector2.right;
	// keep track of tail
	List<Transform> tail = new List<Transform>();
	// tail prefab
	public GameObject tailPrefab;
	// did we eat?
	bool ate = false;
	// are we alive?
	bool alive = true;
	// count our points
	public PointCounter pointCounter;

	// Use this for initialization
	void Start () {
		//clear score
		pointCounter.clear();
		// move the snake every 75ms
		InvokeRepeating("Move", 0.075f, 0.075f);
	}
	
	// Update is called once per frame
	void Update () {
		// key presses
		// movement?
		if (Input.GetKeyDown(KeyCode.UpArrow))
			dir = Vector2.up;
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			dir = Vector2.down;
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			dir = Vector2.left;
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			dir = Vector2.right;
	}

	// Do movement stuff
	void Move () {
		// current position
		Vector2 oldPosition = transform.position;
		// move in the new direction
		transform.Translate(dir);

		// did we eat something?
		if (ate) {
			// load prefab to extend tail
			GameObject newTail = (GameObject) Instantiate(tailPrefab, oldPosition, Quaternion.identity);
			// add new tail to our existing tail (at the front)
			tail.Insert(0, newTail.transform);

			//reset ate
			ate = false;
		}
		// do we have a tail?
		else if (tail.Count > 0) {
			if (transform.position == tail.Last ().position) {
				alive = false;
			}
			// move last element to where head was
			tail.Last ().position = oldPosition;

			// move last element to front of list
			tail.Insert (0, tail.Last ());
			tail.RemoveAt (tail.Count - 1);
		}
	}

	// Check collisions
	void OnTriggerEnter2D(Collider2D collider) {
		// food?
		if (collider.name.StartsWith("FoodPrefab")) {
			ate = true;
			pointCounter.increment();
			Destroy(collider.gameObject);

			GameObject.Find("Main Camera").GetComponent<SpawnFood>().Spawn();
		}
		// if not food, must be border or self...
		else {
			alive = false;
		}
	}

	public bool isAlive() {
		return alive;
	}

	public bool collidesWith(int x, int y) {
		bool collided = false;
		if(transform.position.x == x && transform.position.y == y) {
			collided = true;
		} else {
			Transform[] tailArray = tail.ToArray();
			int i = 0;
			while(!collided && i < tailArray.Length) {
				Transform t = tailArray[i];
				if(t.position.x == x && t.position.y == y) {
					collided = true;
				}

				i++;
			}
		}

		return collided;
	}
}
