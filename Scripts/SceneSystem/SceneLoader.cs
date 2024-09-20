using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField] private SceneSO _gameplayScene = default;

  [Header("Listening to")]
  [SerializeField] private LoadEventChannelSO _coldStartupChannel = default;

  [Header("Broadcasting to")]
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;
  private void OnEnable()
  {
    _coldStartupChannel.OnLoadingRequested += ColdLocationStartup;
  }

  private void OnDisable()
  {
    _coldStartupChannel.OnLoadingRequested -= ColdLocationStartup;
  }

  private void ColdLocationStartup(SceneSO coldStartupLocation, bool showLoadingScreen, bool fadeScreen)
  {
    //parameter scene yang diterima hanya untuk cek apakah scene tersebut adalah location
    if (coldStartupLocation.sceneType == SceneSO.SceneType.Location)
    {
      _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnGameplaySceneReady;
    }
  }

  private void OnGameplaySceneReady(AsyncOperationHandle<SceneInstance> instance)
  {
    OnSceneReady();
  }

  private void OnSceneReady()
  {
    //saat scene ready spawn player dan musuh
    _onSceneReady.RaiseEvent();
  }
}
