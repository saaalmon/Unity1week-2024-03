using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundMasterData")]
public class SoundMasterData : ScriptableObject
{
  public SoundData[] SoundDatas;
}
