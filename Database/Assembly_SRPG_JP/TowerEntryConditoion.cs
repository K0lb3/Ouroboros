// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEntryConditoion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerEntryConditoion : MonoBehaviour, IGameParameter
  {
    public TowerEntryConditoion()
    {
      base.\u002Ector();
    }

    public void UpdateValue()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      List<string> entryQuestConditions = dataOfClass.GetEntryQuestConditions(true, true, true);
      int num = 0;
      if (dataOfClass.EntryCondition != null)
      {
        if (dataOfClass.EntryCondition.plvmin > 0)
          ++num;
        if (dataOfClass.EntryCondition.ulvmin > 0)
          ++num;
      }
      ((Component) this).get_gameObject().SetActive(entryQuestConditions.Count > num);
    }
  }
}
