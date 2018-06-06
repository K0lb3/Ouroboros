// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitGetWindow : MonoBehaviour
  {
    public GameObject PopupUnit;
    [Space(5f)]
    public GameObject PopupAnimation;
    public string PopupRarityVar;
    public string PopupShardVar;
    [Space(5f)]
    public GameObject ShardNum;
    [Space(10f)]
    public GameObject ShardGauge;
    [Space(5f)]
    public GameObject ShardAnimation;
    [Space(5f)]
    public string EndShardState;
    [Space(10f)]
    public GameObject NormalGetEffect;
    public GameObject RareGetEffect;
    public GameObject SRareGetEffect;
    private UnitData mUnitData;
    private Animator mPopupAnimator;
    private GetUnitShard mShardWindow;
    private GameObject mCurrentGetEffect;
    private bool mIsEnd;
    public bool isMaxShard;
    private bool mIsShardEnd;
    private bool mIsEffectEnd;
    private bool mIsClickClose;

    public UnitGetWindow()
    {
      base.\u002Ector();
    }

    public bool IsEnd
    {
      get
      {
        return this.mIsEnd;
      }
    }

    public void Init(string unitId, bool isConver)
    {
      if (string.IsNullOrEmpty(unitId) || Object.op_Equality((Object) this.PopupUnit, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mUnitData = instance.Player.Units.Find((Predicate<UnitData>) (u => u.UnitID == unitId));
      if (this.mUnitData == null)
      {
        Json_Unit json = new Json_Unit();
        json.iid = -1L;
        json.iname = unitId;
        this.mUnitData = new UnitData();
        this.mUnitData.Deserialize(json);
      }
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mUnitData);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), instance.MasterParam.GetItemParam((string) this.mUnitData.UnitParam.piece));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      bool flag = isConver;
      this.mIsShardEnd = !flag;
      if (flag && Object.op_Inequality((Object) this.ShardNum, (Object) null))
      {
        Text component = (Text) this.ShardNum.GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_text(this.mUnitData.GetChangePieces().ToString());
      }
      if (!Object.op_Inequality((Object) this.ShardGauge, (Object) null))
        return;
      this.ShardGauge.SetActive(flag);
      if (!flag)
        return;
      int awakeLv = this.mUnitData.AwakeLv;
      if (awakeLv < this.mUnitData.GetAwakeLevelCap())
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) this.mUnitData.UnitParam.piece);
        this.mShardWindow = (GetUnitShard) this.ShardGauge.GetComponent<GetUnitShard>();
        this.mShardWindow.Refresh(this.mUnitData.UnitParam, itemParam.name, awakeLv, this.mUnitData.GetChangePieces(), 0);
      }
      else
      {
        this.ShardGauge.get_gameObject().SetActive(false);
        this.mIsShardEnd = true;
      }
    }

    public void PlayAnim(bool isConver)
    {
      int rare = (int) this.mUnitData.UnitParam.rare;
      bool flag = isConver;
      this.PopupUnit.SetActive(true);
      this.SpawnGetEffect(rare);
      this.mPopupAnimator = (Animator) this.PopupAnimation.GetComponent<Animator>();
      this.mPopupAnimator.SetInteger(this.PopupRarityVar, rare + 1);
      this.mPopupAnimator.SetBool(this.PopupShardVar, flag);
    }

    public void OnCloseClick()
    {
      this.mIsClickClose = true;
      if (!Object.op_Inequality((Object) this.mShardWindow, (Object) null))
        return;
      this.mShardWindow.OnClicked();
    }

    private void Update()
    {
      if (this.mIsEnd)
        return;
      if (!this.mIsShardEnd && Object.op_Inequality((Object) this.mShardWindow, (Object) null) && !this.mShardWindow.IsRunningAnimator)
      {
        this.mIsShardEnd = true;
      }
      else
      {
        if (this.mIsShardEnd && this.mIsClickClose && (this.mPopupAnimator.GetInteger(this.PopupRarityVar) > 0 && !this.mIsShardEnd))
          this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
        if (this.mIsShardEnd && this.mIsClickClose && (this.mPopupAnimator.GetInteger(this.PopupRarityVar) > 0 && this.isMaxShard) && (Object.op_Inequality((Object) this.mShardWindow, (Object) null) && !this.mShardWindow.IsRunningAnimator))
        {
          this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
          this.isMaxShard = false;
        }
        if (this.mIsEnd || !this.mIsShardEnd || (!this.mIsClickClose || GameUtility.IsAnimatorRunning((Component) this.mPopupAnimator)))
          return;
        this.mIsEnd = true;
      }
    }

    private void SpawnGetEffect(int rarity)
    {
      switch (rarity)
      {
        case 0:
        case 1:
        case 2:
          this.mCurrentGetEffect = this.NormalGetEffect;
          break;
        case 3:
          this.mCurrentGetEffect = this.RareGetEffect;
          break;
        default:
          this.mCurrentGetEffect = this.SRareGetEffect;
          break;
      }
      if (Object.op_Equality((Object) this.mCurrentGetEffect, (Object) null))
        return;
      this.mCurrentGetEffect.SetActive(true);
      this.mCurrentGetEffect.get_transform().SetParent(this.PopupUnit.get_transform(), false);
    }
  }
}
