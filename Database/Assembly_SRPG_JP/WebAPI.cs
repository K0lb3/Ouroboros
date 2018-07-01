// Decompiled with JetBrains decompiler
// Type: SRPG.WebAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.Networking;

namespace SRPG
{
  public abstract class WebAPI
  {
    private static StringBuilder mSB = new StringBuilder(512);
    public readonly string GumiTransactionId = Guid.NewGuid().ToString();
    public string name;
    public string body;
    public Network.ResponseCallback callback;
    public WebAPI.ReqeustType reqtype;
    public DownloadHandler dlHandler;

    protected static StringBuilder GetStringBuilder()
    {
      WebAPI.mSB.Length = 0;
      return WebAPI.mSB;
    }

    public static string EscapeString(string s)
    {
      s = s.Replace("\\", "\\\\");
      s = s.Replace("\"", "\\\"");
      return s;
    }

    protected static string GetRequestString<T>(T param)
    {
      return JsonUtility.ToJson((object) new WebAPI.RequestParamWithTicketId<T>(Network.TicketID, param));
    }

    protected static string GetRequestString(string body)
    {
      string str = "{\"ticket\":" + (object) Network.TicketID;
      if (!string.IsNullOrEmpty(body))
        str = str + ",\"param\":{" + body + "}";
      return str + "}";
    }

    protected static string GetBtlEndParamString(BattleCore.Record record, bool multi = false)
    {
      string str1 = (string) null;
      if (record != null)
      {
        int num = 0;
        string str2 = "win";
        if (multi && record.result == BattleCore.QuestResult.Pending)
          str2 = "retire";
        else if (record.result != BattleCore.QuestResult.Win)
          str2 = "lose";
        int[] numArray1 = new int[record.drops.Length];
        for (int index = 0; index < record.drops.Length; ++index)
          numArray1[index] = (int) record.drops[index];
        int[] numArray2 = new int[record.item_steals.Length];
        for (int index = 0; index < record.item_steals.Length; ++index)
          numArray2[index] = (int) record.item_steals[index];
        int[] numArray3 = new int[record.gold_steals.Length];
        for (int index = 0; index < record.gold_steals.Length; ++index)
          numArray3[index] = (int) record.gold_steals[index];
        int[] numArray4 = new int[record.bonusCount];
        for (int index = 0; index < numArray4.Length; ++index)
          numArray4[index] = (record.bonusFlags & 1 << index) == 0 ? 0 : 1;
        string str3 = str1 + "\"btlendparam\":{" + "\"time\":" + (object) num + "," + "\"result\":\"" + str2 + "\"," + "\"beats\":[";
        for (int index = 0; index < numArray1.Length; ++index)
        {
          str3 += numArray1[index].ToString();
          if (index != numArray1.Length - 1)
            str3 += ",";
        }
        string str4 = str3 + "]," + "\"steals\":{" + "\"items\":[";
        for (int index = 0; index < numArray2.Length; ++index)
        {
          str4 += numArray2[index].ToString();
          if (index != numArray1.Length - 1)
            str4 += ",";
        }
        string str5 = str4 + "]," + "\"golds\":[";
        for (int index = 0; index < numArray3.Length; ++index)
        {
          str5 += numArray3[index].ToString();
          if (index != numArray1.Length - 1)
            str5 += ",";
        }
        string str6 = str5 + "]" + "}," + "\"missions\":[";
        for (int index = 0; index < numArray4.Length; ++index)
        {
          str6 += numArray4[index].ToString();
          if (index != numArray4.Length - 1)
            str6 += ",";
        }
        string str7 = str6 + "]";
        if (multi)
          str7 = str7 + ",\"token\":\"" + JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName) + "\"";
        str1 = str7 + "}";
      }
      return str1;
    }

    public static string KeyValueToString(string key, string value)
    {
      return string.Format("\"{0}\":\"{1}\"", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, bool value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) (!value ? 0 : 1));
    }

    public static string KeyValueToString(string key, byte value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, short value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, int value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, long value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, ushort value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, uint value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, ulong value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public enum ReqeustType
    {
      REQ_GSC,
      REQ_STREAM,
    }

    public class JSON_BaseResponse
    {
      public int stat;
      public string stat_msg;
      public string stat_code;
      public long time;
      public int ticket;
    }

    public class JSON_BodyResponse<T> : WebAPI.JSON_BaseResponse
    {
      public T body;
    }

    protected class RequestParamWithTicketId<T>
    {
      public int ticket;
      public T param;

      public RequestParamWithTicketId(int _ticket, T _param)
      {
        this.ticket = _ticket;
        this.param = _param;
      }
    }
  }
}
