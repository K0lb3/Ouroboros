// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.MatchOutcome
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Multiplayer
{
  public class MatchOutcome
  {
    private List<string> mParticipantIds = new List<string>();
    private Dictionary<string, uint> mPlacements = new Dictionary<string, uint>();
    private Dictionary<string, MatchOutcome.ParticipantResult> mResults = new Dictionary<string, MatchOutcome.ParticipantResult>();
    public const uint PlacementUnset = 0;

    public void SetParticipantResult(string participantId, MatchOutcome.ParticipantResult result, uint placement)
    {
      if (!this.mParticipantIds.Contains(participantId))
        this.mParticipantIds.Add(participantId);
      this.mPlacements[participantId] = placement;
      this.mResults[participantId] = result;
    }

    public void SetParticipantResult(string participantId, MatchOutcome.ParticipantResult result)
    {
      this.SetParticipantResult(participantId, result, 0U);
    }

    public void SetParticipantResult(string participantId, uint placement)
    {
      this.SetParticipantResult(participantId, MatchOutcome.ParticipantResult.Unset, placement);
    }

    public List<string> ParticipantIds
    {
      get
      {
        return this.mParticipantIds;
      }
    }

    public MatchOutcome.ParticipantResult GetResultFor(string participantId)
    {
      if (this.mResults.ContainsKey(participantId))
        return this.mResults[participantId];
      return MatchOutcome.ParticipantResult.Unset;
    }

    public uint GetPlacementFor(string participantId)
    {
      if (this.mPlacements.ContainsKey(participantId))
        return this.mPlacements[participantId];
      return 0;
    }

    public override string ToString()
    {
      string str = "[MatchOutcome";
      using (List<string>.Enumerator enumerator = this.mParticipantIds.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          str += string.Format(" {0}->({1},{2})", (object) current, (object) this.GetResultFor(current), (object) this.GetPlacementFor(current));
        }
      }
      return str + "]";
    }

    public enum ParticipantResult
    {
      Unset = -1,
      None = 0,
      Win = 1,
      Loss = 2,
      Tie = 3,
    }
  }
}
