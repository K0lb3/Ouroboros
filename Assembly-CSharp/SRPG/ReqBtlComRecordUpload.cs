// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComRecordUpload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqBtlComRecordUpload : WebAPI
  {
    public ReqBtlComRecordUpload(string fuid, long btlid, int[] achieved, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response)
    {
      this.name = "btl/com/record/upload";
      this.body = ReqBtlComRecordUpload.CreateRequestString(ReqBtlComRecordUpload.makeBody(fuid, btlid, achieved, time, result, beats, itemSteals, goldSteals, missions, usedItems));
      this.callback = response;
    }

    private static string CreateRequestString(string body)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{\"ticket\":" + (object) Network.TicketID + ",");
      stringBuilder.Append("\"access_token\":\"" + Network.SessionID + "\",");
      stringBuilder.Append("\"device_id\":\"" + MonoSingleton<GameManager>.Instance.DeviceId + "\"");
      if (!string.IsNullOrEmpty(body))
        stringBuilder.Append(",\"param\":{" + body + "}");
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    private static string makeBody(string fuid, long btlid, int[] achieved, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, Dictionary<OString, OInt> usedItems)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Length = 0;
      stringBuilder.Append("\"btlid\":");
      stringBuilder.Append(btlid);
      stringBuilder.Append(',');
      if (achieved != null)
      {
        stringBuilder.Append("\"achieved\":[");
        for (int index = 0; index < achieved.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(',');
          stringBuilder.Append(achieved[index].ToString());
        }
        stringBuilder.Append("],");
      }
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
      if ((int) stringBuilder[stringBuilder.Length - 1] == 44)
        --stringBuilder.Length;
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }
  }
}
