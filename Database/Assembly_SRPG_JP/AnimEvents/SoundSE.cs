// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundSE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SoundSE : AnimEvent
  {
    public CustomSound.EType SoundType;
    public string CueID;

    public override void OnStart(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null) || string.IsNullOrEmpty(this.CueID))
        return;
      CustomSound customSound = (CustomSound) go.GetComponent<CustomSound>();
      if (Object.op_Equality((Object) customSound, (Object) null))
        customSound = (CustomSound) go.AddComponent<CustomSound>();
      customSound.type = this.SoundType;
      customSound.cueID = this.CueID;
      customSound.LoopFlag = false;
      customSound.StopSec = 0.0f;
      customSound.PlayOnAwake = false;
      customSound.Play();
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      CustomSound component = (CustomSound) go.GetComponent<CustomSound>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Object.Destroy((Object) component);
    }
  }
}
