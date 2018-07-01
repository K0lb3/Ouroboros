// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerPlayerInfo : MonoBehaviour
  {
    public Text NowTurn;
    public Text NowDied;
    public Text NowRetire;
    public Text NowRecover;
    public Text BestTurn;
    public Text BestDied;
    public Text BestRetire;
    public Text BestRecover;
    public Text SpdRank;
    public Text TecRank;
    public GameObject TurnScore;
    public GameObject DiedScore;
    public GameObject RetireScore;
    public GameObject RecoverScore;
    public GameObject TotalScore;
    public GameObject PlusObj;
    public GameObject MinusObj;
    public GameObject Leader;
    private string NotSocreText;
    private SpriteSheet Icons;

    public TowerPlayerInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Icons = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
      this.RefreshData();
    }

    private void RefreshData()
    {
      this.UpdateNowScore();
      this.UpdateBestScore();
      this.UpdateTotalScore();
      if (!Object.op_Inequality((Object) this.Leader, (Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      long unitUniqueId = player.Partys[6].GetUnitUniqueID(0);
      DataSource.Bind<UnitData>(this.Leader, player.FindUnitDataByUniqueID(unitUniqueId));
      GameParameter.UpdateAll(this.Leader);
    }

    private void UpdateNowScore()
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (Object.op_Inequality((Object) this.NowTurn, (Object) null))
        this.NowTurn.set_text(towerResuponse.turn_num.ToString());
      if (Object.op_Inequality((Object) this.NowDied, (Object) null))
        this.NowDied.set_text(towerResuponse.died_num.ToString());
      if (Object.op_Inequality((Object) this.NowRetire, (Object) null))
        this.NowRetire.set_text(towerResuponse.retire_num.ToString());
      if (!Object.op_Inequality((Object) this.NowRecover, (Object) null))
        return;
      this.NowRecover.set_text(towerResuponse.recover_num.ToString());
    }

    private void UpdateBestScore()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      if (towerResuponse.speedRank != 0 && towerResuponse.techRank != 0)
      {
        this.SetText(this.BestTurn, towerResuponse.spd_score.ToString());
        this.SetText(this.BestDied, towerResuponse.tec_score.ToString());
        this.SetText(this.BestRetire, towerResuponse.ret_score.ToString());
        this.SetText(this.BestRecover, towerResuponse.rcv_score.ToString());
        this.SetRankIcon(this.TurnScore, instance.ConvertTowerScoreToRank(towerResuponse.spd_score, TOWER_SCORE_TYPE.TURN));
        this.SetRankIcon(this.DiedScore, instance.ConvertTowerScoreToRank(towerResuponse.tec_score, TOWER_SCORE_TYPE.DIED));
        this.SetRankIcon(this.RetireScore, instance.ConvertTowerScoreToRank(towerResuponse.ret_score, TOWER_SCORE_TYPE.RETIRE));
        this.SetRankIcon(this.RecoverScore, instance.ConvertTowerScoreToRank(towerResuponse.rcv_score, TOWER_SCORE_TYPE.RECOVER));
      }
      else
      {
        this.SetText(this.BestTurn, this.NotSocreText);
        this.SetText(this.BestDied, this.NotSocreText);
        this.SetText(this.BestRetire, this.NotSocreText);
        this.SetText(this.BestRecover, this.NotSocreText);
        this.SetDefault(this.TurnScore);
        this.SetDefault(this.DiedScore);
        this.SetDefault(this.RetireScore);
        this.SetDefault(this.RecoverScore);
      }
    }

    private void UpdateTotalScore()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      if (towerResuponse.speedRank != 0 && towerResuponse.techRank != 0)
      {
        if (Object.op_Inequality((Object) this.SpdRank, (Object) null))
          this.SpdRank.set_text(towerResuponse.speedRank.ToString());
        if (Object.op_Inequality((Object) this.TecRank, (Object) null))
          this.TecRank.set_text(towerResuponse.techRank.ToString());
        string str = instance.CalcTowerRank(true).ToString();
        if (Object.op_Inequality((Object) this.TotalScore, (Object) null))
        {
          string name = str.Replace("_PLUS", string.Empty).Replace("_MINUS", string.Empty);
          Image component = (Image) this.TotalScore.GetComponent<Image>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.set_sprite(this.Icons.GetSprite(name));
        }
        if (str.IndexOf("_PLUS") != -1)
        {
          if (!Object.op_Inequality((Object) this.PlusObj, (Object) null))
            return;
          this.PlusObj.get_gameObject().SetActive(true);
        }
        else
        {
          if (str.IndexOf("_MINUS") == -1 || !Object.op_Inequality((Object) this.MinusObj, (Object) null))
            return;
          this.MinusObj.get_gameObject().SetActive(true);
        }
      }
      else
      {
        if (Object.op_Inequality((Object) this.SpdRank, (Object) null))
          ((Component) this.SpdRank).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.TecRank, (Object) null))
          ((Component) this.TecRank).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.TotalScore, (Object) null))
          this.TotalScore.get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.PlusObj, (Object) null))
          this.PlusObj.get_gameObject().SetActive(false);
        if (!Object.op_Inequality((Object) this.MinusObj, (Object) null))
          return;
        this.MinusObj.get_gameObject().SetActive(false);
      }
    }

    private void SetRankIcon(GameObject obj, string rank)
    {
      if (!Object.op_Inequality((Object) obj, (Object) null))
        return;
      Image component = (Image) obj.GetComponent<Image>();
      if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) this.Icons, (Object) null))
      {
        component.set_sprite(this.Icons.GetSprite(rank));
        obj.get_gameObject().SetActive(true);
      }
      else
        obj.get_gameObject().SetActive(false);
    }

    private void SetDefault(GameObject obj)
    {
      if (!Object.op_Inequality((Object) obj, (Object) null))
        return;
      obj.get_gameObject().SetActive(false);
    }

    private void SetText(Text text, string val)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(val);
    }
  }
}
