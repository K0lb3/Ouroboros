// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlMultiTwEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlMultiTwEnd : WebAPI
  {
    public ReqBtlMultiTwEnd(long btlid, int time, BtlResultTypes result, int[] myhp, string[] myUnit, string[] fuid, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null)
    {
      this.name = "btl/multi/tower/end";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Length = 0;
      stringBuilder.Append("\"btlid\":");
      stringBuilder.Append(btlid);
      stringBuilder.Append(',');
      stringBuilder.Append("\"btlendparam\":{");
      stringBuilder.Append("\"time\":");
      stringBuilder.Append(time);
      stringBuilder.Append(',');
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
      stringBuilder.Append("\"token\":\"");
      stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
      stringBuilder.Append("\"");
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
      if (myUnit != null)
      {
        stringBuilder.Append(',');
        stringBuilder.Append("\"myUnit\":[");
        for (int index = 0; index < myhp.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append("\"" + myUnit[index] + "\"");
        }
        stringBuilder.Append("]");
      }
      stringBuilder.Append("}");
      if (fuid != null)
      {
        stringBuilder.Append(",\"fuids\":[");
        for (int index = 0; index < fuid.Length; ++index)
        {
          if (fuid[index] != null)
          {
            if (index != 0)
              stringBuilder.Append(", ");
            stringBuilder.Append("\"");
            stringBuilder.Append(fuid[index]);
            stringBuilder.Append("\"");
          }
        }
        stringBuilder.Append("]");
      }
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
