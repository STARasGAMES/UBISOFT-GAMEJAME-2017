using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

    [SerializeField] Transform _pointer;
    [SerializeField] float _height = 0.1f;

    Camera _camera;
    Vector3 _input;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _input = Input.mousePosition;
    }

    // Update is called once per frame
    void FixedUpdate () {
        
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(_input);
        if (Physics.Raycast(ray, out hit, 10000))
        {
            _pointer.position = hit.point + hit.normal.normalized * _height;
            _pointer.rotation = Quaternion.LookRotation(hit.normal);
            Debug.DrawRay(hit.point, hit.normal, Color.green, Time.fixedDeltaTime);
        }
	}
}
