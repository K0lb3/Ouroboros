// Decompiled with JetBrains decompiler
// Type: SRPG.ObjectiveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ObjectiveParam
  {
    public string iname;
    public JSON_InnerObjective[] objective;

    public void Deserialize(JSON_ObjectiveParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      if (json.objective == null)
        throw new InvalidJSONException();
      this.objective = json.objective;
    }
  }
}
