// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuySetConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class ShopBuySetConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ItemParent;
    public GameObject ItemWindow;
    public GameObject ArtifactWindow;
    private List<ShopSetItemListElement> shop_item_set_list;
    public StatusList ArtifactStatus;
    private ArtifactParam mArtifactParam;
    private bool mIsShowArtifactJob;
    public GameObject ArtifactAbility;
    public Animator ArtifactAbilityAnimation;
    public string AbilityListItemState;
    public int AbilityListItem_Hidden;
    public int AbilityListItem_Unlocked;
    public UnityEngine.UI.Text AmountNum;
    public GameObject Sold;

    public ShopBuySetConfirmWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
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
      ShopItem data = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType).items[GlobalVars.ShopBuyIndex];
      this.ItemWindow.SetActive(!data.IsArtifact);
      this.ArtifactWindow.SetActive(data.IsArtifact);
      if (Object.op_Inequality((Object) this.AmountNum, (Object) null))
        this.AmountNum.set_text(data.remaining_num.ToString());
      if (Object.op_Inequality((Object) this.Sold, (Object) null))
        this.Sold.SetActive(!data.IsNotLimited);
      if (data.IsArtifact)
      {
        ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data.iname);
        DataSource.Bind<ArtifactParam>(((Component) this).get_gameObject(), artifactParam);
        this.mArtifactParam = artifactParam;
        ArtifactData artifactData = new ArtifactData();
        artifactData.Deserialize(new Json_Artifact()
        {
          iname = artifactParam.iname,
          rare = artifactParam.rareini
        });
        BaseStatus fixed_status = new BaseStatus();
        BaseStatus scale_status = new BaseStatus();
        artifactData.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
        this.ArtifactStatus.SetValues(fixed_status, scale_status);
        if (artifactParam.abil_inames != null && artifactParam.abil_inames.Length > 0)
        {
          AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(artifactParam.abil_inames[0]);
          List<AbilityData> learningAbilities = artifactData.LearningAbilities;
          bool flag = false;
          if (learningAbilities != null)
          {
            for (int index = 0; index < learningAbilities.Count; ++index)
            {
              AbilityData abilityData = learningAbilities[index];
              if (abilityData != null && abilityParam.iname == abilityData.Param.iname)
              {
                flag = true;
                break;
              }
            }
          }
          DataSource.Bind<AbilityParam>(this.ArtifactAbility, abilityParam);
          if (flag)
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
          else
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        }
        else
          this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
      }
      else
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data.iname);
        this.shop_item_set_list.Clear();
        if (data.IsSet)
        {
          for (int index = 0; index < data.children.Length; ++index)
          {
            GameObject gameObject = index >= this.shop_item_set_list.Count ? (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate) : ((Component) this.shop_item_set_list[index]).get_gameObject();
            if (Object.op_Inequality((Object) gameObject, (Object) null))
            {
              gameObject.SetActive(true);
              Vector3 localScale = gameObject.get_transform().get_localScale();
              gameObject.get_transform().SetParent(this.ItemParent.get_transform());
              gameObject.get_transform().set_localScale(localScale);
              ShopSetItemListElement component = (ShopSetItemListElement) gameObject.GetComponent<ShopSetItemListElement>();
              StringBuilder stringBuilder = GameUtility.GetStringBuilder();
              if (data.children[index].IsArtifact)
              {
                ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data.children[index].iname);
                if (artifactParam != null)
                  stringBuilder.Append(artifactParam.name);
                component.ArtifactParam = artifactParam;
              }
              else
              {
                ItemData itemData = new ItemData();
                itemData.Setup(0L, data.children[index].iname, data.children[index].num);
                if (itemData != null)
                  stringBuilder.Append(itemData.Param.name);
                component.itemData = itemData;
              }
              stringBuilder.Append("×");
              stringBuilder.Append(data.children[index].num.ToString());
              component.itemName.set_text(stringBuilder.ToString());
              component.SetShopItemDesc(data.children[index]);
              this.shop_item_set_list.Add(component);
            }
          }
        }
        DataSource.Bind<ItemData>(((Component) this).get_gameObject(), itemDataByItemId);
        DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.GetItemParam(data.iname));
      }
      DataSource.Bind<ShopItem>(((Component) this).get_gameObject(), data);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void ShowJobList()
    {
      if (this.mIsShowArtifactJob || this.mArtifactParam == null)
        return;
      GlobalVars.ConditionJobs = this.mArtifactParam.condition_jobs;
      this.mIsShowArtifactJob = true;
    }

    public void CloseJobList()
    {
      this.mIsShowArtifactJob = false;
    }
  }
}
