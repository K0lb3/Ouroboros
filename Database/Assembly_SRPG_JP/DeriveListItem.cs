// Decompiled with JetBrains decompiler
// Type: SRPG.DeriveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class DeriveListItem : MonoBehaviour
  {
    [SerializeField]
    [HeaderBar("▼派生先のスキル/アビリティの罫線")]
    private RectTransform m_DeriveLineV;
    [SerializeField]
    private RectTransform m_DeriveLineH;

    public DeriveListItem()
    {
      base.\u002Ector();
    }

    public void SetLineActive(bool lineActive, bool verticalActive)
    {
      GameUtility.SetGameObjectActive((Component) this.m_DeriveLineH, lineActive);
      if (lineActive)
        GameUtility.SetGameObjectActive((Component) this.m_DeriveLineV, verticalActive);
      else
        GameUtility.SetGameObjectActive((Component) this.m_DeriveLineV, false);
    }
  }
}
