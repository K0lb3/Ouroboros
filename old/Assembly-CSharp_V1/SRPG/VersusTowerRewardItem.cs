// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerRewardItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusTowerRewardItem : MonoBehaviour
  {
    public GameObject unitObj;
    public GameObject itemObj;
    public GameObject amountObj;
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

    public VersusTowerRewardItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Refresh()
    {
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).get_gameObject(), (VersusTowerParam) null);
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.rewardFloor, (Object) null))
        this.rewardFloor.set_text(dataOfClass.Floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
      this.SetData(VersusTowerRewardItem.REWARD_TYPE.Arrival, 0);
    }

    public void SetData(VersusTowerRewardItem.REWARD_TYPE type, int idx = 0)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).get_gameObject(), (VersusTowerParam) null);
      if (dataOfClass == null)
        return;
      VERSUS_ITEM_TYPE versusItemType = VERSUS_ITEM_TYPE.item;
      string str = string.Empty;
      int num = 0;
      if (type == VersusTowerRewardItem.REWARD_TYPE.Arrival)
      {
        versusItemType = dataOfClass.ArrivalItemType;
        str = (string) dataOfClass.ArrivalIteminame;
        num = (int) dataOfClass.ArrivalItemNum;
      }
      else if (idx >= 0 && idx < dataOfClass.SeasonIteminame.Length)
      {
        versusItemType = dataOfClass.SeasonItemType[idx];
        str = (string) dataOfClass.SeasonIteminame[idx];
        num = (int) dataOfClass.SeasonItemnum[idx];
      }
      if (Object.op_Inequality((Object) this.itemObj, (Object) null))
        this.itemObj.SetActive(true);
      if (Object.op_Inequality((Object) this.unitObj, (Object) null))
        this.unitObj.SetActive(false);
      switch (versusItemType)
      {
        case VERSUS_ITEM_TYPE.item:
          if (!Object.op_Inequality((Object) this.itemObj, (Object) null) || !Object.op_Inequality((Object) this.amountObj, (Object) null))
            break;
          ArtifactIcon componentInChildren1 = (ArtifactIcon) this.itemObj.GetComponentInChildren<ArtifactIcon>();
          if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
            ((Behaviour) componentInChildren1).set_enabled(false);
          this.itemObj.SetActive(true);
          ItemParam itemParam = instance.GetItemParam(str);
          DataSource.Bind<ItemParam>(this.itemObj, itemParam);
          ItemData data1 = new ItemData();
          data1.Setup(0L, itemParam, num);
          DataSource.Bind<ItemData>(this.amountObj, data1);
          GameParameter.UpdateAll(this.itemObj);
          if (Object.op_Inequality((Object) this.iconParam, (Object) null))
            this.iconParam.UpdateValue();
          if (Object.op_Inequality((Object) this.frameParam, (Object) null))
            this.frameParam.UpdateValue();
          if (!Object.op_Inequality((Object) this.rewardName, (Object) null))
            break;
          this.rewardName.set_text(itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num));
          break;
        case VERSUS_ITEM_TYPE.gold:
          if (Object.op_Inequality((Object) this.itemTex, (Object) null))
          {
            GameParameter component = (GameParameter) ((Component) this.itemTex).GetComponent<GameParameter>();
            if (Object.op_Inequality((Object) component, (Object) null))
              ((Behaviour) component).set_enabled(false);
            this.itemTex.set_texture(this.goldTex);
          }
          if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.goldBase, (Object) null))
            this.frameTex.set_sprite(this.goldBase);
          if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            this.rewardName.set_text(num.ToString() + LocalizedText.Get("sys.GOLD"));
          if (!Object.op_Inequality((Object) this.amountObj, (Object) null))
            break;
          this.amountObj.SetActive(false);
          break;
        case VERSUS_ITEM_TYPE.coin:
          if (Object.op_Inequality((Object) this.itemTex, (Object) null))
          {
            GameParameter component = (GameParameter) ((Component) this.itemTex).GetComponent<GameParameter>();
            if (Object.op_Inequality((Object) component, (Object) null))
              ((Behaviour) component).set_enabled(false);
            this.itemTex.set_texture(this.coinTex);
          }
          if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
            this.frameTex.set_sprite(this.coinBase);
          if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            this.rewardName.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num));
          if (!Object.op_Inequality((Object) this.amountObj, (Object) null))
            break;
          this.amountObj.SetActive(false);
          break;
        case VERSUS_ITEM_TYPE.unit:
          if (!Object.op_Inequality((Object) this.unitObj, (Object) null))
            break;
          if (Object.op_Inequality((Object) this.itemObj, (Object) null))
            this.itemObj.SetActive(false);
          this.unitObj.SetActive(true);
          UnitParam unitParam = instance.GetUnitParam(str);
          DebugUtility.Assert(unitParam != null, "Invalid unit:" + str);
          UnitData data2 = new UnitData();
          data2.Setup(str, 0, 1, 0, (string) null, 1, EElement.None);
          DataSource.Bind<UnitData>(this.unitObj, data2);
          GameParameter.UpdateAll(this.unitObj);
          if (!Object.op_Inequality((Object) this.rewardName, (Object) null))
            break;
          this.rewardName.set_text(unitParam.name);
          break;
        case VERSUS_ITEM_TYPE.artifact:
          if (!Object.op_Inequality((Object) this.itemObj, (Object) null))
            break;
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(str);
          DataSource.Bind<ArtifactParam>(this.itemObj, artifactParam);
          ArtifactIcon componentInChildren2 = (ArtifactIcon) this.itemObj.GetComponentInChildren<ArtifactIcon>();
          if (!Object.op_Inequality((Object) componentInChildren2, (Object) null))
            break;
          ((Behaviour) componentInChildren2).set_enabled(true);
          componentInChildren2.UpdateValue();
          if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            this.rewardName.set_text(artifactParam.name);
          if (!Object.op_Inequality((Object) this.amountObj, (Object) null))
            break;
          this.amountObj.SetActive(false);
          break;
      }
    }

    public enum REWARD_TYPE
    {
      Arrival,
      Season,
    }
  }
}
