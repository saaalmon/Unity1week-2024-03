using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class ComboManager : MonoBehaviour
  {
    public static ComboManager _instance;

    public IReadOnlyReactiveProperty<int> Combo => _combo;
    private IntReactiveProperty _combo = new IntReactiveProperty(0);

    public void Init()
    {
      _instance = this;
      _combo.Value = 0;
    }

    public void Set(int combo = 0)
    {
      _combo.Value = combo;
    }

    public void Add(int combo = 1)
    {
      _combo.Value += combo;
    }

    public void Sub(int combo = 1)
    {
      _combo.Value -= combo;
    }
  }
}
