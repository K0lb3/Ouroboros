namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class ProgressWindow : MonoBehaviour
    {
        private static ProgressWindow mInstance;
        public Animator WindowAnimator;
        public Slider ProgressBar;
        public ProgressRatio Ratios;
        public string CloseTrigger;
        public float DestroyDelay;
        public Text Title;
        public Text Lore;
        public Text Percentage;
        public string PercentageFormat;
        public Text Complete;
        public string CompleteFormat;
        public ImageArray Phase;
        public GameObject notice0;
        public GameObject notice1;
        public string ImageTable;
        public RawImage[] Images;
        public float DisplayImageThreshold;
        public GameObject ImageGroup;
        public float MinVisibleTime;
        private float mLoadTime;
        private float mLoadProgress;
        private long mKeepTotalDownloadSize;
        private long mKeepCurrentDownloadSize;
        private int mCurrentImageIndex;
        private List<KeyValuePair<string, string>> mImagePairs;

        public ProgressWindow()
        {
            this.Ratios = new ProgressRatio(1f, 0f);
            this.CloseTrigger = "done";
            this.DestroyDelay = 1f;
            this.PercentageFormat = "{0:0}%";
            this.CompleteFormat = "{0:0}/{1:0}";
            this.DisplayImageThreshold = 2f;
            this.mKeepTotalDownloadSize = -1L;
            this.mKeepCurrentDownloadSize = -1L;
            this.mCurrentImageIndex = -1;
            this.mImagePairs = new List<KeyValuePair<string, string>>();
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AnimationThread()
        {
            <AnimationThread>c__Iterator12E iteratore;
            iteratore = new <AnimationThread>c__Iterator12E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        public static void Close()
        {
            Animator animator;
            if ((mInstance != null) == null)
            {
                goto Label_008E;
            }
            animator = ((mInstance.WindowAnimator != null) == null) ? mInstance.GetComponent<Animator>() : mInstance.WindowAnimator;
            if ((animator != null) == null)
            {
                goto Label_005B;
            }
            animator.SetTrigger(mInstance.CloseTrigger);
        Label_005B:
            if (mInstance.DestroyDelay < 0f)
            {
                goto Label_0088;
            }
            Object.Destroy(mInstance.get_gameObject(), mInstance.DestroyDelay);
        Label_0088:
            mInstance = null;
        Label_008E:
            return;
        }

        private void LoadImageTable()
        {
            char[] chArray1;
            TextAsset asset;
            StringReader reader;
            string str;
            string[] strArray;
            asset = Resources.Load<TextAsset>(this.ImageTable);
            if ((asset == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            reader = new StringReader(asset.get_text());
            goto Label_005D;
        Label_002A:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_005D;
            }
            chArray1 = new char[] { 9 };
            strArray = str.Split(chArray1);
            this.mImagePairs.Add(new KeyValuePair<string, string>(strArray[0], strArray[1]));
        Label_005D:
            if ((str = reader.ReadLine()) != null)
            {
                goto Label_002A;
            }
            return;
        }

        private void OnDisable()
        {
            if ((mInstance == this) == null)
            {
                goto Label_0016;
            }
            mInstance = null;
        Label_0016:
            return;
        }

        private void OnEnable()
        {
            if ((mInstance == null) == null)
            {
                goto Label_0016;
            }
            mInstance = this;
        Label_0016:
            return;
        }

        public static void OpenGenericDownloadWindow()
        {
            GameObject obj2;
            Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/AssetsDownloading"));
            return;
        }

        public static void OpenQuestLoadScreen(QuestParam quest)
        {
            string str;
            string str2;
            str = null;
            str2 = null;
            if (quest == null)
            {
                goto Label_0050;
            }
            str = quest.name;
            if (quest.type != 7)
            {
                goto Label_0034;
            }
            str = quest.title + " " + quest.name;
        Label_0034:
            if (string.IsNullOrEmpty(quest.storyTextID) != null)
            {
                goto Label_0050;
            }
            str2 = LocalizedText.Get(quest.storyTextID);
        Label_0050:
            OpenQuestLoadScreen(str, str2);
            return;
        }

        public static void OpenQuestLoadScreen(string title, string lore)
        {
            ProgressWindow window;
            ProgressWindow window2;
            if ((mInstance == null) == null)
            {
                goto Label_007E;
            }
            window = null;
            if (MonoSingleton<GameManager>.Instance.IsVersusMode() == null)
            {
                goto Label_004B;
            }
            if (GlobalVars.IsVersusDraftMode != null)
            {
                goto Label_003B;
            }
            window = AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_VS");
            goto Label_0046;
        Label_003B:
            window = AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_Draft");
        Label_0046:
            goto Label_0056;
        Label_004B:
            window = AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen");
        Label_0056:
            if ((window != null) == null)
            {
                goto Label_007E;
            }
            Object.DontDestroyOnLoad(Object.Instantiate<ProgressWindow>(window).get_gameObject());
            GameUtility.FadeIn(0.1f);
        Label_007E:
            if (string.IsNullOrEmpty(title) == null)
            {
                goto Label_0090;
            }
            title = string.Empty;
        Label_0090:
            if (string.IsNullOrEmpty(lore) == null)
            {
                goto Label_00A2;
            }
            lore = string.Empty;
        Label_00A2:
            SetTexts(title, lore);
            return;
        }

        public static void OpenRankMatchLoadScreen()
        {
            ProgressWindow window;
            ProgressWindow window2;
            if ((mInstance == null) == null)
            {
                goto Label_0045;
            }
            window = null;
            window = AssetManager.Load<ProgressWindow>("UI/HomeRankMatch_Matching");
            if ((window != null) == null)
            {
                goto Label_0045;
            }
            Object.DontDestroyOnLoad(Object.Instantiate<ProgressWindow>(window).get_gameObject());
            GameUtility.FadeIn(0.1f);
        Label_0045:
            return;
        }

        public static void OpenVersusDraftLoadScreen()
        {
            ProgressWindow window;
            ProgressWindow window2;
            if ((mInstance == null) == null)
            {
                goto Label_0045;
            }
            window = null;
            window = AssetManager.Load<ProgressWindow>("UI/HomeVersus_DraftMatching");
            if ((window != null) == null)
            {
                goto Label_0045;
            }
            Object.DontDestroyOnLoad(Object.Instantiate<ProgressWindow>(window).get_gameObject());
            GameUtility.FadeIn(0.1f);
        Label_0045:
            return;
        }

        public static void OpenVersusLoadScreen()
        {
            ProgressWindow window;
            ProgressWindow window2;
            if ((mInstance == null) == null)
            {
                goto Label_0045;
            }
            window = null;
            window = AssetManager.Load<ProgressWindow>("UI/HomeMultiPlay_VersusMatching");
            if ((window != null) == null)
            {
                goto Label_0045;
            }
            Object.DontDestroyOnLoad(Object.Instantiate<ProgressWindow>(window).get_gameObject());
            GameUtility.FadeIn(0.1f);
        Label_0045:
            return;
        }

        public static void SetDestroyDelay(float delay)
        {
            if ((mInstance != null) == null)
            {
                goto Label_001B;
            }
            mInstance.DestroyDelay = delay;
        Label_001B:
            return;
        }

        public static void SetLoadProgress(float t)
        {
            if ((mInstance != null) == null)
            {
                goto Label_001B;
            }
            mInstance.mLoadProgress = t;
        Label_001B:
            return;
        }

        public static void SetTexts(string title, string lore)
        {
            if ((mInstance != null) == null)
            {
                goto Label_005A;
            }
            if ((mInstance.Title != null) == null)
            {
                goto Label_0035;
            }
            mInstance.Title.set_text(title);
        Label_0035:
            if ((mInstance.Lore != null) == null)
            {
                goto Label_005A;
            }
            mInstance.Lore.set_text(lore);
        Label_005A:
            return;
        }

        private void Start()
        {
            int num;
            if (this.Images == null)
            {
                goto Label_006E;
            }
            num = 0;
            goto Label_0060;
        Label_0012:
            if ((this.Images[num] != null) == null)
            {
                goto Label_005C;
            }
            if ((this.Images[num].get_material() != null) == null)
            {
                goto Label_005C;
            }
            this.Images[num].set_material(new Material(this.Images[num].get_material()));
        Label_005C:
            num += 1;
        Label_0060:
            if (num < ((int) this.Images.Length))
            {
                goto Label_0012;
            }
        Label_006E:
            if (string.IsNullOrEmpty(this.ImageTable) != null)
            {
                goto Label_0091;
            }
            this.LoadImageTable();
            base.StartCoroutine(this.AnimationThread());
        Label_0091:
            if ((this.ImageGroup != null) == null)
            {
                goto Label_00AE;
            }
            this.ImageGroup.SetActive(0);
        Label_00AE:
            return;
        }

        private unsafe void Update()
        {
            float num;
            long num2;
            long num3;
            int num4;
            string str;
            string str2;
            num = 0f;
            this.mLoadTime += Time.get_unscaledDeltaTime();
            if (&this.Ratios.Download <= 0f)
            {
                goto Label_0041;
            }
            num += AssetDownloader.Progress / &this.Ratios.Download;
        Label_0041:
            if (&this.Ratios.Deserilize <= 0f)
            {
                goto Label_006B;
            }
            num += this.mLoadProgress / &this.Ratios.Deserilize;
        Label_006B:
            num /= &this.Ratios.Download + &this.Ratios.Deserilize;
            this.ProgressBar.set_value(num);
            num2 = AssetDownloader.TotalDownloadSize;
            num3 = AssetDownloader.CurrentDownloadSize;
            num4 = AssetDownloader.Phase;
            if ((this.Phase != null) == null)
            {
                goto Label_00C0;
            }
            this.Phase.ImageIndex = num4;
        Label_00C0:
            if ((this.notice0 != null) == null)
            {
                goto Label_00F5;
            }
            if (num4 != 1)
            {
                goto Label_00E9;
            }
            this.notice0.SetActive(1);
            goto Label_00F5;
        Label_00E9:
            this.notice0.SetActive(0);
        Label_00F5:
            if ((this.notice1 != null) == null)
            {
                goto Label_012A;
            }
            if (num4 != 1)
            {
                goto Label_011E;
            }
            this.notice1.SetActive(1);
            goto Label_012A;
        Label_011E:
            this.notice1.SetActive(0);
        Label_012A:
            if ((this.Percentage != null) == null)
            {
                goto Label_0179;
            }
            str = string.Format(this.PercentageFormat, (int) ((int) (num * 100f)));
            if ((this.Percentage.get_text() != str) == null)
            {
                goto Label_0179;
            }
            this.Percentage.set_text(str);
        Label_0179:
            if ((this.Complete != null) == null)
            {
                goto Label_01F5;
            }
            if (this.mKeepTotalDownloadSize == num2)
            {
                goto Label_01A5;
            }
            this.mKeepTotalDownloadSize = num2;
            this.mKeepCurrentDownloadSize = -1L;
        Label_01A5:
            if (this.mKeepCurrentDownloadSize >= num3)
            {
                goto Label_01F5;
            }
            this.mKeepCurrentDownloadSize = num3;
            str2 = string.Format(this.CompleteFormat, (long) num3, (long) num2);
            if ((this.Complete.get_text() != str2) == null)
            {
                goto Label_01F5;
            }
            this.Complete.set_text(str2);
        Label_01F5:
            return;
        }

        public static bool ShouldKeepVisible
        {
            get
            {
                if ((mInstance != null) == null)
                {
                    goto Label_0027;
                }
                return (mInstance.mLoadTime < mInstance.MinVisibleTime);
            Label_0027:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <AnimationThread>c__Iterator12E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <r>__0;
            internal ResourceRequest <req>__1;
            internal Animation <anim>__2;
            internal int $PC;
            internal object $current;
            internal ProgressWindow <>f__this;

            public <AnimationThread>c__Iterator12E()
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
                uint num;
                KeyValuePair<string, string> pair;
                KeyValuePair<string, string> pair2;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_002D;

                    case 1:
                        goto Label_004F;

                    case 2:
                        goto Label_0127;

                    case 3:
                        goto Label_019A;

                    case 4:
                        goto Label_0207;
                }
                goto Label_024A;
            Label_002D:
                this.$current = new WaitForSeconds(this.<>f__this.DisplayImageThreshold);
                this.$PC = 1;
                goto Label_024C;
            Label_004F:
                goto Label_0217;
            Label_0054:
                this.<r>__0 = Random.Range(0, this.<>f__this.mImagePairs.Count - 1);
                if (this.<>f__this.mImagePairs.Count < 2)
                {
                    goto Label_009E;
                }
                if (this.<>f__this.mCurrentImageIndex == this.<r>__0)
                {
                    goto Label_0054;
                }
            Label_009E:
                this.<>f__this.mCurrentImageIndex = this.<r>__0;
                if (0 > this.<r>__0)
                {
                    goto Label_01BC;
                }
                if (this.<r>__0 >= this.<>f__this.mImagePairs.Count)
                {
                    goto Label_01BC;
                }
                pair = this.<>f__this.mImagePairs[this.<r>__0];
                this.<req>__1 = Resources.LoadAsync<Texture2D>(&pair.Key);
                if (this.<req>__1.get_isDone() != null)
                {
                    goto Label_0127;
                }
                this.$current = this.<req>__1;
                this.$PC = 2;
                goto Label_024C;
            Label_0127:
                this.<>f__this.Images[0].set_texture(this.<req>__1.get_asset() as Texture);
                pair2 = this.<>f__this.mImagePairs[this.<r>__0];
                this.<req>__1 = Resources.LoadAsync<Texture2D>(&pair2.Value);
                if (this.<req>__1.get_isDone() != null)
                {
                    goto Label_019A;
                }
                this.$current = this.<req>__1;
                this.$PC = 3;
                goto Label_024C;
            Label_019A:
                this.<>f__this.Images[1].set_texture(this.<req>__1.get_asset() as Texture);
            Label_01BC:
                this.<>f__this.ImageGroup.SetActive(1);
                this.<anim>__2 = this.<>f__this.ImageGroup.GetComponent<Animation>();
                this.<anim>__2.Play();
                goto Label_0207;
            Label_01F4:
                this.$current = null;
                this.$PC = 4;
                goto Label_024C;
            Label_0207:
                if (this.<anim>__2.get_isPlaying() != null)
                {
                    goto Label_01F4;
                }
            Label_0217:
                if ((this.<>f__this.ImageGroup != null) == null)
                {
                    goto Label_0243;
                }
                if (this.<>f__this.mImagePairs.Count > 0)
                {
                    goto Label_0054;
                }
            Label_0243:
                this.$PC = -1;
            Label_024A:
                return 0;
            Label_024C:
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

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct ProgressRatio
        {
            [Range(0f, 1f)]
            public float Download;
            [Range(0f, 1f)]
            public float Deserilize;
            public ProgressRatio(float a, float b)
            {
                this.Download = a;
                this.Deserilize = b;
                return;
            }
        }
    }
}

