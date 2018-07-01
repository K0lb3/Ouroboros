// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRankItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerRankItem : MonoBehaviour
  {
    [SerializeField]
    private Text m_UserName;
    [SerializeField]
    private Text m_UserLv;
    [SerializeField]
    private Text m_Rank;
    [SerializeField]
    private Text m_Score;

    public TowerRankItem()
    {
      base.\u002Ector();
    }

    public void Setup(TowerResuponse.TowerRankParam rankData)
    {
      if (rankData == null)
        return;
      this.SetText(this.m_UserName, rankData.name);
      this.SetText(this.m_UserLv, rankData.lv);
      this.SetText(this.m_Rank, rankData.rank);
      this.SetText(this.m_Score, rankData.score);
    }

    private void SetText(Text text, int value)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(value.ToString());
    }

    private void SetText(Text text, string value)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(value);
    }
  }
}
