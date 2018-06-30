namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class BadStatusEffects
    {
        public static List<Desc> Effects;
        public static GameObject CurseEffect;
        public static string CurseEffectAttachTarget;

        [DebuggerHidden]
        public static IEnumerator LoadEffects(QuestAssets assets)
        {
            <LoadEffects>c__Iterator67 iterator;
            iterator = new <LoadEffects>c__Iterator67();
            iterator.assets = assets;
            iterator.<$>assets = assets;
            return iterator;
        }

        public static void UnloadEffects()
        {
            Effects = null;
            CurseEffect = null;
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadEffects>c__Iterator67 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal TextAsset <src>__0;
            internal StringReader <reader>__1;
            internal string <line>__2;
            internal string[] <columns>__3;
            internal int <colID>__4;
            internal int <colColor>__5;
            internal int <colAnimation>__6;
            internal int <colEffect>__7;
            internal int <colAttachTarget>__8;
            internal EUnitCondition <key>__9;
            internal Exception <e>__10;
            internal BadStatusEffects.Desc <desc>__11;
            internal LoadRequest <req>__12;
            internal QuestAssets assets;
            internal int $PC;
            internal object $current;
            internal QuestAssets <$>assets;

            public <LoadEffects>c__Iterator67()
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
                object[] objArray1;
                char[] chArray2;
                char[] chArray1;
                uint num;
                Exception exception;
                string str;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0270;

                    case 2:
                        goto Label_0359;
                }
                goto Label_0360;
            Label_0025:
                this.<src>__0 = Resources.Load<TextAsset>("e/Data/badstatus");
                this.<reader>__1 = new StringReader(this.<src>__0.get_text());
                this.<line>__2 = this.<reader>__1.ReadLine();
                chArray1 = new char[] { 9 };
                this.<columns>__3 = this.<line>__2.Split(chArray1);
                this.<colID>__4 = Array.IndexOf<string>(this.<columns>__3, "ID");
                this.<colColor>__5 = Array.IndexOf<string>(this.<columns>__3, "COLOR");
                this.<colAnimation>__6 = Array.IndexOf<string>(this.<columns>__3, "ANIMATION");
                this.<colEffect>__7 = Array.IndexOf<string>(this.<columns>__3, "EFFECT");
                this.<colAttachTarget>__8 = Array.IndexOf<string>(this.<columns>__3, "TARGET");
                BadStatusEffects.Effects = new List<BadStatusEffects.Desc>();
                goto Label_02FC;
            Label_00F5:
                if (string.IsNullOrEmpty(this.<line>__2) == null)
                {
                    goto Label_010A;
                }
                goto Label_02FC;
            Label_010A:
                chArray2 = new char[] { 9 };
                this.<columns>__3 = this.<line>__2.Split(chArray2);
            Label_0126:
                try
                {
                    this.<key>__9 = (long) Enum.Parse(typeof(EUnitCondition), this.<columns>__3[this.<colID>__4], 1);
                    goto Label_0170;
                }
                catch (Exception exception1)
                {
                Label_0153:
                    exception = exception1;
                    this.<e>__10 = exception;
                    DebugUtility.LogException(this.<e>__10);
                    goto Label_02FC;
                }
            Label_0170:
                this.<desc>__11 = new BadStatusEffects.Desc();
                this.<desc>__11.Key = this.<key>__9;
                this.<desc>__11.AnimationName = this.<columns>__3[this.<colAnimation>__6];
                this.<desc>__11.AttachTarget = this.<columns>__3[this.<colAttachTarget>__8];
                if (string.IsNullOrEmpty(this.<columns>__3[this.<colColor>__5]) != null)
                {
                    goto Label_01FA;
                }
                this.<desc>__11.BlendColor = GameUtility.ParseColor(this.<columns>__3[this.<colColor>__5]);
                goto Label_020A;
            Label_01FA:
                this.<desc>__11.BlendColor = Color.get_clear();
            Label_020A:
                if (string.IsNullOrEmpty(this.<columns>__3[this.<colEffect>__7]) != null)
                {
                    goto Label_02EC;
                }
                this.<req>__12 = AssetManager.LoadAsync<GameObject>("Effects/" + this.<columns>__3[this.<colEffect>__7]);
                if (this.<req>__12.isDone != null)
                {
                    goto Label_0270;
                }
                this.$current = this.<req>__12.StartCoroutine();
                this.$PC = 1;
                goto Label_0362;
            Label_0270:
                if ((this.<req>__12.asset == null) == null)
                {
                    goto Label_02D1;
                }
                objArray1 = new object[] { "状態異常[", (EUnitCondition) this.<desc>__11.Key, "]のエフェクト[", this.<columns>__3[this.<colEffect>__7], "]を読み込めませんでした。" };
                DebugUtility.LogError(string.Concat(objArray1));
            Label_02D1:
                this.<desc>__11.Effect = this.<req>__12.asset as GameObject;
            Label_02EC:
                BadStatusEffects.Effects.Add(this.<desc>__11);
            Label_02FC:
                if ((this.<line>__2 = this.<reader>__1.ReadLine()) != null)
                {
                    goto Label_00F5;
                }
                if ((this.assets != null) == null)
                {
                    goto Label_0346;
                }
                BadStatusEffects.CurseEffect = this.assets.CurseEffect;
                BadStatusEffects.CurseEffectAttachTarget = this.assets.CurseEffectAttachTarget;
            Label_0346:
                this.$current = null;
                this.$PC = 2;
                goto Label_0362;
            Label_0359:
                this.$PC = -1;
            Label_0360:
                return 0;
            Label_0362:
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

        public class Desc
        {
            public EUnitCondition Key;
            public GameObject Effect;
            public string AttachTarget;
            public Color BlendColor;
            public string AnimationName;

            public Desc()
            {
                base..ctor();
                return;
            }
        }
    }
}

