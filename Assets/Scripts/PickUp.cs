using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	public float _healPlayer;
	public bool _isHealthPack;


	// public float _rechargeShield;
	// public bool _isShieldPack;

	void OnTriggerEnter2D(Collider2D other) {

		PlayerController _player = other.GetComponent<PlayerController>();
		
		if(_isHealthPack) {
			if(other.gameObject.tag == "Player") {
				if(_player._playerHealth < _player._maxPlayerHealth) {
					_player._playerHealth += _healPlayer;
					this.gameObject.SetActive(false);
				 
					if(_player._playerHealth >= _player._maxPlayerHealth) {
						_player._playerHealth = _player._maxPlayerHealth;
						this.gameObject.SetActive(false);
					}	
				}
			}
		}
	}
}
