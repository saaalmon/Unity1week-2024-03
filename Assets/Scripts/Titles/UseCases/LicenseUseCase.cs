using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseUseCase : MonoBehaviour
{
  [SerializeField]
  private TitleMainRepository _titleMainRepository;
  [SerializeField]
  private LicenseModel _licenseModel;

  public LicenseModel ShowLicense()
  {
    return _licenseModel.Convert(_titleMainRepository.GetTitleMainDataStore());
  }
}
