using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameMainPresenter : MonoBehaviour
{
  [SerializeField]
  private MoneyManager _moneyManager;

  [SerializeField]
  private GameMainView _view;

  // Start is called before the first frame update
  void Start()
  {
    _moneyManager.Money
    .Subscribe(x =>
    {
      _view.SetMoney(x);
    })
    .AddTo(this);
  }
}
