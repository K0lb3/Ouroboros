namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventShopListItemArena : MonoBehaviour
    {
        public GameObject mLockObject;
        public Text mLockText;

        public EventShopListItemArena()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            object[] objArray1;
            Button button;
            PlayerData data;
            int num;
            GameManager manager;
            UnlockParam[] paramArray;
            int num2;
            UnlockParam param;
            button = base.GetComponent<Button>();
            if (button != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (this.mLockObject == null)
            {
                goto Label_0033;
            }
            if (this.mLockText != null)
            {
                goto Label_0034;
            }
        Label_0033:
            return;
        Label_0034:
            if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(0x10) == null)
            {
                goto Label_0064;
            }
            button.set_interactable(1);
            this.mLockObject.SetActive(0);
            goto Label_00FA;
        Label_0064:
            num = 0;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
            if (paramArray != null)
            {
                goto Label_0081;
            }
            return;
        Label_0081:
            num2 = 0;
            goto Label_00B8;
        Label_0089:
            param = paramArray[num2];
            if (param == null)
            {
                goto Label_00B2;
            }
            if (param.UnlockTarget != 0x10)
            {
                goto Label_00B2;
            }
            num = param.PlayerLevel;
            goto Label_00C3;
        Label_00B2:
            num2 += 1;
        Label_00B8:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0089;
            }
        Label_00C3:
            button.set_interactable(0);
            this.mLockObject.SetActive(1);
            objArray1 = new object[] { (int) num };
            this.mLockText.set_text(LocalizedText.Get("sys.COINLIST_ARENA_LOCK", objArray1));
        Label_00FA:
            return;
        }
    }
}

