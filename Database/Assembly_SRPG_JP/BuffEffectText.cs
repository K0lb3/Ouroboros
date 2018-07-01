// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffectText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class BuffEffectText : MonoBehaviour
  {
    public RichBitmapText Text;

    public BuffEffectText()
    {
      base.\u002Ector();
    }

    public void SetText(ParamTypes type, bool down)
    {
      if (!Object.op_Inequality((Object) this.Text, (Object) null))
        return;
      string str1 = type.ToString();
      if (string.IsNullOrEmpty(str1))
        return;
      StringBuilder stringBuilder1 = GameUtility.GetStringBuilder();
      stringBuilder1.Append("quest.BUFF_");
      stringBuilder1.Append(str1);
      string str2 = LocalizedText.Get(stringBuilder1.ToString());
      StringBuilder stringBuilder2 = GameUtility.GetStringBuilder();
      stringBuilder2.Append(str2);
      stringBuilder2.Append(' ');
      if (down)
      {
        stringBuilder2.Append(LocalizedText.Get("quest.EFF_DOWN"));
        this.Text.BottomColor = GameSettings.Instance.Debuff_TextBottomColor;
        this.Text.TopColor = GameSettings.Instance.Debuff_TextTopColor;
      }
      else
      {
        stringBuilder2.Append(LocalizedText.Get("quest.EFF_UP"));
        this.Text.BottomColor = GameSettings.Instance.Buff_TextBottomColor;
        this.Text.TopColor = GameSettings.Instance.Buff_TextTopColor;
      }
      this.Text.text = stringBuilder2.ToString();
    }
  }
}
