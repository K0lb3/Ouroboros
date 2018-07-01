// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStartUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class VersusStartUp : MonoBehaviour
  {
    public VersusStartUp()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      int lastSelectionIndex;
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PlayerPartyTypes.Versus, out lastSelectionIndex, false);
      if (teams == null || teams.Count <= lastSelectionIndex)
        return;
      PartyEditData partyEditData = teams[lastSelectionIndex];
      UnitData[] src = new UnitData[partyEditData.PartyData.MAX_UNIT];
      for (int index = 0; index < partyEditData.Units.Length && index < partyEditData.PartyData.VSWAITMEMBER_START; ++index)
        src[index] = partyEditData.Units[index];
      partyEditData.SetUnits(src);
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.Versus, lastSelectionIndex, teams, false);
    }
  }
}
