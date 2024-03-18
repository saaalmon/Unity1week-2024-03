using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMainView : MonoBehaviour
{
  [SerializeField]
  private CanvasGroup _titleMainCanvas;
  [SerializeField]
  private Button _startButton;
  [SerializeField]
  private Button _configButton;
  [SerializeField]
  private Button _licenseButton;
  [SerializeField]
  private Button _returnButton;

  public Button StartButton => _startButton;
  public Button ConfigButton => _configButton;
  public Button LicenseButton => _licenseButton;
  public Button ReturnButton => _returnButton;

  public void Init()
  {

  }

  public void Open()
  {
    _titleMainCanvas.interactable = true;
  }

  public void Close()
  {
    _titleMainCanvas.interactable = false;
  }
}
