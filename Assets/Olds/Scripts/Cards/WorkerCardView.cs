using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerCardView : MonoBehaviour
{
  [SerializeField]
  private Button _workerCardButton;
  [SerializeField]
  private Image _iconImage;
  [SerializeField]
  private TextMeshProUGUI _nameText;
  [SerializeField]
  private Image _motivGauge;
  [SerializeField]
  private Image _motivGaugeAmount;

  public Button WorkerCardButton => _workerCardButton;

  public void Init(WorkerData data)
  {
    _iconImage.sprite = data.Icon;
    _nameText.text = data.Name;
    _motivGaugeAmount.fillAmount = data.MotivMax;
  }

  public void SetMotiv(float motiv, float motivMax)
  {
    _motivGaugeAmount.fillAmount = (float)motiv / motivMax;
  }

  public void SetMotivActive(bool isActive)
  {
    _motivGauge.gameObject.SetActive(isActive);
  }
}
