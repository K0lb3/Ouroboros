// Decompiled with JetBrains decompiler
// Type: SRPG.CondEffectText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class CondEffectText : MonoBehaviour
  {
    public RichBitmapText Text;

    public CondEffectText()
    {
      base.\u002Ector();
    }

    public void SetText(EUnitCondition condition)
    {
      if (!Object.op_Inequality((Object) this.Text, (Object) null))
        return;
      string str1 = condition.ToString();
      if (string.IsNullOrEmpty(str1))
        return;
      StringBuilder stringBuilder1 = GameUtility.GetStringBuilder();
      stringBuilder1.Append("quest.COND_");
      stringBuilder1.Append(str1);
      string str2 = LocalizedText.Get(stringBuilder1.ToString());
      StringBuilder stringBuilder2 = GameUtility.GetStringBuilder();
      stringBuilder2.Append(str2);
      stringBuilder2.Append(' ');
      this.Text.BottomColor = GameSettings.Instance.FailCondition_TextBottomColor;
      this.Text.TopColor = GameSettings.Instance.FailCondition_TextTopColor;
      this.Text.text = stringBuilder2.ToString();
    }
  }
}
