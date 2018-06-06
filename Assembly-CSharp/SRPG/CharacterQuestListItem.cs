// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class CharacterQuestListItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject unitIcon1;
    [SerializeField]
    private GameObject unitIcon2;
    [SerializeField]
    private Text conditionText;

    public CharacterQuestListItem()
    {
      base.\u002Ector();
    }

    public void SetUp(UnitData unitData1, UnitData unitData2, QuestParam questParam)
    {
      if (Object.op_Inequality((Object) this.unitIcon1, (Object) null))
        DataSource.Bind<UnitData>(this.unitIcon1, unitData1);
      if (Object.op_Inequality((Object) this.unitIcon2, (Object) null))
        DataSource.Bind<UnitData>(this.unitIcon2, unitData2);
      if (unitData1 == null || unitData2 == null || (questParam == null || questParam == null))
        return;
      List<QuestParam> questParamList = questParam.DetectNotClearConditionQuests();
      if (questParamList == null || questParamList.Count <= 0)
        return;
      this.conditionText.set_text(string.Join(",", questParamList.ConvertAll<string>((Converter<QuestParam, string>) (q => q.name)).ToArray()) + "をクリア");
    }
  }
}
