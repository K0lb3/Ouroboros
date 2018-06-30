namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "更新", 0, 0)]
    public class BeginnerQuestIcon : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private bool ShowOnlyBeginnerPeriod;
        [SerializeField]
        private GameObject Badge;
        [SerializeField]
        private GameObject Emmision;
        [CompilerGenerated]
        private static Func<TipsParam, bool> <>f__am$cache3;
        [CompilerGenerated]
        private static Func<TipsParam, string> <>f__am$cache4;
        [CompilerGenerated]
        private static Func<QuestParam, bool> <>f__am$cache5;
        [CompilerGenerated]
        private static Func<QuestParam, bool> <>f__am$cache6;

        public BeginnerQuestIcon()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__298(TipsParam t)
        {
            return (t.hide == 0);
        }

        [CompilerGenerated]
        private static string <Refresh>m__299(TipsParam t)
        {
            return t.iname;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__29A(QuestParam q)
        {
            return ((q.type != 13) ? 0 : q.IsDateUnlock(-1L));
        }

        [CompilerGenerated]
        private static bool <Refresh>m__29B(QuestParam q)
        {
            return ((q.state == 2) == 0);
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void Refresh()
        {
            IEnumerable<string> enumerable;
            List<string> list;
            bool flag;
            bool flag2;
            if (this.ShowOnlyBeginnerPeriod == null)
            {
                goto Label_0025;
            }
            base.get_gameObject().SetActive(MonoSingleton<GameManager>.Instance.Player.IsBeginner());
        Label_0025:
            if (<>f__am$cache3 != null)
            {
                goto Label_004C;
            }
            <>f__am$cache3 = new Func<TipsParam, bool>(BeginnerQuestIcon.<Refresh>m__298);
        Label_004C:
            if (<>f__am$cache4 != null)
            {
                goto Label_006E;
            }
            <>f__am$cache4 = new Func<TipsParam, string>(BeginnerQuestIcon.<Refresh>m__299);
        Label_006E:
            enumerable = Enumerable.Select<TipsParam, string>(Enumerable.Where<TipsParam>(MonoSingleton<GameManager>.Instance.MasterParam.Tips, <>f__am$cache3), <>f__am$cache4);
            list = MonoSingleton<GameManager>.Instance.Tips;
            flag = Enumerable.Any<string>(Enumerable.Except<string>(enumerable, list));
            if ((this.Badge != null) == null)
            {
                goto Label_00AE;
            }
            this.Badge.SetActive(flag);
        Label_00AE:
            if ((this.Emmision != null) == null)
            {
                goto Label_0128;
            }
            if (<>f__am$cache5 != null)
            {
                goto Label_00E6;
            }
            <>f__am$cache5 = new Func<QuestParam, bool>(BeginnerQuestIcon.<Refresh>m__29A);
        Label_00E6:
            if (<>f__am$cache6 != null)
            {
                goto Label_0108;
            }
            <>f__am$cache6 = new Func<QuestParam, bool>(BeginnerQuestIcon.<Refresh>m__29B);
        Label_0108:
            flag2 = Enumerable.Any<QuestParam>(Enumerable.Where<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, <>f__am$cache5), <>f__am$cache6);
            this.Emmision.SetActive((flag != null) ? 1 : flag2);
        Label_0128:
            return;
        }
    }
}

