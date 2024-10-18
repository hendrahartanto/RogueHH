using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneSO : ScriptableObject
{
  public SceneType sceneType;
  public AssetReference sceneReference;
  public AudioCueSO musicTrack;
  public bool isMainMenu = false;

  public enum SceneType
  {
    Location,
    Initialize,
    Gameplay,
    Menu
  }
}
