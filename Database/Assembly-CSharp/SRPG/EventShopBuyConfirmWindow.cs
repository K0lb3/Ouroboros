// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuyConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class EventShopBuyConfirmWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform UnitLayoutParent;
    public GameObject UnitTemplate;
    public GameObject EnableEquipUnitWindow;
    public GameObject limited_item;
    public GameObject no_limited_item;
    public Text SoldNum;
    private List<GameObject> mUnits;

    public EventShopBuyConfirmWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null) && this.UnitTemplate.get_activeInHierarchy())
        this.UnitTemplate.SetActive(false);
      this.mUnits = new List<GameObject>(MonoSingleton<GameManager>.Instance.Player.Units.Count);
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
      if (Object.op_Equality((Object) this.UnitTemplate, (Object) null))
        return;
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].get_gameObject().SetActive(false);
      EventShopItem data1 = MonoSingleton<GameManager>.Instance.Player.GetEventShopData().items[GlobalVars.ShopBuyIndex];
      if (Object.op_Inequality((Object) this.limited_item, (Object) null))
        this.limited_item.SetActive(!data1.IsNotLimited);
      if (Object.op_Inequality((Object) this.no_limited_item, (Object) null))
        this.no_limited_item.SetActive(data1.IsNotLimited);
      if (Object.op_Inequality((Object) this.SoldNum, (Object) null))
        this.SoldNum.set_text(data1.remaining_num.ToString());
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data1.iname);
      if (Object.op_Inequality((Object) this.EnableEquipUnitWindow, (Object) null))
      {
        this.EnableEquipUnitWindow.get_gameObject().SetActive(false);
        int index1 = 0;
        for (int index2 = 0; index2 < units.Count; ++index2)
        {
          UnitData data2 = units[index2];
          bool flag = false;
          for (int index3 = 0; index3 < data2.Jobs.Length; ++index3)
          {
            JobData job = data2.Jobs[index3];
            if (job.IsActivated)
            {
              int equipSlotByItemId = job.FindEquipSlotByItemID(data1.iname);
              if (equipSlotByItemId != -1 && job.CheckEnableEquipSlot(equipSlotByItemId))
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            if (index1 >= this.mUnits.Count)
            {
              this.UnitTemplate.SetActive(true);
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitTemplate);
              gameObject.get_transform().SetParent((Transform) this.UnitLayoutParent, false);
              this.mUnits.Add(gameObject);
              this.UnitTemplate.SetActive(false);
            }
            GameObject gameObject1 = this.mUnits[index1].get_gameObject();
            DataSource.Bind<UnitData>(gameObject1, data2);
            gameObject1.SetActive(true);
            this.EnableEquipUnitWindow.get_gameObject().SetActive(true);
            ++index1;
          }
        }
      }
      DataSource.Bind<EventShopItem>(((Component) this).get_gameObject(), data1);
      DataSource.Bind<ItemData>(((Component) this).get_gameObject(), itemDataByItemId);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
