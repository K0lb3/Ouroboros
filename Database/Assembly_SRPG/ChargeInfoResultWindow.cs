namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(20, "Close", 1, 20), Pin(0, "Refresh", 0, 0), Pin(10, "Skip", 0, 10)]
    public class ChargeInfoResultWindow : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_REFRESH = 0;
        private const int INPUT_SKIP = 10;
        private const int OUTPUT_CLOSE = 20;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private GameObject UnitTemplate;
        [SerializeField]
        private GameObject ArtifactTemplate;
        [SerializeField]
        private GameObject ConceptCardTemplate;
        [SerializeField]
        private GameObject CoinTemplate;
        [SerializeField]
        private GameObject GoldTemplate;
        [SerializeField]
        private string CheckAnimState;
        [SerializeField]
        private string SkipAnimTrigger;
        [SerializeField]
        private string SkipToAnimState;
        [SerializeField]
        private GameObject BackGround;
        private List<FirstChargeReward> m_Rewards;
        private List<GameObject> m_IconObj;
        private Animator m_WindowAnim;
        private bool m_IsRefresh;
        private bool m_IsClosed;

        public ChargeInfoResultWindow()
        {
            this.CheckAnimState = "opened";
            this.SkipAnimTrigger = "skip";
            this.SkipToAnimState = "dropitem_End";
            this.m_Rewards = new List<FirstChargeReward>();
            this.m_IconObj = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0012;
            }
            this.m_IsRefresh = 1;
            goto Label_0038;
        Label_0012:
            if (pinID != 10)
            {
                goto Label_0038;
            }
            if (this.m_IsClosed == null)
            {
                goto Label_0032;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 20);
            goto Label_0038;
        Label_0032:
            this.SkipIconAnimation();
        Label_0038:
            return;
        }

        private void Awake()
        {
            Animator animator;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            if ((this.UnitTemplate != null) == null)
            {
                goto Label_003A;
            }
            this.UnitTemplate.SetActive(0);
        Label_003A:
            if ((this.ArtifactTemplate != null) == null)
            {
                goto Label_0057;
            }
            this.ArtifactTemplate.SetActive(0);
        Label_0057:
            if ((this.ConceptCardTemplate != null) == null)
            {
                goto Label_0074;
            }
            this.ConceptCardTemplate.SetActive(0);
        Label_0074:
            if ((this.CoinTemplate != null) == null)
            {
                goto Label_0091;
            }
            this.CoinTemplate.SetActive(0);
        Label_0091:
            if ((this.GoldTemplate != null) == null)
            {
                goto Label_00AE;
            }
            this.GoldTemplate.SetActive(0);
        Label_00AE:
            animator = base.get_gameObject().GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00CD;
            }
            this.m_WindowAnim = animator;
        Label_00CD:
            return;
        }

        private void CheckIconAnimation()
        {
            int num;
            int num2;
            GameObject obj2;
            if (((this.m_IsClosed != null) || (this.m_IconObj == null)) || (this.m_IconObj.Count <= 0))
            {
                goto Label_0091;
            }
            num = 0;
            num2 = 0;
            goto Label_0068;
        Label_0030:
            obj2 = this.m_IconObj[num2];
            if ((obj2 != null) == null)
            {
                goto Label_0064;
            }
            num = (GameUtility.CompareAnimatorStateName(obj2, this.SkipToAnimState) == null) ? num : (num + 1);
        Label_0064:
            num2 += 1;
        Label_0068:
            if (num2 < this.m_IconObj.Count)
            {
                goto Label_0030;
            }
            if (num != this.m_IconObj.Count)
            {
                goto Label_0091;
            }
            this.m_IsClosed = 1;
        Label_0091:
            return;
        }

        private void Refresh()
        {
            int num;
            FirstChargeReward reward;
            GameObject obj2;
            if ((this.m_WindowAnim == null) == null)
            {
                goto Label_0023;
            }
            DebugUtility.LogError("Animator Not Found");
            this.m_IsRefresh = 0;
            return;
        Label_0023:
            if (GameUtility.CompareAnimatorStateName(this.m_WindowAnim, this.CheckAnimState) != null)
            {
                goto Label_003A;
            }
            return;
        Label_003A:
            if (this.m_Rewards == null)
            {
                goto Label_0056;
            }
            if (this.m_Rewards.Count >= 0)
            {
                goto Label_0061;
            }
        Label_0056:
            DebugUtility.LogError("受け取り報酬が存在しません.");
            return;
        Label_0061:
            num = 0;
            goto Label_01F9;
        Label_0068:
            reward = this.m_Rewards[num];
            obj2 = null;
            if (reward.CheckGiftTypes(1L) == null)
            {
                goto Label_00B7;
            }
            obj2 = this.SetItem(reward.iname, reward.num);
            if ((obj2 == null) == null)
            {
                goto Label_01DD;
            }
            DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Item)!");
            goto Label_01F5;
            goto Label_01DD;
        Label_00B7:
            if (reward.CheckGiftTypes(2L) == null)
            {
                goto Label_00F1;
            }
            obj2 = this.SetGold(reward.num);
            if ((obj2 == null) == null)
            {
                goto Label_01DD;
            }
            DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Gold)!");
            goto Label_01F5;
            goto Label_01DD;
        Label_00F1:
            if (reward.CheckGiftTypes(4L) == null)
            {
                goto Label_012B;
            }
            obj2 = this.SetCoin(reward.num);
            if ((obj2 == null) == null)
            {
                goto Label_01DD;
            }
            DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Coin)!");
            goto Label_01F5;
            goto Label_01DD;
        Label_012B:
            if (reward.CheckGiftTypes(0x80L) == null)
            {
                goto Label_0169;
            }
            obj2 = this.SetUnit(reward.iname);
            if ((obj2 == null) == null)
            {
                goto Label_01DD;
            }
            DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Unit)!");
            goto Label_01F5;
            goto Label_01DD;
        Label_0169:
            if (reward.CheckGiftTypes(0x40L) == null)
            {
                goto Label_01A4;
            }
            obj2 = this.SetArtifact(reward.iname);
            if ((obj2 == null) == null)
            {
                goto Label_01DD;
            }
            DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Artifact)!");
            goto Label_01F5;
            goto Label_01DD;
        Label_01A4:
            if (reward.CheckGiftTypes(0x1000L) == null)
            {
                goto Label_01DD;
            }
            obj2 = this.SetConceptCard(reward.iname);
            if ((obj2 == null) == null)
            {
                goto Label_01DD;
            }
            DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:ConceptCard)!");
            goto Label_01F5;
        Label_01DD:
            if ((obj2 != null) == null)
            {
                goto Label_01F5;
            }
            this.m_IconObj.Add(obj2);
        Label_01F5:
            num += 1;
        Label_01F9:
            if (num < this.m_Rewards.Count)
            {
                goto Label_0068;
            }
            this.m_IsRefresh = 0;
            return;
        }

        private void SetAnimatorTrigger(GameObject _obj, string _name)
        {
            Animator animator;
            if ((_obj != null) == null)
            {
                goto Label_0026;
            }
            animator = _obj.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0026;
            }
            animator.SetTrigger(_name);
        Label_0026:
            return;
        }

        private GameObject SetArtifact(string _iname)
        {
            GameObject obj2;
            ArtifactParam param;
            ArtifactData data;
            Json_Artifact artifact;
            obj2 = null;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(_iname);
            if (param != null)
            {
                goto Label_0030;
            }
            DebugUtility.LogError("武具 INAME:" + _iname + "は存在しません");
            return null;
        Label_0030:
            if ((this.ArtifactTemplate != null) == null)
            {
                goto Label_00D0;
            }
            obj2 = Object.Instantiate<GameObject>(this.ArtifactTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_00D0;
            }
            obj2.get_transform().SetParent(this.ArtifactTemplate.get_transform().get_parent(), 0);
            obj2.SetActive(1);
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.fav = 0;
            artifact.rare = param.rareini;
            data.Deserialize(artifact);
            DataSource.Bind<ArtifactData>(obj2, data);
            this.SetAnimatorTrigger(obj2, "on");
        Label_00D0:
            return obj2;
        }

        private unsafe void SetBitmapText(GameObject _obj, string _name, int _num)
        {
            SerializeValueBehaviour behaviour;
            Text text;
            behaviour = _obj.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_0039;
            }
            text = behaviour.list.GetUILabel(_name);
            if ((text != null) == null)
            {
                goto Label_0039;
            }
            text.set_text(&_num.ToString());
        Label_0039:
            return;
        }

        private GameObject SetCoin(int _num)
        {
            GameObject obj2;
            obj2 = null;
            if ((this.CoinTemplate != null) == null)
            {
                goto Label_0067;
            }
            obj2 = Object.Instantiate<GameObject>(this.CoinTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_0067;
            }
            obj2.get_transform().SetParent(this.CoinTemplate.get_transform().get_parent(), 0);
            obj2.SetActive(1);
            this.SetBitmapText(obj2, "num", _num);
            this.SetAnimatorTrigger(obj2, "on");
        Label_0067:
            return obj2;
        }

        private GameObject SetConceptCard(string _iname)
        {
            GameObject obj2;
            ConceptCardData data;
            ConceptCardIcon icon;
            obj2 = null;
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(_iname) != null)
            {
                goto Label_002E;
            }
            DebugUtility.LogError("真理念装 INAME:" + _iname + "は存在しません");
            return null;
        Label_002E:
            if ((this.ConceptCardTemplate != null) == null)
            {
                goto Label_00AD;
            }
            obj2 = Object.Instantiate<GameObject>(this.ConceptCardTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_00AD;
            }
            obj2.get_transform().SetParent(this.ConceptCardTemplate.get_transform().get_parent(), 0);
            obj2.SetActive(1);
            data = ConceptCardData.CreateConceptCardDataForDisplay(_iname);
            if (data == null)
            {
                goto Label_00A1;
            }
            icon = obj2.GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_00A1;
            }
            icon.Setup(data);
        Label_00A1:
            this.SetAnimatorTrigger(obj2, "on");
        Label_00AD:
            return obj2;
        }

        private GameObject SetGold(int _num)
        {
            GameObject obj2;
            obj2 = null;
            if ((this.GoldTemplate != null) == null)
            {
                goto Label_0067;
            }
            obj2 = Object.Instantiate<GameObject>(this.GoldTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_0067;
            }
            obj2.get_transform().SetParent(this.GoldTemplate.get_transform().get_parent(), 0);
            obj2.SetActive(1);
            this.SetBitmapText(obj2, "num", _num);
            this.SetAnimatorTrigger(obj2, "on");
        Label_0067:
            return obj2;
        }

        private GameObject SetItem(string _iname, int _num)
        {
            GameObject obj2;
            ItemData data;
            obj2 = null;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0072;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_0072;
            }
            obj2.get_transform().SetParent(this.ItemTemplate.get_transform().get_parent(), 0);
            obj2.SetActive(1);
            data = new ItemData();
            data.Setup(0L, _iname, _num);
            DataSource.Bind<ItemData>(obj2, data);
            this.SetAnimatorTrigger(obj2, "on");
        Label_0072:
            return obj2;
        }

        private GameObject SetUnit(string _iname)
        {
            GameObject obj2;
            UnitParam param;
            UnitData data;
            obj2 = null;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(_iname);
            if (param != null)
            {
                goto Label_0030;
            }
            DebugUtility.LogError("ユニット INAME:" + _iname + "は存在しません.");
            return null;
        Label_0030:
            if ((this.UnitTemplate != null) == null)
            {
                goto Label_00AD;
            }
            obj2 = Object.Instantiate<GameObject>(this.UnitTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_00AD;
            }
            obj2.get_transform().SetParent(this.UnitTemplate.get_transform().get_parent(), 0);
            data = new UnitData();
            data.Setup(_iname, 0, 0, 0, string.Empty, 1, param.element, 0);
            DataSource.Bind<UnitData>(obj2, data);
            obj2.SetActive(1);
            this.SetAnimatorTrigger(obj2, "on");
        Label_00AD:
            return obj2;
        }

        public void SetUp(FirstChargeReward[] _rewards)
        {
            if (_rewards == null)
            {
                goto Label_0026;
            }
            if (((int) _rewards.Length) <= 0)
            {
                goto Label_0026;
            }
            this.m_Rewards.Clear();
            this.m_Rewards.AddRange(_rewards);
        Label_0026:
            return;
        }

        private void SkipIconAnimation()
        {
            int num;
            GameObject obj2;
            Animator animator;
            Button button;
            if (this.m_IsClosed != null)
            {
                goto Label_00A1;
            }
            if (this.m_IconObj == null)
            {
                goto Label_00A1;
            }
            num = 0;
            goto Label_0059;
        Label_001D:
            obj2 = this.m_IconObj[num];
            if ((obj2 != null) == null)
            {
                goto Label_0055;
            }
            animator = obj2.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0055;
            }
            animator.SetTrigger(this.SkipAnimTrigger);
        Label_0055:
            num += 1;
        Label_0059:
            if (num < this.m_IconObj.Count)
            {
                goto Label_001D;
            }
            this.m_IsClosed = 1;
            if ((this.BackGround != null) == null)
            {
                goto Label_00A1;
            }
            button = this.BackGround.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_00A1;
            }
            button.set_interactable(0);
        Label_00A1:
            return;
        }

        private void Update()
        {
            if (this.m_IsRefresh == null)
            {
                goto Label_0016;
            }
            this.Refresh();
            goto Label_001C;
        Label_0016:
            this.CheckIconAnimation();
        Label_001C:
            return;
        }
    }
}

