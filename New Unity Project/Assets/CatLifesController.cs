using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLifesController : MonoBehaviour {

    [SerializeField] ScriptablePlayer _scriptablePlayer;
    [SerializeField] Vector2 _takeDamageForce;
    [SerializeField] float _disableDuration;
    [SerializeField] Animator _animator;
	
	void Update () {
		
	}

    public void TakeDamage()
    {
        _scriptablePlayer._lifes--;
        StartCoroutine(TakeDamageCoroutine());
        if (_scriptablePlayer._lifes <= 0)
        {
            Debug.Log("GAME OVER");
            GameManager.Instance.RestartLevel();
            return;
        }
    }

    IEnumerator TakeDamageCoroutine()
    {
        GetComponent<CatController2D>().enabled = false;
        float random = UnityEngine.Random.Range(0, 10f)-5;
        Debug.Log("Random: "+random);
        random = Mathf.Sign(random);
        
        Vector2 force = new Vector2(_takeDamageForce.x * random, _takeDamageForce.y);

        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        _animator.SetTrigger("Hit");

        yield return new WaitForSeconds(_disableDuration);

        GetComponent<CatController2D>().enabled = true;
    }
}
