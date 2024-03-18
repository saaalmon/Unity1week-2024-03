using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseModel : MonoBehaviour
{
  public string _licenseMainText { get; private set; }
  public string _licenseText { get; private set; }

  public LicenseModel Convert(TitleMainDataStore dataStore)
  {
    _licenseMainText = dataStore.License.LicenseMainText;
    _licenseText = dataStore.License.LicenseText;

    return this;
  }
}
