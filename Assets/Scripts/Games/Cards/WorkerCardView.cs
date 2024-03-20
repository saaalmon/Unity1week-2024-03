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

  public Button WorkerCardButton => _workerCardButton;

  public void Init(WorkerData data)
  {
    _iconImage.sprite = data.Icon;
    _nameText.text = data.Name;
  }
}
