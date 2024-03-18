using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigModel : MonoBehaviour
{
  public string _configMainText { get; private set; }
  public string _bgmMainText { get; private set; }
  public string _seMainText { get; private set; }

  public ConfigModel Convert(TitleMainDataStore dataStore)
  {
    _configMainText = dataStore.Config.ConfigMainText;
    _bgmMainText = dataStore.Config.BgmMainText;
    _seMainText = dataStore.Config.SeMainText;

    return this;
  }
}
