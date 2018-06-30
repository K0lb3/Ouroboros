namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class Location
    {
        private const float TIMEOUT = 60f;
        private Result m_Result;
        private float m_Latitude;
        private float m_Longitude;
        private IEnumerator m_Task;
        private Action<Location> m_Success;
        private Action<Location> m_Failed;

        public Location()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator Coroutine_UpdateLocation()
        {
            <Coroutine_UpdateLocation>c__Iterator7B iteratorb;
            iteratorb = new <Coroutine_UpdateLocation>c__Iterator7B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        public void End()
        {
            float num;
            if (this.m_Task == null)
            {
                goto Label_008F;
            }
            this.OnEnd();
            if (this.m_Result != 2)
            {
                goto Label_0039;
            }
            if (this.m_Success == null)
            {
                goto Label_007A;
            }
            this.m_Success(this);
            goto Label_007A;
        Label_0039:
            if (this.m_Result != 1)
            {
                goto Label_0063;
            }
            Input.get_location().Stop();
            this.m_Latitude = this.m_Longitude = 0f;
        Label_0063:
            if (this.m_Failed == null)
            {
                goto Label_007A;
            }
            this.m_Failed(this);
        Label_007A:
            this.m_Success = null;
            this.m_Failed = null;
            this.m_Task = null;
        Label_008F:
            return;
        }

        public void Initialize()
        {
            this.m_Result = 0;
            this.m_Latitude = 0f;
            this.m_Longitude = 0f;
            this.m_Success = null;
            this.m_Failed = null;
            this.m_Task = null;
            return;
        }

        public bool IsBusy()
        {
            return ((this.m_Task == null) == 0);
        }

        private void OnEnd()
        {
        }

        private void OnStart()
        {
        }

        public void Release()
        {
            this.End();
            return;
        }

        public void Start()
        {
            this.Start(null, null);
            return;
        }

        public void Start(Action<Location> success, Action<Location> failed)
        {
            float num;
            if (this.m_Task != null)
            {
                goto Label_003F;
            }
            this.m_Success = success;
            this.m_Failed = failed;
            this.m_Latitude = this.m_Longitude = 0f;
            this.m_Task = this.Coroutine_UpdateLocation();
            this.OnStart();
        Label_003F:
            return;
        }

        public void Update()
        {
            if (this.m_Task != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.m_Task.MoveNext() != null)
            {
                goto Label_0022;
            }
            this.End();
        Label_0022:
            return;
        }

        public Vector2 location
        {
            get
            {
                return new Vector2(this.m_Latitude, this.m_Longitude);
            }
        }

        public float latitude
        {
            get
            {
                return this.m_Latitude;
            }
        }

        public float longitude
        {
            get
            {
                return this.m_Longitude;
            }
        }

        public Result result
        {
            get
            {
                return this.m_Result;
            }
        }

        public static bool isGPSEnable
        {
            get
            {
                return Input.get_location().get_isEnabledByUser();
            }
        }

        [CompilerGenerated]
        private sealed class <Coroutine_UpdateLocation>c__Iterator7B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <elapse>__0;
            internal int $PC;
            internal object $current;
            internal Location <>f__this;

            public <Coroutine_UpdateLocation>c__Iterator7B()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                object[] objArray1;
                uint num;
                LocationInfo info;
                LocationInfo info2;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0090;
                }
                goto Label_01B6;
            Label_0021:
                this.<elapse>__0 = 0f;
                DebugMenu.Log("Location", "Location Start");
                this.<>f__this.m_Result = 1;
                Input.get_location().Start();
                goto Label_0090;
            Label_0056:
                this.<elapse>__0 += Time.get_deltaTime();
                if (this.<elapse>__0 < 60f)
                {
                    goto Label_007D;
                }
                goto Label_00A0;
            Label_007D:
                this.$current = null;
                this.$PC = 1;
                goto Label_01B8;
            Label_0090:
                if (Input.get_location().get_status() == 1)
                {
                    goto Label_0056;
                }
            Label_00A0:
                if (this.<elapse>__0 < 60f)
                {
                    goto Label_00DA;
                }
                DebugMenu.LogWarning("Location", "Location GPS Initialize Timeout");
                Input.get_location().Stop();
                this.<>f__this.m_Result = 3;
                goto Label_01B6;
            Label_00DA:
                if (Input.get_location().get_status() != 3)
                {
                    goto Label_0114;
                }
                DebugMenu.LogWarning("Location", "Location Device GPS Unabled");
                Input.get_location().Stop();
                this.<>f__this.m_Result = 4;
                goto Label_01B6;
            Label_0114:
                this.<>f__this.m_Latitude = &Input.get_location().get_lastData().get_latitude();
                this.<>f__this.m_Longitude = &Input.get_location().get_lastData().get_longitude();
                Input.get_location().Stop();
                this.<>f__this.m_Result = 2;
                objArray1 = new object[] { "Location  Success > x=", (float) this.<>f__this.m_Latitude, ", y=", (float) this.<>f__this.m_Longitude };
                DebugMenu.Log("Location", string.Concat(objArray1));
                this.$PC = -1;
            Label_01B6:
                return 0;
            Label_01B8:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public enum Result
        {
            None,
            Working,
            Success,
            Timeout,
            DeviceUnable
        }
    }
}

