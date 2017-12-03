using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Pickupable
{
    [SerializeField] float _chanceOfFish = 0.3f;
    [SerializeField] GameObject _hairBall;

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
