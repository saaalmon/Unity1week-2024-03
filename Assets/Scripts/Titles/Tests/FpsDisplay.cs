using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
  private float fps;

  private void Start()
  {
    Application.targetFrameRate = 60;
  }

  private void Update()
  {
    fps = 1f / Time.deltaTime;
    Debug.Log(fps);
  }

}