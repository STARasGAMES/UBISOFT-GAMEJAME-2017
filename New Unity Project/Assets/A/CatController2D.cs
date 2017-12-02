using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController2D : MonoBehaviour {

	[SerializeField] Transform _pointer;
    [Header("Gravity")]
    [SerializeField] float _gravity = 1;
    [SerializeField] float _maxFallSpeed = 5;
    [SerializeField] float _fallSpeed;
    [Header("Moving")]
    [SerializeField] float _stopRange = 0.2f;
	[SerializeField] float _visibilityRange = 5;
    [SerializeField] float _force = 10;
    [SerializeField] float _rotForce = 10;
    [SerializeField] float _fromFloorForce = 10;
    Rigidbody2D _rigidbody;
	GameObject[] _climbObjects;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

		_climbObjects = GameObject.FindGameObjectsWithTag("Climb");
		Debug.Log ("Climb objects");
		Debug.Log (_climbObjects);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool CanSeePointer() 
	{
		return (_pointer.transform.position - transform.position).magnitude < _visibilityRange;
	}

    private void FixedUpdate()
    {
		// if (cat is on any of _climbObjects) {
		//     disable gravity
		//     allow cat to move on wall in direction of pointer if there's climb area 
		//     
		// } else {
		//     enable physics
		// }
        if (!IsGrounded())
        {
            transform.position += Vector3.down * _gravity * Time.deltaTime;
        }


        Vector3 dir = _pointer.position - transform.position;
        dir = new Vector3(dir.x, dir.y, 0);
        if (dir.magnitude < _stopRange)
            return;
        Vector3 force = dir.normalized * _force;
        force = new Vector3(force.x, _fromFloorForce, force.z) * Time.fixedDeltaTime;
		Debug.DrawRay (transform.position, force, CanSeePointer () ? Color.green : Color.red, Time.fixedDeltaTime);

        float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
        
		// if (cat is moving up) {
		// *     disable collisions (to climb from beneath)
		// * } else {
		// *     enable collisions
		// * }
		// * 
		// * /
		///*
		//if (Mathf.Abs(angle) > 10)
  //          _rigidbody.AddTorque(Vector3.up * _rotForce * Mathf.Sign(angle));
  //      if (Mathf.Abs(angle) < 90)
  //          _rigidbody.AddForce(force, ForceMode.Acceleration);
            
    }

    private bool IsGrounded()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        var hit = Physics2D.Raycast(pos, Vector2.down, 0.1f);
        
        if (hit.transform != null && hit.transform.CompareTag("Walkable"))
            return true;
        return false;
    }
}
