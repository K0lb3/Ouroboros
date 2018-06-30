namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0x65, "Selected Quest", 1, 0x65), Pin(1, "Refresh", 0, 1), Pin(100, "Unlock", 1, 100)]
    public class UnitGetUnlockWindow : MonoBehaviour, IFlowInterface
    {
        private UnitParam UnlockUnit;
        public Text UnitName;

        public UnitGetUnlockWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        private void Refresh()
        {
            object[] objArray1;
            string str;
            str = GlobalVars.UnlockUnitID;
            this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(str);
            DataSource.Bind<UnitParam>(base.get_gameObject(), this.UnlockUnit);
            objArray1 = new object[] { this.UnlockUnit.name };
            this.UnitName.set_text(LocalizedText.Get("sys.GET_UNIT_WINDOW_UNIT_NAME", objArray1));
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }
    }
}

