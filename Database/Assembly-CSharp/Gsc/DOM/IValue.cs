// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.DOM
{
  public interface IValue
  {
    bool IsNull();

    bool IsObject();

    bool IsArray();

    bool IsBool();

    bool IsString();

    bool IsInt();

    bool IsUInt();

    bool IsLong();

    bool IsULong();

    bool IsFloat();

    bool IsDouble();

    IObject GetObject();

    IArray GetArray();

    bool ToBool();

    string ToString();

    int ToInt();

    uint ToUInt();

    long ToLong();

    ulong ToULong();

    float ToFloat();

    double ToDouble();

    IValue this[int index] { get; }

    IValue this[string name] { get; }
  }
}
