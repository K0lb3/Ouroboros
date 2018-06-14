// Decompiled with JetBrains decompiler
// Type: SoundSettings
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SoundSettings : ScriptableObject
{
  public const float BGMCrossFadeTime = 1f;
  public string Tap;
  public string OK;
  public string Cancel;
  public string Select;
  public string Buzzer;
  public string Swipe;
  public string ScrollList;
  public string WindowPop;
  public string WindowClose;
  private static SoundSettings mInstance;

  public SoundSettings()
  {
    base.\u002Ector();
  }

  public static SoundSettings Current
  {
    get
    {
      if (Object.op_Equality((Object) SoundSettings.mInstance, (Object) null))
      {
        SoundSettings.mInstance = (SoundSettings) Resources.Load<SoundSettings>(nameof (SoundSettings));
        Object.DontDestroyOnLoad((Object) SoundSettings.mInstance);
      }
      return SoundSettings.mInstance;
    }
  }
}
