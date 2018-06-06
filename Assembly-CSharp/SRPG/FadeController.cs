// Decompiled with JetBrains decompiler
// Type: SRPG.FadeController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("")]
  public class FadeController : MonoBehaviour
  {
    private const int FADE_TYPE_MAX = 3;
    private Color[] mCurrentColor;
    private Color[] mStartColor;
    private Color[] mEndColor;
    private float[] mCurrentTime;
    private float[] mDuration;
    private Canvas[] mCanvas;
    private RawImage[] mImage;
    private bool[] mInitialized;
    private Color mSceneFadeStart;
    private Color mSceneFadeEnd;
    private float mSceneFadeDuration;
    private float mSceneFadeTime;
    private TacticsUnitController[] mSceneFadeExcluders;
    private TacticsUnitController[] mSceneFadeIncluders;
    private static FadeController mInstance;

    public FadeController()
    {
      base.\u002Ector();
    }

    public static bool InstanceExists
    {
      get
      {
        return Object.op_Inequality((Object) FadeController.mInstance, (Object) null);
      }
    }

    public static FadeController Instance
    {
      get
      {
        if (Object.op_Equality((Object) FadeController.mInstance, (Object) null))
          FadeController.mInstance = (FadeController) new GameObject(nameof (FadeController), new System.Type[1]
          {
            typeof (FadeController)
          }).GetComponent<FadeController>();
        return FadeController.mInstance;
      }
    }

    private void Awake()
    {
      Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
      Array values = Enum.GetValues(typeof (FadeController.LayerType));
      string[] strArray = new string[3]{ string.Empty, "Custom/Particle/UnlitAdd NoZTest (TwoSided)", "Custom/Particle/UnlitAlpha NoZTest (TwoSided)" };
      for (int index = 0; index < 3; ++index)
      {
        this.mCurrentColor[index] = new Color(0.0f, 0.0f, 0.0f);
        this.mStartColor[index] = new Color(0.0f, 0.0f, 0.0f);
        this.mEndColor[index] = new Color(0.0f, 0.0f, 0.0f);
        GameObject gameObject = new GameObject(values.GetValue(index).ToString(), new System.Type[2]{ typeof (Canvas), typeof (RawImage) });
        this.mCanvas[index] = (Canvas) gameObject.GetComponent<Canvas>();
        this.mCanvas[index].set_sortingOrder(9999 - index);
        this.mCanvas[index].set_renderMode((RenderMode) 0);
        ((Behaviour) this.mCanvas[index]).set_enabled(false);
        this.mImage[index] = (RawImage) gameObject.GetComponent<RawImage>();
        ((Graphic) this.mImage[index]).set_color(this.mEndColor[index]);
        if (!string.IsNullOrEmpty(strArray[index]))
        {
          Shader shader = Shader.Find(strArray[index]);
          if (Object.op_Inequality((Object) shader, (Object) null))
          {
            ((Graphic) this.mImage[index]).set_material(new Material(shader));
            ((Graphic) this.mImage[index]).get_material().SetColor("_Color", Color.get_white());
          }
        }
        gameObject.get_transform().SetParent(((Component) this).get_gameObject().get_transform());
      }
    }

    public bool IsScreenFaded(int layer = 0)
    {
      return ((Graphic) this.mImage[layer]).get_color().a >= 1.0;
    }

    public bool IsFading(int layer = 0)
    {
      return (double) this.mCurrentTime[layer] < (double) this.mDuration[layer];
    }

    public void FadeTo(Color dest, float time, int layer = 0)
    {
      if (!this.mInitialized[layer])
      {
        this.mCurrentColor[layer] = dest;
        this.mCurrentColor[layer].a = (__Null) (1.0 - this.mCurrentColor[layer].a);
        this.mInitialized[layer] = true;
        ((Graphic) this.mImage[layer]).set_color(this.mCurrentColor[layer]);
      }
      if ((double) time > 0.0)
      {
        this.mStartColor[layer] = this.mCurrentColor[layer];
        this.mEndColor[layer] = dest;
        this.mCurrentTime[layer] = 0.0f;
        this.mDuration[layer] = time;
        ((Behaviour) this.mCanvas[layer]).set_enabled(true);
      }
      else
      {
        this.mCurrentColor[layer] = dest;
        this.mCurrentTime[layer] = 0.0f;
        this.mDuration[layer] = 0.0f;
        ((Graphic) this.mImage[layer]).set_color(this.mCurrentColor[layer]);
        ((Behaviour) this.mCanvas[layer]).set_enabled(this.mCurrentColor[layer].a > 0.0);
      }
    }

    public void ResetSceneFade(float time)
    {
      this.mSceneFadeEnd = Color.get_white();
      this.mSceneFadeStart = CameraHook.ColorMod;
      this.mSceneFadeDuration = time;
      this.mSceneFadeTime = 0.0f;
      if ((double) this.mSceneFadeDuration > 0.0)
        return;
      this.ApplySceneFade(this.mSceneFadeEnd);
    }

    public void BeginSceneFade(Color dest, float time, TacticsUnitController[] excludes, TacticsUnitController[] includes)
    {
      this.mSceneFadeStart = CameraHook.ColorMod;
      this.mSceneFadeEnd = dest;
      this.mSceneFadeDuration = time;
      this.mSceneFadeTime = 0.0f;
      this.mSceneFadeExcluders = excludes;
      this.mSceneFadeIncluders = includes;
      if ((double) this.mSceneFadeDuration > 0.0)
        return;
      this.ApplySceneFade(dest);
    }

    private void ApplySceneFade(Color fadeColor)
    {
      CameraHook.ColorMod = fadeColor;
      if (this.mSceneFadeIncluders != null && this.mSceneFadeExcluders != null)
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
        {
          TacticsUnitController instance = TacticsUnitController.Instances[index];
          if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeIncluders, instance) >= 0 && Array.IndexOf<TacticsUnitController>(this.mSceneFadeExcluders, instance) < 0)
            instance.ColorMod = fadeColor;
          else
            instance.ResetColorMod();
        }
      }
      else if (this.mSceneFadeIncluders != null)
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
        {
          TacticsUnitController instance = TacticsUnitController.Instances[index];
          if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeIncluders, instance) >= 0)
            instance.ColorMod = fadeColor;
          else
            instance.ResetColorMod();
        }
      }
      else if (this.mSceneFadeExcluders != null)
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
        {
          TacticsUnitController instance = TacticsUnitController.Instances[index];
          if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeExcluders, instance) < 0)
            instance.ColorMod = fadeColor;
          else
            instance.ResetColorMod();
        }
      }
      else
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
          TacticsUnitController.Instances[index].ColorMod = fadeColor;
      }
    }

    public bool IsSceneFading
    {
      get
      {
        return (double) this.mSceneFadeTime < (double) this.mSceneFadeDuration;
      }
    }

    private void UpdateSceneFade()
    {
      if ((double) this.mSceneFadeTime >= (double) this.mSceneFadeDuration)
        return;
      this.mSceneFadeTime += Time.get_deltaTime();
      this.ApplySceneFade(Color.Lerp(this.mSceneFadeStart, this.mSceneFadeEnd, Mathf.Clamp01(this.mSceneFadeTime / this.mSceneFadeDuration)));
    }

    private void Update()
    {
      this.UpdateSceneFade();
      for (int index = 0; index < 3; ++index)
      {
        if ((double) this.mCurrentTime[index] >= (double) this.mDuration[index])
        {
          if (this.mCurrentColor[index].a <= 0.0 && ((Behaviour) this.mCanvas[index]).get_enabled())
            ((Behaviour) this.mCanvas[index]).set_enabled(false);
        }
        else
        {
          this.mCurrentTime[index] += Time.get_unscaledDeltaTime();
          float num = Mathf.Clamp01(this.mCurrentTime[index] / this.mDuration[index]);
          this.mCurrentColor[index] = Color.Lerp(this.mStartColor[index], this.mEndColor[index], num);
          ((Graphic) this.mImage[index]).set_color(this.mCurrentColor[index]);
        }
      }
    }

    public enum LayerType
    {
      Normal,
      Add,
      AlphaBlend,
    }
  }
}
