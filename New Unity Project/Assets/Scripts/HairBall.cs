using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairBall : MonoBehaviour {

    [SerializeField] float _speed;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Boss>())
        {
            collider.GetComponent<Boss>().TakeDamage();
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Boss b = FindObjectOfType<Boss>();
        transform.position = Vector3.MoveTowards(transform.position, b.transform.position, _speed * Time.deltaTime);
    }

}
