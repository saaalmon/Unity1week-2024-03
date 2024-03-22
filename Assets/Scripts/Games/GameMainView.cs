using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMainView : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI _moneyText;

  public void SetMoney(int money)
  {
    _moneyText.text = money.ToString("D6");
  }
}
