// Decompiled with JetBrains decompiler
// Type: SRPG.ReturnItemWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ReturnItemWindow : MonoBehaviour, IFlowInterface
  {
    public Transform ItemLayout;
    public GameObject ItemTemplate;
    public Text Title;
    public List<ItemData> ReturnItems;

    public ReturnItemWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      if (this.ReturnItems == null)
      {
        this.ReturnItems = GlobalVars.ReturnItems;
        GlobalVars.ReturnItems = (List<ItemData>) null;
      }
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.Title, (Object) null))
        this.Title.set_text(LocalizedText.Get("sys.RETURN_ITEM_TITLE"));
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.ReturnItems != null)
      {
        this.ItemTemplate.SetActive(true);
        for (int index = 0; index < this.ReturnItems.Count; ++index)
        {
          ItemData returnItem = this.ReturnItems[index];
          if (!string.IsNullOrEmpty(returnItem.ItemID) && returnItem.Num != 0)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
            gameObject.get_transform().SetParent(this.ItemLayout, false);
            DataSource.Bind<ItemData>(gameObject, returnItem);
            gameObject.SetActive(true);
          }
        }
        this.ItemTemplate.SetActive(false);
      }
      ((Behaviour) this).set_enabled(true);
    }
  }
}
