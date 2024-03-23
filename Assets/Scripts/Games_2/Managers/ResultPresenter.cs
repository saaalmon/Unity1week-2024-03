using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class ResultPresenter : MonoBehaviour
  {
    [SerializeField]
    private GameMainManager _gameManager;
    [SerializeField]
    private ResultView _view;

    // Start is called before the first frame update
    void Start()
    {
      _gameManager.ResultSubject
      .Subscribe(x =>
      {
        _view.gameObject.SetActive(true);
        _view.SetResultScore(x);
      })
      .AddTo(this);
    }
  }
}
