namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class StatusList : MonoBehaviour
    {
        public StatusListItem ListItem;
        public bool ShowSign;
        private List<StatusListItem> mItems;
        private Color mDefaultValueColor;
        private Color mDefaultBonusColor;

        public StatusList()
        {
            this.mItems = new List<StatusListItem>();
            base..ctor();
            return;
        }

        private unsafe void AddValue(int index, string type, int value, int bonus, bool multiply, bool isSecret, bool use_bonus_color)
        {
            StatusListItem item;
            StatusListItem item2;
            string str;
            string str2;
            if (this.mItems.Count > index)
            {
                goto Label_003B;
            }
            item = Object.Instantiate<StatusListItem>(this.ListItem);
            item.get_transform().SetParent(base.get_transform(), 0);
            this.mItems.Add(item);
        Label_003B:
            item2 = this.mItems[index];
            item2.get_gameObject().SetActive(1);
            if ((item2.Value != null) == null)
            {
                goto Label_00A0;
            }
            item2.Value.get_gameObject().SetActive(0);
            item2.Value.set_color(((use_bonus_color == null) || (bonus == null)) ? this.mDefaultValueColor : this.mDefaultBonusColor);
        Label_00A0:
            if ((item2.Bonus != null) == null)
            {
                goto Label_00C2;
            }
            item2.Bonus.get_gameObject().SetActive(0);
        Label_00C2:
            if ((item2.Label != null) == null)
            {
                goto Label_00EE;
            }
            item2.Label.set_text(LocalizedText.Get("sys." + type));
        Label_00EE:
            if ((item2.Value != null) == null)
            {
                goto Label_0166;
            }
            str = (isSecret == null) ? &value.ToString() : "???";
            if (this.ShowSign == null)
            {
                goto Label_0136;
            }
            if (value <= 0)
            {
                goto Label_0136;
            }
            str = "+" + str;
        Label_0136:
            if (multiply == null)
            {
                goto Label_0149;
            }
            str = str + "%";
        Label_0149:
            item2.Value.set_text(str);
            item2.Value.get_gameObject().SetActive(1);
        Label_0166:
            if ((item2.Bonus != null) == null)
            {
                goto Label_01DC;
            }
            if (bonus == null)
            {
                goto Label_01DC;
            }
            if (use_bonus_color != null)
            {
                goto Label_01DC;
            }
            str2 = &bonus.ToString();
            if (this.ShowSign == null)
            {
                goto Label_01AC;
            }
            if (bonus <= 0)
            {
                goto Label_01AC;
            }
            str2 = "+" + str2;
        Label_01AC:
            if (multiply == null)
            {
                goto Label_01BF;
            }
            str2 = str2 + "%";
        Label_01BF:
            item2.Bonus.set_text(str2);
            item2.Bonus.get_gameObject().SetActive(1);
        Label_01DC:
            return;
        }

        private unsafe void AddValue_TotalAndBonus(int index, string type, int main_value, int bonus_value, bool is_def_main, bool is_def_bonus, bool multiply)
        {
            Color color;
            Color color2;
            StatusListItem item;
            StatusListItem item2;
            Text text;
            Text text2;
            string str;
            string str2;
            color = (is_def_main == null) ? this.mDefaultValueColor : this.mDefaultBonusColor;
            color2 = (is_def_main == null) ? this.mDefaultBonusColor : this.mDefaultBonusColor;
            if (this.mItems.Count > index)
            {
                goto Label_006D;
            }
            item = Object.Instantiate<StatusListItem>(this.ListItem);
            item.get_transform().SetParent(base.get_transform(), 0);
            this.mItems.Add(item);
        Label_006D:
            item2 = this.mItems[index];
            item2.get_gameObject().SetActive(1);
            text = item2.Value;
            text2 = item2.Bonus;
            text.get_gameObject().SetActive(0);
            text2.get_gameObject().SetActive(0);
            if ((item2.Label != null) == null)
            {
                goto Label_00DC;
            }
            item2.Label.set_text(LocalizedText.Get("sys." + type));
        Label_00DC:
            if ((text != null) == null)
            {
                goto Label_0145;
            }
            str = &main_value.ToString();
            if (this.ShowSign == null)
            {
                goto Label_0112;
            }
            if (main_value <= 0)
            {
                goto Label_0112;
            }
            str = "+" + str;
        Label_0112:
            if (multiply == null)
            {
                goto Label_0127;
            }
            str = str + "%";
        Label_0127:
            text.set_text(str);
            text.set_color(color);
            text.get_gameObject().SetActive(1);
        Label_0145:
            if ((text2 != null) == null)
            {
                goto Label_01C5;
            }
            if (bonus_value == null)
            {
                goto Label_01C5;
            }
            str2 = &bonus_value.ToString();
            if (this.ShowSign == null)
            {
                goto Label_0183;
            }
            if (bonus_value <= 0)
            {
                goto Label_0183;
            }
            str2 = "+" + str2;
        Label_0183:
            if (multiply == null)
            {
                goto Label_0198;
            }
            str2 = str2 + "%";
        Label_0198:
            text2.set_text(string.Format(LocalizedText.Get("sys.STATUS_FORMAT_PARAM_BONUS"), str2));
            text2.set_color(color2);
            text2.get_gameObject().SetActive(1);
        Label_01C5:
            return;
        }

        private void Awake()
        {
            if ((this.ListItem != null) == null)
            {
                goto Label_003D;
            }
            if ((this.ListItem.Value != null) == null)
            {
                goto Label_003D;
            }
            this.mDefaultValueColor = this.ListItem.Value.get_color();
        Label_003D:
            if ((this.ListItem != null) == null)
            {
                goto Label_007A;
            }
            if ((this.ListItem.Bonus != null) == null)
            {
                goto Label_007A;
            }
            this.mDefaultBonusColor = this.ListItem.Bonus.get_color();
        Label_007A:
            return;
        }

        public void SetDirectValues(BaseStatus old_status, BaseStatus new_status)
        {
            string[] strArray;
            Array array;
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.ListItem == null) == null)
            {
                goto Label_002D;
            }
            DebugUtility.LogWarning(SRPG_Extensions.GetPath(base.get_gameObject(), null) + ": ListItem not set");
            return;
        Label_002D:
            num = 0;
            strArray = Enum.GetNames(typeof(ParamTypes));
            array = Enum.GetValues(typeof(ParamTypes));
            num2 = 0;
            goto Label_00B1;
        Label_0056:
            num3 = old_status[(short) array.GetValue(num2)];
            num4 = new_status[(short) array.GetValue(num2)];
            if (num3 == num4)
            {
                goto Label_00AD;
            }
            if (num2 == 2)
            {
                goto Label_00AD;
            }
            this.AddValue(num, strArray[num2], num3, num4, 0, 0, 0);
            num += 1;
        Label_00AD:
            num2 += 1;
        Label_00B1:
            if (num2 < array.Length)
            {
                goto Label_0056;
            }
            goto Label_00DD;
        Label_00C2:
            this.mItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_00DD:
            if (num < this.mItems.Count)
            {
                goto Label_00C2;
            }
            return;
        }

        public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, bool isSecret)
        {
            this.SetValues(paramAdd, paramMul, paramAdd, paramMul, isSecret);
            return;
        }

        public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, bool isSecret)
        {
            string[] strArray;
            Array array;
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.ListItem == null) == null)
            {
                goto Label_002D;
            }
            DebugUtility.LogWarning(SRPG_Extensions.GetPath(base.get_gameObject(), null) + ": ListItem not set");
            return;
        Label_002D:
            num = 0;
            strArray = Enum.GetNames(typeof(ParamTypes));
            array = Enum.GetValues(typeof(ParamTypes));
            num2 = 0;
            goto Label_010D;
        Label_0056:
            num3 = paramAdd[(short) array.GetValue(num2)];
            num4 = modAdd[(short) array.GetValue(num2)] - num3;
            if (num3 == null)
            {
                goto Label_00AF;
            }
            if (num2 == 2)
            {
                goto Label_00AF;
            }
            this.AddValue(num, strArray[num2], num3, num4, 0, isSecret, 0);
            num += 1;
        Label_00AF:
            num3 = paramMul[(short) array.GetValue(num2)];
            num4 = modMul[(short) array.GetValue(num2)] - num3;
            if (num3 == null)
            {
                goto Label_0109;
            }
            if (num2 == 2)
            {
                goto Label_0109;
            }
            this.AddValue(num, strArray[num2], num3, num4, 1, isSecret, 0);
            num += 1;
        Label_0109:
            num2 += 1;
        Label_010D:
            if (num2 < array.Length)
            {
                goto Label_0056;
            }
            goto Label_0139;
        Label_011E:
            this.mItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_0139:
            if (num < this.mItems.Count)
            {
                goto Label_011E;
            }
            return;
        }

        public void SetValues_Restrict(BaseStatus paramBaseAdd, BaseStatus paramBaseMul, BaseStatus paramBonusAdd, BaseStatus paramBonusMul, bool new_param_only)
        {
            string[] strArray;
            Array array;
            int num;
            int num2;
            int num3;
            int num4;
            ParamTypes types;
            if ((this.ListItem == null) == null)
            {
                goto Label_002D;
            }
            DebugUtility.LogWarning(SRPG_Extensions.GetPath(base.get_gameObject(), null) + ": ListItem not set");
            return;
        Label_002D:
            num = 0;
            strArray = Enum.GetNames(typeof(ParamTypes));
            array = Enum.GetValues(typeof(ParamTypes));
            num4 = 0;
            goto Label_014E;
        Label_0057:
            types = (short) array.GetValue(num4);
            if (types != 2)
            {
                goto Label_0073;
            }
            goto Label_0148;
        Label_0073:
            num2 = paramBaseAdd[types];
            num3 = paramBonusAdd[types];
            if (num3 == null)
            {
                goto Label_00DD;
            }
            if (num2 != null)
            {
                goto Label_00BA;
            }
            if (new_param_only == null)
            {
                goto Label_00BA;
            }
            this.AddValue(num, strArray[num4], num3, num3, 0, 0, 0);
            num += 1;
        Label_00BA:
            if (num2 == null)
            {
                goto Label_00DD;
            }
            if (new_param_only != null)
            {
                goto Label_00DD;
            }
            this.AddValue(num, strArray[num4], num3, num3, 0, 0, 0);
            num += 1;
        Label_00DD:
            num2 = paramBaseMul[types];
            num3 = paramBonusMul[types];
            if (num3 == null)
            {
                goto Label_0148;
            }
            if (num2 != null)
            {
                goto Label_0125;
            }
            if (new_param_only == null)
            {
                goto Label_0125;
            }
            this.AddValue(num, strArray[num4], num3, num3, 1, 0, 0);
            num += 1;
        Label_0125:
            if (num2 == null)
            {
                goto Label_0148;
            }
            if (new_param_only != null)
            {
                goto Label_0148;
            }
            this.AddValue(num, strArray[num4], num3, num3, 1, 0, 0);
            num += 1;
        Label_0148:
            num4 += 1;
        Label_014E:
            if (num4 < array.Length)
            {
                goto Label_0057;
            }
            goto Label_017B;
        Label_0160:
            this.mItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_017B:
            if (num < this.mItems.Count)
            {
                goto Label_0160;
            }
            return;
        }

        public unsafe void SetValues_TotalAndBonus(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, BaseStatus paramBonusAdd, BaseStatus paramBonusMul, BaseStatus modBonusAdd, BaseStatus modBonusMul)
        {
            ParamValues local4;
            ParamValues local3;
            ParamValues local2;
            ParamValues local1;
            int num;
            int num2;
            int num3;
            string[] strArray;
            Array array;
            Dictionary<ParamTypes, ParamValues> dictionary;
            Dictionary<ParamTypes, ParamValues> dictionary2;
            int num4;
            ParamTypes types;
            int num5;
            ParamTypes types2;
            ParamTypes types3;
            Dictionary<ParamTypes, ParamValues>.KeyCollection.Enumerator enumerator;
            ParamValues values;
            string str;
            ParamTypes types4;
            Dictionary<ParamTypes, ParamValues>.KeyCollection.Enumerator enumerator2;
            ParamValues values2;
            string str2;
            if ((this.ListItem == null) == null)
            {
                goto Label_002D;
            }
            DebugUtility.LogWarning(SRPG_Extensions.GetPath(base.get_gameObject(), null) + ": ListItem not set");
            return;
        Label_002D:
            num = 0;
            num2 = 0;
            num3 = 0;
            strArray = Enum.GetNames(typeof(ParamTypes));
            array = Enum.GetValues(typeof(ParamTypes));
            dictionary = new Dictionary<ParamTypes, ParamValues>();
            dictionary2 = new Dictionary<ParamTypes, ParamValues>();
            num4 = 0;
            goto Label_0183;
        Label_006A:
            types = (short) array.GetValue(num4);
            if (types != 2)
            {
                goto Label_0087;
            }
            goto Label_017D;
        Label_0087:
            num = modAdd[types] + modBonusAdd[types];
            num2 = num - (paramAdd[types] + paramBonusAdd[types]);
            if (num == null)
            {
                goto Label_0110;
            }
            if (dictionary.ContainsKey(types) != null)
            {
                goto Label_00E5;
            }
            dictionary.Add(types, new ParamValues());
        Label_00E5:
            local1 = dictionary[types];
            local1.main_value += num;
            dictionary[types].is_def_main = (num2 == 0) == 0;
        Label_0110:
            num = modBonusAdd[types];
            num2 = num - paramBonusAdd[types];
            if (num == null)
            {
                goto Label_017D;
            }
            if (dictionary.ContainsKey(types) != null)
            {
                goto Label_0152;
            }
            dictionary.Add(types, new ParamValues());
        Label_0152:
            local2 = dictionary[types];
            local2.bonus_value += num;
            dictionary[types].is_def_bonus = (num2 == 0) == 0;
        Label_017D:
            num4 += 1;
        Label_0183:
            if (num4 < array.Length)
            {
                goto Label_006A;
            }
            num5 = 0;
            goto Label_02B3;
        Label_0199:
            types2 = (short) array.GetValue(num5);
            if (types2 != 2)
            {
                goto Label_01B6;
            }
            goto Label_02AD;
        Label_01B6:
            num = modMul[types2] + modBonusMul[types2];
            num2 = num - (paramMul[types2] + paramBonusMul[types2]);
            if (num == null)
            {
                goto Label_0240;
            }
            if (dictionary2.ContainsKey(types2) != null)
            {
                goto Label_0215;
            }
            dictionary2.Add(types2, new ParamValues());
        Label_0215:
            local3 = dictionary2[types2];
            local3.main_value += num;
            dictionary2[types2].is_def_main = (num2 == 0) == 0;
        Label_0240:
            num = modBonusMul[types2];
            num2 = num - paramBonusMul[types2];
            if (num == null)
            {
                goto Label_02AD;
            }
            if (dictionary2.ContainsKey(types2) != null)
            {
                goto Label_0282;
            }
            dictionary2.Add(types2, new ParamValues());
        Label_0282:
            local4 = dictionary2[types2];
            local4.bonus_value += num;
            dictionary2[types2].is_def_bonus = (num2 == 0) == 0;
        Label_02AD:
            num5 += 1;
        Label_02B3:
            if (num5 < array.Length)
            {
                goto Label_0199;
            }
            enumerator = dictionary.Keys.GetEnumerator();
        Label_02CF:
            try
            {
                goto Label_0318;
            Label_02D4:
                types3 = &enumerator.Current;
                values = dictionary[types3];
                str = strArray[types3];
                this.AddValue_TotalAndBonus(num3, str, values.main_value, values.bonus_value, values.is_def_main, values.is_def_bonus, 0);
                num3 += 1;
            Label_0318:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_02D4;
                }
                goto Label_0336;
            }
            finally
            {
            Label_0329:
                ((Dictionary<ParamTypes, ParamValues>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0336:
            enumerator2 = dictionary2.Keys.GetEnumerator();
        Label_0344:
            try
            {
                goto Label_038D;
            Label_0349:
                types4 = &enumerator2.Current;
                values2 = dictionary2[types4];
                str2 = strArray[types4];
                this.AddValue_TotalAndBonus(num3, str2, values2.main_value, values2.bonus_value, values2.is_def_main, values2.is_def_bonus, 1);
                num3 += 1;
            Label_038D:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0349;
                }
                goto Label_03AB;
            }
            finally
            {
            Label_039E:
                ((Dictionary<ParamTypes, ParamValues>.KeyCollection.Enumerator) enumerator2).Dispose();
            }
        Label_03AB:
            goto Label_03CB;
        Label_03B0:
            this.mItems[num3].get_gameObject().SetActive(0);
            num3 += 1;
        Label_03CB:
            if (num3 < this.mItems.Count)
            {
                goto Label_03B0;
            }
            return;
        }

        public void SetValuesAfterOnly(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, bool isSecret, bool use_bonus_color)
        {
            string[] strArray;
            Array array;
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.ListItem == null) == null)
            {
                goto Label_002D;
            }
            DebugUtility.LogWarning(SRPG_Extensions.GetPath(base.get_gameObject(), null) + ": ListItem not set");
            return;
        Label_002D:
            num = 0;
            strArray = Enum.GetNames(typeof(ParamTypes));
            array = Enum.GetValues(typeof(ParamTypes));
            num2 = 0;
            goto Label_010F;
        Label_0056:
            num3 = modAdd[(short) array.GetValue(num2)];
            num4 = num3 - paramAdd[(short) array.GetValue(num2)];
            if (num3 == null)
            {
                goto Label_00B0;
            }
            if (num2 == 2)
            {
                goto Label_00B0;
            }
            this.AddValue(num, strArray[num2], num3, num4, 0, isSecret, use_bonus_color);
            num += 1;
        Label_00B0:
            num3 = modMul[(short) array.GetValue(num2)];
            num4 = num3 - paramMul[(short) array.GetValue(num2)];
            if (num3 == null)
            {
                goto Label_010B;
            }
            if (num2 == 2)
            {
                goto Label_010B;
            }
            this.AddValue(num, strArray[num2], num3, num4, 1, isSecret, use_bonus_color);
            num += 1;
        Label_010B:
            num2 += 1;
        Label_010F:
            if (num2 < array.Length)
            {
                goto Label_0056;
            }
            goto Label_013B;
        Label_0120:
            this.mItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_013B:
            if (num < this.mItems.Count)
            {
                goto Label_0120;
            }
            return;
        }

        private void Start()
        {
            if ((this.ListItem != null) == null)
            {
                goto Label_0037;
            }
            if (this.ListItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0037;
            }
            this.ListItem.get_gameObject().SetActive(0);
        Label_0037:
            return;
        }

        private class ParamValues
        {
            public bool is_def_main;
            public bool is_def_bonus;
            public int main_value;
            public int bonus_value;

            public ParamValues()
            {
                base..ctor();
                return;
            }
        }
    }
}

