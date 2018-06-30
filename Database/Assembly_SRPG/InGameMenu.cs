namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Start Debug", 0, 0), Pin(2, "Give Up", 0, 0), Pin(100, "Close Give Up Window", 0, 0)]
    public class InGameMenu : MonoBehaviour, IFlowInterface
    {
        public const int PINID_DEBUG = 1;
        public const int PINID_GIVEUP = 2;
        public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
        public GameObject MissionButton;
        public GameObject ExitButton;
        public GameObject OptionButton;
        public GameObject DebugButton;
        public Button AutoPlayOn;
        public Button AutoPlayOff;
        public Toggle AutoPlay;
        public GameObject AutoMode_Parent;
        public GameObject AutoMode_Treasure;
        public GameObject AutoMode_Skill;
        private GameObject mGiveUpWindow;
        public bool HideMissionButton;

        public InGameMenu()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__123(bool yes)
        {
            this.ToggleAutoPlay(yes);
            return;
        }

        public void Activated(int pinID)
        {
            string str;
            Win_Btn_DecideCancel_FL_C l_fl_c;
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_00B5;
            }
            if (num == 2)
            {
                goto Label_0022;
            }
            if (num == 100)
            {
                goto Label_007A;
            }
            goto Label_00B5;
            goto Label_00B5;
        Label_0022:
            str = LocalizedText.Get("sys.CONFIRM_GIVEUP");
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0057;
            }
            if (SceneBattle.Instance.IsPlayingArenaQuest == null)
            {
                goto Label_0057;
            }
            str = LocalizedText.Get("sys.CONFIRM_GIVEUP_ARENA");
        Label_0057:
            this.mGiveUpWindow = UIUtility.ConfirmBox(str, new UIUtility.DialogResultEvent(this.OnGiveUp), null, null, 1, 1, null, null);
            goto Label_00B5;
        Label_007A:
            if ((this.mGiveUpWindow != null) == null)
            {
                goto Label_00B5;
            }
            l_fl_c = this.mGiveUpWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_00A9;
            }
            l_fl_c.BeginClose();
        Label_00A9:
            this.mGiveUpWindow = null;
        Label_00B5:
            return;
        }

        private void OnDestroy()
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0036;
            }
            SceneBattle.Instance.OnQuestEnd = (SceneBattle.QuestEndEvent) Delegate.Remove(SceneBattle.Instance.OnQuestEnd, new SceneBattle.QuestEndEvent(this.OnQuestEnd));
        Label_0036:
            return;
        }

        private void OnGiveUp(GameObject go)
        {
            CanvasGroup group;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_002E;
            }
            if (SceneBattle.Instance.IsPlayingArenaQuest == null)
            {
                goto Label_002E;
            }
            SceneBattle.Instance.ForceEndQuestInArena();
            goto Label_0038;
        Label_002E:
            SceneBattle.Instance.ForceEndQuest();
        Label_0038:
            group = base.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_0052;
            }
            group.set_blocksRaycasts(0);
        Label_0052:
            return;
        }

        private void OnQuestEnd()
        {
            this.Activated(100);
            return;
        }

        private void Start()
        {
            QuestParam param;
            SceneBattle battle;
            bool flag;
            Selectable selectable;
            bool flag2;
            LText text;
            Button button;
            param = null;
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_026F;
            }
            param = battle.CurrentQuest;
            battle.OnQuestEnd = (SceneBattle.QuestEndEvent) Delegate.Combine(battle.OnQuestEnd, new SceneBattle.QuestEndEvent(this.OnQuestEnd));
            if ((param == null) || (param.CheckAllowedAutoBattle() == null))
            {
                goto Label_01B2;
            }
            if ((this.AutoPlayOn != null) == null)
            {
                goto Label_0099;
            }
            this.AutoPlayOn.get_gameObject().SetActive(battle.Battle.RequestAutoBattle == 0);
            this.AutoPlayOn.get_onClick().AddListener(new UnityAction(this, this.TurnOnAutoPlay));
        Label_0099:
            if ((this.AutoPlayOff != null) == null)
            {
                goto Label_00E1;
            }
            this.AutoPlayOff.get_gameObject().SetActive(battle.Battle.RequestAutoBattle);
            this.AutoPlayOff.get_onClick().AddListener(new UnityAction(this, this.TurnOffAutoPlay));
        Label_00E1:
            if ((this.AutoPlay != null) == null)
            {
                goto Label_0135;
            }
            this.AutoPlay.get_gameObject().SetActive(1);
            GameUtility.SetToggle(this.AutoPlay, battle.Battle.RequestAutoBattle);
            this.AutoPlay.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__123));
        Label_0135:
            if ((this.AutoMode_Parent != null) == null)
            {
                goto Label_0161;
            }
            this.AutoMode_Parent.get_gameObject().SetActive(battle.Battle.RequestAutoBattle);
        Label_0161:
            if ((this.AutoMode_Treasure != null) == null)
            {
                goto Label_0187;
            }
            this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
        Label_0187:
            if ((this.AutoMode_Skill != null) == null)
            {
                goto Label_026F;
            }
            this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
            goto Label_026F;
        Label_01B2:
            if ((this.AutoPlayOn != null) == null)
            {
                goto Label_01D4;
            }
            this.AutoPlayOn.get_gameObject().SetActive(0);
        Label_01D4:
            if ((this.AutoPlayOff != null) == null)
            {
                goto Label_01F6;
            }
            this.AutoPlayOff.get_gameObject().SetActive(0);
        Label_01F6:
            if ((this.AutoPlay != null) == null)
            {
                goto Label_0218;
            }
            this.AutoPlay.get_gameObject().SetActive(0);
        Label_0218:
            if ((this.AutoMode_Parent != null) == null)
            {
                goto Label_0235;
            }
            this.AutoMode_Parent.SetActive(0);
        Label_0235:
            if ((this.AutoMode_Treasure != null) == null)
            {
                goto Label_0252;
            }
            this.AutoMode_Treasure.SetActive(0);
        Label_0252:
            if ((this.AutoMode_Skill != null) == null)
            {
                goto Label_026F;
            }
            this.AutoMode_Skill.SetActive(0);
        Label_026F:
            if ((this.DebugButton != null) == null)
            {
                goto Label_028C;
            }
            this.DebugButton.SetActive(0);
        Label_028C:
            if (((this.MissionButton != null) == null) || (param == null))
            {
                goto Label_0319;
            }
            flag = param.HasMission();
            if (((battle == null) || (battle.Battle == null)) || ((battle.Battle.IsOrdeal == null) || (param.state == 2)))
            {
                goto Label_02DE;
            }
            flag = 0;
        Label_02DE:
            if (this.HideMissionButton == null)
            {
                goto Label_02FA;
            }
            this.MissionButton.SetActive(flag);
            goto Label_0319;
        Label_02FA:
            selectable = this.MissionButton.GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_0319;
            }
            selectable.set_interactable(flag);
        Label_0319:
            if ((this.ExitButton != null) == null)
            {
                goto Label_03B9;
            }
            flag2 = (param == null) ? 0 : param.CheckAllowedRetreat();
            this.ExitButton.SetActive(flag2);
            if (flag2 == null)
            {
                goto Label_03B9;
            }
            if ((battle != null) == null)
            {
                goto Label_03B9;
            }
            if (battle.IsPlayingArenaQuest == null)
            {
                goto Label_03B9;
            }
            this.ExitButton.GetComponentInChildren<LText>(1).set_text(LocalizedText.Get("sys.BTN_RETIRE_ARENA"));
            if (battle.Battle.IsArenaSkip == null)
            {
                goto Label_03B9;
            }
            button = this.ExitButton.GetComponent<Button>();
            if (button == null)
            {
                goto Label_03B9;
            }
            button.set_interactable(0);
        Label_03B9:
            return;
        }

        public void ToggleAutoBattle(bool is_active)
        {
            SceneBattle battle;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_001C;
            }
            if (battle.CurrentQuest != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            if (battle.CurrentQuest.CheckAllowedAutoBattle() != null)
            {
                goto Label_002E;
            }
            return;
        Label_002E:
            if (is_active == null)
            {
                goto Label_012E;
            }
            if ((this.AutoPlayOn != null) == null)
            {
                goto Label_0063;
            }
            this.AutoPlayOn.get_gameObject().SetActive(battle.Battle.RequestAutoBattle == 0);
        Label_0063:
            if ((this.AutoPlayOff != null) == null)
            {
                goto Label_008F;
            }
            this.AutoPlayOff.get_gameObject().SetActive(battle.Battle.RequestAutoBattle);
        Label_008F:
            if ((this.AutoPlay != null) == null)
            {
                goto Label_00B1;
            }
            this.AutoPlay.get_gameObject().SetActive(1);
        Label_00B1:
            if ((this.AutoMode_Parent != null) == null)
            {
                goto Label_00DD;
            }
            this.AutoMode_Parent.get_gameObject().SetActive(battle.Battle.RequestAutoBattle);
        Label_00DD:
            if ((this.AutoMode_Treasure != null) == null)
            {
                goto Label_0103;
            }
            this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
        Label_0103:
            if ((this.AutoMode_Skill != null) == null)
            {
                goto Label_01EB;
            }
            this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
            goto Label_01EB;
        Label_012E:
            if ((this.AutoPlayOn != null) == null)
            {
                goto Label_0150;
            }
            this.AutoPlayOn.get_gameObject().SetActive(0);
        Label_0150:
            if ((this.AutoPlayOff != null) == null)
            {
                goto Label_0172;
            }
            this.AutoPlayOff.get_gameObject().SetActive(0);
        Label_0172:
            if ((this.AutoPlay != null) == null)
            {
                goto Label_0194;
            }
            this.AutoPlay.get_gameObject().SetActive(0);
        Label_0194:
            if ((this.AutoMode_Parent != null) == null)
            {
                goto Label_01B1;
            }
            this.AutoMode_Parent.SetActive(0);
        Label_01B1:
            if ((this.AutoMode_Treasure != null) == null)
            {
                goto Label_01CE;
            }
            this.AutoMode_Treasure.SetActive(0);
        Label_01CE:
            if ((this.AutoMode_Skill != null) == null)
            {
                goto Label_01EB;
            }
            this.AutoMode_Skill.SetActive(0);
        Label_01EB:
            return;
        }

        private void ToggleAutoPlay(bool enable)
        {
            Animator animator;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0035;
            }
            SceneBattle.Instance.Battle.RequestAutoBattle = enable;
            if (enable == null)
            {
                goto Label_0030;
            }
            GameUtility.SetNeverSleep();
            goto Label_0035;
        Label_0030:
            GameUtility.SetDefaultSleepSetting();
        Label_0035:
            if ((this.AutoPlayOn != null) == null)
            {
                goto Label_005A;
            }
            this.AutoPlayOn.get_gameObject().SetActive(enable == 0);
        Label_005A:
            if ((this.AutoPlayOff != null) == null)
            {
                goto Label_007C;
            }
            this.AutoPlayOff.get_gameObject().SetActive(enable);
        Label_007C:
            if ((this.AutoMode_Parent != null) == null)
            {
                goto Label_00C2;
            }
            this.AutoMode_Parent.get_gameObject().SetActive(1);
            animator = this.AutoMode_Parent.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00C2;
            }
            animator.SetBool("open", enable);
        Label_00C2:
            return;
        }

        private void TurnOffAutoPlay()
        {
            this.ToggleAutoPlay(0);
            return;
        }

        private void TurnOnAutoPlay()
        {
            this.ToggleAutoPlay(1);
            return;
        }
    }
}

