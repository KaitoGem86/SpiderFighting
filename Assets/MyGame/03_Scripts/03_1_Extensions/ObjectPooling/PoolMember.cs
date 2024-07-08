using UnityEngine;

namespace Core.SystemGame.Pooling{
    public enum MemberStateEnum : byte{
        None,
        InPool,
        InUse,
    }

    public class PoolMember : MonoBehaviour{
        private MemberStateEnum _state;
        private int _memberId;

        public void Init(int poolId){
            _state = MemberStateEnum.InPool;
            _memberId = poolId;
        }


        public void OnEnable(){
            _state = MemberStateEnum.InUse;
        }

        public void OnDisable(){
            _state = MemberStateEnum.InPool;   
        }

        public MemberStateEnum State => _state;
        public int MemberId => _memberId;
    }
}