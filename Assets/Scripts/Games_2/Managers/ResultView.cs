using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Game
{
  public class ResultView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _ResultScoreText;
    [SerializeField]
    private CanvasGroup _ResultCanvas;

    public void SetResultCanvas()
    {
      var seq = DOTween.Sequence()
      .OnStart(() => _ResultCanvas.alpha = 0.0f)
      .Append(_ResultCanvas.DOFade(1.0f, 0.5f))
      .Play();
    }

    public void SetResultScore(int score)
    {
      var seq = DOTween.Sequence()
      .Append(_ResultScoreText.DOCounter(0, score, 0.5f))
      .Play();
    }
  }
}
