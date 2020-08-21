using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Movement
	public float _moveSpeed;
	public float _jumpHeight;
	private float _moveInput;
	public GameManager _gm;

	// Player Health
	public float _maxPlayerHealth = 100f;
	public float _playerHealth;
	public Image _healthBar;
	
	private Rigidbody2D _rb2D;
	private Animator _anim;

	private bool _isGrounded;
	public Transform _groundCheck;
	public LayerMask _groundMask;
	public float _groundCheckradius;

	// Pressure Jump
	private float _jumpTimeCounter;
	public float _jumpTime;
	private bool _isJumping; 

	// Laser Spawn Point
	ObjectPooler _objectPooler;
	public Transform _firePoint;
	public float _fireRate;
	private float _lastTimeFired;

	// Invincible Frames
	public Renderer _playerRend;
	public Renderer _gunRend;
	private float _flashCounter;
	public float _flashMax = 0.1f;
	private float _invincibleCounter;
	public float _invincibleMax = 1.0f;

	// Pushback
	public float _pushback;
	public float _pushbackCount;
	public float _pushbackMax;
	public bool _pushfromRight;

	// Audio

	public AudioSource[] _playSounds;


	// Use this for initialization
	void Start () {
		_rb2D = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_objectPooler = ObjectPooler.Instance;
		_playerHealth = _maxPlayerHealth;
	}

	void FixedUpdate() {
		MovePlayer();
		PlayerPushBack();
	}
	
	// Update is called once per frame
	void Update () {
		FlipCharacter();
		JumpCharacter();
		Shooting();
		InvincibleFrames();

		HealthBar();
	}	

	void MovePlayer() {
		_moveInput = Input.GetAxis("Horizontal");
		_rb2D.velocity = new Vector2(_moveInput * _moveSpeed, _rb2D.velocity.y);
	}

	void FlipCharacter() {
		if(_moveInput > 0) {
			transform.eulerAngles = new Vector2(0, 0);
			_anim.SetFloat("Speed", _moveSpeed);
		} else if(_moveInput < 0) {
			transform.eulerAngles = new Vector2(0, 180);
			_anim.SetFloat("Speed", _moveSpeed);
		} else {
			_anim.SetFloat("Speed", 0);
		}
	}

	void JumpCharacter() {
		_isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckradius, _groundMask);
	
		if(_isGrounded == true && Input.GetKeyDown(KeyCode.Space)) {
			_isJumping = true;
			_jumpTimeCounter = _jumpTime;
			_rb2D.velocity = Vector2.up * _jumpHeight;
		}

		if(Input.GetKey(KeyCode.Space) && _isJumping == true) {
			if(_jumpTimeCounter > 0) {
				_rb2D.velocity = Vector2.up * _jumpHeight;
				_jumpTimeCounter -= Time.deltaTime;
			} else {
				_isJumping = false;
			}
		}

		if(Input.GetKeyUp(KeyCode.Space)) {
			_isJumping = false;
		}
	}

	void Shooting() {
		if( _invincibleCounter <= 0) {
			if(Input.GetMouseButtonDown(0)) {
				if(Time.time -_lastTimeFired > 1 / _fireRate) {
					_lastTimeFired = Time.time;
					_objectPooler.SpawnFromPool("Laser", _firePoint.position, _firePoint.rotation);
					_playSounds[0].Play();
				}
			}
		}
	}

	void InvincibleFrames() {
		if(_invincibleCounter > 0) {
			_invincibleCounter -= Time.deltaTime;

			_flashCounter -= Time.deltaTime;

			if(_flashCounter <= 0) {
				_playerRend.enabled = !_playerRend.enabled;
				_gunRend.enabled = !_gunRend.enabled;

				_flashCounter = _flashMax;
			}

			if(_invincibleCounter <= 0) {
				_playerRend.enabled = true;
				_gunRend.enabled = true;
			}
		}
	}

	public void PlayerDamage(float _amount) {

		if(_invincibleCounter <= 0) {
			_playerHealth -= _amount;

			_invincibleCounter = _invincibleMax;

			_playerRend.enabled = false;
			_gunRend.enabled = false;

			_flashCounter = _flashMax;

			if(_playerHealth <= 0) {
				this.gameObject.SetActive(false);
				_gm.RespawnPlayer();
			}
		}
	}

	private void HealthBar() {
		_healthBar.fillAmount = _playerHealth / _maxPlayerHealth;
	}
	
	public void PlayerPushBack() {	
		if(_pushbackCount <= 0) {
			_rb2D.velocity = new Vector2(_moveInput * _moveSpeed, _rb2D.velocity.y);
		} else {
			if(_pushfromRight)
				_rb2D.velocity = new Vector2(-_pushback, _pushback);
			if(!_pushfromRight)
				_rb2D.velocity = new Vector2(_pushback, _pushback);	
				_pushbackCount -= Time.deltaTime;
		}
	}
}
