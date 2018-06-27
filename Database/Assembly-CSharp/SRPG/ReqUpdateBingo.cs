// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUpdateBingo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqUpdateBingo : WebAPI
  {
    private StringBuilder sb;
    private bool is_begin;

    public ReqUpdateBingo()
    {
    }

    public ReqUpdateBingo(List<TrophyState> trophyprogs, Network.ResponseCallback response, bool finish)
    {
      this.name = "bingo/exec";
      this.BeginBingoReqString();
      this.AddBingoReqString(trophyprogs, finish);
      this.EndBingoReqString();
      this.body = WebAPI.GetRequestString(this.GetBingoReqString());
      this.callback = response;
    }

    public string GetBingoReqString()
    {
      return this.sb.ToString();
    }

    public void BeginBingoReqString()
    {
      this.is_begin = true;
      this.sb = WebAPI.GetStringBuilder();
      this.sb.Append("\"bingoprogs\":[");
    }

    public void EndBingoReqString()
    {
      this.sb.Append(']');
    }

    public void AddBingoReqString(List<TrophyState> progs, bool finish)
    {
      int num1 = 0;
      int num2 = 0;
      if (finish)
        num2 = TimeManager.ServerTime.ToYMD();
      for (int index1 = 0; index1 < progs.Count; ++index1)
      {
        if (this.is_begin)
          this.is_begin = false;
        else
          this.sb.Append(',');
        this.sb.Append("{\"iname\":\"");
        this.sb.Append(progs[index1].iname);
        this.sb.Append("\",");
        this.sb.Append("\"parent\":\"");
        if (progs[index1].Param != null)
          this.sb.Append(progs[index1].Param.ParentTrophy);
        this.sb.Append("\",");
        this.sb.Append("\"pts\":[");
        for (int index2 = 0; index2 < progs[index1].Count.Length; ++index2)
        {
          if (index2 > 0)
            this.sb.Append(',');
          this.sb.Append(progs[index1].Count[index2]);
        }
        this.sb.Append("],");
        this.sb.Append("\"ymd\":");
        this.sb.Append(progs[index1].StartYMD);
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
