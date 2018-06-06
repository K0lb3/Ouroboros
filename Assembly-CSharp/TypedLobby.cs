// Decompiled with JetBrains decompiler
// Type: TypedLobby
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public class TypedLobby
{
  public static readonly TypedLobby Default = new TypedLobby();
  public string Name;
  public LobbyType Type;

  public TypedLobby()
  {
    this.Name = string.Empty;
    this.Type = LobbyType.Default;
  }

  public TypedLobby(string name, LobbyType type)
  {
    this.Name = name;
    this.Type = type;
  }

  public bool IsDefault
  {
    get
    {
      if (this.Type == LobbyType.Default)
        return string.IsNullOrEmpty(this.Name);
      return false;
    }
  }

  public override string ToString()
  {
    return string.Format("lobby '{0}'[{1}]", (object) this.Name, (object) this.Type);
  }
}
