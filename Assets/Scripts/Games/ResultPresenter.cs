using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ResultPresenter : MonoBehaviour
{
  [SerializeField]
  private GameManager _gameManager;
  [SerializeField]
  private ResultView _view;

  // Start is called before the first frame update
  void Start()
  {
    _gameManager.ResultSubject
    .Subscribe(x =>
    {
      _view.SetResultMoney(x);
    })
    .AddTo(this);
  }
}
