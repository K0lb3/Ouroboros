namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class BlackListItem : MonoBehaviour
    {
        [SerializeField]
        private Text Name;
        [SerializeField]
        private Text Lv;
        [SerializeField]
        private Text LastLogin;
        [SerializeField]
        private RawImage Icon;

        public BlackListItem()
        {
            base..ctor();
            return;
        }

        public unsafe void Refresh(ChatBlackListParam param)
        {
            UnitData data;
            int num;
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if ((this.Name != null) == null)
            {
                goto Label_0029;
            }
            this.Name.set_text(param.name);
        Label_0029:
            if ((this.Lv != null) == null)
            {
                goto Label_0058;
            }
            this.Lv.set_text(&PlayerData.CalcLevelFromExp(param.exp).ToString());
        Label_0058:
            if ((this.LastLogin != null) == null)
            {
                goto Label_007F;
            }
            this.LastLogin.set_text(ChatLogItem.GetPostAt(param.lastlogin));
        Label_007F:
            if ((this.Icon != null) == null)
            {
                goto Label_00B9;
            }
            if (param.unit == null)
            {
                goto Label_00B9;
            }
            data = new UnitData();
            data.Deserialize(param.unit);
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
        Label_00B9:
            return;
        }
    }
}

