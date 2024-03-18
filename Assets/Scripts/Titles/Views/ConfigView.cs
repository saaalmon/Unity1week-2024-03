using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ConfigView : MonoBehaviour
{
  [SerializeField]
  private CanvasGroup _configCanvas;
  [SerializeField]
  private Image _backImage;
  [SerializeField]
  private TextMeshProUGUI _configMainText;
  [SerializeField]
  private TextMeshProUGUI _bgmMainText;
  [SerializeField]
  private Slider _bgmSlider;
  [SerializeField]
  private TextMeshProUGUI _seMainText;
  [SerializeField]
  private Slider _seSlider;
  [SerializeField]
  private Button _returnButton;

  public Slider BgmSlider => _bgmSlider;
  public Slider SeSlider => _seSlider;
  public Button ReturnButton => _returnButton;

  public void Init(ConfigModel configModel)
  {
    _configMainText.text = configModel._configMainText;
    _bgmMainText.text = configModel._bgmMainText;
    _bgmSlider.value = SoundManager._instance.BgmVol.Value;
    _seMainText.text = configModel._seMainText;
    _seSlider.value = SoundManager._instance.SeVol.Value;
  }

  public void Open()
  {
    var seq = DOTween.Sequence()
    .OnStart(() =>
    {
      _configCanvas.interactable = true;
      _returnButton.Select();

      _configCanvas.alpha = 0f;
      _backImage.transform.localScale = Vector3.zero;
    })
    .Append(_configCanvas.DOFade(1.0f, 0.2f))
    .Join(_backImage.transform.DOScale(1.0f, 0.2f))
    .Play();

    Debug.Log("開く");
  }

  public void Close()
  {
    var seq = DOTween.Sequence()
    .OnStart(() =>
    {
      _configCanvas.interactable = false;
    })
    .Append(_configCanvas.DOFade(0.0f, 0.2f))
    .Join(_backImage.transform.DOScale(0.0f, 0.2f))
    .OnComplete(() =>
    {
      _configCanvas.alpha = 0f;
      _backImage.transform.localScale = Vector3.zero;

      this.gameObject.SetActive(false);
    })
    .Play();

    Debug.Log("閉じる");
  }
}
