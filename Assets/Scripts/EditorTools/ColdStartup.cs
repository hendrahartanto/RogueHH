using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class ColdStartup : MonoBehaviour
{
  [SerializeField] private SceneSO _currentScene = default;
  [SerializeField] private SceneSO _initialize = default;
  [SerializeField] private AssetReference _coldStartupChannel = default;

  [Header("Broadcasting to")]
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  private bool isColdStartup = false;

  private void Awake()
  {
    string sceneName = _initialize.SceneName; // Assuming SceneSO contains the SceneName
    isColdStartup = !SceneManager.GetSceneByName(sceneName).isLoaded;
  }

  private void Start()
  {
    if (isColdStartup)
      _initialize.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
  }

  private void LoadEventChannel(AsyncOperationHandle<SceneInstance> instance)
  {
    _coldStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnhannelLoaded;
  }

  private void OnhannelLoaded(AsyncOperationHandle<LoadEventChannelSO> instance)
  {
    if (_currentScene != null)
    {
      instance.Result.RaiseEvent(_currentScene);
    }
    else
    {
      _onSceneReady.RaiseEvent();
    }
  }
}
