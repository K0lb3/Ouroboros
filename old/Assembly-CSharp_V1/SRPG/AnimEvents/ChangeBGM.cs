// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ChangeBGM
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
        MonoSingleton<MySound>.Instance.PlayBGM(this.BgmId, (string) null);
    }
  }
}
