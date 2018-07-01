// Decompiled with JetBrains decompiler
// Type: SRPG.MailList2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "メールリスト空", FlowNode.PinTypes.Output, 10)]
  public class MailList2 : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_LIST_EMPTY = 10;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private ListExtras ScrollView;
    private List<GameObject> mMailListItems;

    public MailList2()
    {
      base.\u002Ector();
    }

    private void ActivateOutputLinks(int pinID)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.OnRefresh();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.get_activeInHierarchy())
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      this.InitializeList();
    }

    private void InitializeList()
    {
      if (this.mMailListItems != null)
        return;
      this.mMailListItems = new List<GameObject>();
    }

    private GameObject CreateListItem()
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
      MailListItem component1 = (MailListItem) gameObject.GetComponent<MailListItem>();
      if (Object.op_Inequality((Object) component1.listItemEvents, (Object) null))
      {
        component1.listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        ListItemEvents component2 = (ListItemEvents) component1.Button.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
      }
      return gameObject;
    }

    private void UpdateItems()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || this.mMailListItems == null)
        return;
      List<MailData> currentMails = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
      if (this.mMailListItems.Count < currentMails.Count)
      {
        Transform transform = ((Component) this).get_transform();
        int num = currentMails.Count - this.mMailListItems.Count;
        for (int index = 0; index < num; ++index)
        {
          GameObject listItem = this.CreateListItem();
          listItem.get_transform().SetParent(transform, false);
          this.mMailListItems.Add(listItem);
        }
      }
      for (int index1 = 0; index1 < this.mMailListItems.Count; ++index1)
      {
        GameObject mMailListItem = this.mMailListItems[index1];
        if (index1 >= currentMails.Count)
        {
          mMailListItem.SetActive(false);
        }
        else
        {
          mMailListItem.SetActive(true);
          MailData data1 = currentMails[index1];
          DataSource.Bind<MailData>(mMailListItem, data1);
          DataSource.Bind<MailData>(((MailListItem) mMailListItem.GetComponent<MailListItem>()).Button, data1);
          int num = 0;
          for (int index2 = 0; index2 < data1.gifts.Length; ++index2)
          {
            GiftData gift = data1.gifts[index2];
            if (gift.CheckGiftTypeIncluded(GiftTypes.Item))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Unit))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Coin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Gold))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.ArenaCoin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.MultiCoin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.KakeraCoin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Award))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
              ++num;
            if (num > 1)
              break;
          }
          if (num >= 2)
          {
            MailIcon component = (MailIcon) mMailListItem.GetComponent<MailIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                component.CurrentIcon.SetActive(false);
              component.CurrentIcon = component.SetIconTemplate;
              component.CurrentIcon.SetActive(true);
            }
          }
          else
          {
            MailIcon component = (MailIcon) mMailListItem.GetComponent<MailIcon>();
            for (int index2 = 0; index2 < data1.gifts.Length; ++index2)
            {
              GiftData gift = data1.gifts[index2];
              if (!gift.NotSet)
              {
                if (gift.CheckGiftTypeIncluded(GiftTypes.Item | GiftTypes.Unit | GiftTypes.SelectUnitItem | GiftTypes.SelectItem | GiftTypes.SelectArtifactItem))
                {
                  ItemData data2 = new ItemData();
                  data2.Setup(0L, gift.iname, gift.num);
                  DataSource.Bind<ItemData>(mMailListItem, data2);
                  if (Object.op_Inequality((Object) component, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ItemIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
                {
                  ArtifactData artifactData = gift.CreateArtifactData();
                  if (artifactData != null)
                    DataSource.Bind<ArtifactData>(mMailListItem, artifactData);
                  if (Object.op_Inequality((Object) component, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ArtifactIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Award))
                {
                  AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(gift.iname);
                  ItemData data2 = new ItemData();
                  data2.Setup(0L, awardParam.ToItemParam(), gift.num);
                  DataSource.Bind<ItemData>(mMailListItem, data2);
                  if (Object.op_Inequality((Object) component, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ItemIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Coin))
                {
                  if (Object.op_Inequality((Object) component, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.CoinIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Gold))
                {
                  if (Object.op_Inequality((Object) component, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.GoldIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.ArenaCoin))
                {
                  if (Object.op_Inequality((Object) component, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ArenaCoinIconTemplate;
                    component.CurrentIcon.SetActive(true);
                  }
                }
                else
                {
                  if (gift.CheckGiftTypeIncluded(GiftTypes.MultiCoin))
                  {
                    if (Object.op_Inequality((Object) component, (Object) null))
                    {
                      if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                        component.CurrentIcon.SetActive(false);
                      component.CurrentIcon = component.MultiCoinIconTemplate;
                      component.CurrentIcon.SetActive(true);
                      break;
                    }
                    break;
                  }
                  if (gift.CheckGiftTypeIncluded(GiftTypes.KakeraCoin))
                  {
                    if (Object.op_Inequality((Object) component, (Object) null))
                    {
                      if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                        component.CurrentIcon.SetActive(false);
                      component.CurrentIcon = component.KakeraCoinIconTemplate;
                      component.CurrentIcon.SetActive(true);
                      break;
                    }
                    break;
                  }
                }
              }
            }
          }
          MailListItem component1 = (MailListItem) mMailListItem.GetComponent<MailListItem>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.Set(data1.IsPeriod, data1.IsReadMail(), data1.post_at, data1.read);
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnRefresh()
    {
      this.UpdateItems();
      if (MonoSingleton<GameManager>.Instance.Player.CurrentMails.Count >= 1)
        return;
      this.ActivateOutputLinks(10);
    }

    private void OnSelect(GameObject go)
    {
      MailData dataOfClass = DataSource.FindDataOfClass<MailData>(go, (MailData) null);
      if (dataOfClass != null)
      {
        GlobalVars.SelectedMailUniqueID.Set(dataOfClass.mid);
        GlobalVars.SelectedMailPeriod.Set(dataOfClass.period);
        FlowNode_OnMailSelect nodeOnMailSelect = (FlowNode_OnMailSelect) ((Component) this).GetComponentInParent<FlowNode_OnMailSelect>();
        if (Object.op_Equality((Object) nodeOnMailSelect, (Object) null))
          nodeOnMailSelect = (FlowNode_OnMailSelect) Object.FindObjectOfType<FlowNode_OnMailSelect>();
        if (Object.op_Inequality((Object) nodeOnMailSelect, (Object) null))
          nodeOnMailSelect.Selected();
      }
      this.UpdateItems();
    }
  }
}
