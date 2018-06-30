namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiPlayVersus_ItemMap : MonoBehaviour
    {
        public Text Name;
        public Text Desc;
        public Image Thumnail;

        public MultiPlayVersus_ItemMap()
        {
            base..ctor();
            return;
        }

        public void UpdateParam(QuestParam param)
        {
            if ((this.Name != null) == null)
            {
                goto Label_0022;
            }
            this.Name.set_text(param.name);
        Label_0022:
            if ((this.Desc != null) == null)
            {
                goto Label_0044;
            }
            this.Desc.set_text(param.expr);
        Label_0044:
            if (this.Thumnail == null)
            {
                goto Label_0054;
            }
        Label_0054:
            return;
        }
    }
}

