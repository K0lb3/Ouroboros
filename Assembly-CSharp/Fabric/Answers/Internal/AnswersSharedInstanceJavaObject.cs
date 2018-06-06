// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Internal.AnswersSharedInstanceJavaObject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Fabric.Answers.Internal
{
  internal class AnswersSharedInstanceJavaObject
  {
    private AndroidJavaObject javaObject;

    public AnswersSharedInstanceJavaObject()
    {
      this.javaObject = (AndroidJavaObject) ((AndroidJavaObject) new AndroidJavaClass("com.crashlytics.android.answers.Answers")).CallStatic<AndroidJavaObject>("getInstance", new object[0]);
    }

    public void Log(string methodName, AnswersEventInstanceJavaObject eventInstance)
    {
      this.javaObject.Call(methodName, new object[1]
      {
        (object) eventInstance.javaObject
      });
    }
  }
}
