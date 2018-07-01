// Decompiled with JetBrains decompiler
// Type: SRPG.ObjectiveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
