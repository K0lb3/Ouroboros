// Decompiled with JetBrains decompiler
// Type: SRPG.ChargeCheckData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChargeCheckData
  {
    public int Age;
    public string[] AcceptIds;
    public string[] RejectIds;

    public bool Deserialize(JSON_ChargeCheckResponse json)
    {
      if (json == null)
        return false;
      this.Age = json.age;
      this.AcceptIds = json.accept_ids;
      this.RejectIds = json.reject_ids;
      if (this.RejectIds == null)
        this.RejectIds = new string[0];
      return true;
    }
  }
}
