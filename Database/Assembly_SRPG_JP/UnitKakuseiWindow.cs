// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakuseiWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "ユニットが覚醒した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  public class UnitKakuseiWindow : MonoBehaviour, IFlowInterface
  {
    public UnitKakuseiWindow.KakuseiWindowEvent OnKakuseiAccept;
    public UnitData Unit;
    public JobParam UnlockJobParam;
    public Button KakuseiButton;
    public Text KakeraMsg;
    public Text KakeraCharaMsg;
    public Text KakeraElementMsg;
    public Text KakeraCommonMsg;
    public GameObject JobUnlock;
    private UnitData mCurrentUnit;
    private ItemParam mElementKakera;
    private ItemParam mCommonKakera;

    public UnitKakuseiWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      this.Refresh();
      if (!Object.op_Inequality((Object) this.KakuseiButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.KakuseiButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnKakuseiClick)));
    }

    private void OnKakuseiClick()
    {
      if (this.mElementKakera != null)
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), (object) this.mElementKakera.name), new UIUtility.DialogResultEvent(this.AcceptElementKakera), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.AcceptElementKakera((GameObject) null);
    }

    private void AcceptElementKakera(GameObject go)
    {
      if (this.mCommonKakera != null)
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_COMMON_KAKERA"), (object) this.mCommonKakera.name), new UIUtility.DialogResultEvent(this.AcceptCommonKakera), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.AcceptCommonKakera((GameObject) null);
    }

    private void AcceptCommonKakera(GameObject go)
    {
      this.KakuseiAccept();
    }

    private void KakuseiAccept()
    {
      if (this.OnKakuseiAccept != null)
      {
        this.OnKakuseiAccept();
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public void Refresh()
    {
      this.mCurrentUnit = this.Unit == null ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.Unit;
      if (this.mCurrentUnit == null || this.mCurrentUnit.GetAwakeLevelCap() <= this.mCurrentUnit.AwakeLv)
        return;
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mCurrentUnit);
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.UnitParam.piece);
      int itemAmount1 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece);
      int awakeNeedPieces = this.mCurrentUnit.GetAwakeNeedPieces();
      if (Object.op_Inequality((Object) this.KakuseiButton, (Object) null))
        ((Selectable) this.KakuseiButton).set_interactable(this.mCurrentUnit.CheckUnitAwaking());
      if (Object.op_Inequality((Object) this.KakeraMsg, (Object) null))
        this.KakeraMsg.set_text(string.Format(LocalizedText.Get("sys.CONFIRM_KAKUSEI"), (object) itemParam.name, (object) awakeNeedPieces, (object) itemAmount1));
      int num1 = awakeNeedPieces;
      int itemAmount2 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece);
      int num2 = itemAmount2 < awakeNeedPieces ? itemAmount2 : awakeNeedPieces;
      if (Object.op_Inequality((Object) this.KakeraCharaMsg, (Object) null))
      {
        if (num2 > 0)
        {
          this.KakeraCharaMsg.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_CHARA"), (object) itemParam.name, (object) num2, (object) itemAmount2));
          ((Component) this.KakeraCharaMsg).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.KakeraCharaMsg).get_gameObject().SetActive(false);
      }
      int num3 = Mathf.Max(0, num1 - num2);
      ItemParam elementPieceParam = this.mCurrentUnit.GetElementPieceParam();
      int elementPieces = this.mCurrentUnit.GetElementPieces();
      int num4 = elementPieces < num3 ? elementPieces : num3;
      this.mElementKakera = (ItemParam) null;
      if (Object.op_Inequality((Object) this.KakeraElementMsg, (Object) null) && elementPieceParam != null)
      {
        if (num4 > 0)
        {
          this.KakeraElementMsg.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_ELEMENT"), (object) elementPieceParam.name, (object) num4, (object) elementPieces));
          ((Component) this.KakeraElementMsg).get_gameObject().SetActive(true);
          this.mElementKakera = elementPieceParam;
        }
        else
          ((Component) this.KakeraElementMsg).get_gameObject().SetActive(false);
      }
      int num5 = Mathf.Max(0, num3 - num4);
      ItemParam commonPieceParam = this.mCurrentUnit.GetCommonPieceParam();
      int commonPieces = this.mCurrentUnit.GetCommonPieces();
      int num6 = commonPieces < num5 ? commonPieces : num5;
      this.mCommonKakera = (ItemParam) null;
      if (Object.op_Inequality((Object) this.KakeraCommonMsg, (Object) null) && commonPieceParam != null)
      {
        if (num6 > 0)
        {
          this.KakeraCommonMsg.set_text(string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_COMMON"), (object) commonPieceParam.name, (object) num6, (object) commonPieces));
          ((Component) this.KakeraCommonMsg).get_gameObject().SetActive(true);
          this.mCommonKakera = commonPieceParam;
        }
        else
          ((Component) this.KakeraCommonMsg).get_gameObject().SetActive(false);
      }
      Mathf.Max(0, num5 - num6);
      if (Object.op_Inequality((Object) this.JobUnlock, (Object) null))
      {
        bool flag = false;
        if (this.UnlockJobParam != null)
        {
          DataSource.Bind<JobParam>(this.JobUnlock, this.UnlockJobParam);
          flag = true;
        }
        this.JobUnlock.SetActive(flag);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public delegate void KakuseiWindowEvent();
  }
}
