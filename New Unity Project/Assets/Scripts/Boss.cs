using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public int maxLifes = 10;
    public int lifes;
    [SerializeField] Animator _animator;

    private void Awake()
    {
        lifes = maxLifes;
    }

    public void TakeDamage()
    {
        _animator.SetTrigger("DogHit");
        lifes--;
        if (lifes <= 0)
            GameManager.Instance.NextLevel();
    }

}
