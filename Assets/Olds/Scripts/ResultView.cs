using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultView : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI _ResultMoneyText;

  public void SetResultMoney(int money)
  {
    _ResultMoneyText.text = money.ToString();
  }
}
