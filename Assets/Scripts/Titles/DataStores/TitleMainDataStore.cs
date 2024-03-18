using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TitleMainDataStore")]
public class TitleMainDataStore : ScriptableObject
{
  public LicenseEntity License;
  public ConfigEntity Config;
}
