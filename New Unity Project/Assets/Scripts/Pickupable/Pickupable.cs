using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : MonoBehaviour {

    [SerializeField] protected ScriptablePlayer _scriptablePlayer;
    [SerializeField] protected AudioClip _audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelAudioController.Instance.PlayOneShoot(_audioClip);
            Pickup();
        }
    }


    protected abstract void Pickup();
}
