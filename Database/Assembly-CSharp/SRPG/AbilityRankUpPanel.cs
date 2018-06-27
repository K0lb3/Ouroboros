// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityRankUpPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    public string AB_RANKUP_ADD_WINDOW_PATH;
    private int UseCoin;

    public AbilityRankUpPanel()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateValue();
      this.Update();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResetButton, (UnityEngine.Object) null))
        this.ResetButton.AddListener(new SRPG_Button.ButtonClickEvent(this.Clicked));
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange += new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountChange);
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
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
      if (!((Selectable) button).IsInteractable() || string.IsNullOrEmpty(this.AB_RANKUP_ADD_WINDOW_PATH))
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>(this.AB_RANKUP_ADD_WINDOW_PATH);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      AbilityRankUpPointAddWindow componentInChildren = (AbilityRankUpPointAddWindow) gameObject2.GetComponentInChildren<AbilityRankUpPointAddWindow>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.OnDecide = new AbilityRankUpPointAddWindow.DecideEvent(this.OnDecide);
      componentInChildren.OnCancel = (AbilityRankUpPointAddWindow.CancelEvent) null;
    }

    private void OnDecide(int value)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.Coin < (int) instance.MasterParam.FixParam.AbilityRankupPointCoinRate * value)
      {
        instance.ConfirmBuyCoin((GameManager.BuyCoinEvent) null, (GameManager.BuyCoinEvent) null);
      }
      else
      {
        instance.OnAbilityRankUpCountPreReset(0);
        this.UseCoin = value;
        Network.RequestAPI((WebAPI) new ReqItemAbilPointPaid(value, new Network.ResponseCallback(this.OnRequestResult)), false);
      }
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
        AnalyticsManager.TrackOriginalCurrencyUse(ESaleType.Coin, (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpCountCoin, "AbilityPoint");
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainingCount, (UnityEngine.Object) null))
        this.RemainingCount.set_text(LocalizedText.Get("sys.AB_RANKUPCOUNT", new object[1]
        {
          (object) player.AbilityRankUpCountNum
        }));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResetButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.ResetButton).set_interactable(player.AbilityRankUpCountNum < (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpPointMax && player.Coin > 0);
    }

    private void Update()
    {
      long countRecoverySec = MonoSingleton<GameManager>.Instance.Player.GetNextAbilityRankUpCountRecoverySec();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoveryTimeText, (UnityEngine.Object) null) && countRecoverySec > 0L)
      {
        string minSecString = TimeManager.ToMinSecString(countRecoverySec);
        if (this.RecoveryTimeText.get_text() != minSecString)
          this.RecoveryTimeText.set_text(minSecString);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoveryTimeParent, (UnityEngine.Object) null))
        return;
      this.RecoveryTimeParent.SetActive(countRecoverySec > 0L);
    }

    public delegate void ResetAbilityRankUpCountEvent();
  }
}
