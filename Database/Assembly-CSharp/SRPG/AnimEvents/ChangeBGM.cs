// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ChangeBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
