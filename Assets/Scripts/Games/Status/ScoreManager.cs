using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class ScoreManager : MonoBehaviour
  {
    public static ScoreManager _instance;

    public IReadOnlyReactiveProperty<int> Score => _score;
    private IntReactiveProperty _score = new IntReactiveProperty(0);

    public void Init()
    {
      _instance = this;
      _score.Value = 0;
    }

    public void Set(int score = 0)
    {
      _score.Value = score;
    }

    public void Add(int score = 1)
    {
      _score.Value += score;
    }

    public void Sub(int score = 1)
    {
      _score.Value -= score;
    }
  }
}
