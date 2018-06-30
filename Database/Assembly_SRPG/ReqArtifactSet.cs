namespace SRPG
{
    using System;
    using System.Text;

    public class ReqArtifactSet : WebAPI
    {
        public ReqArtifactSet(long iid_job, long iid_artifact, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid_job\":");
            builder.Append(iid_job);
            builder.Append(",\"iid_artifact\":");
            builder.Append(iid_artifact);
            base.name = "unit/job/artifact/set";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public ReqArtifactSet(long iid_job, long[] iid_artifact, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid_job\":");
            builder.Append(iid_job);
            builder.Append(",\"iid_artifact\":");
            builder.Append(iid_artifact[0]);
            builder.Append(",\"iid_artifacts\":[");
            num = 0;
            goto Label_0067;
        Label_0049:
            if (num <= 0)
            {
                goto Label_0059;
            }
            builder.Append(0x2c);
        Label_0059:
            builder.Append(iid_artifact[num]);
            num += 1;
        Label_0067:
            if (num < ((int) iid_artifact.Length))
            {
                goto Label_0049;
            }
            builder.Append(0x5d);
            base.name = "unit/job/artifact/set";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public ReqArtifactSet(long iid_unit, long iid_job, long[] iid_artifact, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid_unit\":");
            builder.Append(iid_unit);
            builder.Append(",\"iid_job\":");
            builder.Append(iid_job);
            builder.Append(",\"iid_artifacts\":[");
            num = 0;
            goto Label_0065;
        Label_0047:
            if (num <= 0)
            {
                goto Label_0057;
            }
            builder.Append(0x2c);
        Label_0057:
            builder.Append(iid_artifact[num]);
            num += 1;
        Label_0065:
            if (num < ((int) iid_artifact.Length))
            {
                goto Label_0047;
            }
            builder.Append(0x5d);
            base.name = "unit/job/artifact/set";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

