// Decompiled with JetBrains decompiler
// Type: SRPG.QuestMissionItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class QuestMissionItem : MonoBehaviour
  {
    public GameParameter Star;
    public GameParameter FrameParam;
    public GameParameter IconParam;
    public GameParameter NameParam;
    public GameParameter AmountParam;
    public GameParameter ObjectigveParam;
    public GameParameter ProgressBadgeParam;
    public GameParameter ProgressCurrentParam;
    public GameParameter ProgressTargetParam;

    public QuestMissionItem()
    {
      base.\u002Ector();
    }

    public void SetGameParameterIndex(int index)
    {
      this.InternalSetGameParameterIndex(this.Star, index);
      this.InternalSetGameParameterIndex(this.FrameParam, index);
      this.InternalSetGameParameterIndex(this.IconParam, index);
      this.InternalSetGameParameterIndex(this.NameParam, index);
      this.InternalSetGameParameterIndex(this.AmountParam, index);
      this.InternalSetGameParameterIndex(this.ObjectigveParam, index);
      this.InternalSetGameParameterIndex(this.ProgressBadgeParam, index);
      this.InternalSetGameParameterIndex(this.ProgressCurrentParam, index);
      this.InternalSetGameParameterIndex(this.ProgressTargetParam, index);
    }

    private void InternalSetGameParameterIndex(GameParameter target, int index)
    {
      if (Object.op_Equality((Object) target, (Object) null))
        return;
      target.Index = index;
    }
  }
}
