// Decompiled with JetBrains decompiler
// Type: SRPG.TipsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TipsItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject BadgeObj;
    [SerializeField]
    private GameObject CompleteObj;
    [SerializeField]
    private Text TitleObj;
    [SerializeField]
    private GameObject OverayImageObj;
    [SerializeField]
    private Button SelfButton;
    public string Title;
    public bool IsCompleted;
    public bool IsHidden;

    public TipsItem()
    {
      base.\u002Ector();
    }

    public void UpdateContent()
    {
      if (Object.op_Inequality((Object) this.BadgeObj, (Object) null))
        this.BadgeObj.SetActive(!this.IsHidden && !this.IsCompleted);
      if (Object.op_Inequality((Object) this.CompleteObj, (Object) null))
        this.CompleteObj.SetActive(!this.IsHidden && this.IsCompleted);
      if (Object.op_Inequality((Object) this.TitleObj, (Object) null))
        this.TitleObj.set_text(this.Title);
      if (Object.op_Inequality((Object) this.OverayImageObj, (Object) null))
        this.OverayImageObj.SetActive(this.IsHidden);
      if (!Object.op_Inequality((Object) this.SelfButton, (Object) null))
        return;
      ((Selectable) this.SelfButton).set_interactable(!this.IsHidden);
    }
  }
}
