// Decompiled with JetBrains decompiler
// Type: SRPG.ObjectiveParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
