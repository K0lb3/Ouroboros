// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayUnitRank
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MultiPlayUnitRank : MonoBehaviour
  {
    public GameObject ItemTemplate;
    public GameObject Parent;
    public GameObject Root;
    public GameObject NotDataObj;
    public GameObject DataObj;

    public MultiPlayUnitRank()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<MultiRanking> multiUnitRank = instance.MultiUnitRank;
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), instance.FindQuest(GlobalVars.SelectedQuestID));
      if (multiUnitRank == null || multiUnitRank.Count == 0)
      {
        if (Object.op_Inequality((Object) this.NotDataObj, (Object) null))
          this.NotDataObj.get_gameObject().SetActive(true);
        if (Object.op_Inequality((Object) this.DataObj, (Object) null))
          this.DataObj.get_gameObject().SetActive(false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.NotDataObj, (Object) null))
          this.NotDataObj.get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.DataObj, (Object) null))
          this.DataObj.get_gameObject().SetActive(true);
        for (int rank = 0; rank < multiUnitRank.Count; ++rank)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            UnitParam unitParam = instance.GetUnitParam(multiUnitRank[rank].unit);
            UnitData data = new UnitData();
            data.Setup(multiUnitRank[rank].unit, 0, 1, 0, multiUnitRank[rank].job, 1, unitParam.element);
            DataSource.Bind<UnitData>(gameObject, data);
            this.SetParam(gameObject, rank, multiUnitRank[rank]);
            if (Object.op_Inequality((Object) this.Parent, (Object) null))
              gameObject.get_transform().SetParent(this.Parent.get_transform(), false);
            gameObject.get_gameObject().SetActive(true);
          }
        }
      }
      if (!Object.op_Inequality((Object) this.Root, (Object) null))
        return;
      GameParameter.UpdateAll(this.Root);
    }

    private void SetParam(GameObject item, int rank, MultiRanking param)
    {
      Transform child1 = item.get_transform().FindChild("body");
      if (!Object.op_Inequality((Object) child1, (Object) null))
        return;
      Transform child2 = child1.FindChild(nameof (rank));
      if (Object.op_Inequality((Object) child2, (Object) null))
      {
        Transform child3 = child2.FindChild("rank_txt");
        if (Object.op_Inequality((Object) child3, (Object) null))
        {
          LText component = (LText) ((Component) child3).GetComponent<LText>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            string str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_RANK"), (object) (rank + 1).ToString());
            component.set_text(str);
          }
        }
      }
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(param.unit);
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(param.job);
      Transform child4 = child1.FindChild("name");
      if (!Object.op_Inequality((Object) child4, (Object) null))
        return;
      LText component1 = (LText) ((Component) child4).GetComponent<LText>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      string str1 = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_UNIT_NAME"), (object) unitParam.name, (object) jobParam.name);
      component1.set_text(str1);
    }
  }
}
