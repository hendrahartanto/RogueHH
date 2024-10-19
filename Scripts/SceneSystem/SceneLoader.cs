using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField] private SceneSO _gameplayScene = default;
  [SerializeField] private InputReader _inputReader = default;
  private SceneInstance _gameplayManagerSceneinstance = new SceneInstance();
  private bool _isLoading = false;
  private SceneSO _sceneToLoad = default;
  private SceneSO _currentScene = default;
  private bool _showLoadingScreen = default;
  private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle = default;
  private AsyncOperationHandle<SceneInstance> _loadingOperationHandle = default;
  private float _fadeDuration = .5f;
  private bool _fromLocation = false;

  [Header("Listening to")]
  [SerializeField] private LoadEventChannelSO _coldStartupChannel = default;
  [SerializeField] private LoadEventChannelSO _loadLocationEvent = default;
  [SerializeField] private LoadEventChannelSO _loadMenuEvent = default;

  [Header("Broadcasting to")]
  [SerializeField] private FadeEventChannelSO _fadeEvent = default;
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;
  [SerializeField] private VoidEventChannelSO _onLocationReady = default;
  [SerializeField] private BoolEventChannelSO _setGameplayCanvasActiveEvent = default;
  [SerializeField] private RequestSaveableDataEventChannelSO _requestSaveableDataEvent = default;

  private void OnEnable()
  {
    _coldStartupChannel.OnLoadingRequested += ColdLocationStartup;
    _loadMenuEvent.OnLoadingRequested += LoadMenu;
    _loadLocationEvent.OnLoadingRequested += LoadLoaction;
  }

  private void OnDisable()
  {
    _coldStartupChannel.OnLoadingRequested -= ColdLocationStartup;
    _loadMenuEvent.OnLoadingRequested -= LoadMenu;
    _loadLocationEvent.OnLoadingRequested -= LoadLoaction;
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

    _onSceneReady.RaiseEvent();
  }

  private void LoadLoaction(SceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
  {
    if (_isLoading)
      return;

    _sceneToLoad = locationToLoad;
    _showLoadingScreen = showLoadingScreen;
    _isLoading = true;

    _fromLocation = !(_sceneToLoad.name == "UpgradeMenu");

    if (_gameplayManagerSceneinstance.Scene == null || !_gameplayManagerSceneinstance.Scene.isLoaded)
    {
      _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
      _gameplayManagerLoadingOpHandle.Completed += OnGameplayManagerLoaded;
    }
    else
    {
      StartCoroutine(UnloadPreviousScene());
    }
  }

  private void OnGameplayManagerLoaded(AsyncOperationHandle<SceneInstance> obj)
  {
    _gameplayManagerSceneinstance = _gameplayManagerLoadingOpHandle.Result;

    StartCoroutine(UnloadPreviousScene());
  }

  private void LoadMenu(SceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
  {
    if (_isLoading)
      return;

    //cek nama dari scene agar bisa divalidasi untuk save system
    if (menuToLoad.isMainMenu)
    {
      CharacterConfigSO playerData = _requestSaveableDataEvent.RequestPlayerData();
      List<UpgradableItemSO> upgradableItemDataList = _requestSaveableDataEvent.RequestUpgradableItemDataList();
      ExpSO expData = _requestSaveableDataEvent.RequestExpData();
      DungeonSO dungeonData = _requestSaveableDataEvent.RequestDungeonData();
      GoldSO goldData = _requestSaveableDataEvent.RequestGoldData();

      SaveSystem.SaveData(playerData, expData, upgradableItemDataList, dungeonData, goldData);
    }

    _sceneToLoad = menuToLoad;
    _showLoadingScreen = showLoadingScreen;
    _isLoading = true;

    if (_gameplayManagerSceneinstance.Scene != null && _gameplayManagerSceneinstance.Scene.isLoaded)
      Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle, true);

    StartCoroutine(UnloadPreviousScene());
  }

  private IEnumerator UnloadPreviousScene()
  {
    _inputReader.DisableAllInput();
    _fadeEvent.FadeIn(_fadeDuration);

    yield return new WaitForSeconds(_fadeDuration);


    if (_currentScene != null)
    {
      yield return Resources.UnloadUnusedAssets();

      if (_currentScene.sceneReference.OperationHandle.IsValid())
      {
        AsyncOperationHandle unloadHandle = _currentScene.sceneReference.UnLoadScene();
        yield return unloadHandle;
      }
      else
      {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(_currentScene.sceneReference.editorAsset.name);
        if (unloadOperation != null)
        {
          yield return unloadOperation;
        }
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
    _gameplayManagerSceneinstance = _gameplayManagerLoadingOpHandle.Result;
    _onSceneReady.RaiseEvent();
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

    _setGameplayCanvasActiveEvent.RaiseEvent(_fromLocation);

    _onSceneReady.RaiseEvent();

    if (_fromLocation)
      _onLocationReady.RaiseEvent();

    _fadeEvent.FadeOut(_fadeDuration);

    _inputReader.EnableGameplayInput();
  }

}
