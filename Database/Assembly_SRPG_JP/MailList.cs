// Decompiled with JetBrains decompiler
// Type: SRPG.MailList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [AddComponentMenu("UI/リスト/メール")]
  public class MailList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("リストアイテムとして使用するゲームオブジェクト(受け取り期限なし)")]
    public GameObject ItemForeverTemplate;
    public Toggle ToggleRead;
    public Toggle ToggleUnRead;
    public Button ButtonReadAll;
    public int MaxReadCount;
    public ListExtras ScrollView;
    private List<GameObject> mUnreadMails;
    private List<GameObject> mReadMails;
    private List<GameObject> mItems;
    private Dictionary<GiftTypes, int> currentNums;

    public MailList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.OnRefresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemForeverTemplate, (Object) null) && this.ItemForeverTemplate.get_activeInHierarchy())
        this.ItemForeverTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ToggleRead, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleRead.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowRead)));
      }
      if (Object.op_Inequality((Object) this.ToggleUnRead, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleUnRead.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowUnRead)));
      }
      if (!Object.op_Inequality((Object) this.ButtonReadAll, (Object) null))
        return;
      Button component = (Button) ((Component) this.ButtonReadAll).GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAllReadAccept)));
    }

    private void Start()
    {
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        List<MailData> mails = MonoSingleton<GameManager>.Instance.Player.Mails;
        mails.Clear();
        for (int index = 0; index < 50; ++index)
        {
          MailData mailData = new MailData();
          GiftData giftData = new GiftData();
          switch (index % 3)
          {
            case 0:
              giftData.iname = "IT_US_POTION";
              giftData.num = 3;
              break;
            case 1:
              giftData.gold = 1000;
              break;
            case 2:
              giftData.coin = 10;
              break;
          }
          mailData.gifts = new GiftData[1]{ giftData };
          mailData.mid = (long) index;
          mailData.msg = "てすと" + index.ToString();
          mailData.msg += index % 2 != 0 ? "既読" : "未読";
          mailData.read = index % 2 != 0 ? 1L : 0L;
          mailData.post_at = (long) (10000 + index);
          mails.Add(mailData);
        }
      }
      this.OnRefresh();
    }

    private void UpdateItems()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      if (this.mItems != null)
      {
        for (int index = 0; index < this.mReadMails.Count; ++index)
          this.mReadMails[index].SetActive(false);
        for (int index = 0; index < this.mUnreadMails.Count; ++index)
          this.mUnreadMails[index].SetActive(false);
        this.mReadMails.Clear();
        this.mUnreadMails.Clear();
        for (int index = 0; index < this.mItems.Count; ++index)
          Object.Destroy((Object) this.mItems[index].get_gameObject());
      }
      List<MailData> mails = MonoSingleton<GameManager>.Instance.Player.Mails;
      Transform transform = ((Component) this).get_transform();
      this.mItems = new List<GameObject>(mails.Count);
      this.mReadMails = new List<GameObject>();
      this.mUnreadMails = new List<GameObject>();
      for (int index1 = mails.Count - 1; index1 >= 0; --index1)
      {
        MailData data1 = mails[index1];
        GameObject gameObject1 = this.ItemTemplate;
        if (data1.IsPeriod)
          gameObject1 = this.ItemTemplate;
        else if (Object.op_Inequality((Object) this.ItemForeverTemplate, (Object) null))
          gameObject1 = this.ItemForeverTemplate;
        GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
        gameObject2.get_transform().SetParent(transform, false);
        int num = 0;
        for (int index2 = 0; index2 < data1.gifts.Length; ++index2)
        {
          GiftData gift = data1.gifts[index2];
          if (gift.num > 0)
            ++num;
          if (gift.coin > 0)
            ++num;
          if (gift.gold > 0)
            ++num;
          if (gift.arenacoin > 0)
            ++num;
          if (gift.multicoin > 0)
            ++num;
          if (gift.kakeracoin > 0)
            ++num;
        }
        if (num >= 2)
        {
          MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.CurrentIcon = component.SetIconTemplate;
            component.CurrentIcon.SetActive(true);
          }
        }
        else
        {
          for (int index2 = 0; index2 < data1.gifts.Length; ++index2)
          {
            GiftData gift = data1.gifts[index2];
            if (gift.num > 0)
            {
              if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
              {
                ArtifactData artifactData = this.CreateArtifactData(gift.iname);
                if (artifactData != null)
                  DataSource.Bind<ArtifactData>(gameObject2, artifactData);
                MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
                if (Object.op_Inequality((Object) component, (Object) null))
                {
                  component.CurrentIcon = component.ArtifactIconTemplate;
                  component.CurrentIcon.SetActive(true);
                  break;
                }
                break;
              }
              if (gift.CheckGiftTypeIncluded(GiftTypes.Item))
              {
                ItemData data2 = new ItemData();
                data2.Setup(0L, gift.iname, gift.num);
                DataSource.Bind<ItemData>(gameObject2, data2);
                MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
                if (Object.op_Inequality((Object) component, (Object) null))
                {
                  component.CurrentIcon = component.ItemIconTemplate;
                  component.CurrentIcon.SetActive(true);
                  break;
                }
                break;
              }
              break;
            }
            if (gift.coin > 0)
            {
              MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                component.CurrentIcon = component.CoinIconTemplate;
                component.CurrentIcon.SetActive(true);
                break;
              }
              break;
            }
            if (gift.gold > 0)
            {
              MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                component.CurrentIcon = component.GoldIconTemplate;
                component.CurrentIcon.SetActive(true);
              }
            }
            else if (gift.arenacoin > 0)
            {
              MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                component.CurrentIcon = component.ArenaCoinIconTemplate;
                component.CurrentIcon.SetActive(true);
              }
            }
            else if (gift.multicoin > 0)
            {
              MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                component.CurrentIcon = component.MultiCoinIconTemplate;
                component.CurrentIcon.SetActive(true);
              }
            }
            else if (gift.kakeracoin > 0)
            {
              MailIcon component = (MailIcon) gameObject2.GetComponent<MailIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                component.CurrentIcon = component.KakeraCoinIconTemplate;
                component.CurrentIcon.SetActive(true);
              }
            }
          }
        }
        DataSource.Bind<MailData>(gameObject2, data1);
        GameObject gameObject3 = ((Component) gameObject2.get_transform().FindChild("btn_read")).get_gameObject();
        if (Object.op_Inequality((Object) gameObject3, (Object) null))
        {
          DataSource.Bind<MailData>(gameObject3, data1);
          Button component1 = (Button) gameObject3.GetComponent<Button>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            ListItemEvents component2 = (ListItemEvents) ((Component) component1).GetComponent<ListItemEvents>();
            if (Object.op_Inequality((Object) component2, (Object) null))
              component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          }
        }
        if (data1.read > 0L)
        {
          this.mReadMails.Add(gameObject2);
          gameObject2.SetActive(false);
        }
        else
        {
          this.mUnreadMails.Add(gameObject2);
          gameObject2.SetActive(true);
        }
        this.mItems.Add(gameObject2);
      }
    }

    private void ToggleReadAll()
    {
      if (this.mUnreadMails.Count < 1)
        ((Component) this.ButtonReadAll).get_gameObject().SetActive(false);
      else
        ((Component) this.ButtonReadAll).get_gameObject().SetActive(true);
    }

    private void OnRefresh()
    {
      this.UpdateItems();
      this.ToggleReadAll();
    }

    private void OnShowRead(bool isActive)
    {
      if (!isActive)
        return;
      using (List<GameObject>.Enumerator enumerator = this.mUnreadMails.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (DataSource.FindDataOfClass<MailData>(current.get_gameObject(), (MailData) null) != null)
            current.SetActive(false);
        }
      }
      using (List<GameObject>.Enumerator enumerator = this.mReadMails.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (DataSource.FindDataOfClass<MailData>(current.get_gameObject(), (MailData) null) != null)
            current.SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.ScrollView, (Object) null))
        this.ScrollView.SetScrollPos(1f);
      ((Component) this.ButtonReadAll).get_gameObject().SetActive(false);
    }

    private void OnShowUnRead(bool isActive)
    {
      if (!isActive)
        return;
      using (List<GameObject>.Enumerator enumerator = this.mReadMails.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (DataSource.FindDataOfClass<MailData>(current.get_gameObject(), (MailData) null) != null)
            current.SetActive(false);
        }
      }
      using (List<GameObject>.Enumerator enumerator = this.mUnreadMails.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (DataSource.FindDataOfClass<MailData>(current.get_gameObject(), (MailData) null) != null)
            current.SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.ScrollView, (Object) null))
        this.ScrollView.SetScrollPos(1f);
      this.ToggleReadAll();
    }

    private void OnSelect(GameObject go)
    {
      MailData dataOfClass = DataSource.FindDataOfClass<MailData>(go, (MailData) null);
      if (dataOfClass != null)
      {
        GlobalVars.SelectedMailUniqueID.Set(dataOfClass.mid);
        GlobalVars.SelectedMailPeriod.Set(dataOfClass.period);
        RewardData rewardData = this.GiftDataToRewardData(dataOfClass.gifts);
        FlowNode_OnMailSelect nodeOnMailSelect = (FlowNode_OnMailSelect) ((Component) this).GetComponentInParent<FlowNode_OnMailSelect>();
        if (Object.op_Equality((Object) nodeOnMailSelect, (Object) null))
          nodeOnMailSelect = (FlowNode_OnMailSelect) Object.FindObjectOfType<FlowNode_OnMailSelect>();
        if (Object.op_Inequality((Object) nodeOnMailSelect, (Object) null))
          nodeOnMailSelect.Selected();
        GlobalVars.LastReward.Set(rewardData);
      }
      this.UpdateItems();
    }

    private void RefreshCurrentNums()
    {
      this.currentNums = new Dictionary<GiftTypes, int>();
      this.currentNums.Add(GiftTypes.Artifact, MonoSingleton<GameManager>.Instance.Player.ArtifactNum);
    }

    private void AddCurrentNum(GiftRecieveItemData data)
    {
      if (this.currentNums == null || !this.currentNums.ContainsKey(data.type))
        return;
      Dictionary<GiftTypes, int> currentNums;
      GiftTypes type;
      (currentNums = this.currentNums)[type = data.type] = currentNums[type] + data.num;
    }

    private bool CheckRecievable(RewardData reward)
    {
      int num = 0;
      using (Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator = reward.GiftRecieveItemDataDic.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GiftRecieveItemData current = enumerator.Current;
          if (current.type == GiftTypes.Artifact)
            num += this.currentNums[GiftTypes.Artifact] + current.num;
        }
      }
      return num < (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap;
    }

    private void OnAllReadAccept()
    {
      if (this.mUnreadMails.Count < 1)
        return;
      this.RefreshCurrentNums();
      RewardData rewardData1 = new RewardData();
      List<MailList.ItemListEntity> itemListEntityList = new List<MailList.ItemListEntity>();
      int num1 = 0;
      for (int index1 = 0; index1 < this.mUnreadMails.Count && num1 < this.MaxReadCount; ++index1)
      {
        MailData dataOfClass = DataSource.FindDataOfClass<MailData>(this.mUnreadMails[index1], (MailData) null);
        if (dataOfClass != null)
        {
          RewardData rewardData2 = this.GiftDataToRewardData(dataOfClass.gifts);
          if (this.CheckRecievable(rewardData2))
          {
            ++num1;
            using (Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator = rewardData2.GiftRecieveItemDataDic.Values.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                GiftRecieveItemData current = enumerator.Current;
                if (rewardData1.GiftRecieveItemDataDic.ContainsKey(current.iname))
                  rewardData1.GiftRecieveItemDataDic[current.iname].num += current.num;
                else
                  rewardData1.GiftRecieveItemDataDic.Add(current.iname, current);
                this.AddCurrentNum(current);
              }
            }
            rewardData1.Exp += rewardData2.Exp;
            rewardData1.Stamina += rewardData2.Stamina;
            rewardData1.Coin += rewardData2.Coin;
            rewardData1.Gold += rewardData2.Gold;
            rewardData1.ArenaMedal += rewardData2.ArenaMedal;
            rewardData1.MultiCoin += rewardData2.MultiCoin;
            rewardData1.KakeraCoin += rewardData2.KakeraCoin;
            for (int index2 = 0; index2 < rewardData2.Items.Count; ++index2)
            {
              ItemData itemData = rewardData2.Items[index2];
              if (itemListEntityList.Count > 0)
              {
                bool flag = false;
                for (int index3 = 0; index3 < itemListEntityList.Count; ++index3)
                {
                  if (itemListEntityList[index3].Item.ItemID == itemData.ItemID)
                  {
                    itemListEntityList[index3].Num += itemData.Num;
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  itemListEntityList.Add(new MailList.ItemListEntity(itemData.Num, itemData));
              }
              else
                itemListEntityList.Add(new MailList.ItemListEntity(itemData.Num, itemData));
            }
          }
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < itemListEntityList.Count; ++index)
      {
        MailList.ItemListEntity itemListEntity = itemListEntityList[index];
        ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemListEntity.Item.Param.iname);
        int num2 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
        rewardData1.ItemsBeforeAmount.Add(num2);
        if (itemListEntity.Item.HaveCap < itemListEntity.Num)
        {
          itemListEntity.Item.Gain(itemListEntity.Item.HaveCap);
          rewardData1.Items.Add(itemListEntity.Item);
          int num3 = itemListEntity.Num - itemListEntity.Item.HaveCap;
          ItemData itemData = new ItemData();
          if (itemData.Setup(itemListEntity.Item.UniqueID, itemListEntity.Item.Param, num3))
          {
            rewardData1.Items.Add(itemData);
            rewardData1.ItemsBeforeAmount.Add(itemData.HaveCap);
          }
        }
        else
        {
          itemListEntity.Item.Gain(itemListEntity.Num - itemListEntity.Item.Num);
          rewardData1.Items.Add(itemListEntity.Item);
        }
      }
      GlobalVars.LastReward.Set(rewardData1);
      this.UpdateItems();
    }

    private ArtifactData CreateArtifactData(string iname)
    {
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname);
      if (artifactParam == null)
        return (ArtifactData) null;
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = artifactParam.iname,
        fav = 0,
        rare = artifactParam.rareini
      });
      return artifactData;
    }

    private RewardData GiftDataToRewardData(GiftData[] giftDatas)
    {
      RewardData rewardData = new RewardData();
      rewardData.Exp = 0;
      rewardData.Stamina = 0;
      rewardData.MultiCoin = 0;
      rewardData.KakeraCoin = 0;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < giftDatas.Length; ++index)
      {
        GiftData giftData = giftDatas[index];
        rewardData.Coin += giftData.coin;
        rewardData.Gold += giftData.gold;
        rewardData.ArenaMedal += giftData.arenacoin;
        rewardData.MultiCoin += giftData.multicoin;
        rewardData.KakeraCoin += giftData.kakeracoin;
        if (giftData.iname != null)
        {
          if (giftData.CheckGiftTypeIncluded(GiftTypes.Artifact))
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(giftData.iname);
            if (artifactParam != null)
            {
              if (rewardData.GiftRecieveItemDataDic.ContainsKey(giftData.iname))
              {
                rewardData.GiftRecieveItemDataDic[giftData.iname].num += giftData.num;
              }
              else
              {
                GiftRecieveItemData giftRecieveItemData = new GiftRecieveItemData();
                giftRecieveItemData.Set(giftData.iname, GiftTypes.Artifact, artifactParam.rareini, giftData.num);
                rewardData.GiftRecieveItemDataDic.Add(giftData.iname, giftRecieveItemData);
              }
            }
          }
          else
          {
            ItemData itemData = new ItemData();
            if (itemData.Setup(0L, giftData.iname, giftData.num))
            {
              rewardData.Items.Add(itemData);
              ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemData.Param.iname);
              int num = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
              rewardData.ItemsBeforeAmount.Add(num);
            }
          }
        }
      }
      return rewardData;
    }

    private class ItemListEntity
    {
      public int Num;
      public ItemData Item;

      public ItemListEntity(int Num, ItemData Item)
      {
        this.Num = Num;
        this.Item = Item;
      }
    }
  }
}
