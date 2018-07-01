// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_TobiraNoConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ConditionsResult_TobiraNoConditions : ConditionsResult
  {
    public ConditionsResult_TobiraNoConditions()
    {
      this.mIsClear = true;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING");
      }
    }

    public override string errorText
    {
      get
      {
        return LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
      }
    }
  }
}
