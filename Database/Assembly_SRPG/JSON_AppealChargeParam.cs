namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_AppealChargeParam
    {
        public AppealParam fields;

        public JSON_AppealChargeParam()
        {
            base..ctor();
            return;
        }

        public class AppealParam
        {
            public string appeal_id;
            public string before_img_id;
            public string after_img_id;
            public string start_at;
            public string end_at;

            public AppealParam()
            {
                this.appeal_id = string.Empty;
                this.before_img_id = string.Empty;
                this.after_img_id = string.Empty;
                this.start_at = string.Empty;
                this.end_at = string.Empty;
                base..ctor();
                return;
            }
        }
    }
}

