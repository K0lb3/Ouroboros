namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [AddComponentMenu("Scripts/SRPG/Manager/Time")]
    public class TimeManager : MonoSingleton<TimeManager>
    {
        private static readonly DateTime UNIX_EPOCH;
        public static long UTC2LOCAL;
        public static readonly int DEFAULT_FRAME_RATE;
        public static readonly float FPS;
        public static readonly string ISO_8601_FORMAT;
        private float mDeltaTime;
        private float mFixedDeltaTime;
        private float mUnscaledDeltaTime;
        private float mUnscaledFixedDeltaTime;
        private float mTimeScale;
        private float mReqTimeScale;
        private float mHitStop;
        private float[] mTimeScales;

        static TimeManager()
        {
            UNIX_EPOCH = new DateTime(0x7b2, 1, 1, 0, 0, 0, 1);
            UTC2LOCAL = 0x7e90L;
            DEFAULT_FRAME_RATE = 30;
            FPS = 1f / ((float) DEFAULT_FRAME_RATE);
            ISO_8601_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffZ";
            return;
        }

        public TimeManager()
        {
            float[] singleArray1;
            this.mTimeScale = 1f;
            singleArray1 = new float[] { 1f, 1f, 1f };
            this.mTimeScales = singleArray1;
            base..ctor();
            return;
        }

        public static unsafe long FromDateTime(DateTime datetime)
        {
            TimeSpan span;
            span = datetime - UNIX_EPOCH;
            return (((long) &span.TotalSeconds) - UTC2LOCAL);
        }

        public static unsafe DateTime FromUnixTime(long unixtime)
        {
            DateTime time;
            return &UNIX_EPOCH.AddSeconds((double) (unixtime + UTC2LOCAL));
        }

        public static unsafe long GetUnixSec(DateTime targetTime)
        {
            TimeSpan span;
            targetTime = &targetTime.ToUniversalTime();
            span = targetTime - UNIX_EPOCH;
            return (long) &span.TotalSeconds;
        }

        public static void HitStop(float sec)
        {
            MonoSingleton<TimeManager>.Instance.mHitStop = sec;
            return;
        }

        protected override void Initialize()
        {
            if (GameUtility.IsDebugBuild != null)
            {
                goto Label_0014;
            }
            Application.set_targetFrameRate(DEFAULT_FRAME_RATE);
        Label_0014:
            Object.DontDestroyOnLoad(this);
            return;
        }

        public static long Now()
        {
            return FromDateTime(DateTime.UtcNow);
        }

        public static void SetTimeScale(TimeScaleGroups group, float value)
        {
            MonoSingleton<TimeManager>.Instance.mTimeScales[group] = value;
            return;
        }

        public static void StartHitSlow(float rate, float sec)
        {
            SetTimeScale(1, rate);
            MonoSingleton<TimeManager>.Instance.mHitStop = sec;
            return;
        }

        public static unsafe string ToMinSecString(long time)
        {
            string str;
            string str2;
            long num;
            long num2;
            num = time / 60L;
            str = &num.ToString();
            num2 = time % 60L;
            str2 = &num2.ToString();
            str = str.PadLeft(2, 0x30);
            str2 = str2.PadLeft(2, 0x30);
            return (str + ":" + str2);
        }

        private void Update()
        {
            float num;
            int num2;
            float num3;
            if (this.mReqTimeScale == 0f)
            {
                goto Label_0027;
            }
            this.mTimeScale = this.mReqTimeScale;
            this.mReqTimeScale = 0f;
        Label_0027:
            this.mFixedDeltaTime = this.mUnscaledFixedDeltaTime = FPS;
            this.mFixedDeltaTime *= this.mTimeScale;
            this.mDeltaTime = this.mUnscaledDeltaTime = Time.get_deltaTime();
            this.mDeltaTime *= this.mTimeScale;
            if (this.mHitStop <= 0f)
            {
                goto Label_00BB;
            }
            this.mHitStop -= Time.get_unscaledDeltaTime() * this.mTimeScales[0];
            if (this.mHitStop > 0f)
            {
                goto Label_00BB;
            }
            SetTimeScale(1, 1f);
        Label_00BB:
            num = this.mTimeScales[0];
            num2 = 1;
            goto Label_00DA;
        Label_00CB:
            num *= this.mTimeScales[num2];
            num2 += 1;
        Label_00DA:
            if (num2 < ((int) this.mTimeScales.Length))
            {
                goto Label_00CB;
            }
            Time.set_timeScale(num);
            return;
        }

        public static int FrameRate
        {
            get
            {
                return Application.get_targetFrameRate();
            }
        }

        public static float RealTimeSinceStartup
        {
            get
            {
                return Time.get_realtimeSinceStartup();
            }
        }

        public static float DeltaTime
        {
            get
            {
                return MonoSingleton<TimeManager>.Instance.mDeltaTime;
            }
        }

        public static float UnscaledDeltaTime
        {
            get
            {
                return MonoSingleton<TimeManager>.Instance.mUnscaledDeltaTime;
            }
        }

        public static float FixedDeltaTime
        {
            get
            {
                return MonoSingleton<TimeManager>.Instance.mFixedDeltaTime;
            }
        }

        public static float UnscaledFixedDeltaTime
        {
            get
            {
                return MonoSingleton<TimeManager>.Instance.mUnscaledFixedDeltaTime;
            }
        }

        public static float TimeScale
        {
            get
            {
                return MonoSingleton<TimeManager>.Instance.mTimeScale;
            }
            set
            {
                MonoSingleton<TimeManager>.Instance.mReqTimeScale = value;
                return;
            }
        }

        public static DateTime ServerTime
        {
            get
            {
                return FromUnixTime(Network.GetServerTime());
            }
        }

        public enum TimeScaleGroups
        {
            Game,
            HitStop,
            Debug
        }
    }
}

