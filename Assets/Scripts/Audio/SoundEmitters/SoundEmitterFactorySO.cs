using UnityEngine;

[CreateAssetMenu(fileName = "SoundEmitterFactory", menuName = "Factory/SoundEmitterFactory")]
public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
{
  public SoundEmitter prefab = default;

  public override SoundEmitter Create()
  {
    return Instantiate(prefab);
  }
}
