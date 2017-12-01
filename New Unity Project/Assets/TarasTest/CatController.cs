using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    [SerializeField] Transform _pointer;
    [SerializeField] float _force = 10;
    [SerializeField] float _rotForce = 10;
    [SerializeField] float _fromFloorForce = 10;
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        Vector3 dir = (_pointer.position - transform.position).normalized;
        Vector3 force = (_pointer.position - transform.position).normalized * _force;
        force = new Vector3(force.x, _fromFloorForce, force.z) * Time.fixedDeltaTime;
        Debug.DrawRay(transform.position, force, Color.red, Time.fixedDeltaTime);
        float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
        if (Mathf.Abs(angle) > 10)
            _rigidbody.AddTorque(Vector3.up * _rotForce * Mathf.Sign(angle));
        if (Mathf.Abs(angle) < 90)
            _rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
