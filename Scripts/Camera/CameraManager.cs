using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
  public Camera MainCamera;
  [SerializeField] private TransformAnchorSO _playerTransformAnchor = default;
  private Transform _playerTransform = null;
  private Vector3 _cameraOffset;

  [Header("Camera Shake Settings")]
  [SerializeField] private float shakeDuration = 0.2f;
  [SerializeField] private float shakeIntensity = 200f;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _cameraShakeEvent = default;

  private void OnEnable()
  {
    _playerTransformAnchor.OnAnchorProvided += SetupPlayerVCam;
    _cameraShakeEvent.OnEventRaised += ShakeCamera;
  }

  private void OnDisable()
  {
    _playerTransformAnchor.OnAnchorProvided -= SetupPlayerVCam;
    _cameraShakeEvent.OnEventRaised -= ShakeCamera;
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

  public void ShakeCamera()
  {
    StartCoroutine(Shake(shakeDuration, shakeIntensity));
  }

  private IEnumerator Shake(float duration, float intensity)
  {
    Quaternion originalRotation = MainCamera.transform.localRotation;
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
      float shakeAngle = Random.Range(-intensity, intensity); // Random angle for Z-axis rotation
      MainCamera.transform.localRotation = Quaternion.Euler(30f, 45f, shakeAngle);

      elapsedTime += Time.deltaTime;

      yield return null;
    }

    MainCamera.transform.localRotation = originalRotation; // Reset to the original rotation
  }
}
