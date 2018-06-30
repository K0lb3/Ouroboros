namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqUpdateTrophy : WebAPI
    {
        private StringBuilder sb;
        private bool is_begin;

        public ReqUpdateTrophy()
        {
            base..ctor();
            return;
        }

        public ReqUpdateTrophy(List<TrophyState> trophyprogs, Network.ResponseCallback response, bool finish)
        {
            base..ctor();
            base.name = "trophy/exec";
            this.BeginTrophyReqString();
            this.AddTrophyReqString(trophyprogs, finish);
            this.EndTrophyReqString();
            base.body = WebAPI.GetRequestString(this.GetTrophyReqString());
            base.callback = response;
            return;
        }

        public void AddTrophyReqString(List<TrophyState> trophyprogs, bool finish)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = 0;
            num2 = 0;
            if (finish == null)
            {
                goto Label_0015;
            }
            num2 = SRPG_Extensions.ToYMD(TimeManager.ServerTime);
        Label_0015:
            num3 = 0;
            goto Label_0151;
        Label_001C:
            if (this.is_begin == null)
            {
                goto Label_0033;
            }
            this.is_begin = 0;
            goto Label_0041;
        Label_0033:
            this.sb.Append(0x2c);
        Label_0041:
            this.sb.Append("{\"iname\":\"");
            this.sb.Append(trophyprogs[num3].iname);
            this.sb.Append("\",");
            this.sb.Append("\"pts\":[");
            num4 = 0;
            goto Label_00C6;
        Label_0093:
            if (num4 <= 0)
            {
                goto Label_00A8;
            }
            this.sb.Append(0x2c);
        Label_00A8:
            this.sb.Append(trophyprogs[num3].Count[num4]);
            num4 += 1;
        Label_00C6:
            if (num4 < ((int) trophyprogs[num3].Count.Length))
            {
                goto Label_0093;
            }
            this.sb.Append("],");
            this.sb.Append("\"ymd\":");
            this.sb.Append(trophyprogs[num3].StartYMD);
            if (finish == null)
            {
                goto Label_0138;
            }
            this.sb.Append(",\"rewarded_at\":");
            this.sb.Append(num2);
        Label_0138:
            this.sb.Append("}");
            num += 1;
            num3 += 1;
        Label_0151:
            if (num3 < trophyprogs.Count)
            {
                goto Label_001C;
            }
            return;
        }

        public void BeginTrophyReqString()
        {
            this.is_begin = 1;
            this.sb = WebAPI.GetStringBuilder();
            this.sb.Append("\"trophyprogs\":[");
            return;
        }

        public void EndTrophyReqString()
        {
            this.sb.Append(0x5d);
            return;
        }

        public string GetTrophyReqString()
        {
            return this.sb.ToString();
        }
    }
}

