using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class GameMainPresenter : MonoBehaviour
  {
    [SerializeField]
    private ComboManager _comboManager;
    [SerializeField]
    private ScoreManager _scoreManager;

    [SerializeField]
    private GameMainView _view;

    // Start is called before the first frame update
    void Start()
    {
      _comboManager.Combo
      .Subscribe(x =>
      {
        _view.SetCombo(x);
      })
      .AddTo(this);

      _scoreManager.Score
      .Subscribe(x =>
      {
        _view.SetScore(x);
      })
      .AddTo(this);
    }
  }
}