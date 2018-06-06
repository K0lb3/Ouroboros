// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropItemList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
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
      if (dataOfClass == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Transform transform = ((Component) this).get_transform();
      if (dataOfClass.dropItems == null)
        return;
      for (int index = 0; index < dataOfClass.dropItems.Length; ++index)
      {
        ItemParam itemParam = instance.GetItemParam(dataOfClass.dropItems[index]);
        if (itemParam != null)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          gameObject.get_transform().SetParent(transform, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
