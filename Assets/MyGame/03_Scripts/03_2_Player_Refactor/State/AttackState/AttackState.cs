using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class AttackState : ClipTransitionPlayerState
    {
        [SerializeField] private SerializedDictionary<int, int> _comboData;
        protected bool _isCanChangeNextAttack = false;
        private int _maxHitInCombo = 0;
        private int _currentStartHitCombo;
        private int _currentHitInCombo = 0;
        private IHitted _enemy;

        protected override void Awake()
        {
            _fsm.blackBoard.GlobalVelocity = Vector3.zero;
            base.Awake();
            _currentHitInCombo = 0;
        }

        public override void EnterState()
        {
            _enemy = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform, false);

            if (_enemy != null && Vector3.Distance(_enemy.TargetEnemy.position, _fsm.transform.position) > 2f)
            {
                _fsm.ChangeAction(FSMState.StartAttack);
                return;
            }
            //_fsm.blackBoard.Character.useRootMotion = true;
            base.EnterState();
            var forward = _enemy != null ? _enemy.TargetEnemy.position - _fsm.transform.position : _fsm.transform.forward; forward.y = 0;
            _blackBoard.rig.DORotate(forward, 0.1f);
            _isCanChangeNextAttack = false;
        }

        public override void ExitState()
        {
            //_fsm.blackBoard.Character.useRootMotion = false;
            base.ExitState();
        }

        public void FixedUpdate()
        {
            var forward = _enemy != null ? _enemy.TargetEnemy.position - _fsm.transform.position : _fsm.transform.forward; forward.y = 0;
            _fsm.transform.rotation = Quaternion.Slerp(_fsm.transform.rotation, Quaternion.LookRotation(forward), 0.2f);
        }


        public void CanChangeNextAttack()
        {
            _isCanChangeNextAttack = true;
        }

        public void ApplyDamage(int typeAttackIndicator)
        {
            _blackBoard.AttackCount += 1;
            _blackBoard.ResetTime();
            if(_enemy != null)
                _blackBoard.OnShowHitCounter.Raise(_blackBoard.AttackCount);
            switch (typeAttackIndicator)
            {
                case 0:
                    _enemy?.HittedByPlayer(FSMState.Hit);
                    break;
                case 1:
                    _enemy?.HittedByPlayer(FSMState.KnockBack);
                    break;
            }
        }


        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            if (_currentHitInCombo == 0)
            {
                _fsm.ChangeAction(FSMState.LastAttack);
                return;
            }
            _fsm.ChangeAction(FSMState.Attack);
        }

        public void CompleteAttack()
        {
            _currentHitInCombo = 0;
            _fsm.blackBoard.Character.SetMovementDirection(Vector3.zero);
            _fsm.ChangeAction(FSMState.Idle);
        }

        protected override int GetIndexTransition()
        {
            if (_currentHitInCombo == 0)
            {
                var element = _comboData.ElementAt(Random.Range(0, _comboData.Count));
                _currentStartHitCombo = element.Key;
                _maxHitInCombo = element.Value;
                _currentHitInCombo += 1;
                return _currentStartHitCombo;
            }
            else
            {
                var final = _currentStartHitCombo + _currentHitInCombo;
                _currentHitInCombo = (_currentHitInCombo + 1) % _maxHitInCombo;
                return final;
            }
        }

        private KeyValuePair<int, int> GetComboData(int currentHitIndex)
        {
            int previous = _comboData.First().Key;
            foreach (var item in _comboData)
            {
                if (item.Key > currentHitIndex)
                {
                    return new KeyValuePair<int, int>(previous, item.Key);
                }
                previous = item.Key;
            }
            return _comboData.Last();
        }
    }
}