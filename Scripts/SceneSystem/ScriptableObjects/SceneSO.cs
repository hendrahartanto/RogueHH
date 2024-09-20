using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneSO : ScriptableObject
{
  public SceneType sceneType;
  public AssetReference sceneReference;

  public enum SceneType
  {
    Location,
    Initialize,
    Gameplay,
    Menu
  }
}
