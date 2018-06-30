namespace SRPG
{
    using System;

    public class AppealChargeParam
    {
        public string m_AppealId;
        public string m_BeforeImg;
        public string m_AfterImg;
        public long m_StartAt;
        public long m_EndAt;

        public AppealChargeParam()
        {
            this.m_AppealId = string.Empty;
            this.m_BeforeImg = string.Empty;
            this.m_AfterImg = string.Empty;
            base..ctor();
            this.m_AppealId = string.Empty;
            this.m_BeforeImg = string.Empty;
            this.m_AfterImg = string.Empty;
            this.m_StartAt = 0L;
            this.m_EndAt = 0L;
            return;
        }

        public void Deserialize(JSON_AppealChargeParam _json)
        {
            DateTime time;
            DateTime time2;
            Exception exception;
            if (_json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.m_AppealId = _json.fields.appeal_id;
            this.m_BeforeImg = _json.fields.before_img_id;
            this.m_AfterImg = _json.fields.after_img_id;
        Label_003F:
            try
            {
                if (string.IsNullOrEmpty(_json.fields.start_at) != null)
                {
                    goto Label_0071;
                }
                time = DateTime.Parse(_json.fields.start_at);
                this.m_StartAt = TimeManager.GetUnixSec(time);
            Label_0071:
                if (string.IsNullOrEmpty(_json.fields.end_at) != null)
                {
                    goto Label_00A3;
                }
                time2 = DateTime.Parse(_json.fields.end_at);
                this.m_EndAt = TimeManager.GetUnixSec(time2);
            Label_00A3:
                goto Label_00B9;
            }
            catch (Exception exception1)
            {
            Label_00A8:
                exception = exception1;
                DebugUtility.LogError(exception.ToString());
                goto Label_00B9;
            }
        Label_00B9:
            return;
        }

        public string appeal_id
        {
            get
            {
                return this.m_AppealId;
            }
        }

        public string before_img
        {
            get
            {
                return this.m_BeforeImg;
            }
        }

        public string after_img
        {
            get
            {
                return this.m_AfterImg;
            }
        }

        public long start_at
        {
            get
            {
                return this.m_StartAt;
            }
        }

        public long end_at
        {
            get
            {
                return this.m_EndAt;
            }
        }
    }
}

