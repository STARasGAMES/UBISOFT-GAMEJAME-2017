using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] float maxLifes = 10;
    public float lifes;

    private void Awake()
    {
        lifes = maxLifes;
    }

    public void TakeDamage()
    {
        lifes--;
        if (lifes <= 0)
            GameManager.Instance.NextLevel();
    }

}
