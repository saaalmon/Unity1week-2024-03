using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace Game
{
  public class GameMainView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _comboText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _stressText;
    [SerializeField]
    private Image _stressPanel;

    public void SetCombo(int combo)
    {
      var seq = DOTween.Sequence()
      .OnStart(() =>
      {
        if (combo > 0) _comboText.text = combo.ToString() + " Combo";
        else _comboText.text = "";

        _comboText.transform.localScale = Vector3.one * 1.5f;
      })
      .Append(_comboText.transform.DOScale(1.0f, 0.2f))
      .Play();
    }

    public void SetScore(int score)
    {
      var seq = DOTween.Sequence()
      .OnStart(() =>
      {
        _scoreText.text = score.ToString("D6");
        _scoreText.transform.localScale = Vector3.one * 1.5f;
      })
      .Append(_scoreText.transform.DOScale(1.0f, 0.2f))
      .Play();
    }

    public void SetStress(string text)
    {
      var seq = DOTween.Sequence()
      .OnStart(() =>
      {
        _stressText.text = text.ToString();
        _stressPanel.transform.localScale = Vector3.zero;

        _stressPanel.color = Color.white;
        _stressText.alpha = 1.0f;
      })
      .Append(_stressPanel.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutQuint))
      .Append(_stressPanel.transform.DOScale(1.0f, 0.3f))
      .Append(_stressPanel.DOFade(0.0f, 0.1f))
      .Join(_stressText.DOFade(0.0f, 0.1f))
      .Play();
    }
  }
}