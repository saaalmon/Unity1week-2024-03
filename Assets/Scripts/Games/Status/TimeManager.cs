using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class TimeManager : MonoBehaviour
  {
    [SerializeField]
    private float _timerMax;

    public TimeManager _instance;

    public IReadOnlyReactiveProperty<float> Timer => _timer;
    private FloatReactiveProperty _timer = new FloatReactiveProperty(0);

    public void Init()
    {
      _instance = this;
      _timer.Value = _timerMax;
    }

    public void Set(float timer = 0)
    {
      _timer.Value = timer;
    }

    public void Add(float timer = 1)
    {
      _timer.Value += timer;
    }

    public void Sub(float timer = 1)
    {
      _timer.Value -= timer;
    }
  }
}
