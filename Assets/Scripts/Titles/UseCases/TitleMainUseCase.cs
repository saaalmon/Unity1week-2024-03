using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMainUseCase : MonoBehaviour
{
  public void GameStart()
  {
    Debug.Log("開始");

    SceneManager.LoadScene("GameScene");
  }
}
