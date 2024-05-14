using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MoneyManager : MonoBehaviour
{
  [SerializeField]
  private int _moneyMax;

  public static MoneyManager _instance;

  public IReadOnlyReactiveProperty<int> Money => _money;
  private IntReactiveProperty _money = new IntReactiveProperty(0);

  public void Init()
  {
    _instance = this;
    _money.Value = 0;
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
