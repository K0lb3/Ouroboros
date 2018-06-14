// Decompiled with JetBrains decompiler
// Type: SRPG.SwitchByConditions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class SwitchByConditions : MonoBehaviour
  {
    [SerializeField]
    public int lv;

    public SwitchByConditions()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.Player.Lv >= this.lv)
        return;
      ((Component) this).get_gameObject().SetActive(false);
    }
  }
}
