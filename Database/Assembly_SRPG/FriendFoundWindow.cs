namespace SRPG
{
    using System;
    using UnityEngine;

    public class FriendFoundWindow : MonoBehaviour
    {
        public FriendFoundWindow()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            FriendData data;
            UnitData data2;
            data = GlobalVars.FoundFriend;
            if (data == null)
            {
                goto Label_0018;
            }
            DataSource.Bind<FriendData>(base.get_gameObject(), data);
        Label_0018:
            data2 = data.Unit;
            if (data2 == null)
            {
                goto Label_0031;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data2);
        Label_0031:
            return;
        }
    }
}

