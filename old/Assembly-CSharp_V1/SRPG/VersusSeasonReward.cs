// Decompiled with JetBrains decompiler
// Type: SRPG.VersusSeasonReward
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusSeasonReward : MonoBehaviour
  {
    private readonly float WAIT_TIME;
    private readonly float WAIT_OPEN;
    public GameObject item;
    public GameObject root;
    public GameObject template;
    public GameObject parent;
    public GameObject arrival;
    public Text floorTxt;
    public Text floorEffTxt;
    public Text rewardTxt;
    public Text okTxt;
    public string openAnim;
    public string nextAnim;
    public string resultAnim;
    private int mNow;
    private int mMax;
    private float mWaitTime;
    private VersusSeasonReward.MODE mMode;

    public VersusSeasonReward()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      if (Object.op_Inequality((Object) this.floorTxt, (Object) null))
        this.floorTxt.set_text(player.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
      if (Object.op_Inequality((Object) this.floorEffTxt, (Object) null))
        this.floorEffTxt.set_text(player.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      int index = player.VersusTowerFloor - 1;
      if (versusTowerParam == null || index < 0 || index >= versusTowerParam.Length)
        return;
      this.mNow = 0;
      if (versusTowerParam[index].SeasonIteminame != null)
        this.mMax = versusTowerParam[index].SeasonIteminame.Length;
      DataSource.Bind<VersusTowerParam>(this.item, versusTowerParam[index]);
      if (this.mNow + 1 < this.mMax)
        this.SetButtonText(true);
      this.mWaitTime = this.WAIT_OPEN;
      this.mMode = VersusSeasonReward.MODE.NEXT;
    }

    private void SetButtonText(bool bNext)
    {
      if (Object.op_Inequality((Object) this.okTxt, (Object) null))
        this.okTxt.set_text(LocalizedText.Get(!bNext ? "sys.CMD_OK" : "sys.BTN_NEXT"));
      if (bNext || !Object.op_Inequality((Object) this.rewardTxt, (Object) null))
        return;
      this.rewardTxt.set_text(LocalizedText.Get("sys.MULTI_VERSUS_SEND_GIFT"));
    }

    private void Update()
    {
      switch (this.mMode)
      {
        case VersusSeasonReward.MODE.REQ:
          this.SetData(this.mNow, true, (GameObject) null);
          this.mMode = VersusSeasonReward.MODE.COUNTDOWN;
          this.mWaitTime = this.WAIT_TIME;
          break;
        case VersusSeasonReward.MODE.COUNTDOWN:
          this.mWaitTime -= Time.get_deltaTime();
          if ((double) this.mWaitTime > 0.0)
            break;
          this.mMode = VersusSeasonReward.MODE.WAIT;
          break;
        case VersusSeasonReward.MODE.NEXT:
        case VersusSeasonReward.MODE.FINISH:
          this.mWaitTime -= Time.get_deltaTime();
          if ((double) this.mWaitTime > 0.0)
            break;
          if (this.mMode == VersusSeasonReward.MODE.FINISH)
          {
            this.mMode = VersusSeasonReward.MODE.END;
            break;
          }
          this.mMode = VersusSeasonReward.MODE.REQ;
          break;
      }
    }

    private void SetData(int idx, bool bPlay = false, GameObject obj = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = !Object.op_Inequality((Object) obj, (Object) null) ? this.item : obj;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam != null && versusTowerParam.SeasonIteminame != null && (!string.IsNullOrEmpty((string) versusTowerParam.SeasonIteminame[idx]) && Object.op_Inequality((Object) gameObject, (Object) null)))
      {
        gameObject.SetActive(true);
        VersusTowerRewardItem component = (VersusTowerRewardItem) gameObject.GetComponent<VersusTowerRewardItem>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetData(VersusTowerRewardItem.REWARD_TYPE.Season, idx);
        if (bPlay)
          this.ReqAnim(this.openAnim);
        this.SetRewardName(idx, versusTowerParam);
      }
      if (!Object.op_Inequality((Object) this.arrival, (Object) null))
        return;
      this.arrival.SetActive(false);
    }

    private void SetRewardName(int idx, VersusTowerParam param)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = (int) param.SeasonItemnum[idx];
      string key = (string) param.SeasonIteminame[idx];
      VERSUS_ITEM_TYPE versusItemType = param.SeasonItemType[idx];
      if (!Object.op_Inequality((Object) this.rewardTxt, (Object) null))
        return;
      string str = string.Empty;
      switch (versusItemType)
      {
        case VERSUS_ITEM_TYPE.item:
          ItemParam itemParam = instance.GetItemParam(key);
          if (itemParam != null)
          {
            str = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.gold:
          str = num.ToString() + LocalizedText.Get("sys.GOLD");
          break;
        case VERSUS_ITEM_TYPE.coin:
          str = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          break;
        case VERSUS_ITEM_TYPE.unit:
          UnitParam unitParam = instance.GetUnitParam(key);
          if (unitParam != null)
          {
            str = unitParam.name;
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(key);
          if (artifactParam != null)
          {
            str = artifactParam.name;
            break;
          }
          break;
      }
      this.rewardTxt.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_NAME"), (object) str));
    }

    private void ReqAnim(string anim)
    {
      if (!Object.op_Inequality((Object) this.root, (Object) null))
        return;
      Animator component = (Animator) this.root.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Play(anim);
    }

    private void CreateResult()
    {
      VersusTowerParam versusTowerParam = MonoSingleton<GameManager>.Instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam == null || !Object.op_Inequality((Object) this.template, (Object) null))
        return;
      for (int idx = 0; idx < versusTowerParam.SeasonIteminame.Length; ++idx)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.template);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          DataSource.Bind<VersusTowerParam>(gameObject, versusTowerParam);
          gameObject.SetActive(true);
          this.SetData(idx, false, gameObject);
          if (Object.op_Inequality((Object) this.parent, (Object) null))
            gameObject.get_transform().SetParent(this.parent.get_transform(), false);
        }
      }
      this.template.SetActive(false);
      this.item.SetActive(false);
    }

    public void OnClickNext()
    {
      if (this.mMode == VersusSeasonReward.MODE.WAIT)
      {
        this.mWaitTime = this.WAIT_TIME;
        if (++this.mNow < this.mMax)
        {
          this.mMode = VersusSeasonReward.MODE.NEXT;
          this.ReqAnim(this.nextAnim);
        }
        else
        {
          if (this.mMax != 1)
          {
            this.CreateResult();
            this.ReqAnim(this.resultAnim);
          }
          this.SetButtonText(false);
          this.mMode = VersusSeasonReward.MODE.FINISH;
        }
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
      }
      else
      {
        if (this.mMode != VersusSeasonReward.MODE.END)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "Finish");
        this.mMode = VersusSeasonReward.MODE.NONE;
      }
    }

    private enum MODE
    {
      NONE,
      REQ,
      COUNTDOWN,
      WAIT,
      NEXT,
      FINISH,
      END,
    }
  }
}
