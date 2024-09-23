using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField] private SceneSO _gameplayScene = default;
  // [SerializeField] private InputReader _inputReader = default;
  private SceneInstance _gameplayManagerSceneinstance = new SceneInstance();
  private bool _isLoading = false;
  private SceneSO _sceneToLoad = default;
  private SceneSO _currentScene = default;
  private bool _showLoadingScreen = default;
  private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle = default;
  private AsyncOperationHandle<SceneInstance> _loadingOperationHandle = default;
  private float _fadeDuration = .5f;

  [Header("Listening to")]
  [SerializeField] private LoadEventChannelSO _coldStartupChannel = default;
  [SerializeField] private LoadEventChannelSO _loadLocationEvent = default;
  [SerializeField] private LoadEventChannelSO _loadMenuEvent = default;

  [Header("Broadcasting to")]
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;
  [SerializeField] private FadeEventChannelSO _fadeEvent = default;
  private void OnEnable()
  {
    _coldStartupChannel.OnLoadingRequested += ColdLocationStartup;
    _loadMenuEvent.OnLoadingRequested += LoadMenu;
  }

  private void OnDisable()
  {
    _coldStartupChannel.OnLoadingRequested -= ColdLocationStartup;
    _loadMenuEvent.OnLoadingRequested -= LoadMenu;
  }

  private void ColdLocationStartup(SceneSO coldStartupLocation, bool showLoadingScreen, bool fadeScreen)
  {
    //parameter scene yang diterima hanya untuk cek apakah scene tersebut adalah location
    _currentScene = coldStartupLocation;

    if (coldStartupLocation.sceneType == SceneSO.SceneType.Location)
    {
      _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
      _gameplayManagerLoadingOpHandle.Completed += OnGameplaySceneReady;
    }
  }

  private void LoadLoaction(SceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
  {

  }

  private void LoadMenu(SceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
  {
    if (_isLoading)
      return;

    _sceneToLoad = menuToLoad;
    _showLoadingScreen = showLoadingScreen;
    _isLoading = true;

    if (_gameplayManagerSceneinstance.Scene != null && _gameplayManagerSceneinstance.Scene.isLoaded)
      Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle, true);

    StartCoroutine(UnloadPreviousScene());
  }

  private IEnumerator UnloadPreviousScene()
  {
    // _inputReader.DisableAllInput();
    _fadeEvent.FadeIn(_fadeDuration);

    yield return new WaitForSeconds(_fadeDuration);

    if (_currentScene != null)
    {
      if (_currentScene.sceneReference.OperationHandle.IsValid())
      {
        _currentScene.sceneReference.UnLoadScene();
      }
      else
      {
        SceneManager.UnloadSceneAsync(_currentScene.sceneReference.editorAsset.name);
      }
    }

    LoadNewScene();
  }

  private void LoadNewScene()
  {
    if (_showLoadingScreen)
    {
      //TODO: not implemented
    }

    _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
    _loadingOperationHandle.Completed += OnNewSceneLoaded;

  }

  private void OnGameplaySceneReady(AsyncOperationHandle<SceneInstance> instance)
  {
    _gameplayManagerSceneinstance = instance.Result;
    OnSceneReady();
  }

  private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
  {
    _currentScene = _sceneToLoad;

    Scene s = obj.Result.Scene;
    SceneManager.SetActiveScene(s);
    //TODO: kasih lightprobes mungkin?

    _isLoading = false;

    if (_showLoadingScreen)
    {
      //TODO: not implemented
    }

    _fadeEvent.FadeOut(_fadeDuration);

    OnSceneReady();
  }

  private void OnSceneReady()
  {
    //saat scene ready spawn player dan musuh
    _onSceneReady.RaiseEvent();
  }
}
