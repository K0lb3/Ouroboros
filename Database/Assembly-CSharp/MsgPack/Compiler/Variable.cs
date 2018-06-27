// Decompiled with JetBrains decompiler
// Type: MsgPack.Compiler.Variable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Reflection.Emit;

namespace MsgPack.Compiler
{
  public class Variable
  {
    private Variable(VariableType type, int index)
    {
      this.VarType = type;
      this.Index = index;
    }

    public static Variable CreateLocal(LocalBuilder local)
    {
      return new Variable(VariableType.Local, local.LocalIndex);
    }

    public static Variable CreateArg(int idx)
    {
      return new Variable(VariableType.Arg, idx);
    }

    public VariableType VarType { get; set; }

    public int Index { get; set; }
  }
}
