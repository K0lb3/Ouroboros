namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqUpdateBingo : WebAPI
    {
        private StringBuilder sb;
        private bool is_begin;

        public ReqUpdateBingo()
        {
            base..ctor();
            return;
        }

        public ReqUpdateBingo(List<TrophyState> trophyprogs, Network.ResponseCallback response, bool finish)
        {
            base..ctor();
            base.name = "bingo/exec";
            this.BeginBingoReqString();
            this.AddBingoReqString(trophyprogs, finish);
            this.EndBingoReqString();
            base.body = WebAPI.GetRequestString(this.GetBingoReqString());
            base.callback = response;
            return;
        }

        public void AddBingoReqString(List<TrophyState> progs, bool finish)
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
            goto Label_01A1;
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
            this.sb.Append(progs[num3].iname);
            this.sb.Append("\",");
            this.sb.Append("\"parent\":\"");
            if (progs[num3].Param == null)
            {
                goto Label_00BA;
            }
            this.sb.Append(progs[num3].Param.ParentTrophy);
        Label_00BA:
            this.sb.Append("\",");
            this.sb.Append("\"pts\":[");
            num4 = 0;
            goto Label_0116;
        Label_00E3:
            if (num4 <= 0)
            {
                goto Label_00F8;
            }
            this.sb.Append(0x2c);
        Label_00F8:
            this.sb.Append(progs[num3].Count[num4]);
            num4 += 1;
        Label_0116:
            if (num4 < ((int) progs[num3].Count.Length))
            {
                goto Label_00E3;
            }
            this.sb.Append("],");
            this.sb.Append("\"ymd\":");
            this.sb.Append(progs[num3].StartYMD);
            if (finish == null)
            {
                goto Label_0188;
            }
            this.sb.Append(",\"rewarded_at\":");
            this.sb.Append(num2);
        Label_0188:
            this.sb.Append("}");
            num += 1;
            num3 += 1;
        Label_01A1:
            if (num3 < progs.Count)
            {
                goto Label_001C;
            }
            return;
        }

        public void BeginBingoReqString()
        {
            this.is_begin = 1;
            this.sb = WebAPI.GetStringBuilder();
            this.sb.Append("\"bingoprogs\":[");
            return;
        }

        public void EndBingoReqString()
        {
            this.sb.Append(0x5d);
            return;
        }

        public string GetBingoReqString()
        {
            return this.sb.ToString();
        }
    }
}

