namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

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
        private Result m_Result;
        private bool m_Destroy;
        private ResultEvent OnClickOk;
        private ResultEvent OnClickCancel;
        private static Canvas m_ModalCanvas;

        static ContinueWindow()
        {
        }

        public ContinueWindow()
        {
            this.m_TreasureListNodes = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Close(bool destroy)
        {
            if ((this.m_Animator != null) == null)
            {
                goto Label_0022;
            }
            this.m_Animator.SetBool("close", 1);
        Label_0022:
            this.m_Destroy = destroy;
            return;
        }

        public static bool Create(GameObject res, ResultEvent ok, ResultEvent cancel)
        {
            GameObject obj2;
            GameObject obj3;
            ContinueWindow window;
            Destroy();
            obj2 = res;
            if ((obj2 != null) == null)
            {
                goto Label_0076;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_0076;
            }
            m_ModalCanvas = UIUtility.PushCanvas(0, -1);
            obj3.get_transform().SetParent(m_ModalCanvas.get_transform(), 0);
            window = obj3.GetComponent<ContinueWindow>();
            if ((window != null) == null)
            {
                goto Label_0071;
            }
            window.OnClickOk = ok;
            window.OnClickCancel = cancel;
            window.Open();
            return 1;
        Label_0071:
            Destroy();
        Label_0076:
            return 0;
        }

        public static void Destroy()
        {
            if ((m_ModalCanvas != null) == null)
            {
                goto Label_0025;
            }
            UIUtility.PopCanvas(1);
            Object.Destroy(m_ModalCanvas.get_gameObject());
        Label_0025:
            m_ModalCanvas = null;
            return;
        }

        public static void ForceClose()
        {
            ContinueWindow window;
            if ((m_ModalCanvas != null) == null)
            {
                goto Label_0033;
            }
            window = m_ModalCanvas.get_gameObject().GetComponentInChildren<ContinueWindow>();
            if ((window != null) == null)
            {
                goto Label_0033;
            }
            window.Close(1);
        Label_0033:
            return;
        }

        public unsafe bool IsClosed()
        {
            AnimatorStateInfo info;
            if ((this.m_Animator == null) != null)
            {
                goto Label_0053;
            }
            if ((this.m_Animator.get_runtimeAnimatorController() == null) != null)
            {
                goto Label_0053;
            }
            if (this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null)
            {
                goto Label_0053;
            }
            if (((int) this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length) != null)
            {
                goto Label_0055;
            }
        Label_0053:
            return 1;
        Label_0055:
            if (this.m_Animator.GetBool("close") == null)
            {
                goto Label_0084;
            }
            return &this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("closed");
        Label_0084:
            return 0;
        }

        public unsafe bool IsOpend()
        {
            AnimatorStateInfo info;
            if ((this.m_Animator == null) != null)
            {
                goto Label_0053;
            }
            if ((this.m_Animator.get_runtimeAnimatorController() == null) != null)
            {
                goto Label_0053;
            }
            if (this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null)
            {
                goto Label_0053;
            }
            if (((int) this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length) != null)
            {
                goto Label_0055;
            }
        Label_0053:
            return 1;
        Label_0055:
            if (this.m_Animator.GetBool("close") != null)
            {
                goto Label_0084;
            }
            return &this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("opened");
        Label_0084:
            return 0;
        }

        private void OnClickButton(GameObject obj)
        {
            bool flag;
            this.Close(1);
            if ((obj == this.ButtonOk.get_gameObject()) == null)
            {
                goto Label_002B;
            }
            this.m_Result = 1;
            goto Label_0032;
        Label_002B:
            this.m_Result = 2;
        Label_0032:
            return;
        }

        public void Open()
        {
            if ((this.m_Animator != null) == null)
            {
                goto Label_0022;
            }
            this.m_Animator.SetBool("close", 0);
        Label_0022:
            this.m_Result = 0;
            this.m_Destroy = 0;
            return;
        }

        private unsafe void Start()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            SceneBattle battle;
            int num;
            battle = SceneBattle.Instance;
            this.m_Animator = base.GetComponent<Animator>();
            if ((this.Message != null) == null)
            {
                goto Label_0104;
            }
            objArray1 = new object[] { (OInt) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost };
            this.Message.set_text(LocalizedText.Get("sys.CONTINUE_MSG", objArray1));
            if ((battle != null) == null)
            {
                goto Label_0104;
            }
            if (battle.Battle == null)
            {
                goto Label_0104;
            }
            if (battle.Battle.IsMultiTower == null)
            {
                goto Label_00BD;
            }
            objArray2 = new object[] { (OInt) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower };
            this.Message.set_text(LocalizedText.Get("sys.CONTINUE_MSG", objArray2));
            goto Label_0104;
        Label_00BD:
            if (battle.Battle.IsMultiPlay == null)
            {
                goto Label_0104;
            }
            objArray3 = new object[] { (OInt) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti };
            this.Message.set_text(LocalizedText.Get("sys.CONTINUE_MSG", objArray3));
        Label_0104:
            if ((this.CoinNum != null) == null)
            {
                goto Label_0137;
            }
            this.CoinNum.set_text(&MonoSingleton<GameManager>.Instance.Player.Coin.ToString());
        Label_0137:
            ConfigWindow.SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, base.get_gameObject(), this.m_TreasureListNodes);
            UIUtility.AddEventListener(this.ButtonOk.get_gameObject(), this.ButtonOk.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
            UIUtility.AddEventListener(this.ButtonCancel.get_gameObject(), this.ButtonCancel.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void Update()
        {
            if (this.IsClosed() == null)
            {
                goto Label_0070;
            }
            if (this.m_Destroy == null)
            {
                goto Label_0070;
            }
            if (this.m_Result != 1)
            {
                goto Label_0043;
            }
            if (this.OnClickOk == null)
            {
                goto Label_006B;
            }
            this.OnClickOk(base.get_gameObject());
            goto Label_006B;
        Label_0043:
            if (this.m_Result != 2)
            {
                goto Label_006B;
            }
            if (this.OnClickCancel == null)
            {
                goto Label_006B;
            }
            this.OnClickCancel(base.get_gameObject());
        Label_006B:
            Destroy();
        Label_0070:
            return;
        }

        private enum Result
        {
            NONE,
            OK,
            CANCEL
        }

        public delegate void ResultEvent(GameObject dialog);
    }
}

