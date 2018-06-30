namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class BanStatusWindow : MonoBehaviour
    {
        public Text Title;
        public Text LimitDate;
        public Text Message;
        public Text CustomerID;

        public BanStatusWindow()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        private unsafe void Start()
        {
            int num;
            DateTime time;
            DateTime time2;
            if ((this.Title != null) == null)
            {
                goto Label_0021;
            }
            this.Title.set_text("ログイン制限");
        Label_0021:
            if ((this.Message != null) == null)
            {
                goto Label_0042;
            }
            this.Message.set_text("再びログイン可能になるのは以下の日時です。");
        Label_0042:
            if ((this.LimitDate != null) == null)
            {
                goto Label_00A3;
            }
            num = GlobalVars.BanStatus;
            if (num != 1)
            {
                goto Label_0075;
            }
            this.LimitDate.set_text("無期限ログイン不可");
            goto Label_00A3;
        Label_0075:
            &time = new DateTime(0x7b2, 1, 1, 0, 0, 0, 1);
            time2 = &time.AddSeconds((double) num);
            this.LimitDate.set_text(&time2.ToString());
        Label_00A3:
            if ((this.CustomerID != null) == null)
            {
                goto Label_00C4;
            }
            this.CustomerID.set_text(GlobalVars.CustomerID);
        Label_00C4:
            return;
        }
    }
}

