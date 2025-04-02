using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneSO : ScriptableObject
{
  public string SceneName;
  public SceneType sceneType;
  public AssetReference sceneReference;
  public AudioCueSO musicTrack;
  public bool isMainMenu = false;

  public enum SceneType
  {
    Location,
    Initialize,
    Gameplay,
    Menu,
    Initializer
  }
}
