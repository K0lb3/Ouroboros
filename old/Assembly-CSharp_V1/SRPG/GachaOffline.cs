// Decompiled with JetBrains decompiler
// Type: SRPG.GachaOffline
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GachaOffline
  {
    private List<KeyValuePair<int, string>> MakeDropTable(string tablePath)
    {
      List<KeyValuePair<int, string>> keyValuePairList = new List<KeyValuePair<int, string>>();
      TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>(tablePath);
      if (Object.op_Equality((Object) textAsset, (Object) null))
      {
        DebugUtility.LogWarning("ドロップテーブル '" + tablePath + "' の読み込みに失敗しました。");
        return new List<KeyValuePair<int, string>>();
      }
      string[] strArray = textAsset.get_text().Replace("\r\n", "\n").Split('\n');
      string str = (string) null;
      foreach (string s in strArray)
      {
        int key;
        try
        {
          key = int.Parse(s);
        }
        catch (Exception ex)
        {
          str = s;
          if (s.IndexOf("\r") != -1)
          {
            str = str.Replace("\r", string.Empty);
            continue;
          }
          continue;
        }
        keyValuePairList.Add(new KeyValuePair<int, string>(key, str));
      }
      string msg = string.Empty;
      using (List<KeyValuePair<int, string>>.Enumerator enumerator = keyValuePairList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<int, string> current = enumerator.Current;
          msg = msg + (object) current.Key + ": " + current.Value + "\n";
        }
      }
      DebugUtility.Log("# drop table #");
      DebugUtility.Log(msg);
      DebugUtility.Log("##############");
      return keyValuePairList;
    }

    public string ExecGacha(string fileName)
    {
      List<KeyValuePair<int, string>> keyValuePairList = this.MakeDropTable("LocalMaps/drop/" + fileName);
      int num1 = 0;
      using (List<KeyValuePair<int, string>>.Enumerator enumerator = keyValuePairList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<int, string> current = enumerator.Current;
          num1 += current.Key;
        }
      }
      int num2 = Random.Range(0, num1);
      using (List<KeyValuePair<int, string>>.Enumerator enumerator = keyValuePairList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<int, string> current = enumerator.Current;
          num2 -= current.Key;
          if (num2 < 0)
            return current.Value;
        }
      }
      return "none";
    }
  }
}
