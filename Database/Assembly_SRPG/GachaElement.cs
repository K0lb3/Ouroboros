namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaElement : MonoBehaviour
    {
        public GachaTypes GachaType;
        public GameObject GachaButtonParent;
        public GameObject FreeGacha;
        public GameObject SingularGacha;
        public GameObject MultipleGacha;
        public Button BtnFreeGacha;
        public Button BtnSingularGacha;
        public Button BtnMultipleGacha;
        [CompilerGenerated]
        private static Predicate<GachaParam> <>f__am$cache8;
        [CompilerGenerated]
        private static Predicate<GachaParam> <>f__am$cache9;

        public GachaElement()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Reflesh>m__324(GachaParam p)
        {
            return (p.num == 1);
        }

        [CompilerGenerated]
        private static bool <Reflesh>m__325(GachaParam p)
        {
            return (p.num > 1);
        }

        private void Awake()
        {
            if ((this.GachaButtonParent != null) == null)
            {
                goto Label_001D;
            }
            this.GachaButtonParent.SetActive(0);
        Label_001D:
            return;
        }

        public void Reflesh()
        {
            bool flag;
            List<GachaParam> list;
            GachaParam param;
            GachaParam param2;
            GachaTypes types;
            flag = 0;
            list = null;
            types = this.GachaType;
            if (types == null)
            {
                goto Label_0020;
            }
            if (types == 1)
            {
                goto Label_0035;
            }
            goto Label_004A;
        Label_0020:
            list = MonoSingleton<GameManager>.Instance.GetGachaList("gold");
            goto Label_004F;
        Label_0035:
            list = MonoSingleton<GameManager>.Instance.GetGachaList("coin");
            goto Label_004F;
        Label_004A:;
        Label_004F:
            if (list == null)
            {
                goto Label_0060;
            }
            if (list.Count != null)
            {
                goto Label_0061;
            }
        Label_0060:
            return;
        Label_0061:
            if (<>f__am$cache8 != null)
            {
                goto Label_007A;
            }
            <>f__am$cache8 = new Predicate<GachaParam>(GachaElement.<Reflesh>m__324);
        Label_007A:
            param = list.Find(<>f__am$cache8);
            if (param == null)
            {
                goto Label_00C9;
            }
            if ((this.FreeGacha != null) == null)
            {
                goto Label_00AA;
            }
            DataSource.Bind<GachaParam>(this.FreeGacha, param);
            flag = 1;
        Label_00AA:
            if ((this.SingularGacha != null) == null)
            {
                goto Label_00C9;
            }
            DataSource.Bind<GachaParam>(this.SingularGacha, param);
            flag = 1;
        Label_00C9:
            if (<>f__am$cache9 != null)
            {
                goto Label_00E2;
            }
            <>f__am$cache9 = new Predicate<GachaParam>(GachaElement.<Reflesh>m__325);
        Label_00E2:
            param2 = list.Find(<>f__am$cache9);
            if (param2 == null)
            {
                goto Label_0112;
            }
            if ((this.MultipleGacha != null) == null)
            {
                goto Label_0112;
            }
            DataSource.Bind<GachaParam>(this.MultipleGacha, param2);
            flag = 1;
        Label_0112:
            if ((this.GachaButtonParent != null) == null)
            {
                goto Label_012F;
            }
            this.GachaButtonParent.SetActive(flag);
        Label_012F:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

