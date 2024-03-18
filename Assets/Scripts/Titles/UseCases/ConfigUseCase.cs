using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigUseCase : MonoBehaviour
{
  [SerializeField]
  private TitleMainRepository _titleMainRepository;
  [SerializeField]
  private ConfigModel _configModel;

  public ConfigModel ShowConfig()
  {
    return _configModel.Convert(_titleMainRepository.GetTitleMainDataStore());
  }
}
