using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

    Transform _transform;

    Camera _camera;
    Vector3 _input;

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
        var hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));
        Debug.Log(hit.transform);
        Debug.DrawRay(hit.point, Vector3.up, Color.red, Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate () {
       
	}
}
