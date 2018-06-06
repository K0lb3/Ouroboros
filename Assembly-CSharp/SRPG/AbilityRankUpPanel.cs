// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityRankUpPanel
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AbilityRankUpPanel : MonoBehaviour, IGameParameter
  {
    public Text RemainingCount;
    public GameObject RecoveryTimeParent;
    public Text RecoveryTimeText;
    public SRPG_Button ResetButton;
    public AbilityRankUpPanel.ResetAbilityRankUpCountEvent OnAbilityRankUpCountReset;

    public AbilityRankUpPanel()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateValue();
      this.Update();
      if (Object.op_Inequality((Object) this.ResetButton, (Object) null))
        this.ResetButton.AddListener(new SRPG_Button.ButtonClickEvent(this.Clicked));
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange += new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountChange);
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange -= new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountChange);
    }

    private void OnAbilityRankUpCountChange(int count)
    {
      this.UpdateValue();
      this.Update();
    }

    private void Clicked(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      UIUtility.ConfirmBox(LocalizedText.Get("sys.RESTORE_ABILITY_RANKUP_NUM", new object[1]
      {
        (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpCountCoin
      }), new UIUtility.DialogResultEvent(this.OnAccept), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    private void OnAccept(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      int abilityRankUpCountCoin = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpCountCoin;
      if (player.Coin < abilityRankUpCountCoin)
      {
        instance.ConfirmBuyCoin((GameManager.BuyCoinEvent) null, (GameManager.BuyCoinEvent) null);
      }
      else
      {
        instance.OnAbilityRankUpCountPreReset(0);
        if (Network.Mode == Network.EConnectMode.Offline)
        {
          player.DEBUG_CONSUME_COIN(abilityRankUpCountCoin);
          player.RestoreAbilityRankUpCount();
          this.Success();
        }
        else
          Network.RequestAPI((WebAPI) new ReqItemAbilPointPaid(new Network.ResponseCallback(this.OnRequestResult)), false);
      }
    }

    private void OnRequestResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.AbilityCoinShort)
          FlowNode_Network.Back();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Retry();
          return;
        }
        AnalyticsManager.TrackSpendCoin("AbilityPoint", (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpCountCoin);
        this.UpdateValue();
        this.Success();
      }
    }

    private void Success()
    {
      if (this.OnAbilityRankUpCountReset != null)
        this.OnAbilityRankUpCountReset();
      MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
    }

    public void UpdateValue()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Object.op_Inequality((Object) this.RemainingCount, (Object) null))
        this.RemainingCount.set_text(LocalizedText.Get("sys.AB_RANKUPCOUNT", new object[1]
        {
          (object) player.AbilityRankUpCountNum
        }));
      if (!Object.op_Inequality((Object) this.ResetButton, (Object) null))
        return;
      ((Selectable) this.ResetButton).set_interactable(player.AbilityRankUpCountNum <= 0);
    }

    private void Update()
    {
      long countRecoverySec = MonoSingleton<GameManager>.Instance.Player.GetNextAbilityRankUpCountRecoverySec();
      if (Object.op_Inequality((Object) this.RecoveryTimeText, (Object) null) && countRecoverySec > 0L)
      {
        string minSecString = TimeManager.ToMinSecString(countRecoverySec);
        if (this.RecoveryTimeText.get_text() != minSecString)
          this.RecoveryTimeText.set_text(minSecString);
      }
      if (!Object.op_Inequality((Object) this.RecoveryTimeParent, (Object) null))
        return;
      this.RecoveryTimeParent.SetActive(countRecoverySec > 0L);
    }

    public delegate void ResetAbilityRankUpCountEvent();
  }
}
