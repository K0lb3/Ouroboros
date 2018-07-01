// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSellPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqSellPiece : WebAPI
  {
    public ReqSellPiece(Dictionary<long, int> sells, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"sells\":[");
      string str = string.Empty;
      using (Dictionary<long, int>.Enumerator enumerator = sells.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<long, int> current = enumerator.Current;
          str += "{";
          str = str + "\"iid\":" + (object) current.Key + ",";
          str = str + "\"num\":" + (object) current.Value;
          str += "},";
        }
      }
      if (str.Length > 0)
        str = str.Substring(0, str.Length - 1);
      stringBuilder.Append(str);
      stringBuilder.Append("]");
      this.name = "shop/piece/sell";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
