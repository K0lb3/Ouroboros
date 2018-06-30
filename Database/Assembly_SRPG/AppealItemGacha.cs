namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class AppealItemGacha : AppealItemBase
    {
        private readonly string SPRITES_PATH;
        private readonly string MASTER_PATH;
        private string[] mAppealIds;
        private string mAppealId;
        private bool IsLoaded;
        protected Dictionary<string, Sprite> mCacheAppealSprites;
        [SerializeField]
        private GameObject Ballon;
        private bool IsNew;

        public AppealItemGacha()
        {
            this.SPRITES_PATH = "AppealSprites/gacha";
            this.MASTER_PATH = "Data/appeal/AppealGacha";
            this.mCacheAppealSprites = new Dictionary<string, Sprite>();
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if ((this.Ballon != null) == null)
            {
                goto Label_0023;
            }
            this.Ballon.SetActive(0);
        Label_0023:
            return;
        }

        protected override unsafe void Destroy()
        {
            string str;
            Dictionary<string, Sprite>.KeyCollection.Enumerator enumerator;
            base.Destroy();
            enumerator = this.mCacheAppealSprites.Keys.GetEnumerator();
        Label_0017:
            try
            {
                goto Label_0035;
            Label_001C:
                str = &enumerator.Current;
                Resources.UnloadAsset(this.mCacheAppealSprites[str]);
            Label_0035:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001C;
                }
                goto Label_0052;
            }
            finally
            {
            Label_0046:
                ((Dictionary<string, Sprite>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0052:
            this.mCacheAppealSprites = null;
            return;
        }

        private bool LoadAppealMaster(string path)
        {
            string str;
            JSON_AppealGachaMaster[] masterArray;
            long num;
            string str2;
            JSON_AppealGachaMaster master;
            JSON_AppealGachaMaster[] masterArray2;
            int num2;
            AppealGachaMaster master2;
            Exception exception;
            bool flag;
            if (string.IsNullOrEmpty(path) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            str = AssetManager.LoadTextData(path);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            try
            {
                masterArray = JSONParser.parseJSONArray<JSON_AppealGachaMaster>(str);
                if (masterArray != null)
                {
                    goto Label_0034;
                }
                throw new InvalidJSONException();
            Label_0034:
                num = Network.GetServerTime();
                str2 = string.Empty;
                masterArray2 = masterArray;
                num2 = 0;
                goto Label_00A1;
            Label_004B:
                master = masterArray2[num2];
                master2 = new AppealGachaMaster();
                if (master2.Deserialize(master) == null)
                {
                    goto Label_009B;
                }
                if (master2.start_at > num)
                {
                    goto Label_009B;
                }
                if (master2.end_at <= num)
                {
                    goto Label_009B;
                }
                str2 = master2.appeal_id;
                this.IsNew = master2.is_new;
                goto Label_00AC;
            Label_009B:
                num2 += 1;
            Label_00A1:
                if (num2 < ((int) masterArray2.Length))
                {
                    goto Label_004B;
                }
            Label_00AC:
                if (string.IsNullOrEmpty(str2) != null)
                {
                    goto Label_00BE;
                }
                this.mAppealId = str2;
            Label_00BE:
                goto Label_00D9;
            }
            catch (Exception exception1)
            {
            Label_00C3:
                exception = exception1;
                DebugUtility.LogException(exception);
                flag = 0;
                goto Label_00DB;
            }
        Label_00D9:
            return 1;
        Label_00DB:
            return flag;
        }

        [DebuggerHidden]
        private IEnumerator LoadAppealResources()
        {
            <LoadAppealResources>c__IteratorE0 re;
            re = new <LoadAppealResources>c__IteratorE0();
            re.<>f__this = this;
            return re;
        }

        protected override void Start()
        {
            base.Start();
            if (this.LoadAppealMaster(this.MASTER_PATH) == null)
            {
                goto Label_0024;
            }
            base.StartCoroutine(this.LoadAppealResources());
        Label_0024:
            return;
        }

        protected override void Update()
        {
            base.Update();
            if (this.IsLoaded == null)
            {
                goto Label_009A;
            }
            if ((base.AppealSprite == null) == null)
            {
                goto Label_009A;
            }
            if (this.mCacheAppealSprites.ContainsKey(this.mAppealId) != null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            base.AppealSprite = this.mCacheAppealSprites[this.mAppealId];
            if ((this.Ballon != null) == null)
            {
                goto Label_0094;
            }
            if ((base.AppealSprite == null) == null)
            {
                goto Label_0083;
            }
            this.Ballon.SetActive(0);
            goto Label_0094;
        Label_0083:
            this.Ballon.SetActive(this.IsNew);
        Label_0094:
            this.Refresh();
        Label_009A:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadAppealResources>c__IteratorE0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal AppealSprites <sprites>__0;
            internal LoadRequest <resources>__1;
            internal Sprite <sprite>__2;
            internal int $PC;
            internal object $current;
            internal AppealItemGacha <>f__this;

            public <LoadAppealResources>c__IteratorE0()
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

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0074;

                    case 2:
                        goto Label_0133;
                }
                goto Label_013A;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.SPRITES_PATH) != null)
                {
                    goto Label_0120;
                }
                this.<sprites>__0 = null;
                this.<resources>__1 = AssetManager.LoadAsync<AppealSprites>(this.<>f__this.SPRITES_PATH);
                this.$current = this.<resources>__1.StartCoroutine();
                this.$PC = 1;
                goto Label_013C;
            Label_0074:
                if ((this.<resources>__1.asset != null) == null)
                {
                    goto Label_00A0;
                }
                this.<sprites>__0 = this.<resources>__1.asset as AppealSprites;
            Label_00A0:
                if ((this.<sprites>__0 != null) == null)
                {
                    goto Label_0120;
                }
                if (string.IsNullOrEmpty(this.<>f__this.mAppealId) != null)
                {
                    goto Label_0120;
                }
                this.<sprite>__2 = this.<sprites>__0.GetSprite(this.<>f__this.mAppealId);
                if ((this.<sprite>__2 != null) == null)
                {
                    goto Label_0114;
                }
                this.<>f__this.mCacheAppealSprites.Add(this.<>f__this.mAppealId, this.<sprite>__2);
            Label_0114:
                this.<>f__this.IsLoaded = 1;
            Label_0120:
                this.$current = null;
                this.$PC = 2;
                goto Label_013C;
            Label_0133:
                this.$PC = -1;
            Label_013A:
                return 0;
            Label_013C:
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
    }
}

