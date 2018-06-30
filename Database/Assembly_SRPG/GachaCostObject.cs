namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaCostObject : MonoBehaviour
    {
        private GameObject m_root;
        private GameObject m_ticket;
        private GameObject m_default;
        private GameObject m_default_bg;
        private Transform m_CostNum;
        private Transform m_CostFree;

        public GachaCostObject()
        {
            base..ctor();
            return;
        }

        public void Refresh()
        {
            this.UpdateCostData();
            return;
        }

        private unsafe bool RefreshCostNum(GameObject _root, int _cost)
        {
            int num;
            int num2;
            int num3;
            string str;
            Transform transform;
            int num4;
            int num5;
            float num6;
            if ((this.m_CostNum == null) == null)
            {
                goto Label_0027;
            }
            this.m_CostNum = _root.get_transform().FindChild("num");
        Label_0027:
            if ((this.m_CostFree == null) == null)
            {
                goto Label_004E;
            }
            this.m_CostFree = _root.get_transform().FindChild("num_free");
        Label_004E:
            if (((this.m_CostNum == null) == null) && ((this.m_CostFree == null) == null))
            {
                goto Label_007C;
            }
            DebugUtility.LogError("消費コストを表示するオブジェクトが存在しません");
            return 0;
        Label_007C:
            this.m_CostNum.get_gameObject().SetActive(0);
            this.m_CostFree.get_gameObject().SetActive(0);
            if (_cost > 0)
            {
                goto Label_00B8;
            }
            this.m_CostFree.get_gameObject().SetActive(1);
            return 0;
        Label_00B8:
            num = ((int) Math.Log10((double) ((_cost <= 0) ? 1 : _cost))) + 1;
            num2 = _cost;
            num3 = 7;
            goto Label_015D;
        Label_00D9:
            str = "num/value_" + &Mathf.Pow(10f, (float) (num3 - 1)).ToString();
            transform = _root.get_transform().FindChild(str);
            if (num >= num3)
            {
                goto Label_0122;
            }
            transform.get_gameObject().SetActive(0);
            goto Label_0159;
        Label_0122:
            num4 = (int) Mathf.Pow(10f, (float) (num3 - 1));
            num5 = num2 / num4;
            transform.get_gameObject().SetActive(1);
            transform.GetComponent<ImageArray>().ImageIndex = num5;
            num2 = num2 % num4;
        Label_0159:
            num3 -= 1;
        Label_015D:
            if (num3 > 0)
            {
                goto Label_00D9;
            }
            this.m_CostNum.get_gameObject().SetActive(1);
            return 1;
        }

        private void UpdateCostData()
        {
            Button button;
            GachaRequestParam param;
            ItemData data;
            ImageArray array;
            if ((this.m_root.GetComponent<Button>() == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            this.m_default.SetActive(0);
            this.m_ticket.SetActive(0);
            param = DataSource.FindDataOfClass<GachaRequestParam>(this.m_root, null);
            if (param != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            if (param.IsTicketGacha == null)
            {
                goto Label_0095;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.Ticket);
            if (data != null)
            {
                goto Label_006D;
            }
            return;
        Label_006D:
            DataSource.Bind<ItemData>(this.m_ticket, data);
            this.m_ticket.SetActive(1);
            GameParameter.UpdateAll(this.m_ticket);
            goto Label_00D8;
        Label_0095:
            this.m_default_bg.GetComponent<ImageArray>().ImageIndex = (param.IsGold == null) ? 1 : 0;
            this.RefreshCostNum(this.m_default_bg, param.Cost);
            this.m_default.SetActive(1);
        Label_00D8:
            return;
        }

        public GameObject RootObject
        {
            get
            {
                return this.m_root;
            }
            set
            {
                this.m_root = value;
                return;
            }
        }

        public GameObject TicketObject
        {
            get
            {
                return this.m_ticket;
            }
            set
            {
                this.m_ticket = value;
                return;
            }
        }

        public GameObject DefaultObject
        {
            get
            {
                return this.m_default;
            }
            set
            {
                this.m_default = value;
                return;
            }
        }

        public GameObject DefaultBGObject
        {
            get
            {
                return this.m_default_bg;
            }
            set
            {
                this.m_default_bg = value;
                return;
            }
        }
    }
}

