// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCompositeWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class UnitCompositeWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    private ItemParam mItemParam;
    private List<GameObject> mConsumeObjects;

    public UnitCompositeWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.get_activeInHierarchy())
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
      int cost = 0;
      bool is_ikkatsu = false;
      Dictionary<string, int> consumes = (Dictionary<string, int>) null;
      MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes);
      for (int index = 0; index < this.mConsumeObjects.Count; ++index)
        this.mConsumeObjects[index].get_gameObject().SetActive(false);
      if (consumes != null)
      {
        int index = 0;
        using (Dictionary<string, int>.Enumerator enumerator = consumes.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, int> current = enumerator.Current;
            if (index >= this.mConsumeObjects.Count)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
              gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
              this.mConsumeObjects.Add(gameObject);
            }
            GameObject mConsumeObject = this.mConsumeObjects[index];
            DataSource.Bind<ConsumeItemData>(mConsumeObject, new ConsumeItemData()
            {
              param = MonoSingleton<GameManager>.Instance.GetItemParam(current.Key),
              num = current.Value
            });
            mConsumeObject.SetActive(true);
            ++index;
          }
        }
      }
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), this.mItemParam);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
