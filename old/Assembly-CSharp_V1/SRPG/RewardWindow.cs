// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class RewardWindow : MonoBehaviour, IFlowInterface
  {
    public bool RefreshOnStart;
    public bool UseGlobalVar;
    public bool UseBindDataOnly;
    public GameObject ExpRow;
    public GameObject GoldRow;
    public GameObject CoinRow;
    public GameObject ArenaMedalRow;
    public GameObject MultiCoinRow;
    public GameObject KakeraCoinRow;
    public GameObject StaminaRow;
    public GameObject ItemSeparator;
    public GameObject ItemList;
    public GameObject ItemTemplate;
    public GameObject EventCoinTemplate;
    public GameObject ArtifactTemplate;
    protected List<GameObject> mItems;

    public RewardWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.get_gameObject().SetActive(false);
      if (!Object.op_Inequality((Object) this.EventCoinTemplate, (Object) null))
        return;
      this.EventCoinTemplate.get_gameObject().SetActive(false);
    }

    private void Start()
    {
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    public virtual void Refresh()
    {
      if (this.UseGlobalVar)
        DataSource.Bind<RewardData>(((Component) this).get_gameObject(), (RewardData) GlobalVars.LastReward);
      RewardData dataOfClass = DataSource.FindDataOfClass<RewardData>(((Component) this).get_gameObject(), (RewardData) null);
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.ArenaMedalRow, (Object) null))
        this.ArenaMedalRow.SetActive(dataOfClass.ArenaMedal > 0);
      if (Object.op_Inequality((Object) this.MultiCoinRow, (Object) null))
        this.MultiCoinRow.SetActive(dataOfClass.MultiCoin > 0);
      if (Object.op_Inequality((Object) this.KakeraCoinRow, (Object) null))
        this.KakeraCoinRow.SetActive(dataOfClass.KakeraCoin > 0);
      if (Object.op_Inequality((Object) this.ExpRow, (Object) null))
        this.ExpRow.SetActive(dataOfClass.Exp > 0);
      if (Object.op_Inequality((Object) this.GoldRow, (Object) null))
        this.GoldRow.SetActive(dataOfClass.Gold > 0);
      if (Object.op_Inequality((Object) this.CoinRow, (Object) null))
        this.CoinRow.SetActive(dataOfClass.Coin > 0);
      if (Object.op_Inequality((Object) this.StaminaRow, (Object) null))
        this.StaminaRow.SetActive(dataOfClass.Stamina > 0);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      if (Object.op_Inequality((Object) this.ItemSeparator, (Object) null))
        this.ItemSeparator.SetActive(dataOfClass.Items.Count > 0);
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
      {
        Transform transform = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ArtifactTemplate.get_transform().get_parent() : this.ItemList.get_transform();
        using (Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator = dataOfClass.GiftRecieveItemDataDic.Values.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            GiftRecieveItemData current = enumerator.Current;
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactTemplate);
            this.mItems.Add(gameObject);
            DataSource.Bind<GiftRecieveItemData>(gameObject, current);
            gameObject.get_transform().SetParent(transform, false);
            gameObject.SetActive(true);
          }
        }
      }
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      Transform transform1 = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemList.get_transform();
      Transform transform2 = (Transform) null;
      if (Object.op_Inequality((Object) this.EventCoinTemplate, (Object) null))
        transform2 = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.EventCoinTemplate.get_transform().get_parent() : this.ItemList.get_transform();
      List<ItemParam> itemParamList = (List<ItemParam>) null;
      for (int index = 0; index < dataOfClass.Items.Count; ++index)
      {
        ItemData data = dataOfClass.Items[index];
        GameObject gameObject;
        if (data.ItemType != EItemType.EventCoin || Object.op_Equality((Object) transform2, (Object) null))
        {
          gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent(transform1, false);
        }
        else
        {
          gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.EventCoinTemplate);
          gameObject.get_transform().SetParent(transform2, false);
        }
        this.mItems.Add(gameObject);
        DataSource.Bind<ItemData>(gameObject, data);
        gameObject.SetActive(true);
        if (!this.UseBindDataOnly)
        {
          if (itemParamList == null)
            itemParamList = new List<ItemParam>();
          itemParamList.Add(data.Param);
        }
      }
    }
  }
}
