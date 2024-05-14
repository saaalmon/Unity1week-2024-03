using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [CreateAssetMenu(fileName = "FukidashiData", menuName = "ScriptableObjects/FukidashiData")]
  public class FukidashiData : ScriptableObject
  {
    public Sprite Fukidashi;
    public string Text;
  }
}
