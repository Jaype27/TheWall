using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {

	// public GameManager _gm;
	public GameObject _controlScreen;
	public GameObject _gameOverScreen;
	public GameObject _pauseScreen;
	public GameObject _closeCurrentScreen;
	public string _levelLoadName;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ControlButton() {
		_controlScreen.gameObject.SetActive(true);
	}

	public void PlayButton() {
		SceneManager.LoadScene(_levelLoadName);
	}

	public void PauseGame() {
		Time.timeScale = 0;
		_pauseScreen.gameObject.SetActive(true);
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		_pauseScreen.gameObject.SetActive(false);
	}


	public void PlayAgain() {
		_gameOverScreen.gameObject.SetActive(false);
		
	}

	public void CloseCurrentScreen() {
		_closeCurrentScreen.gameObject.SetActive(false);
	}

	public void QuitGame() {
		Application.Quit();
	}
}
