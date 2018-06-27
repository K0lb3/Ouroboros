// Decompiled with JetBrains decompiler
// Type: MsgPack.Compiler.EmitExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MsgPack.Compiler
{
  public static class EmitExtensions
  {
    public static void EmitLd(this ILGenerator il, Variable v)
    {
      switch (v.VarType)
      {
        case VariableType.Local:
          il.EmitLdloc(v);
          break;
        case VariableType.Arg:
          il.EmitLdarg(v);
          break;
        default:
          throw new ArgumentException();
      }
    }

    public static void EmitLd(this ILGenerator il, params Variable[] list)
    {
      for (int index = 0; index < list.Length; ++index)
        il.EmitLd(list[index]);
    }

    public static void EmitLdarg(this ILGenerator il, Variable v)
    {
      if (v.VarType != VariableType.Arg)
        throw new ArgumentException();
      switch (v.Index)
      {
        case 0:
          il.Emit(OpCodes.Ldarg_0);
          break;
        case 1:
          il.Emit(OpCodes.Ldarg_1);
          break;
        case 2:
          il.Emit(OpCodes.Ldarg_2);
          break;
        case 3:
          il.Emit(OpCodes.Ldarg_3);
          break;
        default:
          if (v.Index <= (int) byte.MaxValue)
          {
            il.Emit(OpCodes.Ldarg_S, (byte) v.Index);
            break;
          }
          if (v.Index > (int) short.MaxValue)
            throw new FormatException();
          il.Emit(OpCodes.Ldarg, v.Index);
          break;
      }
    }

    public static void EmitLdloc(this ILGenerator il, Variable v)
    {
      if (v.VarType != VariableType.Local)
        throw new ArgumentException();
      switch (v.Index)
      {
        case 0:
          il.Emit(OpCodes.Ldloc_0);
          break;
        case 1:
          il.Emit(OpCodes.Ldloc_1);
          break;
        case 2:
          il.Emit(OpCodes.Ldloc_2);
          break;
        case 3:
          il.Emit(OpCodes.Ldloc_3);
          break;
        default:
          if (v.Index <= (int) byte.MaxValue)
          {
            il.Emit(OpCodes.Ldloc_S, (byte) v.Index);
            break;
          }
          if (v.Index > (int) short.MaxValue)
            throw new FormatException();
          il.Emit(OpCodes.Ldloc, v.Index);
          break;
      }
    }

    public static void EmitSt(this ILGenerator il, Variable v)
    {
      switch (v.VarType)
      {
        case VariableType.Local:
          il.EmitStloc(v);
          break;
        case VariableType.Arg:
          il.EmitStarg(v);
          break;
        default:
          throw new ArgumentException();
      }
    }

    public static void EmitStarg(this ILGenerator il, Variable v)
    {
      if (v.VarType != VariableType.Arg)
        throw new ArgumentException();
      if (v.Index <= (int) byte.MaxValue)
      {
        il.Emit(OpCodes.Starg_S, (byte) v.Index);
      }
      else
      {
        if (v.Index > (int) short.MaxValue)
          throw new FormatException();
        il.Emit(OpCodes.Starg, v.Index);
      }
    }

    public static void EmitStloc(this ILGenerator il, Variable v)
    {
      if (v.VarType != VariableType.Local)
        throw new ArgumentException();
      switch (v.Index)
      {
        case 0:
          il.Emit(OpCodes.Stloc_0);
          break;
        case 1:
          il.Emit(OpCodes.Stloc_1);
          break;
        case 2:
          il.Emit(OpCodes.Stloc_2);
          break;
        case 3:
          il.Emit(OpCodes.Stloc_3);
          break;
        default:
          if (v.Index <= (int) byte.MaxValue)
          {
            il.Emit(OpCodes.Stloc_S, (byte) v.Index);
            break;
          }
          if (v.Index > (int) short.MaxValue)
            throw new FormatException();
          il.Emit(OpCodes.Stloc, v.Index);
          break;
      }
    }

    public static void EmitLdc(this ILGenerator il, int v)
    {
      switch (v + 1)
      {
        case 0:
          il.Emit(OpCodes.Ldc_I4_M1);
          break;
        case 1:
          il.Emit(OpCodes.Ldc_I4_0);
          break;
        case 2:
          il.Emit(OpCodes.Ldc_I4_1);
          break;
        case 3:
          il.Emit(OpCodes.Ldc_I4_2);
          break;
        case 4:
          il.Emit(OpCodes.Ldc_I4_3);
          break;
        case 5:
          il.Emit(OpCodes.Ldc_I4_4);
          break;
        case 6:
          il.Emit(OpCodes.Ldc_I4_5);
          break;
        case 7:
          il.Emit(OpCodes.Ldc_I4_6);
          break;
        case 8:
          il.Emit(OpCodes.Ldc_I4_7);
          break;
        case 9:
          il.Emit(OpCodes.Ldc_I4_8);
          break;
        default:
          if (v <= (int) sbyte.MaxValue && v >= (int) sbyte.MinValue)
          {
            il.Emit(OpCodes.Ldc_I4_S, (sbyte) v);
            break;
          }
          il.Emit(OpCodes.Ldc_I4, v);
          break;
      }
    }

    public static void EmitLd_False(this ILGenerator il)
    {
      il.Emit(OpCodes.Ldc_I4_1);
    }

    public static void EmitLd_True(this ILGenerator il)
    {
      il.Emit(OpCodes.Ldc_I4_1);
    }

    public static void EmitLdstr(this ILGenerator il, string v)
    {
      il.Emit(OpCodes.Ldstr, v);
    }

    public static void EmitLdMember(this ILGenerator il, MemberInfo m)
    {
      if (m.MemberType == MemberTypes.Field)
      {
        il.Emit(OpCodes.Ldfld, (FieldInfo) m);
      }
      else
      {
        if (m.MemberType != MemberTypes.Property)
          throw new ArgumentException();
        il.Emit(OpCodes.Callvirt, ((PropertyInfo) m).GetGetMethod(true));
      }
    }

    public static void EmitStMember(this ILGenerator il, MemberInfo m)
    {
      if (m.MemberType == MemberTypes.Field)
      {
        il.Emit(OpCodes.Stfld, (FieldInfo) m);
      }
      else
      {
        if (m.MemberType != MemberTypes.Property)
          throw new ArgumentException();
        il.Emit(OpCodes.Callvirt, ((PropertyInfo) m).GetSetMethod(true));
      }
    }
  }
}
