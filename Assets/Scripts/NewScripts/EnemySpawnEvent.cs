using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnEvent : MonoBehaviour {

	ObjectPooler _objectPooler;
	public int _enemyCount;
	public float _startWait;
	public float _spawnWait;
	public Transform _spawnPoint;

	void Start() {
		_objectPooler = ObjectPooler.Instance;
	}

	void OnTriggerEnter2D(Collider2D other)	{
		if(other.gameObject.tag == "Player") {
			StartCoroutine(SpawnEnemies());	
		}
	}

	IEnumerator SpawnEnemies() {
		yield return new WaitForSeconds(_startWait);
		for (int i = 0; i < _enemyCount; i++) {
			_objectPooler.SpawnFromPool("enemy", _spawnPoint.position, _spawnPoint.rotation);
			yield return new WaitForSeconds(_spawnWait);
		}

		this.gameObject.SetActive(false);
	}
}
