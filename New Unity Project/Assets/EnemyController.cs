using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] List<Transform> _points;
    [SerializeField] float speed = 2;
    [SerializeField] float wait = 2;
    [SerializeField] int startPoint = 0;
    [SerializeField] int _current = 0;
    [SerializeField] Transform _enemyT;
    [SerializeField] SpriteRenderer _enemySprite;
    private void Start()
    {
        _current = startPoint;
        _enemyT.transform.position = _points[startPoint].position;
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        for (;;)
        {
            _enemySprite.flipX = _enemyT.position.x < _points[_current].position.x;
            Debug.DrawRay(_enemyT.transform.position, _enemyT.position - _points[_current].position, Color.red);
            while ((_enemyT.position - _points[_current].position).magnitude > 0.1f)
            {
                //Debug.Log("HERE");
                _enemyT.position = Vector3.MoveTowards(_enemyT.position, _points[_current].position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(wait);
            if (_current == 0)
                _current = 1;
            else
                _current = 0;
        }
    }

}
