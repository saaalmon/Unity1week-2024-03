using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class LicensePresenter : MonoBehaviour
{
  [SerializeField]
  private LicenseUseCase _licenseUseCase;
  [SerializeField]
  private LicenseView _licenseView;

  // Start is called before the first frame update
  void Start()
  {
    _licenseView.Init(_licenseUseCase.ShowLicense());

    _licenseView.OnEnableAsObservable()
    .Subscribe(_ =>
    {
      _licenseView.Open();
    })
    .AddTo(this);

    _licenseView.ReturnButton.OnClickAsObservable()
    .Subscribe(_ =>
    {
      _licenseView.Close();
    })
    .AddTo(this);
  }
}
