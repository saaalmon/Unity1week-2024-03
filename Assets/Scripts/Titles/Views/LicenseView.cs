using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LicenseView : MonoBehaviour
{
  [SerializeField]
  private CanvasGroup _licenseCanvas;
  [SerializeField]
  private Image _backImage;
  [SerializeField]
  private TextMeshProUGUI _licenseMainText;
  [SerializeField]
  private TextMeshProUGUI _licenseText;
  [SerializeField]
  private Button _returnButton;

  public Button ReturnButton => _returnButton;

  public void Init(LicenseModel licenseModel)
  {
    _licenseMainText.text = licenseModel._licenseMainText;
    _licenseText.text = licenseModel._licenseText;
  }

  public void Open()
  {
    var seq = DOTween.Sequence()
    .OnStart(() =>
    {
      _licenseCanvas.interactable = true;
      _returnButton.Select();

      _licenseCanvas.alpha = 0f;
      _backImage.transform.localScale = Vector3.zero;
    })
    .Append(_licenseCanvas.DOFade(1.0f, 0.2f))
    .Join(_backImage.transform.DOScale(1.0f, 0.2f))
    .Play();

    Debug.Log("開く");
  }

  public void Close()
  {
    var seq = DOTween.Sequence()
    .OnStart(() =>
    {
      _licenseCanvas.interactable = false;
    })
    .Append(_licenseCanvas.DOFade(0.0f, 0.2f))
    .Join(_backImage.transform.DOScale(0.0f, 0.2f))
    .OnComplete(() =>
    {
      _licenseCanvas.alpha = 0f;
      _backImage.transform.localScale = Vector3.zero;

      this.gameObject.SetActive(false);
    })
    .Play();

    Debug.Log("閉じる");
  }
}
