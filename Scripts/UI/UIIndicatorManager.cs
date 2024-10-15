using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIIndicatorManager : MonoBehaviour
{
  public RectTransform indicatorParent;
  public GameObject indicatorPrefab;
  public Dictionary<int, GameObject> activeIndicators = new Dictionary<int, GameObject>();
  public float spacing = 70f;

  [Header("Listening on")]
  [SerializeField] private UpdateUIIndicatorChannelSO _updateUIIndicator = default;
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  private void OnEnable()
  {
    _updateUIIndicator.OnEventRaised += SetIndicator;
    _onSceneReady.OnEventRaised += ClearActiveIndicators;
  }

  private void OnDisable()
  {
    _updateUIIndicator.OnEventRaised -= SetIndicator;
    _onSceneReady.OnEventRaised -= ClearActiveIndicators;
  }

  private void ClearActiveIndicators()
  {
    foreach (KeyValuePair<int, GameObject> entry in activeIndicators)
    {
      int key = entry.Key;
      GameObject indicator = entry.Value;

      Destroy(indicator);
    }

    activeIndicators.Clear();
  }

  public void SetIndicator(SkillSO skillSO, int key)
  {
    if (!activeIndicators.ContainsKey(key))
    {
      GameObject newIndicator = Instantiate(indicatorPrefab, indicatorParent);

      RectTransform rectTransform = newIndicator.GetComponent<RectTransform>();
      rectTransform.anchorMin = new Vector2(0.5f, 1f);
      rectTransform.anchorMax = new Vector2(0.5f, 1f);
      rectTransform.pivot = new Vector2(0.5f, 1f);
      rectTransform.sizeDelta = new Vector2(70, 70);

      int childCount = indicatorParent.childCount;
      float spacing = 70f;
      int index = childCount - 1;

      int direction = (index % 2 == 0) ? -1 : 1;
      int offsetMultiplier = (index + 1) / 2;
      float xPosition = direction * offsetMultiplier * spacing;

      rectTransform.anchoredPosition = new Vector2(xPosition, 0);

      activeIndicators.Add(key, newIndicator);

      UpdateIndicatorPositions();
    }

    if (skillSO.CurrentActiveTime > 0)
    {
      TextMeshProUGUI text = activeIndicators[key].GetComponentInChildren<TextMeshProUGUI>();
      activeIndicators[key].GetComponentInChildren<Image>().sprite = skillSO.SkillIcon;
      text.SetText(skillSO.CurrentActiveTime.ToString());
    }
    else
    {
      RemoveIndicator(key);
    }
  }

  private void RemoveIndicator(int key)
  {
    Destroy(activeIndicators[key]);
    activeIndicators.Remove(key);
    UpdateIndicatorPositions();
  }

  private void UpdateIndicatorPositions()
  {
    int count = activeIndicators.Count;

    if (count == 0) return;

    float totalWidth = (count - 1) * spacing;

    List<GameObject> indicatorsList = new List<GameObject>(activeIndicators.Values);

    for (int i = 0; i < count; i++)
    {
      RectTransform rectTransform = indicatorsList[i].GetComponent<RectTransform>();

      float xPos = -totalWidth / 2 + i * spacing;

      rectTransform.anchoredPosition = new Vector2(xPos, 0);
    }
  }
}
