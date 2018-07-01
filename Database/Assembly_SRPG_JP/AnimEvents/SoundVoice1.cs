// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundVoice1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SoundVoice1 : AnimEvent
  {
    public string SheetName;
    public string CueID;

    public override void OnStart(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null) || string.IsNullOrEmpty(this.SheetName) || string.IsNullOrEmpty(this.CueID))
        return;
      CustomSound3 customSound3 = (CustomSound3) go.GetComponent<CustomSound3>();
      if (Object.op_Equality((Object) customSound3, (Object) null))
        customSound3 = (CustomSound3) go.AddComponent<CustomSound3>();
      customSound3.sheetName = this.SheetName;
      customSound3.cueID = this.CueID;
      customSound3.PlayFunction = CustomSound3.EPlayFunction.VoicePlay;
      customSound3.CueSheetHandlePlayCategory = MySound.EType.VOICE;
      customSound3.CueSheetHandlePlayLoopType = MySound.CueSheetHandle.ELoopFlag.DEFAULT;
      customSound3.StopOnPlay = false;
      customSound3.StopOnDisable = false;
      customSound3.StopSec = 0.0f;
      customSound3.DelayPlaySec = 0.0f;
      customSound3.PlayOnEnable = true;
      customSound3.Play();
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      CustomSound3 component = (CustomSound3) go.GetComponent<CustomSound3>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Object.Destroy((Object) component);
    }
  }
}
