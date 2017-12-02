using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour {
	private GameObject _cat;

	void Start () {
		_cat = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == _cat) {
			Destroy (gameObject);

			float r = Random.Range (0f, 3f);
			if (r < 2f) {
				// Weapon (66% probability)

			} else {
				// Fish (33%)

			}
		}
	}
}
