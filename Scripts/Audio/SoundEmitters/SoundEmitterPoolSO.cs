using UnityEngine;

[CreateAssetMenu(fileName = "SoundEmitterPool", menuName = "Pool/SoundEmitterPool")]
public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
{
  [SerializeField] private SoundEmitterFactorySO _factory;

  public override IFactory<SoundEmitter> Factory
  {
    get
    {
      return _factory;
    }
    set
    {
      _factory = value as SoundEmitterFactorySO;
    }
  }
}
