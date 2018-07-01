// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusCpuEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqVersusCpuEnd : WebAPI
  {
    public ReqVersusCpuEnd(long btlid, BtlResultTypes result, uint turn, int[] myhp, int[] enhp, int atk, int dmg, int heal, int beat, int[] place, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "vs/com/end";
      stringBuilder.Length = 0;
      stringBuilder.Append("\"btlid\":");
      stringBuilder.Append(btlid);
      stringBuilder.Append(',');
      stringBuilder.Append("\"btlendparam\":{");
      stringBuilder.Append("\"result\":\"");
      switch (result)
      {
        case BtlResultTypes.Win:
          stringBuilder.Append("win");
          break;
        case BtlResultTypes.Lose:
          stringBuilder.Append("lose");
          break;
        case BtlResultTypes.Retire:
          stringBuilder.Append("retire");
          break;
        case BtlResultTypes.Cancel:
          stringBuilder.Append("cancel");
          break;
        case BtlResultTypes.Draw:
          stringBuilder.Append("draw");
          break;
      }
      stringBuilder.Append("\",");
      stringBuilder.Append("\"turn\":\"");
      stringBuilder.Append(turn);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"atk\":\"");
      stringBuilder.Append(atk);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"dmg\":\"");
      stringBuilder.Append(dmg);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"heal\":\"");
      stringBuilder.Append(heal);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"beatcnt\":");
      stringBuilder.Append(beat);
      if (myhp != null)
      {
        stringBuilder.Append(',');
        stringBuilder.Append("\"myhp\":[");
        for (int index = 0; index < myhp.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(myhp[index].ToString());
        }
        stringBuilder.Append("]");
      }
      if (enhp != null)
      {
        stringBuilder.Append(',');
        stringBuilder.Append("\"enhp\":[");
        for (int index = 0; index < enhp.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(enhp[index].ToString());
        }
        stringBuilder.Append("]");
      }
      if (place != null)
      {
        stringBuilder.Append(',');
        stringBuilder.Append("\"place\":[");
        for (int index = 0; index < place.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(place[index].ToString());
        }
        stringBuilder.Append("]");
      }
      if (stringBuilder[stringBuilder.Length - 1] == ',')
        --stringBuilder.Length;
      stringBuilder.Append("}");
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
