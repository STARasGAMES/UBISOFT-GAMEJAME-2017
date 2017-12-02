using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

    Transform _transform;

    Camera _camera;
    Vector3 _input;
    public RaycastHit2D hit;
    public bool isCastOnSomething;// { get; private set; }
    public Collider2D castedCollider;// { get; private set; }

    //public Collider 

    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    private void Update()
    {
        _input = Input.mousePosition;
        _input = new Vector3(_input.x, _input.y, 10);
        _transform.position = _camera.ScreenToWorldPoint(_input);
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, 0);
        hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));
        isCastOnSomething = false;
        castedCollider = null;
        if (hit.collider != null)
        {
            isCastOnSomething = true;
            castedCollider = hit.collider;
        }
        Debug.DrawRay(hit.point, Vector3.up, Color.red, Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate () {
       
	}
}
