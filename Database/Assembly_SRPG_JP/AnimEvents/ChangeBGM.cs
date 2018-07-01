// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ChangeBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ChangeBGM : AnimEvent
  {
    public string BgmId = string.Empty;

    public override void OnStart(GameObject go)
    {
      if (string.IsNullOrEmpty(this.BgmId))
        SceneBattle.Instance.PlayBGM();
      else
        MonoSingleton<MySound>.Instance.PlayBGM(this.BgmId, (string) null, false);
    }
  }
}
