using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TitleMainPresenter : MonoBehaviour
{
  [SerializeField]
  private TitleMainUseCase _titleMainUseCase;
  [SerializeField]
  private TitleMainView _titleMainView;
  [SerializeField]
  private ConfigView _configView;
  [SerializeField]
  private LicenseView _licenseView;

  // Start is called before the first frame update
  void Start()
  {
    _titleMainView.Init();

    _configView.ReturnButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _titleMainView.Open();
      _titleMainView.ConfigButton.Select();
    })
    .AddTo(_configView);

    _licenseView.ReturnButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _titleMainView.Open();
      _titleMainView.LicenseButton.Select();
    })
    .AddTo(_licenseView);

    _titleMainView.StartButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _titleMainView.Close();
      _titleMainUseCase.GameStart();
    })
    .AddTo(this);

    _titleMainView.ConfigButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _titleMainView.Close();
      _configView.gameObject.SetActive(true);
    })
    .AddTo(this);

    _titleMainView.LicenseButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _titleMainView.Close();
      _licenseView.gameObject.SetActive(true);
    })
    .AddTo(this);
  }
}
