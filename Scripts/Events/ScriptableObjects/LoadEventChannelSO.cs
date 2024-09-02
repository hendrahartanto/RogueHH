using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Load Event Channel")]
public class LoadEventChannelSO : ScriptableObject
{
  public UnityAction<SceneSO, bool, bool> OnLoadingRequested;

  public void RaiseEvent(SceneSO locationToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
  {
    if (OnLoadingRequested != null)
    {
      OnLoadingRequested.Invoke(locationToLoad, showLoadingScreen, fadeScreen);
    }
    else
    {
      Debug.LogWarning("Load Event Channel");
    }
  }
}
