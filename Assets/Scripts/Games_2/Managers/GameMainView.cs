using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
  public class GameMainView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _comboText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public void SetCombo(int combo)
    {
      if (combo > 0) _comboText.text = combo.ToString() + " Combo";
      else _comboText.text = "";
    }

    public void SetScore(int score)
    {
      _scoreText.text = score.ToString("D6");
    }
  }
}