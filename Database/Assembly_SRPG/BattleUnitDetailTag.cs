namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleUnitDetailTag : MonoBehaviour
    {
        public Text TextValue;

        public BattleUnitDetailTag()
        {
            base..ctor();
            return;
        }

        public void SetTag(string tag)
        {
            if (tag != null)
            {
                goto Label_000D;
            }
            tag = string.Empty;
        Label_000D:
            if (this.TextValue == null)
            {
                goto Label_0029;
            }
            this.TextValue.set_text(tag);
        Label_0029:
            return;
        }
    }
}

