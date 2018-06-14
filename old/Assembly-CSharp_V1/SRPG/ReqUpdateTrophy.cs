// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUpdateTrophy
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqUpdateTrophy : WebAPI
  {
    private StringBuilder sb;
    private bool is_begin;

    public ReqUpdateTrophy()
    {
    }

    public ReqUpdateTrophy(List<TrophyState> trophyprogs, Network.ResponseCallback response, bool finish)
    {
      this.name = "trophy/exec";
      this.BeginTrophyReqString();
      this.AddTrophyReqString(trophyprogs, finish);
      this.EndTrophyReqString();
      this.body = WebAPI.GetRequestString(this.GetTrophyReqString());
      this.callback = response;
    }

    public string GetTrophyReqString()
    {
      return this.sb.ToString();
    }

    public void BeginTrophyReqString()
    {
      this.is_begin = true;
      this.sb = WebAPI.GetStringBuilder();
      this.sb.Append("\"trophyprogs\":[");
    }

    public void EndTrophyReqString()
    {
      this.sb.Append(']');
    }

    public void AddTrophyReqString(List<TrophyState> trophyprogs, bool finish)
    {
      int num1 = 0;
      int num2 = 0;
      if (finish)
        num2 = TimeManager.ServerTime.ToYMD();
      for (int index1 = 0; index1 < trophyprogs.Count; ++index1)
      {
        if (this.is_begin)
          this.is_begin = false;
        else
          this.sb.Append(',');
        this.sb.Append("{\"iname\":\"");
        this.sb.Append(trophyprogs[index1].iname);
        this.sb.Append("\",");
        this.sb.Append("\"pts\":[");
        for (int index2 = 0; index2 < trophyprogs[index1].Count.Length; ++index2)
        {
          if (index2 > 0)
            this.sb.Append(',');
          this.sb.Append(trophyprogs[index1].Count[index2]);
        }
        this.sb.Append("],");
        this.sb.Append("\"ymd\":");
        this.sb.Append(trophyprogs[index1].StartYMD);
        if (finish)
        {
          this.sb.Append(",\"rewarded_at\":");
          this.sb.Append(num2);
        }
        this.sb.Append("}");
        ++num1;
      }
    }
  }
}
