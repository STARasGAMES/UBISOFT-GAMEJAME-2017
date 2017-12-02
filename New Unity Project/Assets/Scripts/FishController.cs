using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    [SerializeField] ScriptablePlayer _scrPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _scrPlayer._lifes++;
            Destroy(this.gameObject);
        }
    }

}
