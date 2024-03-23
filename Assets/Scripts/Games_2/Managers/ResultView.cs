using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
  public class ResultView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _ResultScoreText;

    public void SetResultScore(int score)
    {
      _ResultScoreText.text = score.ToString();
    }
  }
}
