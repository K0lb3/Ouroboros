// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeQuestMilestone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Quests;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeQuestMilestone : BaseReferenceHolder, IQuestMilestone
  {
    internal NativeQuestMilestone(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    public string Id
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => QuestMilestone.QuestMilestone_Id(this.SelfPtr(), out_string, out_size)));
      }
    }

    public string EventId
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => QuestMilestone.QuestMilestone_EventId(this.SelfPtr(), out_string, out_size)));
      }
    }

    public string QuestId
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => QuestMilestone.QuestMilestone_QuestId(this.SelfPtr(), out_string, out_size)));
      }
    }

    public ulong CurrentCount
    {
      get
      {
        return QuestMilestone.QuestMilestone_CurrentCount(this.SelfPtr());
      }
    }

    public ulong TargetCount
    {
      get
      {
        return QuestMilestone.QuestMilestone_TargetCount(this.SelfPtr());
      }
    }

    public byte[] CompletionRewardData
    {
      get
      {
        return PInvokeUtilities.OutParamsToArray<byte>((PInvokeUtilities.OutMethod<byte>) ((out_bytes, out_size) => QuestMilestone.QuestMilestone_CompletionRewardData(this.SelfPtr(), out_bytes, out_size)));
      }
    }

    public MilestoneState State
    {
      get
      {
        Types.QuestMilestoneState questMilestoneState = QuestMilestone.QuestMilestone_State(this.SelfPtr());
        switch (questMilestoneState)
        {
          case Types.QuestMilestoneState.NOT_STARTED:
            return MilestoneState.NotStarted;
          case Types.QuestMilestoneState.NOT_COMPLETED:
            return MilestoneState.NotCompleted;
          case Types.QuestMilestoneState.COMPLETED_NOT_CLAIMED:
            return MilestoneState.CompletedNotClaimed;
          case Types.QuestMilestoneState.CLAIMED:
            return MilestoneState.Claimed;
          default:
            throw new InvalidOperationException("Unknown state: " + (object) questMilestoneState);
        }
      }
    }

    internal bool Valid()
    {
      return QuestMilestone.QuestMilestone_Valid(this.SelfPtr());
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      QuestMilestone.QuestMilestone_Dispose(selfPointer);
    }

    public override string ToString()
    {
      return string.Format("[NativeQuestMilestone: Id={0}, EventId={1}, QuestId={2}, CurrentCount={3}, TargetCount={4}, State={5}]", (object) this.Id, (object) this.EventId, (object) this.QuestId, (object) this.CurrentCount, (object) this.TargetCount, (object) this.State);
    }

    internal static NativeQuestMilestone FromPointer(IntPtr pointer)
    {
      if (pointer == IntPtr.Zero)
        return (NativeQuestMilestone) null;
      return new NativeQuestMilestone(pointer);
    }
  }
}
