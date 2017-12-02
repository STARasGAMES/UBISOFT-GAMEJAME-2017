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
	[SerializeField] float _handDownTime = 1f;
	[SerializeField] AnimationCurve _handDownCurve;
	[SerializeField] float _handUpTime = 1.5f;
	[SerializeField] AnimationCurve _handUpCurve;
	[SerializeField] float _nearestHandThreshold = 4f;
	[SerializeField] Transform _leftHand;
	[SerializeField] Transform _rightHand;
	[SerializeField] public bool _currentAttackHit;

	private bool _attacking;
	private Vector3 _leftHandStartingPos = new Vector3();
	private Vector3 _rightHandStartingPos = new Vector3();
	private float _timeSinceLastAttack;
	private float _currentAttackDuration;
	private float _currentAttackPositionX;

	// Use this for initialization
	void Start () {
		_timeSinceLastAttack = 0f;
		_leftHandStartingPos.Set (_leftHand.transform.position.x, _leftHand.transform.position.y, _leftHand.transform.position.z);
		_rightHandStartingPos.Set (_rightHand.transform.position.x, _rightHand.transform.position.y, _rightHand.transform.position.z);
	}

	// Update is called once per frame
	void FixedUpdate () {
		_timeSinceLastAttack += Time.fixedDeltaTime;
		if (_timeSinceLastAttack > _currentAttackDelay) {
			// Attack
			_attacking = true;
			_currentAttackPositionX = _cat.position.x;
			_currentAttackHit = false;

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
			_timeSinceLastAttack = 0f;
		}

		if (_attacking) {
			if (_timeSinceLastAttack < _handDownTime + _handUpTime) {
				// Hand is moving
				_attacking = true;

				Transform hand = _lastAttackIsRightHand ? _rightHand : _leftHand;
				BoxCollider2D collider = hand.GetComponent<BoxCollider2D> ();
				AnimationCurve curve;
				float handPosCoeff = 0f;
				if (_timeSinceLastAttack < _handDownTime) {
					// Down
					handPosCoeff = _timeSinceLastAttack / _handDownTime;
					curve = _handDownCurve;
				} else {
					// Up
					handPosCoeff = 1f - (_timeSinceLastAttack - _handDownTime) / _handUpTime;
					curve = _handUpCurve;
				}
				if (handPosCoeff < 0f) {
					handPosCoeff = 0f;
				} else if (handPosCoeff > 1f) {
					handPosCoeff = 1f;
				}

				handPosCoeff = curve.Evaluate (handPosCoeff);

				if (_lastAttackIsRightHand) {
					_leftHand.transform.position = _leftHandStartingPos;

					Vector3 newPos = new Vector3 ();
					newPos.Set (
						_rightHandStartingPos.x + (_currentAttackPositionX - _rightHandStartingPos.x) * handPosCoeff, 
						_rightHandStartingPos.y + (_attackPositionY - _rightHandStartingPos.y) * handPosCoeff, 
						_rightHandStartingPos.z 
					);
					_rightHand.transform.position = newPos;
				} else {
					_rightHand.transform.position = _rightHandStartingPos;

					Vector3 newPos = new Vector3 ();
					newPos.Set (
						_leftHandStartingPos.x + (_currentAttackPositionX - _leftHandStartingPos.x) * handPosCoeff, 
						_leftHandStartingPos.y + (_attackPositionY - _leftHandStartingPos.y) * handPosCoeff, 
						_leftHandStartingPos.z 
					);
					_leftHand.transform.position = newPos;
				}

				collider.enabled = !(_currentAttackHit || _timeSinceLastAttack > _handDownTime); // Cat was hit or hand is moving up
			} else {
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
