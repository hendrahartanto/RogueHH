using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUIManager : MonoBehaviour
{
  [SerializeField] GameObject _textPrefab = default;
  private Quaternion _rotationValue;

  [Header("Listening to")]
  [SerializeField] private Vector3_Int_EvetChanel _damagePopUpEvent = default;
  private void Awake()
  {
    _rotationValue = Quaternion.Euler(30f, 45f, 0f);
  }

  private void OnEnable()
  {
    _damagePopUpEvent.OnEventRaised += Setup;
  }

  private void OnDisable()
  {
    _damagePopUpEvent.OnEventRaised -= Setup;
  }

  private void Setup(Vector3 pos, int damageValue)
  {
    pos.y += 2f;

    GameObject instance = Instantiate(_textPrefab, pos, _rotationValue);
    TextMeshPro textMesh = instance.GetComponent<TextMeshPro>();
    textMesh.SetText(damageValue.ToString());

    StartCoroutine(AnimateDamagePopup(instance, textMesh));
  }

  private IEnumerator AnimateDamagePopup(GameObject popup, TextMeshPro textMesh)
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
