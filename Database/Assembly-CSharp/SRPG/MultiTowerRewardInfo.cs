// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerRewardInfo : MonoBehaviour
  {
    public GameObject unitObj;
    public GameObject itemObj;
    public GameObject amountObj;
    public GameObject artifactObj;
    public GameParameter iconParam;
    public GameParameter frameParam;
    public RawImage itemTex;
    public Image frameTex;
    public Texture coinTex;
    public Texture goldTex;
    public Sprite coinBase;
    public Sprite goldBase;
    public Text rewardName;
    public Text rewardFloor;
    public RectTransform pos;
    public Text rewardDetailName;
    public Text rewardDetailInfo;
    public GameObject amountOther;
    public Text amountCount;
    public bool amountDisp;
    public Text Artifactamount;

    public MultiTowerRewardInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Refresh()
    {
      this.SetData();
    }

    private void SetData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerRewardItem dataOfClass = DataSource.FindDataOfClass<MultiTowerRewardItem>(((Component) this).get_gameObject(), (MultiTowerRewardItem) null);
      if (dataOfClass == null)
      {
        if (Object.op_Inequality((Object) this.itemObj, (Object) null))
          this.itemObj.SetActive(false);
        if (Object.op_Inequality((Object) this.amountObj, (Object) null))
          this.amountObj.SetActive(false);
        if (Object.op_Inequality((Object) this.unitObj, (Object) null))
          this.unitObj.SetActive(false);
        if (!Object.op_Inequality((Object) this.rewardName, (Object) null))
          return;
        ((Component) this.rewardName).get_gameObject().SetActive(false);
      }
      else
      {
        MultiTowerRewardItem.RewardType type = dataOfClass.type;
        string itemname = dataOfClass.itemname;
        int num = dataOfClass.num;
        if (Object.op_Inequality((Object) this.itemObj, (Object) null))
          this.itemObj.SetActive(true);
        if (Object.op_Inequality((Object) this.amountObj, (Object) null))
          this.amountObj.SetActive(true);
        if (Object.op_Inequality((Object) this.unitObj, (Object) null))
          this.unitObj.SetActive(false);
        if (Object.op_Inequality((Object) this.rewardName, (Object) null))
          ((Component) this.rewardName).get_gameObject().SetActive(true);
        switch (type)
        {
          case MultiTowerRewardItem.RewardType.Item:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.itemObj, (Object) null) || !Object.op_Inequality((Object) this.amountObj, (Object) null))
              break;
            this.itemObj.SetActive(true);
            DataSource component1 = (DataSource) this.itemObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component1, (Object) null))
              component1.Clear();
            DataSource component2 = (DataSource) this.amountObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component2, (Object) null))
              component2.Clear();
            ItemParam itemParam = instance.GetItemParam(itemname);
            DataSource.Bind<ItemParam>(this.itemObj, itemParam);
            ItemData data1 = new ItemData();
            data1.Setup(0L, itemParam, num);
            DataSource.Bind<ItemData>(this.amountObj, data1);
            Transform child1 = this.itemObj.get_transform().FindChild("icon");
            if (Object.op_Inequality((Object) child1, (Object) null))
            {
              GameParameter component3 = (GameParameter) ((Component) child1).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component3, (Object) null))
                ((Behaviour) component3).set_enabled(true);
            }
            GameParameter.UpdateAll(this.itemObj);
            if (Object.op_Inequality((Object) this.iconParam, (Object) null))
              this.iconParam.UpdateValue();
            if (Object.op_Inequality((Object) this.frameParam, (Object) null))
              this.frameParam.UpdateValue();
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.set_text(itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num));
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Coin:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (Object.op_Inequality((Object) this.itemTex, (Object) null))
            {
              GameParameter component3 = (GameParameter) ((Component) this.itemTex).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component3, (Object) null))
                ((Behaviour) component3).set_enabled(false);
              this.itemTex.set_texture(this.coinTex);
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
              this.frameTex.set_sprite(this.coinBase);
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num));
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            if (this.amountDisp)
            {
              this.amountOther.SetActive(true);
              if (!Object.op_Inequality((Object) this.amountCount, (Object) null))
                break;
              this.amountCount.set_text(dataOfClass.num.ToString());
              break;
            }
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Artifact:
            if (Object.op_Inequality((Object) this.itemObj, (Object) null))
              this.itemObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.artifactObj, (Object) null))
              break;
            this.artifactObj.SetActive(true);
            DataSource component4 = (DataSource) this.artifactObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component4, (Object) null))
              component4.Clear();
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(itemname);
            DataSource.Bind<ArtifactParam>(this.artifactObj, artifactParam);
            ArtifactIcon componentInChildren = (ArtifactIcon) this.artifactObj.GetComponentInChildren<ArtifactIcon>();
            if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
              break;
            ((Behaviour) componentInChildren).set_enabled(true);
            componentInChildren.UpdateValue();
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name));
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.Artifactamount, (Object) null))
              break;
            this.Artifactamount.set_text(dataOfClass.num.ToString());
            break;
          case MultiTowerRewardItem.RewardType.Award:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.itemObj, (Object) null) || !Object.op_Inequality((Object) this.amountObj, (Object) null))
              break;
            this.itemObj.SetActive(true);
            AwardParam awardParam = instance.GetAwardParam(itemname);
            Transform child2 = this.itemObj.get_transform().FindChild("icon");
            if (Object.op_Inequality((Object) child2, (Object) null))
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) child2).get_gameObject());
              if (!string.IsNullOrEmpty(awardParam.icon))
                iconLoader.ResourcePath = AssetPath.ItemIcon(awardParam.icon);
              GameParameter component3 = (GameParameter) ((Component) child2).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component3, (Object) null))
                ((Behaviour) component3).set_enabled(false);
              if (Object.op_Inequality((Object) this.rewardName, (Object) null))
                this.rewardName.set_text(awardParam.name);
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
              this.frameTex.set_sprite(this.coinBase);
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Unit:
            if (!Object.op_Inequality((Object) this.unitObj, (Object) null))
              break;
            if (Object.op_Inequality((Object) this.itemObj, (Object) null))
              this.itemObj.SetActive(false);
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            this.unitObj.SetActive(true);
            UnitParam unitParam = instance.GetUnitParam(itemname);
            DebugUtility.Assert(unitParam != null, "Invalid unit:" + itemname);
            UnitData data2 = new UnitData();
            data2.Setup(itemname, 0, 1, 0, (string) null, 1, EElement.None);
            DataSource.Bind<UnitData>(this.unitObj, data2);
            GameParameter.UpdateAll(this.unitObj);
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name));
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Gold:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (Object.op_Inequality((Object) this.itemTex, (Object) null))
            {
              GameParameter component3 = (GameParameter) ((Component) this.itemTex).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component3, (Object) null))
                ((Behaviour) component3).set_enabled(false);
              this.itemTex.set_texture(this.goldTex);
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.goldBase, (Object) null))
              this.frameTex.set_sprite(this.goldBase);
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.set_text(num.ToString() + LocalizedText.Get("sys.GOLD"));
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            if (this.amountDisp)
            {
              this.amountOther.SetActive(true);
              if (!Object.op_Inequality((Object) this.amountCount, (Object) null))
                break;
              this.amountCount.set_text(dataOfClass.num.ToString());
              break;
            }
            this.amountOther.SetActive(false);
            break;
        }
      }
    }

    public void OnDetailClick()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerRewardItem dataOfClass = DataSource.FindDataOfClass<MultiTowerRewardItem>(((Component) this).get_gameObject(), (MultiTowerRewardItem) null);
      if (dataOfClass == null)
        return;
      MultiTowerRewardItem.RewardType type = dataOfClass.type;
      string itemname = dataOfClass.itemname;
      int num = 0;
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (type)
      {
        case MultiTowerRewardItem.RewardType.Item:
          ItemParam itemParam = instance.GetItemParam(itemname);
          if (itemParam != null)
          {
            str1 = itemParam.name;
            str2 = itemParam.expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Coin:
          str1 = LocalizedText.Get("sys.COIN");
          str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          break;
        case MultiTowerRewardItem.RewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(itemname);
          if (artifactParam != null)
          {
            str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            str2 = artifactParam.expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(itemname);
          if (awardParam != null)
          {
            str1 = awardParam.name;
            str2 = awardParam.expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Unit:
          str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) instance.GetUnitParam(itemname).name);
          break;
        case MultiTowerRewardItem.RewardType.Gold:
          str1 = LocalizedText.Get("sys.GOLD");
          str2 = num.ToString() + LocalizedText.Get("sys.GOLD");
          break;
      }
      if (Object.op_Inequality((Object) this.rewardDetailName, (Object) null))
        this.rewardDetailName.set_text(str1);
      if (Object.op_Inequality((Object) this.rewardDetailInfo, (Object) null))
        this.rewardDetailInfo.set_text(str2);
      if (Object.op_Inequality((Object) this.pos, (Object) null))
        ((Transform) this.pos).set_position(((Component) this).get_gameObject().get_transform().get_position());
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "OPEN_DETAIL");
    }
  }
}
