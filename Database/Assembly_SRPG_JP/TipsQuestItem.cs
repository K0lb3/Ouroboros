// Decompiled with JetBrains decompiler
// Type: SRPG.TipsQuestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TipsQuestItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject BadgeObj;
    [SerializeField]
    private GameObject CompleteObj;
    [SerializeField]
    private Text TitleTxt;
    [SerializeField]
    private Text NameTxt;
    public string Title;
    public string Name;
    public bool IsCompleted;

    public TipsQuestItem()
    {
      base.\u002Ector();
    }

    public void UpdateContent()
    {
      if (Object.op_Inequality((Object) this.BadgeObj, (Object) null))
        this.BadgeObj.SetActive(!this.IsCompleted);
      if (Object.op_Inequality((Object) this.CompleteObj, (Object) null))
        this.CompleteObj.SetActive(this.IsCompleted);
      if (Object.op_Inequality((Object) this.TitleTxt, (Object) null))
        this.TitleTxt.set_text(this.Title);
      if (!Object.op_Inequality((Object) this.NameTxt, (Object) null))
        return;
      this.NameTxt.set_text(this.Name);
    }
  }
}
