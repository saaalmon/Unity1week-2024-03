using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class TitleView : MonoBehaviour
  {
    [SerializeField]
    private Slider _bgmSlider;
    [SerializeField]
    private Slider _seSlider;

    public Slider BgmSlider => _bgmSlider;
    public Slider SeSlider => _seSlider;
  }
}
