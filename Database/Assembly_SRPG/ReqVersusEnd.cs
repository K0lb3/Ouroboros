// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqVersusEnd : WebAPI
  {
    public ReqVersusEnd(long btlid, BtlResultTypes result, string uid, string fuid, uint turn, int[] myhp, int[] enhp, int atk, int dmg, int heal, int beat, Network.ResponseCallback response, VERSUS_TYPE type, string trophyprog = null, string bingoprog = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("vs/");
      stringBuilder.Append(type.ToString().ToLower());
      stringBuilder.Append("match/end");
      this.name = stringBuilder.ToString();
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
      stringBuilder.Append("\"token\":\"");
      stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
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
      if ((int) stringBuilder[stringBuilder.Length - 1] == 44)
        --stringBuilder.Length;
      stringBuilder.Append("},");
      stringBuilder.Append("\"uid\":\"");
      stringBuilder.Append(uid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"fuid\":\"");
      stringBuilder.Append(fuid);
      stringBuilder.Append("\"");
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
