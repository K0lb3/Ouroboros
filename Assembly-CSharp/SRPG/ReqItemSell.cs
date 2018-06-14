// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class ReqItemSell : WebAPI
  {
    public ReqItemSell(Dictionary<long, int> sells, bool is_item_convert, Network.ResponseCallback response)
    {
      this.name = !is_item_convert ? "item/sell" : "item/auto/sell";
      this.body = "\"sells\":[";
      string str = string.Empty;
      using (Dictionary<long, int>.Enumerator enumerator = sells.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<long, int> current = enumerator.Current;
          str += "{";
          str = str + "\"iid\":" + current.Key.ToString() + ",";
          str = str + "\"num\":" + current.Value.ToString();
          str += "},";
        }
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      this.body += str;
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
