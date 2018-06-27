// Decompiled with JetBrains decompiler
// Type: PartyUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;

public static class PartyUtility
{
  public static List<PartyEditData> LoadTeamPresets(PlayerPartyTypes partyType, out int lastSelectionIndex)
  {
    return PartyUtility.LoadTeamPresets(partyType.ToEditPartyType(), out lastSelectionIndex);
  }

  public static List<PartyEditData> LoadTeamPresets(PartyWindow2.EditPartyTypes partyType, out int lastSelectionIndex)
  {
    lastSelectionIndex = -1;
    List<PartyEditData> partyEditDataList = (List<PartyEditData>) null;
    string currentTeamId = PartyUtility.GetCurrentTeamID(partyType);
    if (PlayerPrefsUtility.HasKey(currentTeamId))
    {
      partyEditDataList = new List<PartyEditData>();
      PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[(int) partyType.ToPlayerPartyType()];
      UnitData[] src = new UnitData[party.MAX_UNIT];
      string str = PlayerPrefsUtility.GetString(currentTeamId, string.Empty);
      DebugUtility.Log(str);
      try
      {
        PartyUtility.JSON_TeamSettings jsonObject = JSONParser.parseJSONObject<PartyUtility.JSON_TeamSettings>(str);
        for (int index1 = 0; index1 < jsonObject.t.Length; ++index1)
        {
          PartyUtility.JSON_Team jsonTeam = jsonObject.t[index1];
          for (int index2 = 0; index2 < party.MAX_UNIT; ++index2)
          {
            if (index2 >= jsonTeam.u.Length)
            {
              src[index2] = (UnitData) null;
            }
            else
            {
              UnitData unitData = jsonTeam.u[index2] == 0L ? (UnitData) null : MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(jsonTeam.u[index2]);
              src[index2] = unitData;
            }
          }
          string name = !string.IsNullOrEmpty(jsonObject.t[index1].name) ? jsonObject.t[index1].name : PartyUtility.CreateDefaultPartyNameFromIndex(index1);
          PartyEditData partyEditData = new PartyEditData(src, name, party);
          partyEditDataList.Add(partyEditData);
        }
        lastSelectionIndex = jsonObject.n;
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }
    return partyEditDataList;
  }

  public static void SaveTeamPresets(PartyWindow2.EditPartyTypes partyType, int teamIndex, List<PartyEditData> teams)
  {
    string currentTeamId = PartyUtility.GetCurrentTeamID(partyType);
    if (string.IsNullOrEmpty(currentTeamId) || teamIndex < 0 || teamIndex >= teams.Count)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{\"n\":");
    stringBuilder.Append(teamIndex);
    stringBuilder.Append(",\"t\":[");
    for (int index1 = 0; index1 < teams.Count; ++index1)
    {
      if (index1 > 0)
        stringBuilder.Append(",");
      stringBuilder.Append("{\"name\":\"");
      stringBuilder.Append(teams[index1].Name);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"u\":[");
      for (int index2 = 0; index2 < teams[teamIndex].PartyData.MAX_UNIT; ++index2)
      {
        long num = 0;
        if (index2 > 0)
          stringBuilder.Append(',');
        if (teams[index1].Units[index2] != null)
          num = teams[index1].Units[index2].UniqueID;
        stringBuilder.Append(num.ToString());
      }
      stringBuilder.Append("]}");
    }
    stringBuilder.Append("]}");
    DebugUtility.Log(stringBuilder.ToString());
    PlayerPrefsUtility.SetString(currentTeamId, stringBuilder.ToString(), true);
  }

  public static string CreateDefaultPartyNameFromIndex(int index)
  {
    return string.Format(LocalizedText.Get("sys.TEAMNAME"), (object) (index + 1));
  }

  public static string GetCurrentTeamID(PartyWindow2.EditPartyTypes partyType)
  {
    return PlayerPrefsUtility.PARTY_TEAM_PREFIX + partyType.ToString().ToUpper();
  }

  public static bool SetUnitIfEmptyParty(QuestParam quest, List<PartyEditData> teams, string[] units_list)
  {
    bool flag1 = false;
    List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
    for (int index = 0; index < teams.Count; ++index)
    {
      bool flag2 = PartyUtility.AutoSetLeaderUnit(quest, teams[index], units_list, units);
      flag1 = flag1 || flag2;
    }
    return flag1;
  }

