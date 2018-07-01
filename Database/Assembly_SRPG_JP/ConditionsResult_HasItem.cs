// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_HasItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ConditionsResult_HasItem : ConditionsResult
  {
    public ConditionsResult_HasItem(string iname, int condsItemNum)
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
      if (itemDataByItemId != null)
        this.mCurrentValue = itemDataByItemId.Num;
      this.mTargetValue = condsItemNum;
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
        return LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
      }
    }
  }
}
