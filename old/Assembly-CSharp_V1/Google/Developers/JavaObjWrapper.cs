// Decompiled with JetBrains decompiler
// Type: Google.Developers.JavaObjWrapper
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Google.Developers
{
  public class JavaObjWrapper
  {
    private IntPtr raw;

    protected JavaObjWrapper()
    {
    }

    public JavaObjWrapper(string clazzName)
    {
      this.raw = AndroidJNI.AllocObject(AndroidJNI.FindClass(clazzName));
    }

    public JavaObjWrapper(IntPtr rawObject)
    {
      this.raw = rawObject;
    }

    public IntPtr RawObject
    {
      get
      {
        return this.raw;
      }
    }

    public void CreateInstance(string clazzName, params object[] args)
    {
      if (this.raw != IntPtr.Zero)
        throw new Exception("Java object already set");
      IntPtr num = AndroidJNI.FindClass(clazzName);
      IntPtr constructorId = AndroidJNIHelper.GetConstructorID(num, args);
      jvalue[] jvalueArray = JavaObjWrapper.ConstructArgArray(args);
      this.raw = AndroidJNI.NewObject(num, constructorId, jvalueArray);
    }

    protected static jvalue[] ConstructArgArray(object[] theArgs)
    {
      object[] objArray = new object[theArgs.Length];
      for (int index = 0; index < theArgs.Length; ++index)
        objArray[index] = !(theArgs[index] is JavaObjWrapper) ? theArgs[index] : (object) ((JavaObjWrapper) theArgs[index]).raw;
      jvalue[] jniArgArray = AndroidJNIHelper.CreateJNIArgArray(objArray);
      for (int index = 0; index < theArgs.Length; ++index)
      {
        if (theArgs[index] is JavaObjWrapper)
          jniArgArray[index].l = (__Null) ((JavaObjWrapper) theArgs[index]).raw;
        else if (theArgs[index] is JavaInterfaceProxy)
        {
          IntPtr javaProxy = AndroidJNIHelper.CreateJavaProxy((AndroidJavaProxy) theArgs[index]);
          jniArgArray[index].l = (__Null) javaProxy;
        }
      }
      if (jniArgArray.Length == 1)
      {
        for (int index = 0; index < jniArgArray.Length; ++index)
          Debug.Log((object) ("---- [" + (object) index + "] -- " + (object) (IntPtr) jniArgArray[index].l));
      }
      return jniArgArray;
    }

    public static T StaticInvokeObjectCall<T>(string type, string name, string sig, params object[] args)
    {
      IntPtr num = AndroidJNI.FindClass(type);
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(num, name, sig);
      jvalue[] jvalueArray = JavaObjWrapper.ConstructArgArray(args);
      IntPtr ptr = AndroidJNI.CallStaticObjectMethod(num, staticMethodId, jvalueArray);
      ConstructorInfo constructor = typeof (T).GetConstructor(new Type[1]{ ptr.GetType() });
      if ((object) constructor != null)
        return (T) constructor.Invoke(new object[1]{ (object) ptr });
      if (typeof (T).IsArray)
        return AndroidJNIHelper.ConvertFromJNIArray<T>(ptr);
      Debug.Log((object) "Trying cast....");
      Type structureType = typeof (T);
      return (T) Marshal.PtrToStructure(ptr, structureType);
    }

    public static void StaticInvokeCallVoid(string type, string name, string sig, params object[] args)
    {
      IntPtr num = AndroidJNI.FindClass(type);
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(num, name, sig);
      jvalue[] jvalueArray = JavaObjWrapper.ConstructArgArray(args);
      AndroidJNI.CallStaticVoidMethod(num, staticMethodId, jvalueArray);
    }

    public static T GetStaticObjectField<T>(string clsName, string name, string sig)
    {
      IntPtr num = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(num, name, sig);
      IntPtr staticObjectField = AndroidJNI.GetStaticObjectField(num, staticFieldId);
      ConstructorInfo constructor = typeof (T).GetConstructor(new Type[1]{ staticObjectField.GetType() });
      if ((object) constructor != null)
        return (T) constructor.Invoke(new object[1]{ (object) staticObjectField });
      Type structureType = typeof (T);
      return (T) Marshal.PtrToStructure(staticObjectField, structureType);
    }

    public static int GetStaticIntField(string clsName, string name)
    {
      IntPtr num = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(num, name, "I");
      return AndroidJNI.GetStaticIntField(num, staticFieldId);
    }

    public static string GetStaticStringField(string clsName, string name)
    {
      IntPtr num = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(num, name, "Ljava/lang/String;");
      return AndroidJNI.GetStaticStringField(num, staticFieldId);
    }

    public static float GetStaticFloatField(string clsName, string name)
    {
      IntPtr num = AndroidJNI.FindClass(clsName);
      IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(num, name, "F");
      return AndroidJNI.GetStaticFloatField(num, staticFieldId);
    }

    public void InvokeCallVoid(string name, string sig, params object[] args)
    {
      AndroidJNI.CallVoidMethod(this.raw, AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(this.raw), name, sig), JavaObjWrapper.ConstructArgArray(args));
    }

    public T InvokeCall<T>(string name, string sig, params object[] args)
    {
      Type type = typeof (T);
      IntPtr methodId = AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(this.raw), name, sig);
      jvalue[] jvalueArray = JavaObjWrapper.ConstructArgArray(args);
      if ((object) type == (object) typeof (bool))
        return (T) (ValueType) AndroidJNI.CallBooleanMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (string))
        return (T) AndroidJNI.CallStringMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (int))
        return (T) (ValueType) AndroidJNI.CallIntMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (float))
        return (T) (ValueType) AndroidJNI.CallFloatMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (double))
        return (T) (ValueType) AndroidJNI.CallDoubleMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (byte))
        return (T) (ValueType) AndroidJNI.CallByteMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (char))
        return (T) (ValueType) AndroidJNI.CallCharMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (long))
        return (T) (ValueType) AndroidJNI.CallLongMethod(this.raw, methodId, jvalueArray);
      if ((object) type == (object) typeof (short))
        return (T) (ValueType) AndroidJNI.CallShortMethod(this.raw, methodId, jvalueArray);
      return this.InvokeObjectCall<T>(name, sig, args);
    }

    public static T StaticInvokeCall<T>(string type, string name, string sig, params object[] args)
    {
      Type type1 = typeof (T);
      IntPtr num = AndroidJNI.FindClass(type);
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(num, name, sig);
      jvalue[] jvalueArray = JavaObjWrapper.ConstructArgArray(args);
      if ((object) type1 == (object) typeof (bool))
        return (T) (ValueType) AndroidJNI.CallStaticBooleanMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (string))
        return (T) AndroidJNI.CallStaticStringMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (int))
        return (T) (ValueType) AndroidJNI.CallStaticIntMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (float))
        return (T) (ValueType) AndroidJNI.CallStaticFloatMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (double))
        return (T) (ValueType) AndroidJNI.CallStaticDoubleMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (byte))
        return (T) (ValueType) AndroidJNI.CallStaticByteMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (char))
        return (T) (ValueType) AndroidJNI.CallStaticCharMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (long))
        return (T) (ValueType) AndroidJNI.CallStaticLongMethod(num, staticMethodId, jvalueArray);
      if ((object) type1 == (object) typeof (short))
        return (T) (ValueType) AndroidJNI.CallStaticShortMethod(num, staticMethodId, jvalueArray);
      return JavaObjWrapper.StaticInvokeObjectCall<T>(type, name, sig, args);
    }

    public T InvokeObjectCall<T>(string name, string sig, params object[] theArgs)
    {
      IntPtr ptr = AndroidJNI.CallObjectMethod(this.raw, AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(this.raw), name, sig), JavaObjWrapper.ConstructArgArray(theArgs));
      if (ptr.Equals((object) IntPtr.Zero))
        return default (T);
      ConstructorInfo constructor = typeof (T).GetConstructor(new Type[1]{ ptr.GetType() });
      if ((object) constructor != null)
        return (T) constructor.Invoke(new object[1]{ (object) ptr });
      Type structureType = typeof (T);
      return (T) Marshal.PtrToStructure(ptr, structureType);
    }
  }
}
