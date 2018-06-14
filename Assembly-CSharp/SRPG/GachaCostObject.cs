// Decompiled with JetBrains decompiler
// Type: SRPG.GachaCostObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaCostObject : MonoBehaviour
  {
    private GameObject m_root;
    private GameObject m_ticket;
    private GameObject m_default;
    private GameObject m_default_bg;

    public GachaCostObject()
    {
      base.\u002Ector();
    }

    public GameObject RootObject
    {
      get
      {
        return this.m_root;
      }
      set
      {
        this.m_root = value;
      }
    }

    public GameObject TicketObject
    {
      get
      {
        return this.m_ticket;
      }
      set
      {
        this.m_ticket = value;
      }
    }

    public GameObject DefaultObject
    {
      get
      {
        return this.m_default;
      }
      set
      {
        this.m_default = value;
      }
    }

    public GameObject DefaultBGObject
    {
      get
      {
        return this.m_default_bg;
      }
      set
      {
        this.m_default_bg = value;
      }
    }

    public void Refresh()
    {
      this.UpdateCostData();
    }

    private void UpdateCostData()
    {
      Button component = (Button) this.m_root.GetComponent<Button>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      this.m_default.SetActive(false);
      this.m_ticket.SetActive(false);
      ((Selectable) component).set_interactable(true);
      GachaRequsetParam dataOfClass = DataSource.FindDataOfClass<GachaRequsetParam>(this.m_root, (GachaRequsetParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsTicketGacha)
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(dataOfClass.Ticket);
        if (itemDataByItemId == null)
          return;
        DataSource.Bind<ItemData>(this.m_ticket, itemDataByItemId);
        this.m_ticket.SetActive(true);
        GameParameter.UpdateAll(this.m_ticket);
        ((Selectable) component).set_interactable(itemDataByItemId.Num > 0);
      }
      else
      {
        ((ImageArray) this.m_default_bg.GetComponent<ImageArray>()).ImageIndex = !(dataOfClass.Type == "gold") ? 1 : 0;
        this.RefreshCostNum(this.m_default_bg, dataOfClass.Cost);
        this.m_default.SetActive(true);
      }
    }

    private bool RefreshCostNum(GameObject _root, int _cost = 0)
    {
      if (_cost <= 0)
        return false;
      int num1 = (int) Math.Log10(_cost <= 0 ? 1.0 : (double) _cost) + 1;
      int num2 = _cost;
      for (int index = 7; index > 0; --index)
      {
        string str = "num/value_" + Mathf.Pow(10f, (float) (index - 1)).ToString();
        Transform child = _root.get_transform().FindChild(str);
        if (num1 < index)
        {
          ((Component) child).get_gameObject().SetActive(false);
        }
        else
        {
          int num3 = (int) Mathf.Pow(10f, (float) (index - 1));
          int num4 = num2 / num3;
          ((Component) child).get_gameObject().SetActive(true);
          ((ImageArray) ((Component) child).GetComponent<ImageArray>()).ImageIndex = num4;
          num2 %= num3;
        }
      }
      return true;
    }
  }
}
