using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Anchors/Transform")]
public class TransformAnchorSO : ScriptableObject
{
  public UnityAction OnAnchorProvided;
  [SerializeField] private Transform _value;
  public bool isSet = false;
  public Transform Value
  {
    get { return _value; }
  }

  public void Provide(Transform value)
  {
    if (value == null)
      return;

    _value = value;
    isSet = true;

    OnAnchorProvided?.Invoke();
  }

  private void OnDisable()
  {
    Unset();
  }

  public void Unset()
  {
    _value = null;
    isSet = false;
  }
}
