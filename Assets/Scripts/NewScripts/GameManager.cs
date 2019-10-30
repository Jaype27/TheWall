using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public PlayerController _player;
	public Transform _spawnPoint;
	public float _spawnWait = 2.0f;

	public static GameManager Instance { get { return _instance; } }
	private static GameManager _instance = null;

	void Awake() {
		if(_instance != null && _instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
		RespawnPlayer();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RespawnPlayer() {
		StartCoroutine(SpawnPlayer());
	}

	private IEnumerator SpawnPlayer() {
		yield return new  WaitForSeconds(_spawnWait);
		_player.transform.position = _spawnPoint.position;
		_player.gameObject.SetActive(true);
		_player._knockbackCount = 0f;
	}
}
