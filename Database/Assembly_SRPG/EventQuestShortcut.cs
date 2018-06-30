namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class EventQuestShortcut : MonoBehaviour
    {
        public GameObject KeyQuestOpenEffect;

        public EventQuestShortcut()
        {
            base..ctor();
            return;
        }

        private void RefreshSwitchButton()
        {
            ChapterParam[] paramArray;
            bool flag;
            long num;
            int num2;
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            flag = 0;
            if (paramArray == null)
            {
                goto Label_004A;
            }
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_0041;
        Label_0020:
            if (paramArray[num2].IsKeyQuest() == null)
            {
                goto Label_003D;
            }
            if (paramArray[num2].IsKeyUnlock(num) == null)
            {
                goto Label_003D;
            }
            flag = 1;
        Label_003D:
            num2 += 1;
        Label_0041:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0020;
            }
        Label_004A:
            if ((this.KeyQuestOpenEffect != null) == null)
            {
                goto Label_0067;
            }
            this.KeyQuestOpenEffect.SetActive(flag);
        Label_0067:
            return;
        }

        private void Start()
        {
            this.RefreshSwitchButton();
            return;
        }
    }
}

