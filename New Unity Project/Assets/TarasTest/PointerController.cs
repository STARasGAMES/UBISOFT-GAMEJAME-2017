using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

    [SerializeField] Color _activeColor;
    [SerializeField] Color _inactiveColor;

    SpriteRenderer _renderer;
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
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _camera = Camera.main;
        Cursor.visible = false;
        _input = Input.mousePosition;
        _input = new Vector3(_input.x, _input.y, 10);
        _transform.position = _camera.ScreenToWorldPoint(_input);
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, 0);
        Ray ray = new Ray(_transform.position, _camera.transform.forward);
        hit = Physics2D.GetRayIntersection(ray);
        isCastOnSomething = false;
        castedCollider = null;
        if (hit.collider != null)
        {
            isCastOnSomething = true;
            castedCollider = hit.collider;
        }
    }

    public void SetActive(bool active)
    {
        _renderer.color = active ? _activeColor : _inactiveColor;
    }

    // Update is called once per frame
    void FixedUpdate () {
       
	}
}
