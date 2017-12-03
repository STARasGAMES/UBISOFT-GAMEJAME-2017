using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pickupable {

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
        FindObjectOfType<CatLifesController>().TakeDamage();
    }

}
