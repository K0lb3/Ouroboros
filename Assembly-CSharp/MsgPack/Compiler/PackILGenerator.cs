// Decompiled with JetBrains decompiler
// Type: MsgPack.Compiler.PackILGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace MsgPack.Compiler
{
  internal static class PackILGenerator
  {
    public static void EmitPackCode(Type type, MethodInfo mi, ILGenerator il, Func<Type, MemberInfo[]> targetMemberSelector, Func<MemberInfo, string> memberNameFormatter, Func<Type, MethodInfo> lookupPackMethod)
    {
      if (type.IsPrimitive || type.IsInterface)
        throw new NotSupportedException();
      Variable variable1 = Variable.CreateArg(0);
      Variable variable2 = Variable.CreateArg(1);
      Variable local = Variable.CreateLocal(il.DeclareLocal(typeof (int)));
      if (!type.IsValueType)
      {
        Label label = il.DefineLabel();
        il.EmitLd(variable2);
        il.Emit(OpCodes.Brtrue_S, label);
        il.EmitLd(variable1);
        il.Emit(OpCodes.Call, typeof (MsgPackWriter).GetMethod("WriteNil", new Type[0]));
        il.Emit(OpCodes.Ret);
        il.MarkLabel(label);
      }
      if (type.IsArray)
      {
        PackILGenerator.EmitPackArrayCode(mi, il, type, variable1, variable2, local, lookupPackMethod);
      }
      else
      {
        MemberInfo[] memberInfoArray = targetMemberSelector(type);
        il.EmitLd(variable1);
        il.EmitLdc(memberInfoArray.Length);
        il.Emit(OpCodes.Callvirt, typeof (MsgPackWriter).GetMethod("WriteMapHeader", new Type[1]
        {
          typeof (int)
        }));
        for (int index = 0; index < memberInfoArray.Length; ++index)
        {
          MemberInfo memberInfo = memberInfoArray[index];
          Type memberType = memberInfo.GetMemberType();
          il.EmitLd(variable1);
          il.EmitLdstr(memberNameFormatter(memberInfo));
          il.EmitLd_True();
          il.Emit(OpCodes.Call, typeof (MsgPackWriter).GetMethod("Write", new Type[2]
          {
            typeof (string),
            typeof (bool)
          }));
          PackILGenerator.EmitPackMemberValueCode(memberType, il, variable1, variable2, memberInfo, (Variable) null, type, mi, lookupPackMethod);
        }
      }
      il.Emit(OpCodes.Ret);
    }

    private static void EmitPackArrayCode(MethodInfo mi, ILGenerator il, Type t, Variable var_writer, Variable var_obj, Variable var_loop, Func<Type, MethodInfo> lookupPackMethod)
    {
      Type elementType = t.GetElementType();
      il.EmitLd(var_writer, var_obj);
      il.Emit(OpCodes.Ldlen);
      il.Emit(OpCodes.Call, typeof (MsgPackWriter).GetMethod("WriteArrayHeader", new Type[1]
      {
        typeof (int)
      }));
      Label label1 = il.DefineLabel();
      Label label2 = il.DefineLabel();
      il.EmitLdc(0);
      il.EmitSt(var_loop);
      il.Emit(OpCodes.Br_S, label2);
      il.MarkLabel(label1);
      PackILGenerator.EmitPackMemberValueCode(elementType, il, var_writer, var_obj, (MemberInfo) null, var_loop, t, mi, lookupPackMethod);
      il.EmitLd(var_loop);
      il.Emit(OpCodes.Ldc_I4_1);
      il.Emit(OpCodes.Add);
      il.EmitSt(var_loop);
      il.MarkLabel(label2);
      il.EmitLd(var_loop, var_obj);
      il.Emit(OpCodes.Ldlen);
      il.Emit(OpCodes.Blt_S, label1);
    }

    private static void EmitPackMemberValueCode(Type type, ILGenerator il, Variable var_writer, Variable var_obj, MemberInfo m, Variable elementIdx, Type currentType, MethodInfo currentMethod, Func<Type, MethodInfo> lookupPackMethod)
    {
      il.EmitLd(var_writer, var_obj);
      if ((object) m != null)
        il.EmitLdMember(m);
      if (elementIdx != null)
      {
        il.EmitLd(elementIdx);
        il.Emit(OpCodes.Ldelem, type);
      }
      MethodInfo meth;
      if (type.IsPrimitive)
        meth = typeof (MsgPackWriter).GetMethod("Write", new Type[1]
        {
          type
        });
      else
        meth = (object) currentType != (object) type ? lookupPackMethod(type) : currentMethod;
      il.Emit(OpCodes.Call, meth);
    }

    public static void EmitUnpackCode(Type type, MethodInfo mi, ILGenerator il, Func<Type, MemberInfo[]> targetMemberSelector, Func<MemberInfo, string> memberNameFormatter, Func<Type, MethodInfo> lookupUnpackMethod, Func<Type, IDictionary<string, int>> lookupMemberMapping, MethodInfo lookupMemberMappingMethod)
    {
      if (type.IsArray)
        PackILGenerator.EmitUnpackArrayCode(type, mi, il, targetMemberSelector, memberNameFormatter, lookupUnpackMethod);
      else
        PackILGenerator.EmitUnpackMapCode(type, mi, il, targetMemberSelector, memberNameFormatter, lookupUnpackMethod, lookupMemberMapping, lookupMemberMappingMethod);
    }

    private static void EmitUnpackMapCode(Type type, MethodInfo mi, ILGenerator il, Func<Type, MemberInfo[]> targetMemberSelector, Func<MemberInfo, string> memberNameFormatter, Func<Type, MethodInfo> lookupUnpackMethod, Func<Type, IDictionary<string, int>> lookupMemberMapping, MethodInfo lookupMemberMappingMethod)
    {
      MethodInfo method = typeof (PackILGenerator).GetMethod("UnpackFailed", BindingFlags.Static | BindingFlags.NonPublic);
      MemberInfo[] memberInfoArray = targetMemberSelector(type);
      IDictionary<string, int> dictionary = lookupMemberMapping(type);
      for (int index = 0; index < memberInfoArray.Length; ++index)
        dictionary.Add(memberNameFormatter(memberInfoArray[index]), index);
      Variable variable = Variable.CreateArg(0);
      Variable local1 = Variable.CreateLocal(il.DeclareLocal(type));
      Variable local2 = Variable.CreateLocal(il.DeclareLocal(typeof (int)));
      Variable local3 = Variable.CreateLocal(il.DeclareLocal(typeof (int)));
      Variable local4 = Variable.CreateLocal(il.DeclareLocal(typeof (IDictionary<string, int>)));
      Variable local5 = Variable.CreateLocal(il.DeclareLocal(typeof (int)));
      Variable local6 = Variable.CreateLocal(il.DeclareLocal(typeof (Type)));
      PackILGenerator.EmitUnpackReadAndTypeCheckCode(il, variable, typeof (MsgPackReader).GetMethod("IsMap"), method, true);
      il.Emit(OpCodes.Ldtoken, type);
      il.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
      il.EmitSt(local6);
      il.EmitLd(local6);
      il.Emit(OpCodes.Call, lookupMemberMappingMethod);
      il.EmitSt(local4);
      il.EmitLd(local6);
      il.Emit(OpCodes.Call, typeof (FormatterServices).GetMethod("GetUninitializedObject"));
      il.Emit(OpCodes.Castclass, type);
      il.EmitSt(local1);
      il.EmitLd(variable);
      il.Emit(OpCodes.Call, typeof (MsgPackReader).GetProperty("Length").GetGetMethod());
      il.EmitSt(local2);
      Label label1 = il.DefineLabel();
      Label label2 = il.DefineLabel();
      il.EmitLdc(0);
      il.EmitSt(local3);
      il.Emit(OpCodes.Br, label2);
      il.MarkLabel(label1);
      PackILGenerator.EmitUnpackReadAndTypeCheckCode(il, variable, typeof (MsgPackReader).GetMethod("IsRaw"), method, false);
      Label label3 = il.DefineLabel();
      il.EmitLd(local4);
      il.EmitLd(variable);
      il.Emit(OpCodes.Call, typeof (MsgPackReader).GetMethod("ReadRawString", new Type[0]));
      il.Emit(OpCodes.Ldloca_S, (byte) local5.Index);
      il.Emit(OpCodes.Callvirt, typeof (IDictionary<string, int>).GetMethod("TryGetValue"));
      il.Emit(OpCodes.Brtrue, label3);
      il.Emit(OpCodes.Call, method);
      il.MarkLabel(label3);
      Label[] labels = new Label[memberInfoArray.Length];
      for (int index = 0; index < labels.Length; ++index)
        labels[index] = il.DefineLabel();
      Label label4 = il.DefineLabel();
      il.EmitLd(local5);
      il.Emit(OpCodes.Switch, labels);
      il.Emit(OpCodes.Call, method);
      for (int index = 0; index < labels.Length; ++index)
      {
        il.MarkLabel(labels[index]);
        MemberInfo memberInfo = memberInfoArray[index];
        Type memberType = memberInfo.GetMemberType();
        MethodInfo meth = lookupUnpackMethod(memberType);
        il.EmitLd(local1);
        il.EmitLd(variable);
        il.Emit(OpCodes.Call, meth);
        il.EmitStMember(memberInfo);
        il.Emit(OpCodes.Br, label4);
      }
      il.MarkLabel(label4);
      il.EmitLd(local3);
      il.EmitLdc(1);
      il.Emit(OpCodes.Add);
      il.EmitSt(local3);
      il.MarkLabel(label2);
      il.EmitLd(local3);
      il.EmitLd(local2);
      il.Emit(OpCodes.Blt, label1);
      il.EmitLd(local1);
      il.Emit(OpCodes.Ret);
    }

    private static void EmitUnpackArrayCode(Type arrayType, MethodInfo mi, ILGenerator il, Func<Type, MemberInfo[]> targetMemberSelector, Func<MemberInfo, string> memberNameFormatter, Func<Type, MethodInfo> lookupUnpackMethod)
    {
      Type elementType = arrayType.GetElementType();
      MethodInfo method = typeof (PackILGenerator).GetMethod("UnpackFailed", BindingFlags.Static | BindingFlags.NonPublic);
      Variable variable = Variable.CreateArg(0);
      Variable local1 = Variable.CreateLocal(il.DeclareLocal(arrayType));
      Variable local2 = Variable.CreateLocal(il.DeclareLocal(typeof (int)));
      Variable local3 = Variable.CreateLocal(il.DeclareLocal(typeof (int)));
      Variable local4 = Variable.CreateLocal(il.DeclareLocal(typeof (Type)));
      PackILGenerator.EmitUnpackReadAndTypeCheckCode(il, variable, typeof (MsgPackReader).GetMethod("IsArray"), method, true);
      il.Emit(OpCodes.Ldtoken, elementType);
      il.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
      il.EmitSt(local4);
      il.EmitLd(variable);
      il.Emit(OpCodes.Call, typeof (MsgPackReader).GetProperty("Length").GetGetMethod());
      il.EmitSt(local2);
      il.EmitLd(local4);
      il.EmitLd(local2);
      il.Emit(OpCodes.Call, typeof (Array).GetMethod("CreateInstance", new Type[2]
      {
        typeof (Type),
        typeof (int)
      }));
      il.Emit(OpCodes.Castclass, arrayType);
      il.EmitSt(local1);
      MethodInfo meth = lookupUnpackMethod(elementType);
      Label label1 = il.DefineLabel();
      Label label2 = il.DefineLabel();
      il.EmitLdc(0);
      il.EmitSt(local3);
      il.Emit(OpCodes.Br, label2);
      il.MarkLabel(label1);
      il.EmitLd(local1, local3);
      il.EmitLd(variable);
      il.Emit(OpCodes.Call, meth);
      il.Emit(OpCodes.Stelem, elementType);
      il.EmitLd(local3);
      il.EmitLdc(1);
      il.Emit(OpCodes.Add);
      il.EmitSt(local3);
      il.MarkLabel(label2);
      il.EmitLd(local3);
      il.EmitLd(local2);
      il.Emit(OpCodes.Blt, label1);
      il.EmitLd(local1);
      il.Emit(OpCodes.Ret);
    }

    private static void EmitUnpackReadAndTypeCheckCode(ILGenerator il, Variable msgpackReader, MethodInfo typeCheckMethod, MethodInfo failedMethod, bool nullCheckAndReturn)
    {
      Label label1 = il.DefineLabel();
      Label label2 = !nullCheckAndReturn ? new Label() : il.DefineLabel();
      Label label3 = il.DefineLabel();
      il.EmitLd(msgpackReader);
      il.Emit(OpCodes.Call, typeof (MsgPackReader).GetMethod("Read"));
      il.Emit(OpCodes.Brfalse_S, label1);
      if (nullCheckAndReturn)
      {
        il.EmitLd(msgpackReader);
        il.Emit(OpCodes.Call, typeof (MsgPackReader).GetProperty("Type").GetGetMethod());
        il.EmitLdc(192);
        il.Emit(OpCodes.Beq_S, label2);
      }
      il.EmitLd(msgpackReader);
      il.Emit(OpCodes.Call, typeCheckMethod);
      il.Emit(OpCodes.Brtrue_S, label3);
      il.Emit(OpCodes.Br, label1);
      if (nullCheckAndReturn)
      {
        il.MarkLabel(label2);
        il.Emit(OpCodes.Ldnull);
        il.Emit(OpCodes.Ret);
      }
      il.MarkLabel(label1);
      il.Emit(OpCodes.Call, failedMethod);
      il.MarkLabel(label3);
    }

    internal static void UnpackFailed()
    {
      throw new FormatException();
    }

    private static Type GetMemberType(this MemberInfo mi)
    {
      if (mi.MemberType == MemberTypes.Field)
        return ((FieldInfo) mi).FieldType;
      if (mi.MemberType == MemberTypes.Property)
        return ((PropertyInfo) mi).PropertyType;
      throw new ArgumentException();
    }
  }
}
