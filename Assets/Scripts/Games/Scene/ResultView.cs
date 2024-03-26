using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace Game
{
  public class ResultView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _resultScoreText;
    [SerializeField]
    private CanvasGroup _resultPanel;

    public void SetResultScore(int score)
    {
      var seq = DOTween.Sequence()
      .OnStart(() =>
      {
        _resultPanel.alpha = 0.0f;
      })
      .Append(_resultPanel.DOFade(1.0f, 0.5f))
      .Join(_resultScoreText.DOCounter(0, score, 0.5f))
      .Play();
    }
  }
}
