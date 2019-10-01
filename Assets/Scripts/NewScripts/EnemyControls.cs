using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour {

	public float _enemySpeed;

	// Ground and Wall Detection
	public Transform _groundSpot;
	public Transform _wallSpot;
	public float _wallCheckRadius;
	public LayerMask _isWall;
	private float _rayDistance = 0.5f;
	private bool _isRight;
	private bool _hitWall;

	// // Patrol Action
	// public float _startWaitTime;
	// public Transform[] _waypoints;
	// private float _standbyTime;
	// private int _randomSpot;
	
	// Update is called once per frame
	void Update () {
		SurfaceDetection();
		// EnemyPatrol();
	}

	void SurfaceDetection() {
		transform.Translate(Vector2.right * -_enemySpeed * Time.deltaTime);

		_hitWall = Physics2D.OverlapCircle(_wallSpot.position, _wallCheckRadius,_isWall);

		RaycastHit2D _groundDetect = Physics2D.Raycast(_groundSpot.position, Vector2.down, _rayDistance);
		if(_groundDetect.collider == false || _hitWall) {
			if(_isRight == true) {
				transform.eulerAngles = new Vector3(0, 0, 0);
				_isRight = false;
			} else {
				transform.eulerAngles = new Vector3(0, -180, 0);
				_isRight = true;
			}
		}
	}

	// void EnemyPatrol() {
	// 	transform.position = Vector2.MoveTowards(transform.position, _waypoints[_randomSpot].position, _enemySpeed * Time.deltaTime);

	// 	if(_standbyTime <= 0) {
	// 		_randomSpot = Random.Range(0, _waypoints.Length);
	// 		_standbyTime = _startWaitTime;
	// 	} else {
	// 		_standbyTime -= Time.deltaTime;
	// 	}
	// }
}
