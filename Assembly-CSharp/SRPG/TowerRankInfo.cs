// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRankInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerRankInfo : MonoBehaviour
  {
    private readonly int SHOW_VIP_RANK;
    public Text Ranking;
    public Image OwnIcon;
    public GameObject OwnObj;
    public Image OwnTotalScore;
    public Text OwnSpeedScore;
    public Text OwnTechScore;
    public GameObject OwnSpeedObj;
    public GameObject OwnTechObj;
    public GameObject ClearPage;
    public GameObject ClearPageParent;
    public ListItemEvents ClearTemplate;
    public GameObject NotClearPage;
    public GameObject NotClearPageParent;
    public ListItemEvents NotClearTemplate;
    public Toggle Speed;
    public Toggle Tech;
    public GameObject Root;
    public GameObject NotDataObj;
    private SpriteSheet mSheet;
    private bool IsSpeed;

    public TowerRankInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mSheet = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
      this.UpdateOwnValue();
      this.UpdateRankValue(true);
      if (Object.op_Inequality((Object) this.Speed, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.Speed.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeSpeed)));
      }
      if (!Object.op_Inequality((Object) this.Tech, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.Tech.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeTech)));
    }

    private int GetSameRank(int score, int rank)
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse.SpdRank != null && towerResuponse.SpdRank.Length > 0 || towerResuponse.TecRank != null && towerResuponse.TecRank.Length > 0)
      {
        TowerResuponse.TowerRankParam[] towerRankParamArray = !this.IsSpeed ? towerResuponse.TecRank : towerResuponse.SpdRank;
        for (int index = 0; index < towerRankParamArray.Length; ++index)
        {
          if (towerRankParamArray[index].score == score)
            return index + 1;
        }
      }
      return rank;
    }

    private void UpdateOwnValue()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      if (towerResuponse != null)
      {
        bool flag = towerResuponse.speedRank != 0 && towerResuponse.techRank != 0;
        if (Object.op_Inequality((Object) this.ClearPage, (Object) null))
          this.ClearPage.get_gameObject().SetActive(flag);
        if (Object.op_Inequality((Object) this.NotClearPage, (Object) null))
          this.NotClearPage.get_gameObject().SetActive(!flag);
      }
      int rank = !this.IsSpeed ? towerResuponse.techRank : towerResuponse.speedRank;
      int sameRank = this.GetSameRank(!this.IsSpeed ? towerResuponse.tec_score : towerResuponse.spd_score, rank);
      if (Object.op_Inequality((Object) this.OwnIcon, (Object) null))
      {
        if (sameRank <= this.SHOW_VIP_RANK)
          this.OwnIcon.set_sprite(this.mSheet.GetSprite((sameRank - 1).ToString()));
        else
          this.OwnIcon.set_sprite(this.mSheet.GetSprite("normal"));
      }
      if (Object.op_Inequality((Object) this.Ranking, (Object) null))
      {
        ((Component) this.Ranking).get_gameObject().SetActive(sameRank > this.SHOW_VIP_RANK);
        this.Ranking.set_text(sameRank.ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL"));
      }
      if (Object.op_Inequality((Object) this.OwnObj, (Object) null))
      {
        PlayerData player = instance.Player;
        long unitUniqueId = player.Partys[6].GetUnitUniqueID(0);
        DataSource.Bind<UnitData>(this.OwnObj, player.FindUnitDataByUniqueID(unitUniqueId));
      }
      if (Object.op_Inequality((Object) this.OwnSpeedObj, (Object) null))
        this.OwnSpeedObj.get_gameObject().SetActive(this.IsSpeed);
      if (Object.op_Inequality((Object) this.OwnTechObj, (Object) null))
        this.OwnTechObj.get_gameObject().SetActive(!this.IsSpeed);
      if (this.IsSpeed)
      {
        if (Object.op_Inequality((Object) this.OwnSpeedScore, (Object) null))
          this.OwnSpeedScore.set_text(towerResuponse.spd_score.ToString());
      }
      else if (Object.op_Inequality((Object) this.OwnSpeedScore, (Object) null))
        this.OwnTechScore.set_text(towerResuponse.tec_score.ToString());
      if (!Object.op_Inequality((Object) this.OwnTotalScore, (Object) null))
        return;
      string empty = string.Empty;
      this.OwnTotalScore.set_sprite(this.mSheet.GetSprite(!this.IsSpeed ? instance.ConvertTowerScoreToRank(towerResuponse.tec_score, TOWER_SCORE_TYPE.DIED) : instance.ConvertTowerScoreToRank(towerResuponse.spd_score, TOWER_SCORE_TYPE.TURN)));
    }

    private void UpdateRankValue(bool isSpeedRank = true)
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      GameObject gameObject = this.NotClearPageParent;
      ListItemEvents listItemEvents1 = this.NotClearTemplate;
      if (towerResuponse != null)
      {
        gameObject = towerResuponse.speedRank == 0 || towerResuponse.techRank == 0 ? this.NotClearPageParent : this.ClearPageParent;
        listItemEvents1 = towerResuponse.speedRank == 0 || towerResuponse.techRank == 0 ? this.NotClearTemplate : this.ClearTemplate;
        if (towerResuponse.SpdRank == null || towerResuponse.SpdRank.Length == 0 || (towerResuponse.TecRank == null || towerResuponse.TecRank.Length == 0))
        {
          if (!Object.op_Inequality((Object) this.NotDataObj, (Object) null))
            return;
          this.NotDataObj.get_gameObject().SetActive(true);
          return;
        }
      }
      if (Object.op_Inequality((Object) this.NotClearTemplate, (Object) null))
        ((Component) this.NotClearTemplate).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.ClearTemplate, (Object) null))
        ((Component) this.ClearTemplate).get_gameObject().SetActive(false);
      Transform transform = gameObject.get_transform();
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          Object.DestroyImmediate((Object) ((Component) child).get_gameObject());
      }
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        gameObject.get_gameObject().SetActive(true);
      TowerResuponse.TowerRankParam[] towerRankParamArray = !isSpeedRank ? towerResuponse.TecRank : towerResuponse.SpdRank;
      for (int index1 = 0; index1 < towerRankParamArray.Length; ++index1)
      {
        ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
        int num = index1;
        if (Object.op_Inequality((Object) listItemEvents2, (Object) null))
        {
          DataSource.Bind<UnitData>(((Component) listItemEvents2).get_gameObject(), towerRankParamArray[index1].unit);
          listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
          ((Component) listItemEvents2).get_transform().SetParent(gameObject.get_transform(), false);
          ((Component) listItemEvents2).get_gameObject().SetActive(true);
          for (int index2 = 0; index2 < index1; ++index2)
          {
            if (towerRankParamArray[index2].score == towerRankParamArray[index1].score)
            {
              num = index2;
              break;
            }
          }
          this.UpdateValue(listItemEvents2, num, towerRankParamArray[index1], isSpeedRank);
        }
      }
      if (!Object.op_Inequality((Object) this.Root, (Object) null))
        return;
      GameParameter.UpdateAll(this.Root);
    }

    private void UpdateValue(ListItemEvents obj, int num, TowerResuponse.TowerRankParam param, bool isSpeed)
    {
      if (Object.op_Equality((Object) this.mSheet, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Transform child1 = ((Component) obj).get_transform().FindChild("body");
      if (!Object.op_Inequality((Object) child1, (Object) null))
        return;
      Transform child2 = ((Component) child1).get_transform().FindChild("ranking");
      if (Object.op_Inequality((Object) child2, (Object) null))
      {
        Image component = (Image) ((Component) child2).GetComponent<Image>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          if (num < this.SHOW_VIP_RANK)
            component.set_sprite(this.mSheet.GetSprite(num.ToString()));
          else
            component.set_sprite(this.mSheet.GetSprite("normal"));
        }
      }
      Transform child3 = ((Component) child1).get_transform().FindChild("rank");
      if (Object.op_Inequality((Object) child3, (Object) null))
      {
        Text component = (Text) ((Component) child3).GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          if (num < this.SHOW_VIP_RANK)
            component.set_text(string.Empty);
          else
            component.set_text((num + 1).ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL"));
        }
      }
      Transform child4 = ((Component) child1).get_transform().FindChild("Text_player");
      if (Object.op_Inequality((Object) child4, (Object) null))
      {
        Text component = (Text) ((Component) child4).GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_text(param.name);
      }
      Transform child5 = ((Component) child1).get_transform().FindChild("player_level");
      if (Object.op_Inequality((Object) child5, (Object) null))
      {
        Text component = (Text) ((Component) child5).GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_text(LocalizedText.Get("sys.TOWER_RANK_LBL_LV") + (object) ' ' + param.lv.ToString());
      }
      if (isSpeed)
      {
        Transform child6 = ((Component) child1).get_transform().FindChild("speed");
        if (Object.op_Inequality((Object) child6, (Object) null))
        {
          Transform child7 = ((Component) child6).get_transform().FindChild("speed_cnt");
          if (Object.op_Inequality((Object) child7, (Object) null))
          {
            Text component = (Text) ((Component) child7).GetComponent<Text>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.set_text(param.score.ToString());
          }
          ((Component) child6).get_gameObject().SetActive(true);
        }
        string rank = instance.ConvertTowerScoreToRank(param.score, TOWER_SCORE_TYPE.TURN);
        Transform child8 = ((Component) child1).get_transform().FindChild("score_img");
        if (!Object.op_Inequality((Object) child8, (Object) null))
          return;
        Image component1 = (Image) ((Component) child8).GetComponent<Image>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          return;
        component1.set_sprite(this.mSheet.GetSprite(rank));
      }
      else
      {
        Transform child6 = ((Component) child1).get_transform().FindChild("tech");
        if (Object.op_Inequality((Object) child6, (Object) null))
        {
          Transform child7 = ((Component) child6).get_transform().FindChild("tech_cnt");
          if (Object.op_Inequality((Object) child7, (Object) null))
          {
            Text component = (Text) ((Component) child7).GetComponent<Text>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.set_text(param.score.ToString());
          }
          ((Component) child6).get_gameObject().SetActive(true);
        }
        string rank = instance.ConvertTowerScoreToRank(param.score, TOWER_SCORE_TYPE.DIED);
        Transform child8 = ((Component) child1).get_transform().FindChild("score_img");
        if (!Object.op_Inequality((Object) child8, (Object) null))
          return;
        Image component1 = (Image) ((Component) child8).GetComponent<Image>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          return;
        component1.set_sprite(this.mSheet.GetSprite(rank));
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
    }

    public void OnChangeSpeed(bool val)
    {
      if (!val)
        return;
      this.IsSpeed = true;
      this.UpdateOwnValue();
      this.UpdateRankValue(this.IsSpeed);
    }

    public void OnChangeTech(bool val)
    {
      if (!val)
        return;
      this.IsSpeed = false;
      this.UpdateOwnValue();
      this.UpdateRankValue(this.IsSpeed);
    }
  }
}
