using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour, IPooledObject {

	public float _speed = 20f;
	public float _damage = 5;
	public Rigidbody2D _rb;
	public float _activeDuration = 2.0f;
	private float _activeTimer;

	void OnEnable() {
		_activeTimer = _activeDuration;
	}

	// Use this for initialization
	public void OnObjectSpawn () {
		_rb.velocity = transform.right * _speed;	
	}

	void Update() {
		_activeTimer -= Time.deltaTime;
		if(_activeTimer <= 0f) {
			this.gameObject.SetActive(false);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		EnemyControls _enemy = other.GetComponent<EnemyControls>();
		

		if(other.gameObject.tag == "enemy") {
			_enemy.TakeDamage(_damage);

			this.gameObject.SetActive(false);
		}

		if(other.gameObject.tag == "wall") {
			this.gameObject.SetActive(false);
		}
	}
}
