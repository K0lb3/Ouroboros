// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchingParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Security.Cryptography;
using System.Text;

namespace SRPG
{
  public class VersusMatchingParam
  {
    public static string TOWER_KEY = "tower";
    public static string FREE_KEY = "free";
    public static string FRIEND_KEY = "friend";
    public string MatchKey;
    public string MatchKeyHash;
    public int MatchLinePoint;

    public void Deserialize(JSON_VersusMatchingParam json)
    {
      if (json == null)
        return;
      this.MatchKey = json.key;
      this.MatchKeyHash = VersusMatchingParam.CalcHash(json.key);
      this.MatchLinePoint = json.point;
    }

    public static string CalcHash(string msg)
    {
      byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(msg));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
      return stringBuilder.ToString();
    }
  }
}
