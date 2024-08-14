using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class BaseEnemyState<T> : BaseState<T, BaseEnemyBlackBoard> where T : ITransition
    {
        protected override int GetIndexTransition()
        {
            switch(_fsm.blackBoard.weaponType){
                case WeaponType.Hand:
                    return 0;
                case WeaponType.Club:
                    return 1;
                case WeaponType.Pistol:
                    return 2;
                case WeaponType.Rifle:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}