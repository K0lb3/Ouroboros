// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerPlayerInfo : MonoBehaviour
  {
    [HeaderBar("▼現在のスコア")]
    public Text NowTurn;
    public Text NowDied;
    public Text NowRetire;
    public Text NowRecover;
    public Text NowChallenge;
    public Text NowLose;
    public Text NowReset;
    [HeaderBar("▼ベストスコア")]
    public Text BestTurn;
    public Text BestDied;
    public Text BestRetire;
    public Text BestRecover;
    public Text BestChallenge;
    public Text BestLose;
    public Text BestReset;
    [HeaderBar("▼ランキングの順位")]
    public Text SpdRank;
    public Text TecRank;
    [HeaderBar("▼スコアのアートフォント")]
    public GameObject TurnScore;
    public GameObject DiedScore;
    public GameObject RetireScore;
    public GameObject RecoverScore;
    public GameObject ChallengeScore;
    public GameObject LoseScore;
    public GameObject ResetScore;
    public GameObject TotalScore;
    public GameObject PlusObj;
    public GameObject MinusObj;
    [HeaderBar("▼ユニット表示用オブジェクトの親")]
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
      if (Object.op_Inequality((Object) this.Leader, (Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId = player.Partys[6].GetUnitUniqueID(0);
        DataSource.Bind<UnitData>(this.Leader, player.FindUnitDataByUniqueID(unitUniqueId));
        GameParameter.UpdateAll(this.Leader);
      }
      DataSource.Bind<PlayerData>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.Player);
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
      if (Object.op_Inequality((Object) this.NowRecover, (Object) null))
        this.NowRecover.set_text(towerResuponse.recover_num.ToString());
      if (Object.op_Inequality((Object) this.NowChallenge, (Object) null))
        this.NowChallenge.set_text(towerResuponse.challenge_num.ToString());
      if (Object.op_Inequality((Object) this.NowLose, (Object) null))
        this.NowLose.set_text(towerResuponse.lose_num.ToString());
      if (!Object.op_Inequality((Object) this.NowReset, (Object) null))
        return;
      this.NowReset.set_text(towerResuponse.reset_num.ToString());
    }

    private void UpdateBestScore()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      TowerParam tower = instance.FindTower(towerResuponse.TowerID);
      if (towerResuponse.spd_score != 0)
      {
        this.SetText(this.BestTurn, towerResuponse.spd_score.ToString());
        this.SetText(this.BestDied, towerResuponse.tec_score.ToString());
        this.SetText(this.BestRetire, towerResuponse.ret_score.ToString());
        this.SetText(this.BestRecover, towerResuponse.rcv_score.ToString());
        this.SetText(this.BestChallenge, towerResuponse.challenge_score.ToString());
        this.SetText(this.BestLose, towerResuponse.lose_score.ToString());
        this.SetText(this.BestReset, towerResuponse.reset_score.ToString());
        this.SetRankIcon(this.TurnScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.spd_score, TOWER_SCORE_TYPE.TURN));
        this.SetRankIcon(this.DiedScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.tec_score, TOWER_SCORE_TYPE.DIED));
        this.SetRankIcon(this.RetireScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.ret_score, TOWER_SCORE_TYPE.RETIRE));
        this.SetRankIcon(this.RecoverScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.rcv_score, TOWER_SCORE_TYPE.RECOVER));
        this.SetDefault(this.ChallengeScore);
        this.SetDefault(this.LoseScore);
        this.SetDefault(this.ResetScore);
      }
      else
      {
        this.SetText(this.BestTurn, this.NotSocreText);
        this.SetText(this.BestDied, this.NotSocreText);
        this.SetText(this.BestRetire, this.NotSocreText);
        this.SetText(this.BestRecover, this.NotSocreText);
        this.SetText(this.BestChallenge, this.NotSocreText);
        this.SetText(this.BestLose, this.NotSocreText);
        this.SetText(this.BestReset, this.NotSocreText);
        this.SetDefault(this.TurnScore);
        this.SetDefault(this.DiedScore);
        this.SetDefault(this.RetireScore);
        this.SetDefault(this.RecoverScore);
        this.SetDefault(this.ChallengeScore);
        this.SetDefault(this.LoseScore);
        this.SetDefault(this.ResetScore);
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
