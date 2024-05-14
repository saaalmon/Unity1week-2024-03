using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class HpManager : MonoBehaviour
  {
    [SerializeField]
    private float _hpMax;

    public static HpManager _instance;

    public IReadOnlyReactiveProperty<float> Hp => _hp;
    private FloatReactiveProperty _hp = new FloatReactiveProperty(0);

    public void Init()
    {
      _instance = this;
      _hp.Value = _hpMax;
    }

    public void Set(float hp = 0)
    {
      _hp.Value = hp;
    }

    public void Add(float hp = 1)
    {
      _hp.Value += hp;
    }

    public void Sub(float hp = 1)
    {
      _hp.Value -= hp;
    }
  }
}
