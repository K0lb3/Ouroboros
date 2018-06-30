namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqGalleryItem : WebAPI
    {
        public ReqGalleryItem(List<ItemParam> items, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            base.name = "gallery/item";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"inames\":[");
            if (items == null)
            {
                goto Label_008A;
            }
            if (items.Count <= 0)
            {
                goto Label_008A;
            }
            num = 0;
            goto Label_007E;
        Label_003C:
            if (num <= 0)
            {
                goto Label_004F;
            }
            builder.Append(",");
        Label_004F:
            builder.Append("\"");
            builder.Append(items[num].iname);
            builder.Append("\"");
            num += 1;
        Label_007E:
            if (num < items.Count)
            {
                goto Label_003C;
            }
        Label_008A:
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

