// Decompiled with JetBrains decompiler
// Type: SRPG.SwitchByConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
