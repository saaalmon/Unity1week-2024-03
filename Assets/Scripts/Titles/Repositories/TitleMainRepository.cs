using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMainRepository : MonoBehaviour
{
  [SerializeField]
  private TitleMainDataStore _titleMainDataStore;

  public TitleMainDataStore GetTitleMainDataStore()
  {
    return _titleMainDataStore;
  }
}
