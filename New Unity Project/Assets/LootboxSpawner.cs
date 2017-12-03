using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxSpawner : MonoBehaviour {

    [SerializeField] GameObject _lootboxPrefab;
    [SerializeField] float _force = 4;
    [SerializeField] List<Transform> _spawnPoints;
    

    public void Spawn()
    {
        int rand = UnityEngine.Random.Range(0, _spawnPoints.Count - 1);
        GameObject go = Instantiate(_lootboxPrefab);
        go.transform.position = _spawnPoints[rand].position;
        Vector2 dir = new Vector2(_spawnPoints[rand].right.x, _spawnPoints[rand].right.y);
        dir.Normalize();
        go.GetComponent<Rigidbody2D>().isKinematic = false;
        go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        go.GetComponent<Rigidbody2D>().AddForce(dir * _force, ForceMode2D.Impulse);
        go.GetComponent<Collider2D>().isTrigger = false;
        Debug.DrawRay(_spawnPoints[rand].position, _spawnPoints[rand].right, Color.red, 1);
        //Debug.Break();
    }
}
