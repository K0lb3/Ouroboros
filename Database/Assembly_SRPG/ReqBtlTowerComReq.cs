namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlTowerComReq : WebAPI
    {
        public ReqBtlTowerComReq(string qid, string fid, PartyData partyIndex, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/btl/req";
            builder.Append("\"qid\":\"");
            builder.Append(qid);
            builder.Append("\",");
            builder.Append("\"fid\":\"");
            builder.Append(fid);
            builder.Append("\",");
            builder.Append("\"fuid\":\"");
            builder.Append(GlobalVars.SelectedFriendID);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

