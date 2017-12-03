using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour {
	[SerializeField] Transform _boss;
    [SerializeField] ScriptablePlayer _scriptablePlayer;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player"))
        {
            _boss.GetComponent<BossController>()._currentAttackHit = true;
            other.GetComponent<CatLifesController>().TakeDamage();
        }
	}
}
