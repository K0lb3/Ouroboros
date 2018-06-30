namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class StreamingMovie : MonoSingleton<StreamingMovie>
    {
        private const string PREFAB_PATH = "UI/FullScreenMovie";
        private OnFinished onFinished;
        private bool skip;
        private Result playing_movie_info;
        private bool m_NotReplay;

        public StreamingMovie()
        {
            base..ctor();
            return;
        }

        private void Finish()
        {
            Exception exception;
            if (this.onFinished == null)
            {
                goto Label_0032;
            }
        Label_000B:
            try
            {
                this.onFinished(this.m_NotReplay);
                goto Label_0032;
            }
            catch (Exception exception1)
            {
            Label_0021:
                exception = exception1;
                Debug.Log(exception.ToString());
                goto Label_0032;
            }
        Label_0032:
            this.playing_movie_info = null;
            return;
        }

        protected override void Initialize()
        {
            base.Initialize();
            return;
        }

        private string MakePlatformMoviePath(string fileName)
        {
            object[] objArray1;
            string str;
            str = "{0}{1}{2}{3}";
            objArray1 = new object[] { this.URL, fileName, this.PlatformMovieSuffix, this.Extention };
            return string.Format(str, objArray1);
        }

        public bool Play(string fileName, OnFinished callback, string prefabPath)
        {
            string str;
            if (IsPlaying == null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            this.onFinished = callback;
            this.m_NotReplay = 0;
            str = this.MakePlatformMoviePath(fileName);
            DebugUtility.Log("Play streaming movie " + str);
            base.StartCoroutine(this.PlayInternal(str, prefabPath));
            return 1;
        }

        private static Result PlayFullScreenMovie(string url, string prefabPath)
        {
            GameObject obj2;
            GameObject obj3;
            FlowNode_MediaPlayerController controller;
            FlowNode_MediaPlayerDispatchFinishEvent event2;
            obj2 = AssetManager.Load<GameObject>((string.IsNullOrEmpty(prefabPath) == null) ? prefabPath : "UI/FullScreenMovie");
            obj3 = null;
            if ((obj2 != null) == null)
            {
                goto Label_0031;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
        Label_0031:
            controller = null;
            event2 = null;
            if ((obj3 != null) == null)
            {
                goto Label_004F;
            }
            controller = obj3.GetComponent<FlowNode_MediaPlayerController>();
            event2 = obj3.GetComponent<FlowNode_MediaPlayerDispatchFinishEvent>();
        Label_004F:
            if ((controller == null) == null)
            {
                goto Label_0065;
            }
            DebugUtility.LogError("FlowNode_MediaPlayerControllerが見つかりませんでした");
        Label_0065:
            if ((event2 == null) == null)
            {
                goto Label_007B;
            }
            DebugUtility.LogError("FlowNode_MediaPlayerNotifyFinishが見つかりませんでした");
        Label_007B:
            controller.SetVolume(GameUtility.Config_MusicVolume);
            controller.Load(url);
            return new Result(event2);
        }

        [DebuggerHidden]
        private IEnumerator PlayInternal(string url, string prefabPath)
        {
            <PlayInternal>c__Iterator85 iterator;
            iterator = new <PlayInternal>c__Iterator85();
            iterator.url = url;
            iterator.prefabPath = prefabPath;
            iterator.<$>url = url;
            iterator.<$>prefabPath = prefabPath;
            iterator.<>f__this = this;
            return iterator;
        }

        protected override void Release()
        {
            base.Release();
            return;
        }

        public void Skip()
        {
            this.skip = 1;
            return;
        }

        private string URL
        {
            get
            {
                return AssetDownloader.ExDownloadURL;
            }
        }

        private string PlatformMovieSuffix
        {
            get
            {
                return "_win";
            }
        }

        private string Extention
        {
            get
            {
                return ".mp4";
            }
        }

        public bool IsNotReplay
        {
            get
            {
                return this.m_NotReplay;
            }
            set
            {
                this.m_NotReplay = value;
                return;
            }
        }

        public static bool IsPlaying
        {
            get
            {
                if ((MonoSingleton<StreamingMovie>.Instance == null) == null)
                {
                    goto Label_0012;
                }
                return 0;
            Label_0012:
                if (MonoSingleton<StreamingMovie>.Instance.playing_movie_info != null)
                {
                    goto Label_0023;
                }
                return 0;
            Label_0023:
                if (MonoSingleton<StreamingMovie>.Instance.playing_movie_info.isEnd == null)
                {
                    goto Label_0039;
                }
                return 0;
            Label_0039:
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PlayInternal>c__Iterator85 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string url;
            internal string prefabPath;
            internal int $PC;
            internal object $current;
            internal string <$>url;
            internal string <$>prefabPath;
            internal StreamingMovie <>f__this;

            public <PlayInternal>c__Iterator85()
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
                        goto Label_0029;

                    case 1:
                        goto Label_004C;

                    case 2:
                        goto Label_009C;

                    case 3:
                        goto Label_00C8;
                }
                goto Label_00DA;
            Label_0029:
                this.<>f__this.set_enabled(1);
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_00DC;
            Label_004C:
                this.<>f__this.playing_movie_info = StreamingMovie.PlayFullScreenMovie(this.url, this.prefabPath);
                goto Label_009C;
            Label_006D:
                if (this.<>f__this.skip == null)
                {
                    goto Label_0089;
                }
                this.<>f__this.skip = 0;
            Label_0089:
                this.$current = null;
                this.$PC = 2;
                goto Label_00DC;
            Label_009C:
                if (this.<>f__this.playing_movie_info.isEnd == null)
                {
                    goto Label_006D;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_00DC;
            Label_00C8:
                this.<>f__this.Finish();
                this.$PC = -1;
            Label_00DA:
                return 0;
            Label_00DC:
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

        public delegate void OnFinished(bool is_replay);

        private class Result
        {
            public bool isEnd;

            public Result(FlowNode_MediaPlayerDispatchFinishEvent finishEventDispatcher)
            {
                base..ctor();
                finishEventDispatcher.onEnd = (SRPG.FlowNode_MediaPlayerDispatchFinishEvent.OnEnd) Delegate.Combine(finishEventDispatcher.onEnd, new SRPG.FlowNode_MediaPlayerDispatchFinishEvent.OnEnd(this.OnEnd));
                return;
            }

            private void OnEnd()
            {
                this.isEnd = 1;
                return;
            }
        }
    }
}