  public static bool AutoSetLeaderUnit(QuestParam quest, PartyEditData party, string[] kyouseiUnitIds, List<UnitData> units)
  {
    if (party.Units[0] != null || units == null || units.Count <= 0)
      return false;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PartyUtility.\u003CAutoSetLeaderUnit\u003Ec__AnonStorey29B unitCAnonStorey29B = new PartyUtility.\u003CAutoSetLeaderUnit\u003Ec__AnonStorey29B();
    using (List<UnitData>.Enumerator enumerator = units.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        // ISSUE: reference to a compiler-generated field
        unitCAnonStorey29B.u = enumerator.Current;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated field
        if (unitCAnonStorey29B.u != null && (kyouseiUnitIds == null || -1 == Array.FindIndex<string>(kyouseiUnitIds, new Predicate<string>(unitCAnonStorey29B.\u003C\u003Em__263))) && (-1 == Array.FindIndex<UnitData>(party.Units, new Predicate<UnitData>(unitCAnonStorey29B.\u003C\u003Em__264)) && (!PartyUtility.IsHeloQuest(quest) || (int) unitCAnonStorey29B.u.UnitParam.hero == 0)))
        {
          // ISSUE: reference to a compiler-generated field
          party.Units[0] = unitCAnonStorey29B.u;
          return true;
        }
      }
    }
    return party.Units[0] != null;
  }

  public static bool IsHeloQuest(QuestParam quest)
  {
    if (quest.type != QuestTypes.Story && quest.type != QuestTypes.Free && quest.type != QuestTypes.Extra)
      return quest.type == QuestTypes.Character;
    return true;
  }

  public static bool IsTeamLegal(PlayerPartyTypes partyType, QuestParam quest, PartyEditData team)
  {
    return PartyUtility.IsTeamLegal(partyType.ToEditPartyType(), quest, team);
  }

  public static bool IsTeamLegal(PartyWindow2.EditPartyTypes partyType, QuestParam quest, PartyEditData team)
  {
    if (team.Units[0] == null)
      return false;
    for (int index1 = 0; index1 < team.Units.Length; ++index1)
    {
      if (team.Units[index1] != null)
      {
        for (int index2 = index1 + 1; index2 < team.Units.Length; ++index2)
        {
          if (team.Units[index1] == team.Units[index2])
            return false;
        }
      }
    }
    if (!PartyUtility.IsHeroesAvailable(partyType, quest))
    {
      for (int index = 0; index < team.Units.Length; ++index)
      {
        if (team.Units[index] != null && (int) team.Units[index].UnitParam.hero != 0)
          return false;
      }
    }
    return true;
  }

  public static bool IsHeroesAvailable(PartyWindow2.EditPartyTypes partyType, QuestParam quest)
  {
    if (partyType != PartyWindow2.EditPartyTypes.Normal)
      return true;
    if (quest != null)
      return quest.UseFixEditor;
    return false;
  }

  public static bool IsSoloStoryParty(QuestParam quest)
  {
    if (!PartyUtility.IsHeloQuest(quest))
      return false;
    QuestCondParam entryCondition = quest.EntryCondition;
    return quest.units.IsNotNull() && entryCondition != null && (entryCondition.unit != null && quest.units.Length == 1) && (entryCondition.unit.Length == 1 && quest.units.Get(0) == entryCondition.unit[0]);
  }

  public static bool IsSoloStoryOrEventParty(QuestParam quest)
  {
    QuestCondParam entryCondition = quest.EntryCondition;
    if (!quest.units.IsNotNull() || entryCondition == null || (entryCondition.unit == null || !quest.UseFixEditor))
      return false;
    if (quest.type != QuestTypes.Event)
      return quest.type == QuestTypes.Beginner;
    return true;
  }

  public static bool KyouseiUnitPartyEdit(QuestParam quest, PartyEditData edit)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PartyUtility.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey29C editCAnonStorey29C = new PartyUtility.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey29C();
    // ISSUE: reference to a compiler-generated field
    editCAnonStorey29C.edit = edit;
    bool flag = false;
    QuestCondParam entryCondition = quest.EntryCondition;
    string[] list = quest.units.GetList();
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PartyUtility.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey29D editCAnonStorey29D = new PartyUtility.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey29D();
    // ISSUE: reference to a compiler-generated field
    editCAnonStorey29D.\u003C\u003Ef__ref\u0024668 = editCAnonStorey29C;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    for (editCAnonStorey29D.i = 0; editCAnonStorey29D.i < editCAnonStorey29C.edit.Units.Length; ++editCAnonStorey29D.i)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (editCAnonStorey29C.edit.Units[editCAnonStorey29D.i] != null)
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.FindIndex<string>(list, new Predicate<string>(editCAnonStorey29D.\u003C\u003Em__265)) != -1)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          editCAnonStorey29C.edit.Units[editCAnonStorey29D.i] = (UnitData) null;
          flag = true;
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          if (Array.FindIndex<string>(entryCondition.unit, new Predicate<string>(editCAnonStorey29D.\u003C\u003Em__266)) == -1)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            editCAnonStorey29C.edit.Units[editCAnonStorey29D.i] = (UnitData) null;
            flag = true;
          }
        }
      }
    }
    return flag;
  }

  public static bool PartyUnitsRemoveHelo(PartyEditData party, string[] kyouseiUnitIds)
  {
    bool flag = false;
    for (int index = 0; index < party.Units.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyUtility.\u003CPartyUnitsRemoveHelo\u003Ec__AnonStorey29E heloCAnonStorey29E = new PartyUtility.\u003CPartyUnitsRemoveHelo\u003Ec__AnonStorey29E();
      if (party.Units[index] != null)
      {
        // ISSUE: reference to a compiler-generated field
        heloCAnonStorey29E.ptu = party.Units[index];
        // ISSUE: reference to a compiler-generated method
        if (-1 != Array.FindIndex<string>(kyouseiUnitIds, new Predicate<string>(heloCAnonStorey29E.\u003C\u003Em__267)))
        {
          party.Units[index] = (UnitData) null;
          flag = true;
        }
      }
    }
    return flag;
  }

  public static void ResetToDefaultTeamIfNeeded(PlayerPartyTypes partyType, QuestParam quest, List<PartyEditData> teams)
  {
    UnitData[] src = (UnitData[]) null;
    PlayerData player = MonoSingleton<GameManager>.Instance.Player;
    for (int index1 = 0; index1 < teams.Count; ++index1)
    {
      if (!PartyUtility.IsTeamLegal(partyType, quest, teams[index1]))
      {
        if (src == null)
        {
          PartyData partyOfType = player.FindPartyOfType(partyType);
          src = new UnitData[partyOfType.MAX_UNIT];
          for (int index2 = 0; index2 < src.Length; ++index2)
          {
            long unitUniqueId = partyOfType.GetUnitUniqueID(index2);
            if (unitUniqueId != 0L)
            {
              UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(unitUniqueId);
              if (unitDataByUniqueId != null)
                src[index2] = unitDataByUniqueId;
            }
          }
        }
        teams[index1].SetUnits(src);
      }
      if (quest != null && quest.UseFixEditor)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyUtility.\u003CResetToDefaultTeamIfNeeded\u003Ec__AnonStorey29F neededCAnonStorey29F = new PartyUtility.\u003CResetToDefaultTeamIfNeeded\u003Ec__AnonStorey29F();
        string[] list = quest.units.GetList();
        // ISSUE: reference to a compiler-generated field
        neededCAnonStorey29F.edit = teams[index1];
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyUtility.\u003CResetToDefaultTeamIfNeeded\u003Ec__AnonStorey2A0 neededCAnonStorey2A0 = new PartyUtility.\u003CResetToDefaultTeamIfNeeded\u003Ec__AnonStorey2A0();
        // ISSUE: reference to a compiler-generated field
        neededCAnonStorey2A0.\u003C\u003Ef__ref\u0024671 = neededCAnonStorey29F;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (neededCAnonStorey2A0.j = 0; neededCAnonStorey2A0.j < neededCAnonStorey29F.edit.Units.Length; ++neededCAnonStorey2A0.j)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (neededCAnonStorey29F.edit.Units[neededCAnonStorey2A0.j] != null && Array.FindIndex<string>(list, new Predicate<string>(neededCAnonStorey2A0.\u003C\u003Em__268)) != -1)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            neededCAnonStorey29F.edit.Units[neededCAnonStorey2A0.j] = (UnitData) null;
          }
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        neededCAnonStorey29F.edit.SetUnits(neededCAnonStorey29F.edit.Units);
        // ISSUE: reference to a compiler-generated field
        teams[index1] = neededCAnonStorey29F.edit;
      }
    }
  }

  public static PartyWindow2.EditPartyTypes GetEditPartyTypes(QuestParam quest)
  {
    switch (quest.type)
    {
      case QuestTypes.Multi:
        return PartyWindow2.EditPartyTypes.MP;
      case QuestTypes.Event:
      case QuestTypes.Gps:
      case QuestTypes.Beginner:
        return PartyWindow2.EditPartyTypes.Event;
      case QuestTypes.Character:
        return PartyWindow2.EditPartyTypes.Character;
      case QuestTypes.Tower:
        return PartyWindow2.EditPartyTypes.Tower;
      case QuestTypes.VersusFree:
      case QuestTypes.VersusRank:
        return PartyWindow2.EditPartyTypes.Versus;
      default:
        return PartyWindow2.EditPartyTypes.Auto;
    }
  }

  public static int RecommendTypeToComparatorOrder(GlobalVars.RecommendType targetType)
  {
    switch (targetType)
    {
      case GlobalVars.RecommendType.Total:
        return 0;
      case GlobalVars.RecommendType.Attack:
        return 2;
      case GlobalVars.RecommendType.Defence:
        return 3;
      case GlobalVars.RecommendType.Magic:
        return 4;
      case GlobalVars.RecommendType.Mind:
        return 5;
      case GlobalVars.RecommendType.Speed:
        return 6;
      case GlobalVars.RecommendType.AttackTypeSlash:
        return 7;
      case GlobalVars.RecommendType.AttackTypeStab:
        return 8;
      case GlobalVars.RecommendType.AttackTypeBlow:
        return 9;
      case GlobalVars.RecommendType.AttackTypeShot:
        return 10;
      case GlobalVars.RecommendType.AttackTypeMagic:
        return 11;
      case GlobalVars.RecommendType.AttackTypeNone:
        return 12;
      case GlobalVars.RecommendType.Hp:
        return 1;
      default:
        return 0;
    }
  }

  private class JSON_Team
  {
    public string name;
    public long[] u;
  }

  private class JSON_TeamSettings
  {
    public int n;
    public PartyUtility.JSON_Team[] t;
  }
}
