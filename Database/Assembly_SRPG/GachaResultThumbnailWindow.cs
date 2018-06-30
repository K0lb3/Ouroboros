namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(20, "OneMore Exec", 0, 0), Pin(100, "UnitDetail", 1, 100), Pin(0x6f, "UnitDetailSingle", 1, 0x6f), Pin(0x65, "ItemDetail", 1, 0x65), Pin(0x66, "PieceDetail", 1, 0x66), Pin(0, "Refresh", 0, 0), Pin(0x67, "ArtifactDetail", 1, 0x67), Pin(0x68, "ConceptCardDetail", 1, 0x68), Pin(200, "BackGachaTop", 1, 200), Pin(0x52, "引き直し終了", 1, 0), Pin(90, "Interactable更新(True)", 0, 90), Pin(0x5b, "Interactable更新(False", 0, 0x5b), Pin(1, "BackToUnitDetail", 0, 1), Pin(10, "Back Top", 0, 0), Pin(11, "Back Top", 1, 0), Pin(0x15, "OneMore Exec", 1, 0), Pin(210, "Redraw Confirm", 1, 210), Pin(50, "Click Icon(アイコン選択)", 0, 210), Pin(0x33, "Click Card Unit Icon", 0, 0xd3), Pin(60, "アイコン表示初期化完了", 1, 0), Pin(0x3b, "アイコン表示初期化完了(ユニット単発の場合))", 1, 0x3b), Pin(0x3d, "オマケ表示スタート", 0, 0), Pin(70, "オマケ表示終了", 1, 0), Pin(0x47, "Click Bonus(おまけ表示)", 0, 0), Pin(80, "引き直し確定確認", 1, 0), Pin(0x51, "引き直し終了", 0, 0)]
    public class GachaResultThumbnailWindow : MonoBehaviour, IFlowInterface
    {
        private const int PIN_OT_GACHA_CONFIRM = 210;
        private const int PIN_IN_CLICK_ICON = 50;
        private const int PIN_IN_CLICK_CARDUNIT_ICON = 0x33;
        private const int PIN_OT_ICON_INITALIZED_UNIT = 0x3b;
        private const int PIN_OT_ICON_INITALIZED = 60;
        private const int PIN_IN_START_BONUS = 0x3d;
        private const int PIN_OT_FINISHED_BONUS = 70;
        private const int PIN_IN_CLICK_BONUS = 0x47;
        private const int PIN_OT_CONFIRM_REDRAW = 80;
        private const int PIN_IN_CLOSE_REDRAW = 0x51;
        private const int PIN_OT_CLOSE_REDRAW = 0x52;
        private const int PIN_IN_BTN_INTERACTABLE_TRUE = 90;
        private const int PIN_IN_BTN_INTERACTABLE_FALSE = 0x5b;
        private readonly int IN_REFRESH;
        private readonly int IN_BACKTO_UNITDETAIL;
        private readonly int IN_BACK_TOP;
        private readonly int OUT_BACK_TOP;
        private readonly int IN_ONEMORE_GACHA;
        private readonly int OUT_ONEMORE_GACHA;
        private readonly int OUT_UNITDETAIL;
        private readonly int OUT_UNITDETAIL_SINGLE;
        private readonly int OUT_ITEM_DETAIL;
        private readonly int OUT_ARTIFACT_DETAIL;
        private readonly int OUT_CONCEPTCARD_DETAIL;
        public static readonly int CONTENT_VIEW_MAX;
        public GameObject CautionText;
        private bool is_gift;
        private bool m_inialize;
        private bool m_FinishedBonusEffect;
        private GameObject m_Thumbnail;
        private GameObject m_UnitDetail;
        [SerializeField]
        private GameObject BlockTemplate;
        private GameObject m_ResultBlock;
        private List<GameObject> m_ResultIconRootList;
        [SerializeField]
        private GameObject DetailRoot;
        [SerializeField]
        private string BonusWindowPrefab;
        [SerializeField]
        private float WaitBonusWindow;
        [SerializeField]
        private Transform BonusRoot;
        [SerializeField]
        private SerializeValueBehaviour ButtonList;
        private Button m_OnemoreBtn;
        private Button m_DefaultBtn;
        private Button m_BonusBtn;
        private Button m_RedrawBtn;
        private GachaRequestParam mRequest;
        public static readonly int VIEW_COUNT;

        static GachaResultThumbnailWindow()
        {
            CONTENT_VIEW_MAX = 10;
            VIEW_COUNT = 10;
            return;
        }

        public GachaResultThumbnailWindow()
        {
            this.IN_BACKTO_UNITDETAIL = 1;
            this.IN_BACK_TOP = 10;
            this.OUT_BACK_TOP = 11;
            this.IN_ONEMORE_GACHA = 20;
            this.OUT_ONEMORE_GACHA = 0x15;
            this.OUT_UNITDETAIL = 100;
            this.OUT_UNITDETAIL_SINGLE = 0x6f;
            this.OUT_ITEM_DETAIL = 0x65;
            this.OUT_ARTIFACT_DETAIL = 0x67;
            this.OUT_CONCEPTCARD_DETAIL = 0x68;
            this.m_ResultIconRootList = new List<GameObject>();
            this.BonusWindowPrefab = "UI/GachaBonusWindow";
            this.WaitBonusWindow = 0.5f;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != this.IN_REFRESH)
            {
                goto Label_0045;
            }
            if (this.m_inialize == null)
            {
                goto Label_023D;
            }
            if ((this.m_Thumbnail != null) == null)
            {
                goto Label_023D;
            }
            this.m_UnitDetail.SetActive(0);
            this.m_Thumbnail.SetActive(1);
            goto Label_023D;
        Label_0045:
            if (pinID != this.IN_BACKTO_UNITDETAIL)
            {
                goto Label_008C;
            }
            if (this.CheckTutorial() == null)
            {
                goto Label_0073;
            }
            this.m_inialize = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 210);
            goto Label_008B;
        Label_0073:
            this.m_UnitDetail.SetActive(0);
            this.m_Thumbnail.SetActive(1);
        Label_008B:
            return;
        Label_008C:
            if (pinID != this.IN_BACK_TOP)
            {
                goto Label_00CF;
            }
            if (GachaResultData.IsRedrawGacha == null)
            {
                goto Label_00AF;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 80);
            goto Label_00CA;
        Label_00AF:
            this.m_inialize = 0;
            this.m_FinishedBonusEffect = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, this.OUT_BACK_TOP);
            return;
        Label_00CA:
            goto Label_023D;
        Label_00CF:
            if (pinID != this.IN_ONEMORE_GACHA)
            {
                goto Label_0113;
            }
            this.m_inialize = 0;
            this.m_FinishedBonusEffect = 0;
            if ((this.m_Thumbnail != null) == null)
            {
                goto Label_0106;
            }
            this.m_Thumbnail.SetActive(0);
        Label_0106:
            FlowNode_GameObject.ActivateOutputLinks(this, this.OUT_ONEMORE_GACHA);
            return;
        Label_0113:
            if (pinID != 50)
            {
                goto Label_0126;
            }
            this.OnSelectIcon();
            goto Label_023D;
        Label_0126:
            if (pinID != 0x33)
            {
                goto Label_0139;
            }
            this.OnSelectCardUnitIcon();
            goto Label_023D;
        Label_0139:
            if (pinID != 0x3d)
            {
                goto Label_01AC;
            }
            if (this.IsFinishedBonus != null)
            {
                goto Label_019F;
            }
            if (this.IsThumbnailActive() == null)
            {
                goto Label_019F;
            }
            if (GachaResultData.dropMails == null)
            {
                goto Label_0181;
            }
            if (((int) GachaResultData.dropMails.Length) <= 0)
            {
                goto Label_0181;
            }
            base.StartCoroutine(this.ShowBonus(1));
            goto Label_019A;
        Label_0181:
            ButtonEvent.UnLock("gacha_result_initalize");
            this.m_FinishedBonusEffect = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 70);
        Label_019A:
            goto Label_01A7;
        Label_019F:
            FlowNode_GameObject.ActivateOutputLinks(this, 70);
        Label_01A7:
            goto Label_023D;
        Label_01AC:
            if (pinID != 0x47)
            {
                goto Label_01EB;
            }
            if (GachaResultData.dropMails == null)
            {
                goto Label_01DE;
            }
            if (((int) GachaResultData.dropMails.Length) <= 0)
            {
                goto Label_01DE;
            }
            base.StartCoroutine(this.ShowBonus(0));
            goto Label_01E6;
        Label_01DE:
            FlowNode_GameObject.ActivateOutputLinks(this, 70);
        Label_01E6:
            goto Label_023D;
        Label_01EB:
            if (pinID != 0x51)
            {
                goto Label_021A;
            }
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "0");
            GlobalEvent.Invoke("ENABLE_HOME_BUTTON", this);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x52);
            goto Label_023D;
        Label_021A:
            if (pinID != 90)
            {
                goto Label_022E;
            }
            this.RefreshButtonInteractable(1);
            goto Label_023D;
        Label_022E:
            if (pinID != 0x5b)
            {
                goto Label_023D;
            }
            this.RefreshButtonInteractable(0);
        Label_023D:
            return;
        }

        private void Awake()
        {
            this.InitButton();
            return;
        }

        private bool CheckIsGiftData(GachaDropData[] _data)
        {
            bool flag;
            int num;
            flag = 0;
            if (_data == null)
            {
                goto Label_0030;
            }
            num = 0;
            goto Label_0027;
        Label_000F:
            if (_data[num].isGift == null)
            {
                goto Label_0023;
            }
            flag = 1;
            goto Label_0030;
        Label_0023:
            num += 1;
        Label_0027:
            if (num < ((int) _data.Length))
            {
                goto Label_000F;
            }
        Label_0030:
            return flag;
        }

        private bool CheckSingleDropForUnit()
        {
            return (((GachaResultData.drops == null) || (((int) GachaResultData.drops.Length) != 1)) ? 0 : (GachaResultData.drops[0].type == 2));
        }

        private bool CheckTutorial()
        {
            return ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L);
        }

        private ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
        {
            ArtifactData data;
            Json_Artifact artifact;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.fav = 0;
            artifact.rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax);
            data.Deserialize(artifact);
            return data;
        }

        private void CreateContentList()
        {
            if ((this.BlockTemplate == null) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("召喚結果等を表示するブロックのテンプレートが指定されていません");
            return;
        Label_001C:
            this.BlockTemplate.SetActive(0);
            if ((this.m_ResultBlock == null) == null)
            {
                goto Label_0087;
            }
            this.m_ResultBlock = Object.Instantiate<GameObject>(this.BlockTemplate);
            if ((this.m_ResultBlock == null) == null)
            {
                goto Label_0066;
            }
            DebugUtility.LogError("召喚結果表示ブロックの生成に失敗しました");
            return;
        Label_0066:
            this.m_ResultBlock.get_transform().SetParent(this.BlockTemplate.get_transform().get_parent(), 0);
        Label_0087:
            return;
        }

        private ItemData CreateItemData(ItemParam iparam, int num)
        {
            ItemData data;
            data = new ItemData();
            data.Setup(0L, iparam, num);
            return data;
        }

        private UnitData CreateUnitData(UnitParam uparam)
        {
            UnitData data;
            Json_Unit unit;
            List<Json_Job> list;
            int num;
            int num2;
            JobSetParam param;
            Json_Job job;
            data = new UnitData();
            unit = new Json_Unit();
            unit.iid = 1L;
            unit.iname = uparam.iname;
            unit.exp = 0;
            unit.lv = 1;
            unit.plus = 0;
            unit.rare = 0;
            unit.select = new Json_UnitSelectable();
            unit.select.job = 0L;
            unit.jobs = null;
            unit.abil = null;
            if (uparam.jobsets == null)
            {
                goto Label_0117;
            }
            if (((int) uparam.jobsets.Length) <= 0)
            {
                goto Label_0117;
            }
            list = new List<Json_Job>((int) uparam.jobsets.Length);
            num = 1;
            num2 = 0;
            goto Label_00FC;
        Label_0093:
            param = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[num2]);
            if (param != null)
            {
                goto Label_00B4;
            }
            goto Label_00F6;
        Label_00B4:
            job = new Json_Job();
            job.iid = (long) num++;
            job.iname = param.job;
            job.rank = 0;
            job.equips = null;
            job.abils = null;
            list.Add(job);
        Label_00F6:
            num2 += 1;
        Label_00FC:
            if (num2 < ((int) uparam.jobsets.Length))
            {
                goto Label_0093;
            }
            unit.jobs = list.ToArray();
        Label_0117:
            data.Deserialize(unit);
            data.SetUniqueID(1L);
            data.JobRankUp(0);
            return data;
        }

        private void Initalize()
        {
            object[] objArray1;
            GachaRequestParam param;
            int num;
            SerializeValueBehaviour behaviour;
            GameObject obj2;
            Text text;
            string str;
            SerializeValueBehaviour behaviour2;
            Text text2;
            string str2;
            if (this.IsInialize == null)
            {
                goto Label_0030;
            }
            this.RefreshIcons(1);
            if (this.IsFinishedBonus != null)
            {
                goto Label_002F;
            }
            ButtonEvent.Lock("gacha_initialize");
            FlowNode_GameObject.ActivateOutputLinks(this, 60);
        Label_002F:
            return;
        Label_0030:
            if (GachaResultData.drops != null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            if (GachaResultData.IsRedrawGacha == null)
            {
                goto Label_0050;
            }
            GlobalEvent.Invoke("DISABLE_HOME_BUTTON", this);
        Label_0050:
            this.mRequest = null;
            param = DataSource.FindDataOfClass<GachaRequestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0071;
            }
            this.mRequest = param;
        Label_0071:
            this.SetDetailActiveStatus(0);
            this.is_gift = this.CheckIsGiftData(GachaResultData.drops);
            num = 0;
            goto Label_00BB;
        Label_0090:
            if (GachaResultData.drops[num].type != 2)
            {
                goto Label_00B7;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(0);
            goto Label_00C8;
        Label_00B7:
            num += 1;
        Label_00BB:
            if (num < ((int) GachaResultData.drops.Length))
            {
                goto Label_0090;
            }
        Label_00C8:
            if ((this.m_OnemoreBtn != null) == null)
            {
                goto Label_010F;
            }
            this.RefreshGachaCostObject(this.m_OnemoreBtn.get_gameObject());
            this.m_OnemoreBtn.get_gameObject().SetActive((GachaResultData.UseOneMore == null) ? 0 : (GachaResultData.IsRedrawGacha == 0));
        Label_010F:
            if ((this.m_BonusBtn != null) == null)
            {
                goto Label_0147;
            }
            this.m_BonusBtn.get_gameObject().SetActive((GachaResultData.dropMails == null) ? 0 : (((int) GachaResultData.dropMails.Length) > 0));
        Label_0147:
            if ((this.m_RedrawBtn != null) == null)
            {
                goto Label_0228;
            }
            behaviour = this.m_RedrawBtn.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_01F3;
            }
            obj2 = behaviour.list.GetGameObject("option");
            if ((obj2 != null) == null)
            {
                goto Label_0198;
            }
            obj2.SetActive(GachaResultData.IsPending);
        Label_0198:
            text = behaviour.list.GetUILabel("txt_count");
            if ((text != null) == null)
            {
                goto Label_01F3;
            }
            str = (GachaResultData.IsPending == null) ? string.Empty : LocalizedText.Get("sys.GACHA_REDRAW_COUNT_LIMIT", objArray1 = new object[] { (int) GachaResultData.RedrawRest });
            text.set_text(str);
        Label_01F3:
            this.m_RedrawBtn.get_gameObject().SetActive(GachaResultData.IsRedrawGacha);
            this.m_RedrawBtn.set_interactable((GachaResultData.IsPending == null) ? 0 : (GachaResultData.RedrawRest > 0));
        Label_0228:
            if ((this.m_DefaultBtn != null) == null)
            {
                goto Label_0294;
            }
            behaviour2 = this.m_DefaultBtn.GetComponent<SerializeValueBehaviour>();
            if ((behaviour2 != null) == null)
            {
                goto Label_0294;
            }
            text2 = behaviour2.list.GetUILabel("text");
            str2 = (GachaResultData.IsPending == null) ? LocalizedText.Get("sys.BTN_GACHA_OK") : LocalizedText.Get("sys.BTN_DECIDE_CONFIRM");
            text2.set_text(str2);
        Label_0294:
            this.Refresh();
            this.m_inialize = 1;
            return;
        }

        private void InitButton()
        {
            Button button;
            Button button2;
            Button button3;
            Button button4;
            if ((this.ButtonList != null) == null)
            {
                goto Label_00F9;
            }
            if ((this.m_DefaultBtn == null) == null)
            {
                goto Label_004B;
            }
            button = this.ButtonList.list.GetComponent<Button>("default");
            if ((button != null) == null)
            {
                goto Label_004B;
            }
            this.m_DefaultBtn = button;
        Label_004B:
            if ((this.m_OnemoreBtn == null) == null)
            {
                goto Label_0085;
            }
            button2 = this.ButtonList.list.GetComponent<Button>("onemore");
            if ((button2 != null) == null)
            {
                goto Label_0085;
            }
            this.m_OnemoreBtn = button2;
        Label_0085:
            if ((this.m_BonusBtn == null) == null)
            {
                goto Label_00BF;
            }
            button3 = this.ButtonList.list.GetComponent<Button>("bonus");
            if ((button3 != null) == null)
            {
                goto Label_00BF;
            }
            this.m_BonusBtn = button3;
        Label_00BF:
            if ((this.m_RedrawBtn == null) == null)
            {
                goto Label_00F9;
            }
            button4 = this.ButtonList.list.GetComponent<Button>("redraw");
            if ((button4 != null) == null)
            {
                goto Label_00F9;
            }
            this.m_RedrawBtn = button4;
        Label_00F9:
            return;
        }

        private bool IsThumbnailActive()
        {
            bool flag;
            flag = 1;
            if ((this.m_UnitDetail != null) == null)
            {
                goto Label_0022;
            }
            flag = GameObjectExtensions.GetActive(this.m_UnitDetail) == 0;
        Label_0022:
            return flag;
        }

        private void OnDestroy()
        {
            this.Reset();
            return;
        }

        private void OnEnable()
        {
            if (GachaResultData.drops == null)
            {
                goto Label_0017;
            }
            if (((int) GachaResultData.drops.Length) > 0)
            {
                goto Label_0018;
            }
        Label_0017:
            return;
        Label_0018:
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_0033;
            }
            HomeWindow.Current.SetVisible(1);
        Label_0033:
            this.Initalize();
            return;
        }

        private void OnSelectCardUnitIcon()
        {
            SerializeValueList list;
            string str;
            UnitParam param;
            GachaResultUnitDetail detail;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_0087;
            }
            str = list.GetString("select_unit");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0087;
            }
            param = MonoSingleton<GameManager>.Instance.GetUnitParam(str);
            if (param == null)
            {
                goto Label_0087;
            }
            this.m_UnitDetail.GetComponent<GachaResultUnitDetail>().Setup(this.CreateUnitData(param));
            ButtonEvent.Invoke("CONCEPT_CARD_DETAIL_BTN_CLOSE", null);
            this.m_Thumbnail.SetActive(0);
            this.m_UnitDetail.get_gameObject().SetActive(1);
            FlowNode_GameObject.ActivateOutputLinks(this, this.OUT_UNITDETAIL);
        Label_0087:
            return;
        }

        private void OnSelectIcon()
        {
            SerializeValueList list;
            int num;
            int num2;
            bool flag;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_003E;
            }
            num = list.GetInt("index");
            num2 = list.GetInt("type");
            flag = list.GetBool("bonus");
            this.Select(num, num2, flag);
        Label_003E:
            return;
        }

        private void Refresh()
        {
            if (this.CheckSingleDropForUnit() == null)
            {
                goto Label_0014;
            }
            this.Select(0, 1, 0);
        Label_0014:
            this.CreateContentList();
            this.Reset();
            this.RefreshResult(GachaResultData.drops, this.m_ResultBlock, 0, 0);
            if ((this.CautionText != null) == null)
            {
                goto Label_0055;
            }
            this.CautionText.SetActive(this.is_gift);
        Label_0055:
            if (this.CheckSingleDropForUnit() != null)
            {
                goto Label_0074;
            }
            this.RefreshIcons(1);
            FlowNode_GameObject.ActivateOutputLinks(this, 60);
            goto Label_007C;
        Label_0074:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3b);
        Label_007C:
            this.RefreshButtonInteractable(0);
            return;
        }

        private void RefreshButtonInteractable(bool _active)
        {
            bool flag;
            int num;
            if ((this.m_RedrawBtn != null) == null)
            {
                goto Label_003D;
            }
            this.m_RedrawBtn.set_interactable((_active == null) ? _active : ((GachaResultData.IsPending == null) ? 0 : (GachaResultData.RedrawRest > 0)));
        Label_003D:
            if ((this.m_OnemoreBtn != null) == null)
            {
                goto Label_00A7;
            }
            flag = _active;
            if (((this.mRequest == null) || (this.mRequest.IsTicketGacha == null)) || (_active == null))
            {
                goto Label_009B;
            }
            flag = (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mRequest.Ticket) <= 0) ? 0 : 1;
        Label_009B:
            this.m_OnemoreBtn.set_interactable(flag);
        Label_00A7:
            if ((this.m_BonusBtn != null) == null)
            {
                goto Label_00C4;
            }
            this.m_BonusBtn.set_interactable(_active);
        Label_00C4:
            if ((this.m_DefaultBtn != null) == null)
            {
                goto Label_00E1;
            }
            this.m_DefaultBtn.set_interactable(_active);
        Label_00E1:
            return;
        }

        private void RefreshGachaCostObject(GameObject button)
        {
            GachaCostObject obj2;
            GachaCostObject obj3;
            SerializeValueBehaviour behaviour;
            obj2 = button.GetComponent<GachaCostObject>();
            if ((obj2 == null) == null)
            {
                goto Label_0083;
            }
            obj3 = button.AddComponent<GachaCostObject>();
            behaviour = button.GetComponent<SerializeValueBehaviour>();
            if ((behaviour == null) == null)
            {
                goto Label_0038;
            }
            DebugUtility.LogError("再召喚ボタン用のSerializeValueListがありません");
            return;
        Label_0038:
            obj3.RootObject = button;
            obj3.TicketObject = behaviour.list.GetGameObject("ticket");
            obj3.DefaultObject = behaviour.list.GetGameObject("default");
            obj3.DefaultBGObject = behaviour.list.GetGameObject("bg");
            obj2 = obj3;
        Label_0083:
            obj2.Refresh();
            return;
        }

        private void RefreshIcons(bool _active)
        {
            int num;
            if (this.m_ResultIconRootList == null)
            {
                goto Label_004A;
            }
            if (this.m_ResultIconRootList.Count <= 0)
            {
                goto Label_004A;
            }
            num = 0;
            goto Label_0039;
        Label_0023:
            this.m_ResultIconRootList[num].SetActive(_active);
            num += 1;
        Label_0039:
            if (num < this.m_ResultIconRootList.Count)
            {
                goto Label_0023;
            }
        Label_004A:
            return;
        }

        private void RefreshResult(GachaDropData[] _drops, GameObject _block, int _block_type, bool _is_anim)
        {
            int num;
            SerializeValueBehaviour behaviour;
            GameObject obj2;
            int num2;
            GachaDropData data;
            int num3;
            GachaResultType type;
            GameObject obj3;
            Animator animator;
            SerializeValueBehaviour behaviour2;
            GameObject obj4;
            ItemIcon icon;
            ConceptCardData data2;
            ConceptCardIcon icon2;
            SerializeValueBehaviour behaviour3;
            GameObject obj5;
            UnitData data3;
            GameObject obj6;
            bool flag;
            int num4;
            ConceptCardEffectsParam param;
            SerializeValueBehaviour behaviour4;
            GameObject obj7;
            ButtonEvent event2;
            ButtonEvent.Event event3;
            SerializeValueBehaviour behaviour5;
            GameObject obj8;
            if ((_drops != null) && (((int) _drops.Length) >= 0))
            {
                goto Label_0010;
            }
            return;
        Label_0010:
            if ((_block == null) == null)
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            _block.SetActive(1);
            num = (int) _drops.Length;
            behaviour = _block.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_0443;
            }
            obj2 = behaviour.list.GetGameObject("icon");
            obj2.SetActive(0);
            num2 = 0;
            goto Label_043C;
        Label_005A:
            data = _drops[num2];
            num3 = num2;
            type = 0;
            if (data != null)
            {
                goto Label_0071;
            }
            goto Label_0438;
        Label_0071:
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.get_transform().SetParent(obj2.get_transform().get_parent(), 0);
            obj3.GetComponent<Animator>().set_enabled(_is_anim);
            behaviour2 = obj3.GetComponent<SerializeValueBehaviour>();
            if ((behaviour2 == null) == null)
            {
                goto Label_00BE;
            }
            goto Label_0443;
        Label_00BE:
            obj4 = null;
            if (data.type != 2)
            {
                goto Label_00FD;
            }
            obj4 = behaviour2.list.GetGameObject("unit");
            DataSource.Bind<UnitData>(obj4, this.CreateUnitData(data.unit));
            type = 1;
            goto Label_0327;
        Label_00FD:
            if (data.type != 1)
            {
                goto Label_0179;
            }
            obj4 = behaviour2.list.GetGameObject("item");
            DataSource.Bind<ItemData>(obj4, this.CreateItemData(data.item, data.num));
            icon = obj4.GetComponent<ItemIcon>();
            if ((icon != null) == null)
            {
                goto Label_0155;
            }
            icon.UpdateValue();
        Label_0155:
            type = (string.IsNullOrEmpty(data.item.Flavor) == null) ? 2 : 3;
            goto Label_0327;
        Label_0179:
            if (data.type != 3)
            {
                goto Label_01BC;
            }
            obj4 = behaviour2.list.GetGameObject("artifact");
            DataSource.Bind<ArtifactData>(obj4, this.CreateArtifactData(data.artifact, data.Rare));
            type = 4;
            goto Label_0327;
        Label_01BC:
            if (data.type != 4)
            {
                goto Label_0327;
            }
            obj4 = behaviour2.list.GetGameObject("conceptcard");
            data2 = ConceptCardData.CreateConceptCardDataForDisplay(data.conceptcard.iname);
            icon2 = obj4.GetComponent<ConceptCardIcon>();
            if ((icon2 != null) == null)
            {
                goto Label_0324;
            }
            icon2.Setup(data2);
            behaviour3 = obj4.GetComponent<SerializeValueBehaviour>();
            if ((behaviour3 != null) == null)
            {
                goto Label_0324;
            }
            obj5 = behaviour3.list.GetGameObject("unit_icon");
            if ((obj5 != null) == null)
            {
                goto Label_027F;
            }
            data3 = null;
            if (data.cardunit == null)
            {
                goto Label_0262;
            }
            data3 = this.CreateUnitData(data.cardunit);
        Label_0262:
            DataSource.Bind<UnitData>(obj5, data3);
            obj5.SetActive((data.cardunit == null) == 0);
        Label_027F:
            obj6 = behaviour3.list.GetGameObject("skin");
            if ((obj6 != null) == null)
            {
                goto Label_0324;
            }
            flag = 0;
            if (data.conceptcard.effects == null)
            {
                goto Label_031B;
            }
            if (((int) data.conceptcard.effects.Length) <= 0)
            {
                goto Label_031B;
            }
            num4 = 0;
            goto Label_0306;
        Label_02CF:
            param = data.conceptcard.effects[num4];
            if (param == null)
            {
                goto Label_0300;
            }
            if (string.IsNullOrEmpty(param.skin) != null)
            {
                goto Label_0300;
            }
            flag = 1;
            goto Label_031B;
        Label_0300:
            num4 += 1;
        Label_0306:
            if (num4 < ((int) data.conceptcard.effects.Length))
            {
                goto Label_02CF;
            }
        Label_031B:
            obj6.SetActive(flag);
        Label_0324:
            type = 5;
        Label_0327:
            if ((obj4 == null) == null)
            {
                goto Label_0343;
            }
            DebugUtility.LogError("アイコンオブジェクトがありません");
            goto Label_0443;
        Label_0343:
            behaviour4 = obj4.GetComponent<SerializeValueBehaviour>();
            if ((behaviour4 != null) == null)
            {
                goto Label_0387;
            }
            obj7 = behaviour4.list.GetGameObject("new");
            if ((behaviour4 != null) == null)
            {
                goto Label_0387;
            }
            obj7.SetActive(data.isNew);
        Label_0387:
            event2 = obj4.GetComponent<ButtonEvent>();
            if ((event2 != null) == null)
            {
                goto Label_0410;
            }
            event3 = event2.GetEvent("CLICK_ICON");
            if (event3 == null)
            {
                goto Label_0410;
            }
            event3.valueList.SetField("index", num3);
            event3.valueList.SetField("type", type);
            event3.valueList.SetField("block", _block_type);
            if (type != 5)
            {
                goto Label_0410;
            }
            event3.valueList.SetField("is_first_get_unit", (data.cardunit == null) == 0);
        Label_0410:
            obj4.SetActive(1);
            this.m_ResultIconRootList.Add(obj3);
            if (_block_type != null)
            {
                goto Label_0438;
            }
            this.m_ResultIconRootList.Add(obj3);
        Label_0438:
            num2 += 1;
        Label_043C:
            if (num2 < num)
            {
                goto Label_005A;
            }
        Label_0443:
            behaviour5 = base.get_gameObject().GetComponent<SerializeValueBehaviour>();
            if ((behaviour5 != null) == null)
            {
                goto Label_0498;
            }
            obj8 = behaviour5.list.GetGameObject("space");
            if ((obj8 != null) == null)
            {
                goto Label_0498;
            }
            obj8.get_transform().SetAsLastSibling();
            obj8.SetActive(num > VIEW_COUNT);
        Label_0498:
            return;
        }

        private void Reset()
        {
            GameUtility.DestroyGameObjects(this.m_ResultIconRootList);
            this.m_ResultIconRootList.Clear();
            return;
        }

        private void Select(int _index, int _type, bool _bonus)
        {
            int num;
            GameObject obj2;
            GachaResultUnitDetail detail;
            UnitParam param;
            UnitData data;
            ItemParam param2;
            ArtifactParam param3;
            int num2;
            ArtifactData data2;
            string str;
            num = 0;
            obj2 = null;
            if (_type != 1)
            {
                goto Label_009B;
            }
            detail = this.m_UnitDetail.GetComponent<GachaResultUnitDetail>();
            if ((detail != null) == null)
            {
                goto Label_0077;
            }
            param = (_bonus == null) ? GachaResultData.drops[_index].unit : GachaResultData.dropMails[_index].unit;
            data = this.CreateUnitData(param);
            detail.Setup(data);
            num = (this.CheckSingleDropForUnit() == null) ? this.OUT_UNITDETAIL : this.OUT_UNITDETAIL_SINGLE;
        Label_0077:
            this.m_UnitDetail.SetActive(1);
            this.m_Thumbnail.SetActive(0);
            num = this.OUT_UNITDETAIL;
            goto Label_01A7;
        Label_009B:
            if ((_type != 2) && (_type != 3))
            {
                goto Label_00E7;
            }
            param2 = (_bonus == null) ? GachaResultData.drops[_index].item : GachaResultData.dropMails[_index].item;
            if (this.SelectItem(param2) == null)
            {
                goto Label_01A7;
            }
            num = this.OUT_ITEM_DETAIL;
            goto Label_01A7;
        Label_00E7:
            if (_type != 4)
            {
                goto Label_015D;
            }
            param3 = (_bonus == null) ? GachaResultData.drops[_index].artifact : GachaResultData.dropMails[_index].artifact;
            num2 = (_bonus == null) ? GachaResultData.drops[_index].Rare : GachaResultData.dropMails[_index].Rare;
            data2 = this.CreateArtifactData(param3, num2);
            if (this.SelectArtifact(data2) == null)
            {
                goto Label_01A7;
            }
            num = this.OUT_ARTIFACT_DETAIL;
            goto Label_01A7;
        Label_015D:
            if (_type != 5)
            {
                goto Label_01A7;
            }
            str = (_bonus == null) ? GachaResultData.drops[_index].conceptcard.iname : GachaResultData.dropMails[_index].conceptcard.iname;
            if (this.SelectConceptCard(str) == null)
            {
                goto Label_01A7;
            }
            num = this.OUT_CONCEPTCARD_DETAIL;
        Label_01A7:
            if (num <= 0)
            {
                goto Label_01B5;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, num);
        Label_01B5:
            return;
        }

        private bool SelectArtifact(ArtifactData _data)
        {
            if (_data == null)
            {
                goto Label_0031;
            }
            if ((this.DetailRoot != null) == null)
            {
                goto Label_0031;
            }
            DataSource.Bind<ArtifactData>(this.DetailRoot, null);
            DataSource.Bind<ArtifactData>(this.DetailRoot, _data);
            return 1;
        Label_0031:
            return 0;
        }

        private bool SelectConceptCard(string _iname)
        {
            ConceptCardData data;
            if (string.IsNullOrEmpty(_iname) != null)
            {
                goto Label_001F;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(_iname);
            GlobalVars.SelectedConceptCardData.Set(data);
            return 1;
        Label_001F:
            return 0;
        }

        private bool SelectItem(ItemParam _param)
        {
            ItemData data;
            if (_param == null)
            {
                goto Label_005F;
            }
            if ((this.DetailRoot != null) == null)
            {
                goto Label_005F;
            }
            DataSource.Bind<ItemParam>(this.DetailRoot, null);
            DataSource.Bind<ItemData>(this.DetailRoot, null);
            DataSource.Bind<ItemParam>(this.DetailRoot, _param);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(_param.iname);
            DataSource.Bind<ItemData>(this.DetailRoot, data);
            return 1;
        Label_005F:
            return 0;
        }

        public void SetDetailActiveStatus(bool _active)
        {
            SerializeValueBehaviour behaviour;
            behaviour = base.get_gameObject().GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_00AA;
            }
            if ((this.m_Thumbnail == null) == null)
            {
                goto Label_005B;
            }
            this.m_Thumbnail = behaviour.list.GetGameObject("thumbnail");
            if ((this.m_Thumbnail == null) == null)
            {
                goto Label_005B;
            }
            DebugUtility.LogError("GachaResultThumbnailWindow.cs:unit_detailの指定がありません.");
            return;
        Label_005B:
            if ((this.m_UnitDetail == null) == null)
            {
                goto Label_009E;
            }
            this.m_UnitDetail = behaviour.list.GetGameObject("unit_detail");
            if ((this.m_UnitDetail == null) == null)
            {
                goto Label_009E;
            }
            DebugUtility.LogError("GachaResultThumbnailWindow.cs:unit_detailの指定がありません.");
            return;
        Label_009E:
            this.m_UnitDetail.SetActive(_active);
        Label_00AA:
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowBonus(bool _is_anim)
        {
            <ShowBonus>c__Iterator10E iteratore;
            iteratore = new <ShowBonus>c__Iterator10E();
            iteratore._is_anim = _is_anim;
            iteratore.<$>_is_anim = _is_anim;
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void Start()
        {
        }

        public bool IsInialize
        {
            get
            {
                return this.m_inialize;
            }
        }

        public bool IsFinishedBonus
        {
            get
            {
                return this.m_FinishedBonusEffect;
            }
        }

        [CompilerGenerated]
        private sealed class <ShowBonus>c__Iterator10E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GachaDropData[] <bonus>__0;
            internal LoadRequest <req>__1;
            internal bool _is_anim;
            internal GameObject <BonusWindow>__2;
            internal List<Animator> <animList>__3;
            internal SerializeValueBehaviour <valueList>__4;
            internal GameObject <icon_template>__5;
            internal int <i>__6;
            internal int <index>__7;
            internal GachaDropData <drop>__8;
            internal GachaResultThumbnailWindow.GachaResultType <type>__9;
            internal GameObject <iconRoot>__10;
            internal SerializeValueBehaviour <iconValueList>__11;
            internal GameObject <iconObj>__12;
            internal ItemIcon <itemIcon>__13;
            internal ConceptCardData <ccardData>__14;
            internal ConceptCardIcon <ccardicon>__15;
            internal ButtonEvent <bevent>__16;
            internal ButtonEvent.Event <event_value>__17;
            internal SerializeValueBehaviour <newValueList>__18;
            internal GameObject <newObj>__19;
            internal Animator <anim>__20;
            internal GameObject <single_msg>__21;
            internal SerializeValueBehaviour <msgValueList>__22;
            internal Text <nameText>__23;
            internal Text <valueText>__24;
            internal GachaDropData <drop>__25;
            internal string <item_name>__26;
            internal int <item_value>__27;
            internal GameObject <multi_msg>__28;
            internal WaitForSeconds <wait_sec>__29;
            internal int <i>__30;
            internal int $PC;
            internal object $current;
            internal bool <$>_is_anim;
            internal GachaResultThumbnailWindow <>f__this;

            public <ShowBonus>c__Iterator10E()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0031;

                    case 1:
                        goto Label_00A3;

                    case 2:
                        goto Label_00D0;

                    case 3:
                        goto Label_07E3;

                    case 4:
                        goto Label_0848;

                    case 5:
                        goto Label_08A9;
                }
                goto Label_08E1;
            Label_0031:
                if (string.IsNullOrEmpty(this.<>f__this.BonusWindowPrefab) == null)
                {
                    goto Label_0055;
                }
                DebugUtility.LogError("おまけウィンドウのPrefab指定がありません.");
                goto Label_08E1;
            Label_0055:
                this.<bonus>__0 = GachaResultData.dropMails;
                this.<req>__1 = AssetManager.LoadAsync<GameObject>(this.<>f__this.BonusWindowPrefab);
                if (this.<req>__1.isDone != null)
                {
                    goto Label_00A3;
                }
                this.$current = this.<req>__1.StartCoroutine();
                this.$PC = 1;
                goto Label_08E3;
            Label_00A3:
                if (this._is_anim == null)
                {
                    goto Label_00D0;
                }
                this.$current = new WaitForSeconds(this.<>f__this.WaitBonusWindow);
                this.$PC = 2;
                goto Label_08E3;
            Label_00D0:
                ButtonEvent.UnLock("gacha_initialize");
                ButtonEvent.Lock("open_bonus");
                this.<BonusWindow>__2 = Object.Instantiate(this.<req>__1.asset) as GameObject;
                this.<BonusWindow>__2.get_transform().SetParent(this.<>f__this.BonusRoot, 0);
                this.<animList>__3 = new List<Animator>();
                this.<valueList>__4 = this.<BonusWindow>__2.GetComponent<SerializeValueBehaviour>();
                if ((this.<valueList>__4 != null) == null)
                {
                    goto Label_07C0;
                }
                this.<icon_template>__5 = this.<valueList>__4.list.GetGameObject("icon");
                if ((this.<icon_template>__5 == null) == null)
                {
                    goto Label_0183;
                }
                DebugUtility.LogError("おまけのアイコン表示用のオブジェクトの指定がありません.");
                goto Label_08E1;
            Label_0183:
                this.<icon_template>__5.SetActive(0);
                this.<i>__6 = 0;
                goto Label_058F;
            Label_019B:
                this.<index>__7 = this.<i>__6;
                this.<drop>__8 = this.<bonus>__0[this.<i>__6];
                if (this.<drop>__8 != null)
                {
                    goto Label_01CA;
                }
                goto Label_0581;
            Label_01CA:
                this.<type>__9 = 0;
                this.<iconRoot>__10 = Object.Instantiate<GameObject>(this.<icon_template>__5);
                if ((this.<iconRoot>__10 != null) == null)
                {
                    goto Label_0581;
                }
                this.<iconRoot>__10.get_transform().SetParent(this.<icon_template>__5.get_transform().get_parent(), 0);
                this.<iconValueList>__11 = this.<iconRoot>__10.GetComponent<SerializeValueBehaviour>();
                if ((this.<iconValueList>__11 == null) == null)
                {
                    goto Label_0240;
                }
                DebugUtility.LogError("iconValueList is Null!");
            Label_0240:
                this.<iconObj>__12 = null;
                if (this.<drop>__8.type != 2)
                {
                    goto Label_02A0;
                }
                this.<iconObj>__12 = this.<iconValueList>__11.list.GetGameObject("unit");
                DataSource.Bind<UnitData>(this.<iconObj>__12, this.<>f__this.CreateUnitData(this.<drop>__8.unit));
                this.<type>__9 = 1;
                goto Label_0416;
            Label_02A0:
                if (this.<drop>__8.type != 1)
                {
                    goto Label_0331;
                }
                this.<iconObj>__12 = this.<iconValueList>__11.list.GetGameObject("item");
                DataSource.Bind<ItemData>(this.<iconObj>__12, this.<>f__this.CreateItemData(this.<drop>__8.item, this.<drop>__8.num));
                this.<itemIcon>__13 = this.<iconObj>__12.GetComponent<ItemIcon>();
                if ((this.<itemIcon>__13 != null) == null)
                {
                    goto Label_0325;
                }
                this.<itemIcon>__13.UpdateValue();
            Label_0325:
                this.<type>__9 = 2;
                goto Label_0416;
            Label_0331:
                if (this.<drop>__8.type != 3)
                {
                    goto Label_0395;
                }
                this.<iconObj>__12 = this.<iconValueList>__11.list.GetGameObject("artifact");
                DataSource.Bind<ArtifactData>(this.<iconObj>__12, this.<>f__this.CreateArtifactData(this.<drop>__8.artifact, this.<drop>__8.Rare));
                this.<type>__9 = 4;
                goto Label_0416;
            Label_0395:
                if (this.<drop>__8.type != 4)
                {
                    goto Label_0416;
                }
                this.<iconObj>__12 = this.<iconValueList>__11.list.GetGameObject("conceptcard");
                this.<ccardData>__14 = ConceptCardData.CreateConceptCardDataForDisplay(this.<drop>__8.conceptcard.iname);
                this.<ccardicon>__15 = this.<iconObj>__12.GetComponent<ConceptCardIcon>();
                if ((this.<ccardicon>__15 != null) == null)
                {
                    goto Label_040F;
                }
                this.<ccardicon>__15.Setup(this.<ccardData>__14);
            Label_040F:
                this.<type>__9 = 5;
            Label_0416:
                this.<bevent>__16 = this.<iconObj>__12.GetComponent<ButtonEvent>();
                if ((this.<bevent>__16 != null) == null)
                {
                    goto Label_04A5;
                }
                this.<event_value>__17 = this.<bevent>__16.GetEvent("CLICK_ICON");
                if (this.<event_value>__17 == null)
                {
                    goto Label_04A5;
                }
                this.<event_value>__17.valueList.SetField("index", this.<index>__7);
                this.<event_value>__17.valueList.SetField("type", this.<type>__9);
                this.<event_value>__17.valueList.SetField("bonus", 1);
            Label_04A5:
                if ((this.<iconObj>__12 != null) == null)
                {
                    goto Label_051A;
                }
                this.<newValueList>__18 = this.<iconObj>__12.GetComponent<SerializeValueBehaviour>();
                if ((this.<newValueList>__18 != null) == null)
                {
                    goto Label_051A;
                }
                this.<newObj>__19 = this.<newValueList>__18.list.GetGameObject("new");
                if ((this.<newObj>__19 != null) == null)
                {
                    goto Label_051A;
                }
                this.<newObj>__19.SetActive(this.<drop>__8.isNew);
            Label_051A:
                this.<anim>__20 = this.<iconRoot>__10.GetComponent<Animator>();
                if ((this.<anim>__20 != null) == null)
                {
                    goto Label_0569;
                }
                if (this._is_anim == null)
                {
                    goto Label_055D;
                }
                this.<animList>__3.Add(this.<anim>__20);
                goto Label_0569;
            Label_055D:
                this.<anim>__20.set_enabled(0);
            Label_0569:
                this.<iconRoot>__10.SetActive(1);
                this.<iconObj>__12.SetActive(1);
            Label_0581:
                this.<i>__6 += 1;
            Label_058F:
                if (this.<i>__6 < ((int) this.<bonus>__0.Length))
                {
                    goto Label_019B;
                }
                this.<single_msg>__21 = this.<valueList>__4.list.GetGameObject("single_msg");
                if ((this.<single_msg>__21 != null) == null)
                {
                    goto Label_077E;
                }
                this.<msgValueList>__22 = this.<single_msg>__21.GetComponent<SerializeValueBehaviour>();
                if ((this.<msgValueList>__22 != null) == null)
                {
                    goto Label_0768;
                }
                this.<nameText>__23 = this.<msgValueList>__22.list.GetUILabel("name");
                this.<valueText>__24 = this.<msgValueList>__22.list.GetUILabel("value");
                if ((this.<nameText>__23 != null) == null)
                {
                    goto Label_0768;
                }
                if ((this.<valueText>__24 != null) == null)
                {
                    goto Label_0768;
                }
                this.<drop>__25 = this.<bonus>__0[0];
                this.<item_name>__26 = string.Empty;
                this.<item_value>__27 = 1;
                if (this.<drop>__25.type != 2)
                {
                    goto Label_0694;
                }
                this.<item_name>__26 = this.<drop>__25.unit.name;
                goto Label_0741;
            Label_0694:
                if (this.<drop>__25.type != 1)
                {
                    goto Label_06D7;
                }
                this.<item_name>__26 = this.<drop>__25.item.name;
                this.<item_value>__27 = Math.Max(1, this.<drop>__25.num);
                goto Label_0741;
            Label_06D7:
                if (this.<drop>__25.type != 3)
                {
                    goto Label_0703;
                }
                this.<item_name>__26 = this.<drop>__25.artifact.name;
                goto Label_0741;
            Label_0703:
                if (this.<drop>__25.type != 4)
                {
                    goto Label_0741;
                }
                this.<item_name>__26 = this.<drop>__25.conceptcard.name;
                this.<item_value>__27 = Math.Max(1, this.<drop>__25.num);
            Label_0741:
                this.<nameText>__23.set_text(this.<item_name>__26);
                this.<valueText>__24.set_text(&this.<item_value>__27.ToString());
            Label_0768:
                this.<single_msg>__21.SetActive(((int) this.<bonus>__0.Length) == 1);
            Label_077E:
                this.<multi_msg>__28 = this.<valueList>__4.list.GetGameObject("multi_msg");
                if ((this.<multi_msg>__28 != null) == null)
                {
                    goto Label_07C0;
                }
                this.<multi_msg>__28.SetActive(((int) this.<bonus>__0.Length) > 1);
            Label_07C0:
                if (this._is_anim == null)
                {
                    goto Label_0887;
                }
                goto Label_07E3;
            Label_07D0:
                this.$current = null;
                this.$PC = 3;
                goto Label_08E3;
            Label_07E3:
                if (GameUtility.CompareAnimatorStateName(this.<BonusWindow>__2, "opened") == null)
                {
                    goto Label_07D0;
                }
                if (this.<animList>__3 == null)
                {
                    goto Label_0887;
                }
                if (this.<animList>__3.Count <= 0)
                {
                    goto Label_0887;
                }
                this.<wait_sec>__29 = new WaitForSeconds(0.1f);
                this.<i>__30 = 0;
                goto Label_0871;
            Label_0830:
                this.$current = this.<wait_sec>__29;
                this.$PC = 4;
                goto Label_08E3;
            Label_0848:
                this.<animList>__3[this.<i>__30].SetTrigger("on");
                this.<i>__30 += 1;
            Label_0871:
                if (this.<i>__30 < this.<animList>__3.Count)
                {
                    goto Label_0830;
                }
            Label_0887:
                ButtonEvent.UnLock("open_bonus");
                goto Label_08A9;
            Label_0896:
                this.$current = null;
                this.$PC = 5;
                goto Label_08E3;
            Label_08A9:
                if ((this.<BonusWindow>__2 != null) != null)
                {
                    goto Label_0896;
                }
                this.<>f__this.m_FinishedBonusEffect = 1;
                this.<BonusWindow>__2 = null;
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 70);
                this.$PC = -1;
            Label_08E1:
                return 0;
            Label_08E3:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public enum GachaResultType
        {
            None,
            Unit,
            Item,
            Piece,
            Artifact,
            ConceptCard,
            End
        }
    }
}

