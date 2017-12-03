using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Pickupable
{
    [SerializeField] float _chanceOfFish = 0.3f;
    [SerializeField] GameObject _hairBall;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            LevelAudioController.Instance.PlayOneShoot(_audioClip);
            Pickup();
        }
    }

    protected override void Pickup()
    {
        float random = UnityEngine.Random.Range(0.0f,1f);
        if (random < _chanceOfFish)
        {
            _scriptablePlayer._lifes++;
        }
        else
        {
            Instantiate(_hairBall, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
