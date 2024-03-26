using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using unityroom.Api;

namespace Game
{
  public class ResultPresenter : MonoBehaviour
  {
    [SerializeField]
    private GameMainManager _gameManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private ResultView _view;

    // Start is called before the first frame update
    void Start()
    {
      _gameManager.ResultSubject
      .Subscribe(_ =>
      {
        UnityroomApiClient.Instance.SendScore(1, _scoreManager.Score.Value, ScoreboardWriteMode.Always);
        _view.SetResultScore(_scoreManager.Score.Value);
      })
      .AddTo(this);
    }
  }
}
