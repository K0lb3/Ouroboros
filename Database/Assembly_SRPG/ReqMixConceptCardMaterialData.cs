namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqMixConceptCardMaterialData : WebAPI
    {
        public ReqMixConceptCardMaterialData(long base_id, List<SelecteConceptCardMaterial> materials, Network.ResponseCallback response, string trophyProgs, string bingoProgs)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            base.name = "unit/concept/mixall";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"base_id\":");
            builder.Append(base_id);
            builder.Append(",");
            builder.Append("\"use_item\":[");
            num = 0;
            goto Label_00D4;
        Label_004A:
            builder.Append("{");
            builder.Append("\"unique_id\":");
            builder.Append(materials[num].mUniqueID);
            builder.Append(",");
            builder.Append("\"use_num\":");
            builder.Append(materials[num].mSelectNum);
            if (num >= (materials.Count - 1))
            {
                goto Label_00C4;
            }
            builder.Append("},");
            goto Label_00D0;
        Label_00C4:
            builder.Append("}");
        Label_00D0:
            num += 1;
        Label_00D4:
            if (num < materials.Count)
            {
                goto Label_004A;
            }
            builder.Append("]");
            if (string.IsNullOrEmpty(trophyProgs) != null)
            {
                goto Label_010D;
            }
            builder.Append(",");
            builder.Append(trophyProgs);
        Label_010D:
            if (string.IsNullOrEmpty(bingoProgs) != null)
            {
                goto Label_012E;
            }
            builder.Append(",");
            builder.Append(bingoProgs);
        Label_012E:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

