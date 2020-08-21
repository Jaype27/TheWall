using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControls : MonoBehaviour {

	public float _enemySpeed;
	public float _enemyInflictDamage;

	// Ground and Wall Detection
	public Transform _groundSpot;
	public Transform _wallSpot;
	public float _wallCheckRadius;
	public LayerMask _isWall;
	private float _rayDistance = 0.5f;
	private bool _isRight;
	private bool _hitWall;

	// Health
	public float _maxHealth = 100;
	private float _enemyHealth;
	public Image _healthBar;

	// // Patrol Action
	// public float _startWaitTime;
	// public Transform[] _waypoints;
	// private float _standbyTime;
	// private int _randomSpot;

	void Start() {
		_enemyHealth = _maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		SurfaceDetection();
		// EnemyPatrol();
	}

	public void TakeDamage (float _amount) {
		_enemyHealth -= _amount;

		_healthBar.fillAmount = _enemyHealth / _maxHealth;

		if(_enemyHealth <= 0) {
			Die();
		}
	}

	void Die() {
		this.gameObject.SetActive(false);
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

	void OnTriggerEnter2D(Collider2D other) {

		PlayerController _player = other.GetComponent<PlayerController>();
		
		if(other.gameObject.tag == "Player") {
			_player.PlayerDamage(_enemyInflictDamage);

			_player._pushbackCount = _player._pushbackMax;

			if(other.transform.position.x < transform.position.x)
				_player._pushfromRight = true;
			else
				_player._pushfromRight = false;
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
