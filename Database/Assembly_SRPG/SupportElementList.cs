namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class SupportElementList : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_SupportUnits;

        public SupportElementList()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        public void Clear()
        {
            int num;
            if (this.m_SupportUnits != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("m_SupportUnitsがnullです。");
            return;
        Label_0016:
            num = 0;
            goto Label_0029;
        Label_001D:
            this.Refresh(num, null);
            num += 1;
        Label_0029:
            if (num < ((int) this.m_SupportUnits.Length))
            {
                goto Label_001D;
            }
            return;
        }

        public void Refresh(UnitData[] units)
        {
            int num;
            if (this.m_SupportUnits != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("m_SupportUnitsがnullです。");
            return;
        Label_0016:
            if (units != null)
            {
                goto Label_0027;
            }
            DebugUtility.LogError("unitsがnullです。");
            return;
        Label_0027:
            if (((int) this.m_SupportUnits.Length) >= Enum.GetValues(typeof(EElement)).Length)
            {
                goto Label_0053;
            }
            DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
            return;
        Label_0053:
            if (((int) units.Length) >= ((int) this.m_SupportUnits.Length))
            {
                goto Label_006E;
            }
            DebugUtility.LogError("unitsの数が足りません。");
            return;
        Label_006E:
            num = 0;
            goto Label_0083;
        Label_0075:
            this.Refresh(num, units[num]);
            num += 1;
        Label_0083:
            if (num < ((int) units.Length))
            {
                goto Label_0075;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void Refresh(long[] uniqs)
        {
            int num;
            if (this.m_SupportUnits != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("m_SupportUnitsがnullです。");
            return;
        Label_0016:
            if (uniqs != null)
            {
                goto Label_0027;
            }
            DebugUtility.LogError("unitsがnullです。");
            return;
        Label_0027:
            if (((int) this.m_SupportUnits.Length) >= Enum.GetValues(typeof(EElement)).Length)
            {
                goto Label_0053;
            }
            DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
            return;
        Label_0053:
            if (((int) uniqs.Length) >= ((int) this.m_SupportUnits.Length))
            {
                goto Label_006E;
            }
            DebugUtility.LogError("unitsの数が足りません。");
            return;
        Label_006E:
            num = 0;
            goto Label_0092;
        Label_0075:
            this.Refresh(num, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqs[num]));
            num += 1;
        Label_0092:
            if (num < ((int) uniqs.Length))
            {
                goto Label_0075;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void Refresh(int element, UnitData unit)
        {
            GameObject obj2;
            SerializeValueBehaviour behaviour;
            DataSource source;
            if (this.m_SupportUnits != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("m_SupportUnitsがnullです。");
            return;
        Label_0016:
            if (((int) this.m_SupportUnits.Length) >= Enum.GetValues(typeof(EElement)).Length)
            {
                goto Label_0042;
            }
            DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
            return;
        Label_0042:
            if (element < ((int) this.m_SupportUnits.Length))
            {
                goto Label_005B;
            }
            DebugUtility.LogError("unitsの数が足りません。");
            return;
        Label_005B:
            if ((this.m_SupportUnits[element] != null) == null)
            {
                goto Label_0120;
            }
            obj2 = this.m_SupportUnits[element].get_gameObject();
            if ((obj2 != null) == null)
            {
                goto Label_0120;
            }
            behaviour = obj2.GetComponent<SerializeValueBehaviour>();
            source = DataSource.Create(obj2);
            if ((source != null) == null)
            {
                goto Label_0120;
            }
            source.Clear();
            if (unit == null)
            {
                goto Label_00EF;
            }
            source.Add(typeof(UnitData), unit);
            if ((behaviour != null) == null)
            {
                goto Label_011A;
            }
            behaviour.list.SetInteractable("btn", 1);
            behaviour.list.SetActive(1, 1);
            goto Label_011A;
        Label_00EF:
            if ((behaviour != null) == null)
            {
                goto Label_011A;
            }
            behaviour.list.SetInteractable("btn", 0);
            behaviour.list.SetActive(1, 0);
        Label_011A:
            GameParameter.UpdateAll(obj2);
        Label_0120:
            return;
        }
    }
}

