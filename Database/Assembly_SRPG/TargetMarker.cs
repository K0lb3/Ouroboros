namespace SRPG
{
    using System;
    using UnityEngine;

    public class TargetMarker : MonoBehaviour
    {
        private Transform m_Transform;

        public TargetMarker()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.m_Transform = base.GetComponent<Transform>();
            return;
        }

        private unsafe void LateUpdate()
        {
            SceneBattle battle;
            Vector3 vector;
            battle = SceneBattle.Instance;
            vector = Vector3.get_zero();
            if ((battle != null) == null)
            {
                goto Label_0036;
            }
            if (battle.isUpView == null)
            {
                goto Label_0036;
            }
            &vector.y += 0.65f;
        Label_0036:
            this.m_Transform.set_localPosition(vector);
            return;
        }
    }
}

