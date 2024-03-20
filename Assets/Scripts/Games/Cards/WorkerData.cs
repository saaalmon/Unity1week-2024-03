using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WorkerData")]
public class WorkerData : ScriptableObject
{
  public Sprite Icon;
  public string Name;

  public float MotivMax;   //やる気上限
  public float Interval;    //作業効率 
}
