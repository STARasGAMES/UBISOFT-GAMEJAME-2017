using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour {
	[SerializeField] Transform _boss;

	void OnTriggerEnter2D(Collider2D other) {
		// TODO check if Cat was hit
		_boss.GetComponent<BossController>()._currentAttackHit = true;
	}
}
