// Decompiled with JetBrains decompiler
// Type: IntVector2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

[Serializable]
public struct IntVector2
{
  public int x;
  public int y;

  public IntVector2(int a, int b)
  {
    this.x = a;
    this.y = b;
  }

  public override string ToString()
  {
    return string.Format("[IntVector2] {0}, {1}", (object) this.x, (object) this.y);
  }

  public override bool Equals(object obj)
  {
    if (obj is IntVector2)
      return (IntVector2) obj == this;
    return false;
  }

  public override int GetHashCode()
  {
    return base.GetHashCode();
  }

  public static bool operator ==(IntVector2 a, IntVector2 b)
  {
    if (a.x == b.x)
      return a.y == b.y;
    return false;
  }

  public static bool operator !=(IntVector2 a, IntVector2 b)
  {
    if (a.x == b.x)
      return a.y != b.y;
    return true;
  }
}
