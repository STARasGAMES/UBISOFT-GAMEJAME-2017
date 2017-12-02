using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController2D : MonoBehaviour {

	[SerializeField] PointerController _pointer;
    [SerializeField] Transform _groundDetector;
    [Header("Moving")]
    [SerializeField]
    float _maxMoveSpeed = 5;
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

		//_climbObjects = GameObject.FindGameObjectsWithTag("Climb");
		//Debug.Log ("Climb objects");
		//Debug.Log (_climbObjects);
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
            Debug.Log("not Grounded");
            return;
        }

        if (!CanSeePointer())
        {
            Debug.Log("dont see pointer");
            return;
        }
        Vector2 targetPos = Vector3.zero;

        if (_pointer.isCastOnSomething)
        {
            if (_pointer.castedCollider.CompareTag("Obstacle"))
            {
                Debug.Log("Pointer on Obstacle");
                return;
            }
            targetPos = _pointer.transform.position;
        }
        else
        {
            Vector2 pos = new Vector2(_pointer.transform.position.x, _pointer.transform.position.y);
            Debug.DrawRay(pos, Vector2.down, Color.white);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, 100);
            if (hit.transform == null)
            {
                Debug.Log("cant find collider");
                return;
            }
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Pointer on Obstacle");
                return;
            }
            if (hit.collider.CompareTag("Walkable"))
                targetPos = hit.point;
            else
                Debug.Log("error");

        }
        Vector2 some = targetPos - new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = new Vector2(some.x, some.y);
        if (dir.magnitude < _stopRange)
        {
            Debug.Log("in stop range");
            return;
        }
        Vector2 force = dir.normalized * _force * Time.fixedDeltaTime;
        _rigidbody.AddForce(force);
		Debug.DrawRay (transform.position, force, CanSeePointer () ? Color.green : Color.red, Time.fixedDeltaTime);
        Debug.DrawRay(targetPos, Vector3.up, Color.yellow);
        
        
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
        Vector2 pos = new Vector2(_groundDetector.position.x, _groundDetector.position.y);
        var hit = Physics2D.Raycast(pos, Vector2.down, 0.2f);
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
        if (hit.transform != null && (hit.point - pos).magnitude < _stopRange && hit.transform.CompareTag("Walkable"))
            return true;
        return false;
    }

    private bool CanMoveInDir(Vector2 dir)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        var hit = Physics2D.Raycast(pos, dir, 1000);
        Debug.DrawRay(transform.position, dir, Color.green);
        if (hit.transform != null && (hit.point - pos).magnitude < _stopRange && hit.transform.CompareTag("Walkable"))
            return true;
        return false;
    }
}
