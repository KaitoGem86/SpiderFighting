using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(HoldMeleeAction), menuName = ("GamePlay/Player/InteractionState/" + nameof(HoldMeleeAction)), order = 0)]
    public class HoldMeleeAction : BaseInteractionAction{
        Transform _meleeWeapon;

        public override void Init(PlayerController playerController, ActionEnum actionEnum){
            base.Init(playerController, actionEnum);
            _meleeWeapon = playerController.MeleeWeapon;
        }

        public override void Enter(){
            
            _meleeWeapon.gameObject.SetActive(true);
        }

        public override void Update(){
            base.Update();
            if(Input.GetKeyDown(KeyCode.C)){
                _stateContainer.ChangeAction(ActionEnum.CuttingTree);
                Debug.Log("Cutting Tree");
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if(actionAfter != ActionEnum.CuttingTree)
                _meleeWeapon.gameObject.SetActive(false);
            return base.Exit(actionAfter);
        }
    }
}