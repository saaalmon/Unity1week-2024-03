using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class moneyManager : MonoBehaviour
{
  [SerializeField]
  private int _moneyMax;

  public IReadOnlyReactiveProperty<int> Money => _money;
  private IntReactiveProperty _money = new IntReactiveProperty(0);

  public void Init()
  {
    _money.Value = _moneyMax;
  }

  public void Set(int money)
  {
    _money.Value = money;
  }

  public void Add(int money)
  {
    _money.Value += money;
  }

  public void Sub(int money)
  {
    _money.Value -= money;
  }
}
