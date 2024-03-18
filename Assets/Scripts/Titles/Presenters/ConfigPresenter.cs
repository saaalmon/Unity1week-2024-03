using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ConfigPresenter : MonoBehaviour
{
  [SerializeField]
  private ConfigUseCase _configUseCase;
  [SerializeField]
  private ConfigView _configView;

  // Start is called before the first frame update
  void Start()
  {
    _configView.Init(_configUseCase.ShowConfig());

    _configView.OnEnableAsObservable()
    .Subscribe(_ =>
    {
      _configView.Open();
    })
    .AddTo(this);

    _configView.ReturnButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _configView.Close();
    })
    .AddTo(this);

    _configView.BgmSlider.OnValueChangedAsObservable()
    .Subscribe(x =>
    {
      SoundManager._instance?.SetBgmVolume(x);
    })
    .AddTo(this);

    _configView.SeSlider.OnValueChangedAsObservable()
    .Subscribe(x =>
    {
      SoundManager._instance?.SetSeVolume(x);
    })
    .AddTo(this);
  }
}
