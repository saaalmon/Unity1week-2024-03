using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : MonoBehaviour
{
  [SerializeField]
  private Image _motivGauge;

  public void SetMotiv(float motiv, float motivMax)
  {
    _motivGauge.fillAmount = (float)motiv / motivMax;
  }
}
