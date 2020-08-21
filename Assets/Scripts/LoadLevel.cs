using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

	public string _loadLevel;
	public float _startTime;
	private float _countDownTime;
	public Text _messageTimer;
	public Text _countDownNumber;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player") {
			StartCoroutine(Timer());
		}
	}

	void LoadNextLevel() {
		SceneManager.LoadScene(_loadLevel);
	}

	IEnumerator Timer() {

		_countDownNumber.gameObject.SetActive(true);
		_messageTimer.gameObject.SetActive(true);

		yield return new WaitForSeconds(0.5f);
		_countDownNumber.GetComponent<Text>().text = "3";

		_countDownNumber.gameObject.SetActive(true);
		yield return new WaitForSeconds (1);
		_countDownNumber.gameObject.SetActive(false);
		_countDownNumber.GetComponent<Text>().text = "2";

		_countDownNumber.gameObject.SetActive(true);
		yield return new WaitForSeconds (1);
		_countDownNumber.gameObject.SetActive(false);
		_countDownNumber.GetComponent<Text>().text = "1";

		_countDownNumber.gameObject.SetActive(true);
		yield return new WaitForSeconds (1);
		_countDownNumber.gameObject.SetActive(false);

		_messageTimer.gameObject.SetActive(false);
		_countDownNumber.gameObject.SetActive(false);		

		LoadNextLevel();
		
	}
}
