namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

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
            this.mCurrentColor = new Color[3];
            this.mStartColor = new Color[3];
            this.mEndColor = new Color[3];
            this.mCurrentTime = new float[3];
            this.mDuration = new float[3];
            this.mCanvas = new Canvas[3];
            this.mImage = new RawImage[3];
            this.mInitialized = new bool[3];
            base..ctor();
            return;
        }

        private void ApplySceneFade(Color fadeColor)
        {
            int num;
            TacticsUnitController controller;
            int num2;
            TacticsUnitController controller2;
            int num3;
            TacticsUnitController controller3;
            int num4;
            CameraHook.ColorMod = fadeColor;
            if (this.mSceneFadeIncluders == null)
            {
                goto Label_0080;
            }
            if (this.mSceneFadeExcluders == null)
            {
                goto Label_0080;
            }
            num = TacticsUnitController.Instances.Count - 1;
            goto Label_0074;
        Label_002E:
            controller = TacticsUnitController.Instances[num];
            if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeIncluders, controller) < 0)
            {
                goto Label_006A;
            }
            if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeExcluders, controller) >= 0)
            {
                goto Label_006A;
            }
            controller.ColorMod = fadeColor;
            goto Label_0070;
        Label_006A:
            controller.ResetColorMod();
        Label_0070:
            num -= 1;
        Label_0074:
            if (num >= 0)
            {
                goto Label_002E;
            }
            goto Label_0176;
        Label_0080:
            if (this.mSceneFadeIncluders == null)
            {
                goto Label_00DD;
            }
            num2 = TacticsUnitController.Instances.Count - 1;
            goto Label_00D1;
        Label_009D:
            controller2 = TacticsUnitController.Instances[num2];
            if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeIncluders, controller2) < 0)
            {
                goto Label_00C7;
            }
            controller2.ColorMod = fadeColor;
            goto Label_00CD;
        Label_00C7:
            controller2.ResetColorMod();
        Label_00CD:
            num2 -= 1;
        Label_00D1:
            if (num2 >= 0)
            {
                goto Label_009D;
            }
            goto Label_0176;
        Label_00DD:
            if (this.mSceneFadeExcluders == null)
            {
                goto Label_0143;
            }
            num3 = TacticsUnitController.Instances.Count - 1;
            goto Label_0136;
        Label_00FB:
            controller3 = TacticsUnitController.Instances[num3];
            if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeExcluders, controller3) >= 0)
            {
                goto Label_0129;
            }
            controller3.ColorMod = fadeColor;
            goto Label_0130;
        Label_0129:
            controller3.ResetColorMod();
        Label_0130:
            num3 -= 1;
        Label_0136:
            if (num3 >= 0)
            {
                goto Label_00FB;
            }
            goto Label_0176;
        Label_0143:
            num4 = TacticsUnitController.Instances.Count - 1;
            goto Label_016E;
        Label_0156:
            TacticsUnitController.Instances[num4].ColorMod = fadeColor;
            num4 -= 1;
        Label_016E:
            if (num4 >= 0)
            {
                goto Label_0156;
            }
        Label_0176:
            return;
        }

        private unsafe void Awake()
        {
            Type[] typeArray1;
            string[] textArray1;
            Array array;
            string[] strArray;
            int num;
            GameObject obj2;
            Shader shader;
            Object.DontDestroyOnLoad(base.get_gameObject());
            array = Enum.GetValues(typeof(LayerType));
            textArray1 = new string[] { string.Empty, "Custom/Particle/UnlitAdd NoZTest (TwoSided)", "Custom/Particle/UnlitAlpha NoZTest (TwoSided)" };
            strArray = textArray1;
            num = 0;
            goto Label_01BA;
        Label_0041:
            *(&(this.mCurrentColor[num])) = new Color(0f, 0f, 0f);
            *(&(this.mStartColor[num])) = new Color(0f, 0f, 0f);
            *(&(this.mEndColor[num])) = new Color(0f, 0f, 0f);
            typeArray1 = new Type[] { typeof(Canvas), typeof(RawImage) };
            obj2 = new GameObject(array.GetValue(num).ToString(), typeArray1);
            this.mCanvas[num] = obj2.GetComponent<Canvas>();
            this.mCanvas[num].set_sortingOrder(0x270f - num);
            this.mCanvas[num].set_renderMode(0);
            this.mCanvas[num].set_enabled(0);
            this.mImage[num] = obj2.GetComponent<RawImage>();
            this.mImage[num].set_color(*(&(this.mEndColor[num])));
            if (string.IsNullOrEmpty(strArray[num]) != null)
            {
                goto Label_01A0;
            }
            shader = Shader.Find(strArray[num]);
            if ((shader != null) == null)
            {
                goto Label_01A0;
            }
            this.mImage[num].set_material(new Material(shader));
            this.mImage[num].get_material().SetColor("_Color", Color.get_white());
        Label_01A0:
            obj2.get_transform().SetParent(base.get_gameObject().get_transform());
            num += 1;
        Label_01BA:
            if (num < 3)
            {
                goto Label_0041;
            }
            return;
        }

        public void BeginSceneFade(Color dest, float time, TacticsUnitController[] excludes, TacticsUnitController[] includes)
        {
            this.mSceneFadeStart = CameraHook.ColorMod;
            this.mSceneFadeEnd = dest;
            this.mSceneFadeDuration = time;
            this.mSceneFadeTime = 0f;
            this.mSceneFadeExcluders = excludes;
            this.mSceneFadeIncluders = includes;
            if (this.mSceneFadeDuration > 0f)
            {
                goto Label_004A;
            }
            this.ApplySceneFade(dest);
        Label_004A:
            return;
        }

        public unsafe void FadeTo(Color dest, float time, int layer)
        {
            if (this.mInitialized[layer] != null)
            {
                goto Label_006E;
            }
            *(&(this.mCurrentColor[layer])) = dest;
            &(this.mCurrentColor[layer]).a = 1f - &(this.mCurrentColor[layer]).a;
            this.mInitialized[layer] = 1;
            this.mImage[layer].set_color(*(&(this.mCurrentColor[layer])));
        Label_006E:
            if (time <= 0f)
            {
                goto Label_00D6;
            }
            *(&(this.mStartColor[layer])) = *(&(this.mCurrentColor[layer]));
            *(&(this.mEndColor[layer])) = dest;
            this.mCurrentTime[layer] = 0f;
            this.mDuration[layer] = time;
            this.mCanvas[layer].set_enabled(1);
            goto Label_0145;
        Label_00D6:
            *(&(this.mCurrentColor[layer])) = dest;
            this.mCurrentTime[layer] = 0f;
            this.mDuration[layer] = 0f;
            this.mImage[layer].set_color(*(&(this.mCurrentColor[layer])));
            this.mCanvas[layer].set_enabled(&(this.mCurrentColor[layer]).a > 0f);
        Label_0145:
            return;
        }

        public bool IsFading(int layer)
        {
            return (this.mCurrentTime[layer] < this.mDuration[layer]);
        }

        public unsafe bool IsScreenFaded(int layer)
        {
            Color color;
            return ((&this.mImage[layer].get_color().a < 1f) == 0);
        }

        public void ResetSceneFade(float time)
        {
            this.mSceneFadeEnd = Color.get_white();
            this.mSceneFadeStart = CameraHook.ColorMod;
            this.mSceneFadeDuration = time;
            this.mSceneFadeTime = 0f;
            if (this.mSceneFadeDuration > 0f)
            {
                goto Label_0044;
            }
            this.ApplySceneFade(this.mSceneFadeEnd);
        Label_0044:
            return;
        }

        private unsafe void Update()
        {
            int num;
            float num2;
            this.UpdateSceneFade();
            num = 0;
            goto Label_00E9;
        Label_000D:
            if (this.mCurrentTime[num] < this.mDuration[num])
            {
                goto Label_0062;
            }
            if (&(this.mCurrentColor[num]).a > 0f)
            {
                goto Label_00E5;
            }
            if (this.mCanvas[num].get_enabled() == null)
            {
                goto Label_00E5;
            }
            this.mCanvas[num].set_enabled(0);
            goto Label_00E5;
        Label_0062:
            *((float*) &(this.mCurrentTime[num])) += Time.get_unscaledDeltaTime();
            num2 = Mathf.Clamp01(this.mCurrentTime[num] / this.mDuration[num]);
            *(&(this.mCurrentColor[num])) = Color.Lerp(*(&(this.mStartColor[num])), *(&(this.mEndColor[num])), num2);
            this.mImage[num].set_color(*(&(this.mCurrentColor[num])));
        Label_00E5:
            num += 1;
        Label_00E9:
            if (num < 3)
            {
                goto Label_000D;
            }
            return;
        }

        private void UpdateSceneFade()
        {
            float num;
            Color color;
            if (this.mSceneFadeTime < this.mSceneFadeDuration)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mSceneFadeTime += Time.get_deltaTime();
            num = Mathf.Clamp01(this.mSceneFadeTime / this.mSceneFadeDuration);
            color = Color.Lerp(this.mSceneFadeStart, this.mSceneFadeEnd, num);
            this.ApplySceneFade(color);
            return;
        }

        public static bool InstanceExists
        {
            get
            {
                return (mInstance != null);
            }
        }

        public static FadeController Instance
        {
            get
            {
                Type[] typeArray1;
                GameObject obj2;
                if ((mInstance == null) == null)
                {
                    goto Label_0039;
                }
                typeArray1 = new Type[] { typeof(FadeController) };
                obj2 = new GameObject("FadeController", typeArray1);
                mInstance = obj2.GetComponent<FadeController>();
            Label_0039:
                return mInstance;
            }
        }

        public bool IsSceneFading
        {
            get
            {
                return (this.mSceneFadeTime < this.mSceneFadeDuration);
            }
        }

        public enum LayerType
        {
            Normal,
            Add,
            AlphaBlend
        }
    }
}

