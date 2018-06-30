namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class TobiraStatusCheckWindow : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_TobiraStatusListItemRoot;
        [SerializeField]
        private TobiraStatusListItem m_TobiraStatusListItemTemplate;
        [CompilerGenerated]
        private static Func<TobiraParam, bool> <>f__am$cache2;

        public TobiraStatusCheckWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Setup>m__41E(TobiraParam tobiraParam)
        {
            return ((tobiraParam.TobiraCategory == 0) == 0);
        }

        private TobiraStatusListItem CreateListItem()
        {
            GameObject obj2;
            TobiraStatusListItem item;
            obj2 = Object.Instantiate<GameObject>(this.m_TobiraStatusListItemTemplate.get_gameObject());
            item = obj2.GetComponent<TobiraStatusListItem>();
            obj2.get_transform().SetParent(this.m_TobiraStatusListItemRoot, 0);
            obj2.SetActive(1);
            return item;
        }

        private void Setup(UnitData unit_data)
        {
            TobiraParam[] paramArray;
            int num;
            TobiraData data;
            TobiraStatusListItem item;
            TobiraData data2;
            if ((this.m_TobiraStatusListItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (unit_data != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            this.m_TobiraStatusListItemTemplate.get_gameObject().SetActive(0);
            if (<>f__am$cache2 != null)
            {
                goto Label_005E;
            }
            <>f__am$cache2 = new Func<TobiraParam, bool>(TobiraStatusCheckWindow.<Setup>m__41E);
        Label_005E:
            paramArray = Enumerable.ToArray<TobiraParam>(Enumerable.Where<TobiraParam>(MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unit_data.UnitParam.iname), <>f__am$cache2));
            num = 0;
            goto Label_010B;
        Label_0075:
            data = unit_data.GetTobiraData(paramArray[num].TobiraCategory);
            item = this.CreateListItem();
            if (data == null)
            {
                goto Label_00A2;
            }
            item.SetTobiraLvIsMax(data.IsMaxLv);
            goto Label_00A9;
        Label_00A2:
            item.SetTobiraLvIsMax(0);
        Label_00A9:
            if (paramArray[num].Enable != null)
            {
                goto Label_00C4;
            }
            item.Setup(paramArray[num]);
            goto Label_0107;
        Label_00C4:
            data2 = new TobiraData();
            data2.Setup(unit_data.UnitParam.iname, paramArray[num].TobiraCategory, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap);
            item.Setup(data2);
        Label_0107:
            num += 1;
        Label_010B:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0075;
            }
            return;
        }

        private void Start()
        {
            UnitData data;
            data = MonoSingleton<GameManager>.Instance.Player.GetUnitData(UnitTobiraInventory.InitTimeUniqueID);
            this.Setup(data);
            return;
        }
    }
}

