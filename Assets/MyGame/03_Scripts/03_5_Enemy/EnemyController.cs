using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class EnemyController : AIFSM, IHitted
    {
        [SerializeField] private HPBarController _hpBarController;
        [SerializeField] private float _maxHP = 100;
        private EnemySO _soController;

        public void Init(EnemySO soConTroller)
        {
            _soController = soConTroller;
        }


        public void HittedByPlayer()
        {
            _maxHP -= 10;
            _hpBarController.SetHP(_maxHP, 100);
            ChangeAction(AIState.Hit);
        }

        public Transform TargetEnemy { get => this.transform; }
    }
}