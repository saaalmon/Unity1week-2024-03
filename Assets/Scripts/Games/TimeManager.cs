using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimeManager : MonoBehaviour
{
  [SerializeField]
  private float _timerMax;

  public IReadOnlyReactiveProperty<float> Timer => _timer;
  private FloatReactiveProperty _timer = new FloatReactiveProperty(0);

  public void Init()
  {
    _timer.Value = _timerMax;
  }

  public void Set(float timer)
  {
    _timer.Value = timer;
  }

  public void Add(float timer)
  {
    _timer.Value += timer;
  }

  public void Sub(float timer)
  {
    _timer.Value -= timer;
  }
}
