// Decompiled with JetBrains decompiler
// Type: SRPG.SwitchByConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
