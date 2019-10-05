using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public Transform _playerTransform;
	public Vector3 _offset;
	public bool _isBounds;
	public Vector3 _minCamPos;
	public Vector3 _maxCamPos;

	void FixedUpdate() {
		transform.position = _playerTransform.position + _offset;

		if(_isBounds) {
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minCamPos.x, _maxCamPos.x),
				Mathf.Clamp(transform.position.y, _minCamPos.y, _maxCamPos.y),
				Mathf.Clamp(transform.position.z, _minCamPos.z, _maxCamPos.z));
		}
	}

	
	// Update is called once per frame
	void LateUpdate () {

		
		
	}
}
