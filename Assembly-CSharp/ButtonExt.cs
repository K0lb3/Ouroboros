// Decompiled with JetBrains decompiler
// Type: ButtonExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Button))]
public class ButtonExt : MonoBehaviour
{
  private ButtonExt.ButtonClickEvent mOnClick;

  public ButtonExt()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    // ISSUE: method pointer
    ((UnityEvent) ((Button) ((Component) this).GetComponent<Button>()).get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClick)));
  }

  private void OnClick()
  {
    this.mOnClick(((Component) this).get_gameObject());
  }

  public void AddListener(ButtonExt.ButtonClickEvent listener)
  {
    this.mOnClick += listener;
  }

  public void RemoveListener(ButtonExt.ButtonClickEvent listener)
  {
    this.mOnClick -= listener;
  }

  public delegate void ButtonClickEvent(GameObject go);
}
