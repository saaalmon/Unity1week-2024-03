using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class TitlePresenter : MonoBehaviour
  {
    [SerializeField]
    private TitleView _view;

    // Start is called before the first frame update
    void Start()
    {
      _view.Init();

      _view.BgmSlider.OnValueChangedAsObservable()
      .Subscribe(x =>
      {
        SoundManager._instance?.SetBgmVolume(x);
      })
      .AddTo(this);

      _view.SeSlider.OnValueChangedAsObservable()
      .Subscribe(x =>
      {
        SoundManager._instance?.SetSeVolume(x);
      })
      .AddTo(this);
    }
  }
}
