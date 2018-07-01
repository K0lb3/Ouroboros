// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_HasGold
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ConditionsResult_HasGold : ConditionsResult
  {
    public ConditionsResult_HasGold(int condsNum)
    {
      this.mCurrentValue = MonoSingleton<GameManager>.Instance.Player.Gold;
      this.mTargetValue = condsNum;
      this.mIsClear = this.mCurrentValue >= this.mTargetValue;
    }

    public override string text
    {
      get
      {
        return string.Empty;
      }
    }

    public override string errorText
    {
      get
      {
        return LocalizedText.Get("sys.GOLD_NOT_ENOUGH");
      }
    }
  }
}
