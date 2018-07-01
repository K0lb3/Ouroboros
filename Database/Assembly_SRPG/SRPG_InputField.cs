// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_InputField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/InputField (SRPG)")]
  public class SRPG_InputField : InputField
  {
    public SRPG_InputField()
    {
      base.\u002Ector();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
      ((Selectable) this).OnPointerEnter(eventData);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
      ((Selectable) this).OnPointerExit(eventData);
    }

    public virtual void OnUpdateSelected(BaseEventData eventData)
    {
      base.OnUpdateSelected(eventData);
    }

    private bool GetMouseButtonDown()
    {
      if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        return Input.GetMouseButtonDown(2);
      return true;
    }

    public virtual void ForceSetText(string text)
    {
      if (this.get_characterLimit() > 0 && text.Length > this.get_characterLimit())
        text = text.Substring(0, this.get_characterLimit());
      this.m_Text = (__Null) text;
      if (this.m_Keyboard != null)
        ((TouchScreenKeyboard) this.m_Keyboard).set_text((string) this.m_Text);
      if (this.m_CaretPosition > ((string) this.m_Text).Length)
        this.m_CaretPosition = (__Null) (int) (this.m_CaretSelectPosition = (__Null) ((string) this.m_Text).Length);
      if (this.get_onValueChanged() != null)
        ((UnityEvent<string>) this.get_onValueChanged()).Invoke(text);
      this.UpdateLabel();
    }
  }
}
