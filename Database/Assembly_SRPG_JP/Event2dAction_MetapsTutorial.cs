// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MetapsTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("MetapsTutorial用", "チュートリアルのトラッキング埋め込み用", 5592405, 4473992)]
  public class Event2dAction_MetapsTutorial : EventAction
  {
    public string Point = string.Empty;

    public override void OnActivate()
    {
      if (!string.IsNullOrEmpty(this.Point))
      {
        try
        {
          MyMetaps.TrackTutorialPoint(this.Point);
        }
        catch
        {
        }
      }
      this.ActivateNext();
    }
  }
}
