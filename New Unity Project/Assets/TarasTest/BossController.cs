using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
	[SerializeField] Transform _cat;
	[SerializeField] bool _lastAttackIsRightHand;
	[SerializeField] float _currentAttackDelay = 1f;
	[SerializeField] float _defaultAttackDelay;
	[SerializeField] float _catHitAttackDelay;
	[SerializeField] float _attackPositionY;
	[SerializeField] float _handMoveDownTime = 1f;
	[SerializeField] float _handMoveTime = 0.5f;
	[SerializeField] float _handMoveMultiplier = 0.5f;
	[SerializeField] float _handReturnRotationTime = 0.2f;
	[SerializeField] AnimationCurve _handMoveCurve;
	// [SerializeField] float _handDownTime = 1f;
	// [SerializeField] AnimationCurve _handDownCurve;
	// [SerializeField] float _handUpTime = 1.5f;
	// [SerializeField] AnimationCurve _handUpCurve;
	[SerializeField] float _nearestHandThreshold = 1f;
	[SerializeField] Transform _leftHand;
	[SerializeField] Transform _rightHand;
	[SerializeField] public bool _currentAttackHit;

	private CameraShake _cameraShake;
	private Animator _leftHandAnimator;
	private Animator _rightHandAnimator;
	private float _targetHandZAngle;
	private bool _attacking;
	private Vector3 _leftHandStartingPos = new Vector3();
	private Vector3 _rightHandStartingPos = new Vector3();
	private float _timeSinceLastAttack;
	private bool _handLanded;
	private float _currentAttackPositionX;

	// Use this for initialization
	void Start () {
		_cameraShake = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
		_leftHandAnimator = _leftHand.GetComponent<Animator> ();
		_rightHandAnimator = _rightHand.GetComponent<Animator> ();
		_timeSinceLastAttack = 0f;
		_leftHandStartingPos.Set (_leftHand.transform.position.x, _leftHand.transform.position.y, _leftHand.transform.position.z);
		_rightHandStartingPos.Set (_rightHand.transform.position.x, _rightHand.transform.position.y, _rightHand.transform.position.z);
	}

	private float GetZAngleBetweenPoints(Vector2 pointOne, Vector2 pointTwo) {
		return Mathf.Rad2Deg * Mathf.Atan2(pointTwo.y - pointOne.y, pointTwo.x - pointOne.x) + 90f;
	}

	private void OnAttackStart() {
		_currentAttackPositionX = _cat.position.x;
		_currentAttackHit = false;
		_handLanded = false;

		if (_currentAttackPositionX < _leftHandStartingPos.x + _nearestHandThreshold) {
			// Left side of the screen
			_lastAttackIsRightHand = false;
		} else if (_currentAttackPositionX > _rightHandStartingPos.x - _nearestHandThreshold) {
			// Right side of the screen
			_lastAttackIsRightHand = true;
		} else {
			// Between two hands
			_lastAttackIsRightHand = !_lastAttackIsRightHand;
		}

		if (_lastAttackIsRightHand) {
			Vector3 position = new Vector3 (_rightHandStartingPos.x + (_cat.transform.position.x - _rightHandStartingPos.x) * _handMoveMultiplier, _rightHand.transform.position.y, _rightHand.transform.position.z);
			_rightHand.transform.position = position;

			_targetHandZAngle = GetZAngleBetweenPoints (_rightHand.transform.position, _cat.transform.position);
			_rightHandAnimator.SetTrigger ("RightAttack");
		} else {
			Vector3 position = new Vector3 (_leftHandStartingPos.x + (_cat.transform.position.x - _leftHandStartingPos.x) * _handMoveMultiplier, _leftHand.transform.position.y, _leftHand.transform.position.z);
			_leftHand.transform.position = position;

			_targetHandZAngle = GetZAngleBetweenPoints (_leftHand.transform.position, _cat.transform.position);
			_leftHandAnimator.SetTrigger ("LeftAttack");
		}
	}

	private void OnAttackProgress() {
		float moveCoeff = 0f;
		if (_timeSinceLastAttack > _handMoveDownTime) {
			moveCoeff = 1f - ((_timeSinceLastAttack -_handMoveDownTime) / _handReturnRotationTime);
		} else {
			moveCoeff = _timeSinceLastAttack / _handMoveTime;
		}

		Mathf.Clamp01 (moveCoeff);
		moveCoeff = _handMoveCurve.Evaluate (moveCoeff);

		Vector3 rotation = new Vector3 (0f, 0f, _targetHandZAngle * moveCoeff);
		if (_lastAttackIsRightHand) {
			_rightHand.transform.rotation = new Quaternion ();
			_rightHand.transform.Rotate (rotation);
		} else {
			_leftHand.transform.rotation = new Quaternion ();
			_leftHand.transform.Rotate (rotation);
		}

		if (_timeSinceLastAttack >= _handMoveDownTime) {
			if (!_handLanded) {
				// Произошел удар лапой
				_cameraShake.DoShake();
				_handLanded = true;
			}
		}

		if (_timeSinceLastAttack >= _handMoveDownTime + _handReturnRotationTime) {
			_rightHand.transform.position = _rightHandStartingPos;
			_leftHand.transform.position = _leftHandStartingPos;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		_timeSinceLastAttack += Time.fixedDeltaTime;
		if (_timeSinceLastAttack > _currentAttackDelay) {
			// Attack
			_timeSinceLastAttack = 0f;
			_attacking = true;
			OnAttackStart();
		}

		if (_attacking) {
			OnAttackProgress ();

			if (_timeSinceLastAttack >= _currentAttackDelay) {
				// Hands stoped moving (end of attack)
				if (_currentAttackHit) {
					_currentAttackDelay = _catHitAttackDelay;
				} else {
					_currentAttackDelay = _defaultAttackDelay;
				}
				_timeSinceLastAttack = 0f;

				_attacking = false;
			}
		}
	}
}
