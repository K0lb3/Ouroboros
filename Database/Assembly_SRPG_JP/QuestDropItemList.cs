// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class QuestDropItemList : MonoBehaviour
  {
    public GameObject ItemTemplate;
    protected List<GameObject> mItems;

    public QuestDropItemList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    protected virtual void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        Object.Destroy((Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) QuestDropParam.Instance, (Object) null))
        return;
      List<BattleCore.DropItemParam> dropItemParamList = QuestDropParam.Instance.GetQuestDropItemParamList(dataOfClass.iname, GlobalVars.GetDropTableGeneratedDateTime());
      if (dropItemParamList == null)
        return;
      for (int index = 0; index < dropItemParamList.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = dropItemParamList[index];
        if (dropItemParam != null)
        {
          GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          if (dropItemParam.IsItem)
            DataSource.Bind<ItemParam>(root, dropItemParam.itemParam);
          else if (dropItemParam.IsConceptCard)
            DataSource.Bind<ConceptCardParam>(root, dropItemParam.conceptCardParam);
          root.get_transform().SetParent(((Component) this).get_transform(), false);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
      }
    }
  }
}
