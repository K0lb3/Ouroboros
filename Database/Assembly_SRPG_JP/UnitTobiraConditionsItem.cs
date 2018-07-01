// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraConditionsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitTobiraConditionsItem : MonoBehaviour
  {
    [SerializeField]
    private ImageArray m_ClearIcon;
    [SerializeField]
    private Text m_ConditionsText;
    [SerializeField]
    private UnitTobiraConditionsItem.TextColor m_TextColor;

    public UnitTobiraConditionsItem()
    {
      base.\u002Ector();
    }

    public void Setup(ConditionsResult conds)
    {
      if (conds == null)
      {
        this.SetConditionsText(LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING"));
        this.SetClearIcon(true);
      }
      else
      {
        this.SetConditionsText(conds.text);
        this.SetClearIcon(conds.isClear);
      }
    }

    public void SetConditionsText(string text)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ConditionsText, (UnityEngine.Object) null))
        return;
      this.m_ConditionsText.set_text(text);
    }

    public void SetClearIcon(bool isClear)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ClearIcon, (UnityEngine.Object) null))
        return;
      if (isClear)
      {
        this.m_ClearIcon.ImageIndex = 1;
        ((Graphic) this.m_ConditionsText).set_color(this.m_TextColor.m_Clear);
      }
      else
      {
        this.m_ClearIcon.ImageIndex = 0;
        ((Graphic) this.m_ConditionsText).set_color(this.m_TextColor.m_NotClear);
      }
    }

    [Serializable]
    private class TextColor
    {
      public Color m_Clear = Color.get_black();
      public Color m_NotClear = Color.get_black();
    }
  }
}
