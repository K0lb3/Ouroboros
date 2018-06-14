// Decompiled with JetBrains decompiler
// Type: SRPG.ContinueWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ContinueWindow : MonoBehaviour
  {
    public Text Message;
    public Text CoinNum;
    public Button ButtonOk;
    public Button ButtonCancel;
    public GameObject Prefab_NewItemBadge;
    public GameObject TreasureList;
    public GameObject TreasureListNode;
    private Animator m_Animator;
    private List<GameObject> m_TreasureListNodes;
    private ContinueWindow.Result m_Result;
    private bool m_Destroy;
    private ContinueWindow.ResultEvent OnClickOk;
    private ContinueWindow.ResultEvent OnClickCancel;
    private static Canvas m_ModalCanvas;

    public ContinueWindow()
    {
      base.\u002Ector();
    }

    public static bool Create(GameObject res, ContinueWindow.ResultEvent ok, ContinueWindow.ResultEvent cancel)
    {
      ContinueWindow.Destroy();
      GameObject gameObject1 = res;
      if (Object.op_Inequality((Object) gameObject1, (Object) null))
      {
        GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
        if (Object.op_Inequality((Object) gameObject2, (Object) null))
        {
          ContinueWindow.m_ModalCanvas = UIUtility.PushCanvas(false, -1);
          gameObject2.get_transform().SetParent(((Component) ContinueWindow.m_ModalCanvas).get_transform(), false);
          ContinueWindow component = (ContinueWindow) gameObject2.GetComponent<ContinueWindow>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.OnClickOk = ok;
            component.OnClickCancel = cancel;
            component.Open();
            return true;
          }
          ContinueWindow.Destroy();
        }
      }
      return false;
    }

    public static void Destroy()
    {
      if (Object.op_Inequality((Object) ContinueWindow.m_ModalCanvas, (Object) null))
      {
        UIUtility.PopCanvas(true);
        Object.Destroy((Object) ((Component) ContinueWindow.m_ModalCanvas).get_gameObject());
      }
      ContinueWindow.m_ModalCanvas = (Canvas) null;
    }

    public static void ForceClose()
    {
      if (!Object.op_Inequality((Object) ContinueWindow.m_ModalCanvas, (Object) null))
        return;
      ContinueWindow componentInChildren = (ContinueWindow) ((Component) ContinueWindow.m_ModalCanvas).get_gameObject().GetComponentInChildren<ContinueWindow>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Close(true);
    }

    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      this.m_Animator = (Animator) ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) this.Message, (Object) null))
      {
        this.Message.set_text(LocalizedText.Get("sys.CONTINUE_MSG", new object[1]
        {
          (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost
        }));
        if (Object.op_Inequality((Object) instance, (Object) null) && instance.Battle != null)
        {
          if (instance.Battle.IsMultiTower)
            this.Message.set_text(LocalizedText.Get("sys.CONTINUE_MSG", new object[1]
            {
              (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower
            }));
          else if (instance.Battle.IsMultiPlay)
            this.Message.set_text(LocalizedText.Get("sys.CONTINUE_MSG", new object[1]
            {
              (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti
            }));
        }
      }
      if (Object.op_Inequality((Object) this.CoinNum, (Object) null))
        this.CoinNum.set_text(MonoSingleton<GameManager>.Instance.Player.Coin.ToString());
      ConfigWindow.SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, ((Component) this).get_gameObject(), this.m_TreasureListNodes);
      UIUtility.AddEventListener(((Component) this.ButtonOk).get_gameObject(), (UnityEvent) this.ButtonOk.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
      UIUtility.AddEventListener(((Component) this.ButtonCancel).get_gameObject(), (UnityEvent) this.ButtonCancel.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void Update()
    {
      if (!this.IsClosed() || !this.m_Destroy)
        return;
      if (this.m_Result == ContinueWindow.Result.OK)
      {
        if (this.OnClickOk != null)
          this.OnClickOk(((Component) this).get_gameObject());
      }
      else if (this.m_Result == ContinueWindow.Result.CANCEL && this.OnClickCancel != null)
        this.OnClickCancel(((Component) this).get_gameObject());
      ContinueWindow.Destroy();
    }

    public void Open()
    {
      if (Object.op_Inequality((Object) this.m_Animator, (Object) null))
        this.m_Animator.SetBool("close", false);
      this.m_Result = ContinueWindow.Result.NONE;
      this.m_Destroy = false;
    }

    public void Close(bool destroy)
    {
      if (Object.op_Inequality((Object) this.m_Animator, (Object) null))
        this.m_Animator.SetBool("close", true);
      this.m_Destroy = destroy;
    }

    public bool IsOpend()
    {
      if (Object.op_Equality((Object) this.m_Animator, (Object) null) || Object.op_Equality((Object) this.m_Animator.get_runtimeAnimatorController(), (Object) null) || (this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null || this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length == 0))
        return true;
      if (this.m_Animator.GetBool("close"))
        return false;
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      return ((AnimatorStateInfo) @animatorStateInfo).IsName("opened");
    }

    public bool IsClosed()
    {
      if (Object.op_Equality((Object) this.m_Animator, (Object) null) || Object.op_Equality((Object) this.m_Animator.get_runtimeAnimatorController(), (Object) null) || (this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null || this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length == 0))
        return true;
      if (!this.m_Animator.GetBool("close"))
        return false;
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      return ((AnimatorStateInfo) @animatorStateInfo).IsName("closed");
    }

    private void OnClickButton(GameObject obj)
    {
      this.Close(true);
      if (Object.op_Equality((Object) obj, (Object) ((Component) this.ButtonOk).get_gameObject()))
        this.m_Result = ContinueWindow.Result.OK;
      else
        this.m_Result = ContinueWindow.Result.CANCEL;
    }

    private enum Result
    {
      NONE,
      OK,
      CANCEL,
    }

    public delegate void ResultEvent(GameObject dialog);
  }
}
