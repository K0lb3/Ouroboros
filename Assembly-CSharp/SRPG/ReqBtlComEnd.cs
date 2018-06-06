// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComEnd
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqBtlComEnd : WebAPI
  {
    public ReqBtlComEnd(long btlid, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, string[] fuid, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response, BtlEndTypes apiType, string trophyprog = null, string bingoprog = null, string maxdata = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("btl/");
      stringBuilder.Append(apiType.ToString());
      stringBuilder.Append("/end");
      this.name = stringBuilder.ToString();
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
      }
      if (result == BtlResultTypes.Win)
      {
        if (beats == null)
          beats = new int[0];
        if (itemSteals == null)
          itemSteals = new int[0];
        if (goldSteals == null)
          goldSteals = new int[0];
        if (missions == null)
          missions = new int[3];
      }
      if (result != BtlResultTypes.Cancel && usedItems == null)
        usedItems = new Dictionary<OString, OInt>();
      stringBuilder.Append("\",");
      if (beats != null)
      {
        stringBuilder.Append("\"beats\":[");
        for (int index = 0; index < beats.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(beats[index].ToString());
        }
        stringBuilder.Append("],");
      }
      if (itemSteals != null || goldSteals != null)
      {
        stringBuilder.Append("\"steals\":{");
        if (itemSteals != null)
        {
          stringBuilder.Append("\"items\":[");
          for (int index = 0; index < itemSteals.Length; ++index)
          {
            stringBuilder.Append(itemSteals[index].ToString());
            if (index != beats.Length - 1)
              stringBuilder.Append(',');
          }
          stringBuilder.Append("]");
        }
        if (goldSteals != null)
        {
          if (itemSteals != null)
            stringBuilder.Append(',');
          stringBuilder.Append("\"golds\":[");
          for (int index = 0; index < goldSteals.Length; ++index)
          {
            stringBuilder.Append(goldSteals[index].ToString());
            if (index != beats.Length - 1)
              stringBuilder.Append(",");
          }
          stringBuilder.Append("]");
        }
        stringBuilder.Append("},");
      }
      if (missions != null)
      {
        stringBuilder.Append("\"missions\":[");
        for (int index = 0; index < missions.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(missions[index].ToString());
        }
        stringBuilder.Append("],");
      }
      if (usedItems != null)
      {
        stringBuilder.Append("\"inputs\":[");
        int num = 0;
        using (Dictionary<OString, OInt>.Enumerator enumerator = usedItems.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<OString, OInt> current = enumerator.Current;
            if (num > 0)
              stringBuilder.Append(',');
            stringBuilder.Append("{");
            stringBuilder.Append("\"use\":\"");
            stringBuilder.Append((string) current.Key);
            stringBuilder.Append("\",");
            stringBuilder.Append("\"n\":");
            stringBuilder.Append((int) current.Value);
            stringBuilder.Append("}");
            ++num;
          }
        }
        stringBuilder.Append("],");
      }
      if (apiType == BtlEndTypes.multi)
      {
        stringBuilder.Append("\"token\":\"");
        stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
        stringBuilder.Append("\",");
      }
      if ((int) stringBuilder[stringBuilder.Length - 1] == 44)
        --stringBuilder.Length;
      stringBuilder.Append('}');
      if (apiType == BtlEndTypes.multi && fuid != null)
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
      if (!string.IsNullOrEmpty(maxdata))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(maxdata);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
