// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjectiveList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class TrophyObjectiveList : MonoBehaviour
  {
    public GameObject Item_Complete;
    public GameObject Item_Incomplete;

    public TrophyObjectiveList()
    {
      base.\u002Ector();
    }

    public virtual void Start()
    {
      if (Object.op_Inequality((Object) this.Item_Complete, (Object) null))
        this.Item_Complete.SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Incomplete, (Object) null))
        this.Item_Incomplete.SetActive(false);
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(dataOfClass);
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
        GameObject gameObject1 = !flag ? this.Item_Incomplete : this.Item_Complete;
        if (!Object.op_Equality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
          DataSource.Bind<TrophyObjectiveData>(gameObject2, data);
          gameObject2.get_transform().SetParent(transform, false);
          gameObject2.SetActive(true);
        }
      }
    }
  }
}
