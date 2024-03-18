using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LicenseEntity")]
public class LicenseEntity : ScriptableObject
{
  public string LicenseMainText;
  public string LicenseText;
}
