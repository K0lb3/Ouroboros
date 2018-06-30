namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "キークエスト", 0, 1), Pin(0, "ノーマルクエスト", 0, 0), Pin(100, "イベントページへ", 1, 100), Pin(2, "塔クエスト", 0, 2)]
    public class StoryQuestShortcut : MonoBehaviour, IFlowInterface
    {
        public Button EventQuestButton;
        public Button KeyQuestButton;
        public Button TowerQuestButton;
        public GameObject KeyQuestOpenEffect;

        public StoryQuestShortcut()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0024;

                case 2:
                    goto Label_002F;
            }
            goto Label_003A;
        Label_0019:
            GlobalVars.ReqEventPageListType = 0;
            goto Label_003A;
        Label_0024:
            GlobalVars.ReqEventPageListType = 1;
            goto Label_003A;
        Label_002F:
            GlobalVars.ReqEventPageListType = 2;
        Label_003A:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public bool IsOpendTower()
        {
            GameManager manager;
            QuestParam[] paramArray;
            long num;
            int num2;
            TowerParam param;
            int num3;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_0089;
        Label_001F:
            param = manager.Towers[num2];
            num3 = 0;
            goto Label_007B;
        Label_0031:
            if (paramArray[num3].type == 7)
            {
                goto Label_0045;
            }
            goto Label_0075;
        Label_0045:
            if ((paramArray[num3].iname != param.iname) == null)
            {
                goto Label_0064;
            }
            goto Label_0075;
        Label_0064:
            if (paramArray[num3].IsDateUnlock(num) == null)
            {
                goto Label_0075;
            }
            return 1;
        Label_0075:
            num3 += 1;
        Label_007B:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_0031;
            }
            num2 += 1;
        Label_0089:
            if (num2 < ((int) manager.Towers.Length))
            {
                goto Label_001F;
            }
            num4 = 0;
            goto Label_00C9;
        Label_009F:
            if (paramArray[num4].IsMultiTower != null)
            {
                goto Label_00B2;
            }
            goto Label_00C3;
        Label_00B2:
            if (paramArray[num4].IsDateUnlock(num) == null)
            {
                goto Label_00C3;
            }
            return 1;
        Label_00C3:
            num4 += 1;
        Label_00C9:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_009F;
            }
            return 0;
        }

        private void Start()
        {
            ChapterParam[] paramArray;
            bool flag;
            bool flag2;
            long num;
            int num2;
            LevelLock @lock;
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            flag = 0;
            flag2 = 0;
            if (paramArray == null)
            {
                goto Label_0063;
            }
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_0059;
        Label_0023:
            if (paramArray[num2].IsKeyQuest() == null)
            {
                goto Label_0053;
            }
            if (paramArray[num2].IsDateUnlock(num) == null)
            {
                goto Label_0042;
            }
            flag2 = 1;
        Label_0042:
            if (paramArray[num2].IsKeyUnlock(num) == null)
            {
                goto Label_0053;
            }
            flag = 1;
        Label_0053:
            num2 += 1;
        Label_0059:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0023;
            }
        Label_0063:
            if ((this.KeyQuestOpenEffect != null) == null)
            {
                goto Label_0080;
            }
            this.KeyQuestOpenEffect.SetActive(flag);
        Label_0080:
            if ((this.KeyQuestButton != null) == null)
            {
                goto Label_00AE;
            }
            this.KeyQuestButton.get_gameObject().SetActive(1);
            this.KeyQuestButton.set_interactable(flag2);
        Label_00AE:
            if ((this.TowerQuestButton != null) == null)
            {
                goto Label_0117;
            }
            @lock = this.TowerQuestButton.GetComponent<LevelLock>();
            if ((@lock != null) == null)
            {
                goto Label_0106;
            }
            @lock.UpdateLockState();
            if (this.TowerQuestButton.get_interactable() == null)
            {
                goto Label_0117;
            }
            this.TowerQuestButton.set_interactable(this.IsOpendTower());
            goto Label_0117;
        Label_0106:
            this.TowerQuestButton.set_interactable(this.IsOpendTower());
        Label_0117:
            return;
        }
    }
}

