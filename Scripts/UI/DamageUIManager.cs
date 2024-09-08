using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUIManager : MonoBehaviour
{
  [SerializeField] GameObject _textPrefab = default;
  private TextMeshPro _textMesh = default;
  private Quaternion _rotationValue;

  [Header("Listening to")]
  [SerializeField] private Vector3_Int_EvetChanel _damagePopUpEvent = default;
  private void Awake()
  {
    _textMesh = _textPrefab.GetComponent<TextMeshPro>();
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
    //kasi variasi lokasi spawn
    pos.y += 2f; // + Random.Range(-0.2f, 0.2f);
    // pos.x += Random.Range(-0.3f, 0.3f);
    // pos.z += Random.Range(-0.3f, 0.3f);

    GameObject instance = Instantiate(_textPrefab, pos, _rotationValue);
    TextMeshPro textMesh = instance.GetComponent<TextMeshPro>();
    textMesh.SetText(damageValue.ToString());

    // Start animation coroutine
    StartCoroutine(AnimateDamagePopup(instance, textMesh));
  }

  private IEnumerator AnimateDamagePopup(GameObject popup, TextMeshPro textMesh)
  {
    float duration = 0.5f;
    Vector3 initialPosition = popup.transform.position;
    Vector3 targetPosition = initialPosition + new Vector3(0, 0.5f, 0); // Move upwards
    Color initialColor = textMesh.color;

    // Generate random curve direction
    float randomCurveX = Random.Range(-0.2f, 0.2f);
    float randomCurveZ = Random.Range(-0.2f, 0.2f);

    float elapsedTime = 0f;
    while (elapsedTime < duration)
    {
      // Move the popup upwards with a slight curve in x or z direction
      float t = elapsedTime / duration;
      Vector3 curvedPosition = Vector3.Lerp(initialPosition, targetPosition, t);
      curvedPosition.x += randomCurveX * t; // Add curve effect on x axis
      curvedPosition.z += randomCurveZ * t; // Add curve effect on z axis

      popup.transform.position = curvedPosition;

      // Fade out the text
      Color newColor = initialColor;
      newColor.a = Mathf.Lerp(1f, 0f, t);
      textMesh.color = newColor;

      elapsedTime += Time.deltaTime;
      yield return null;
    }

    // Ensure the text is fully transparent and destroy the object
    textMesh.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    Destroy(popup);
  }
}
