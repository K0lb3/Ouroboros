namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class AppealItemQuest : AppealItemBase
    {
        private readonly string SPRITES_PATH;
        private readonly string MASTER_PATH;
        [SerializeField]
        private Image AppealObject1;
        [SerializeField]
        private CanvasGroup AppealGroup0;
        [SerializeField]
        private CanvasGroup AppealGroup1;
        private string[] mAppealIds;
        private int mCurrentIndex;
        private bool IsLoaded;
        protected Dictionary<string, Sprite> mCacheAppealSprites;
        private readonly float WAIT_SWAP_APPEAL;
        private float mWaitSwapeAppealTime;
        private Sprite mCurrentSprite;
        private Sprite mNextSprite;
        private bool IsUpdated;

        public AppealItemQuest()
        {
            this.SPRITES_PATH = "AppealSprites/quest";
            this.MASTER_PATH = "Data/appeal/AppealQuest";
            this.mCacheAppealSprites = new Dictionary<string, Sprite>();
            this.WAIT_SWAP_APPEAL = 5f;
            this.IsUpdated = 1;
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if ((this.AppealObject1 != null) == null)
            {
                goto Label_0028;
            }
            this.AppealObject1.get_gameObject().SetActive(0);
        Label_0028:
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
            JSON_AppealQuestMaster[] masterArray;
            long num;
            List<string> list;
            JSON_AppealQuestMaster master;
            JSON_AppealQuestMaster[] masterArray2;
            int num2;
            AppealQuestMaster master2;
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
                masterArray = JSONParser.parseJSONArray<JSON_AppealQuestMaster>(str);
                if (masterArray != null)
                {
                    goto Label_0034;
                }
                throw new InvalidJSONException();
            Label_0034:
                num = Network.GetServerTime();
                list = new List<string>();
                masterArray2 = masterArray;
                num2 = 0;
                goto Label_0094;
            Label_004B:
                master = masterArray2[num2];
                master2 = new AppealQuestMaster();
                if (master2.Deserialize(master) == null)
                {
                    goto Label_008E;
                }
                if (master2.start_at > num)
                {
                    goto Label_008E;
                }
                if (master2.end_at <= num)
                {
                    goto Label_008E;
                }
                list.Add(master2.appeal_id);
            Label_008E:
                num2 += 1;
            Label_0094:
                if (num2 < ((int) masterArray2.Length))
                {
                    goto Label_004B;
                }
                if (list == null)
                {
                    goto Label_00CE;
                }
                if (list.Count <= 0)
                {
                    goto Label_00CE;
                }
                this.mAppealIds = new string[list.Count];
                this.mAppealIds = list.ToArray();
            Label_00CE:
                goto Label_00E9;
            }
            catch (Exception exception1)
            {
            Label_00D3:
                exception = exception1;
                DebugUtility.LogException(exception);
                flag = 0;
                goto Label_00EB;
            }
        Label_00E9:
            return 1;
        Label_00EB:
            return flag;
        }

        [DebuggerHidden]
        private IEnumerator LoadAppealResources()
        {
            <LoadAppealResources>c__IteratorE2 re;
            re = new <LoadAppealResources>c__IteratorE2();
            re.<>f__this = this;
            return re;
        }

        protected override void Refresh()
        {
            if ((((int) this.mAppealIds.Length) <= this.mCurrentIndex) || (this.mCacheAppealSprites.ContainsKey(this.mAppealIds[this.mCurrentIndex]) == null))
            {
                goto Label_0053;
            }
            this.mCurrentSprite = this.mCacheAppealSprites[this.mAppealIds[this.mCurrentIndex]];
            goto Label_006C;
        Label_0053:
            this.mCurrentSprite = this.mCacheAppealSprites[this.mAppealIds[0]];
        Label_006C:
            if ((((int) this.mAppealIds.Length) <= (this.mCurrentIndex + 1)) || (this.mCacheAppealSprites.ContainsKey(this.mAppealIds[this.mCurrentIndex + 1]) == null))
            {
                goto Label_00C5;
            }
            this.mNextSprite = this.mCacheAppealSprites[this.mAppealIds[this.mCurrentIndex + 1]];
            goto Label_00F2;
        Label_00C5:
            this.mNextSprite = (((int) this.mAppealIds.Length) != 1) ? this.mCacheAppealSprites[this.mAppealIds[0]] : null;
        Label_00F2:
            if ((base.AppealObject != null) == null)
            {
                goto Label_0151;
            }
            base.AppealObject.set_sprite(this.mCurrentSprite);
            base.AppealObject.get_gameObject().SetActive(this.mCurrentSprite != null);
            if ((this.AppealGroup0 != null) == null)
            {
                goto Label_0151;
            }
            this.AppealGroup0.set_alpha(1f);
        Label_0151:
            if ((this.AppealObject1 != null) == null)
            {
                goto Label_01C1;
            }
            if ((this.mNextSprite != null) == null)
            {
                goto Label_01C1;
            }
            this.AppealObject1.set_sprite(this.mNextSprite);
            this.AppealObject1.get_gameObject().SetActive(this.mNextSprite != null);
            if ((this.AppealGroup1 != null) == null)
            {
                goto Label_01C1;
            }
            this.AppealGroup1.set_alpha(0f);
        Label_01C1:
            this.mCurrentIndex += 1;
            if (this.mCurrentIndex < ((int) this.mAppealIds.Length))
            {
                goto Label_01E9;
            }
            this.mCurrentIndex = 0;
        Label_01E9:
            this.IsUpdated = this.mNextSprite == null;
            return;
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
                goto Label_002D;
            }
            if (this.IsUpdated == null)
            {
                goto Label_0027;
            }
            this.Refresh();
            goto Label_002D;
        Label_0027:
            this.UpdateAppeal();
        Label_002D:
            return;
        }

        private void UpdateAppeal()
        {
            float num;
            if ((this.mCurrentSprite == null) != null)
            {
                goto Label_0022;
            }
            if ((this.mNextSprite == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            this.mWaitSwapeAppealTime -= Time.get_deltaTime();
            if (this.mWaitSwapeAppealTime >= 0f)
            {
                goto Label_0120;
            }
            if ((this.AppealGroup0 != null) == null)
            {
                goto Label_0067;
            }
            if ((this.AppealGroup1 != null) != null)
            {
                goto Label_0068;
            }
        Label_0067:
            return;
        Label_0068:
            num = Time.get_deltaTime();
            this.AppealGroup0.set_alpha(this.AppealGroup0.get_alpha() + -num);
            this.AppealGroup0.set_alpha(Mathf.Clamp(this.AppealGroup0.get_alpha() - num, 0f, 1f));
            this.AppealGroup1.set_alpha(this.AppealGroup1.get_alpha() + num);
            this.AppealGroup1.set_alpha(Mathf.Clamp(this.AppealGroup1.get_alpha() + num, 0f, 1f));
            if (this.AppealGroup1.get_alpha() <= 0f)
            {
                goto Label_010D;
            }
            if (this.AppealGroup1.get_alpha() < 1f)
            {
                goto Label_0120;
            }
        Label_010D:
            this.IsUpdated = 1;
            this.mWaitSwapeAppealTime = this.WAIT_SWAP_APPEAL;
        Label_0120:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadAppealResources>c__IteratorE2 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <resources>__0;
            internal AppealSprites <sprites>__1;
            internal int <i>__2;
            internal Sprite <sprite>__3;
            internal int $PC;
            internal object $current;
            internal AppealItemQuest <>f__this;

            public <LoadAppealResources>c__IteratorE2()
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
                        goto Label_006D;

                    case 2:
                        goto Label_0164;
                }
                goto Label_016B;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.SPRITES_PATH) != null)
                {
                    goto Label_0151;
                }
                this.<resources>__0 = AssetManager.LoadAsync<AppealSprites>(this.<>f__this.SPRITES_PATH);
                this.$current = this.<resources>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_016D;
            Label_006D:
                this.<sprites>__1 = this.<resources>__0.asset as AppealSprites;
                if ((this.<sprites>__1 != null) == null)
                {
                    goto Label_0151;
                }
                if (this.<>f__this.mAppealIds == null)
                {
                    goto Label_0151;
                }
                if (((int) this.<>f__this.mAppealIds.Length) <= 0)
                {
                    goto Label_0151;
                }
                this.<i>__2 = 0;
                goto Label_012D;
            Label_00C3:
                this.<sprite>__3 = this.<sprites>__1.GetSprite(this.<>f__this.mAppealIds[this.<i>__2]);
                if ((this.<sprite>__3 != null) == null)
                {
                    goto Label_011F;
                }
                this.<>f__this.mCacheAppealSprites.Add(this.<>f__this.mAppealIds[this.<i>__2], this.<sprite>__3);
            Label_011F:
                this.<i>__2 += 1;
            Label_012D:
                if (this.<i>__2 < ((int) this.<>f__this.mAppealIds.Length))
                {
                    goto Label_00C3;
                }
                this.<>f__this.IsLoaded = 1;
            Label_0151:
                this.$current = null;
                this.$PC = 2;
                goto Label_016D;
            Label_0164:
                this.$PC = -1;
            Label_016B:
                return 0;
            Label_016D:
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

