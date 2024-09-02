using UnityEngine;

public class SceneLoader : MonoBehaviour
{
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
      //TODO: load scene Gameplay secara synchronous, ditunggu hingga selesai baru mulai game (invoke channel onSceneLoaded supaya player bisa di spawn)
      //lebih jelasnya cek di referensi code UOP

      OnSceneReady();
    }
  }

  private void OnSceneReady()
  {
    //saat scene ready spawn player dan musuh
    _onSceneReady.RaiseEvent();
  }
}
