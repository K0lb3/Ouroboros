// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjectiveBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class TrophyObjectiveBox : TrophyObjectiveList
  {
    public override void Start()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(dataOfClass, false);
      Transform transform = ((Component) this).get_transform();
      for (int index = 0; index < dataOfClass.Objectives.Length; ++index)
      {
        TrophyObjective objective = dataOfClass.Objectives[index];
        TrophyObjectiveData data = new TrophyObjectiveData();
        data.CountMax = objective.RequiredCount;
        data.Description = objective.GetDescription();
        data.Objective = objective;
        bool flag;
        if (index < trophyCounter.Count.Length)
        {
          data.Count = trophyCounter.Count[index];
          flag = objective.RequiredCount <= trophyCounter.Count[index];
        }
        else
          flag = false;
        GameObject gameObject = !flag ? this.Item_Incomplete : this.Item_Complete;
        if (!Object.op_Equality((Object) gameObject, (Object) null))
        {
          DataSource.Bind<TrophyObjectiveData>(gameObject, data);
          gameObject.get_transform().SetParent(transform, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
