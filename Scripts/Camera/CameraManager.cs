using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public Camera MainCamera;
  [SerializeField] private TransformAnchorSO _playerTransformAnchor = default;
  private Transform _playerTransform = null;
  private Vector3 _cameraOffset;

  private void OnEnable()
  {
    _playerTransformAnchor.OnAnchorProvided += SetupPlayerVCam;
  }

  private void OnDisable()
  {
    _playerTransformAnchor.OnAnchorProvided -= SetupPlayerVCam;
  }

  private void Start()
  {
    if (_playerTransformAnchor.isSet)
      SetupPlayerVCam();
  }

  private void LateUpdate()
  {
    if (_playerTransform != null)
    {
      MainCamera.transform.position = _playerTransform.position + _cameraOffset;
    }
  }

  private void SetupPlayerVCam()
  {
    _playerTransform = _playerTransformAnchor.Value;
    _cameraOffset = new Vector3(-10, 10 - _playerTransform.position.y, -10);
  }
}
