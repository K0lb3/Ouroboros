// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      List<ItemParam> questDropList = QuestDropParam.Instance.GetQuestDropList(dataOfClass.iname, GlobalVars.GetDropTableGeneratedDateTime());
      if (questDropList == null)
        return;
      for (int index = 0; index < questDropList.Count; ++index)
      {
        ItemParam data = questDropList[index];
        if (data != null)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          DataSource.Bind<ItemParam>(gameObject, data);
          gameObject.get_transform().SetParent(((Component) this).get_transform(), false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
