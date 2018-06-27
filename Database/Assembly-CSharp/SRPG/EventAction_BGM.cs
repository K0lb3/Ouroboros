// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_BGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [EventActionInfo("BGM再生", "BGMを再生します", 7368789, 8947780)]
  public class EventAction_BGM : EventAction
  {
    public static readonly int DEMO_BGM_ST = 34;
    public static readonly int DEMO_BGM_ED = 99;
    public string BGM;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.BGM))
        MonoSingleton<MySound>.Instance.StopBGM();
      else
        MonoSingleton<MySound>.Instance.PlayBGM(this.BGM, (string) null, EventAction.IsUnManagedAssets(this.BGM, true));
      this.ActivateNext();
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (string.IsNullOrEmpty(this.BGM) || string.IsNullOrEmpty(this.BGM))
        return (string[]) null;
      return EventAction.GetUnManagedStreamAssets(new string[1]{ this.BGM }, true);
    }
  }
}
