using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : MonoBehaviour {

    [SerializeField] protected ScriptablePlayer _scriptablePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup();
            Destroy(this.gameObject);
        }
    }

    protected abstract void Pickup();
}
