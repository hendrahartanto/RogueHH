using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextPopupManager : MonoBehaviour
{
  [SerializeField] GameObject _textPrefab = default;
  [SerializeField] GameObject _criticalTextPrefab = default;
  [SerializeField] List<GameObject> _coloredTextPrefabs;
  private Quaternion _rotationValue;

  [Header("Listening to")]
  [SerializeField] private DamagePopupEventChannel _damagePopUpEvent = default;
  [SerializeField] private TextPopupEventChannelSO _textPopupEvent = default;

  private void Awake()
  {
    _rotationValue = Quaternion.Euler(30f, 45f, 0f);
  }

  private void OnEnable()
  {
    _damagePopUpEvent.OnEventRaised += SetupDamagePopup;
    _textPopupEvent.OnEventRaised += SetupTextPopup;
  }

  private void OnDisable()
  {
    _damagePopUpEvent.OnEventRaised -= SetupDamagePopup;
    _textPopupEvent.OnEventRaised -= SetupTextPopup;
  }

  private void SetupDamagePopup(Vector3 pos, int damageValue, bool critical)
  {
    pos.y += 2f;

    GameObject _textToInstantiate = critical ? _criticalTextPrefab : _textPrefab;

    GameObject instance = Instantiate(_textToInstantiate, pos, _rotationValue);
    TextMeshPro textMesh = instance.GetComponent<TextMeshPro>();
    textMesh.SetText(damageValue.ToString());

    StartCoroutine(AnimateTextPopup(instance, textMesh));
  }

  private void SetupTextPopup(Vector3 pos, String text, TextColor color, float yOffset)
  {
    pos.y += 2f + yOffset;

    GameObject _textToInstantiate = _coloredTextPrefabs[(int)color];

    GameObject instance = Instantiate(_textToInstantiate, pos, _rotationValue);
    TextMeshPro textMesh = instance.GetComponent<TextMeshPro>();
    textMesh.SetText(text);

    StartCoroutine(AnimateTextPopup(instance, textMesh));
  }

  private IEnumerator AnimateTextPopup(GameObject popup, TextMeshPro textMesh)
  {
    float duration = 0.5f;
    Vector3 initialPosition = popup.transform.position;
    Vector3 targetPosition = initialPosition + new Vector3(0, 0.5f, 0);
    Color initialColor = textMesh.color;

    float randomCurveX = Random.Range(-0.2f, 0.2f);
    float randomCurveZ = Random.Range(-0.2f, 0.2f);

    float elapsedTime = 0f;
    while (elapsedTime < duration)
    {
      float t = elapsedTime / duration;
      Vector3 curvedPosition = Vector3.Lerp(initialPosition, targetPosition, t);
      curvedPosition.x += randomCurveX * t;
      curvedPosition.z += randomCurveZ * t;

      popup.transform.position = curvedPosition;

      Color newColor = initialColor;
      newColor.a = Mathf.Lerp(1f, 0f, t);
      textMesh.color = newColor;

      elapsedTime += Time.deltaTime;
      yield return null;
    }

    textMesh.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    Destroy(popup);
  }
}

public enum TextColor
{
  Yellow,
  Green,
  Red
}
