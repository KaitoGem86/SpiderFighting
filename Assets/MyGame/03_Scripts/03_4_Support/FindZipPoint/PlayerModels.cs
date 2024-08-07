using Animancer;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public AnimancerComponent animancer;
        public Animator animator;
        public Transform PlayerDisplay;
        public Transform leftHand;
        public Transform rightHand;
        public Transform checkWallPivot;

#if UNITY_EDITOR
        [ContextMenu("Init Player Models")]
        public void InitPlayerModels()
        {
            animator = GetComponent<Animator>();
            PlayerDisplay = transform;
            leftHand = FindChildWithNameContaining(PlayerDisplay, "hand_l");
            rightHand = FindChildWithNameContaining(PlayerDisplay, "hand_r");
            if (leftHand == null)
            {
                leftHand = FindChildWithNameContaining(PlayerDisplay, "lefthand");
            }
            if (rightHand == null)
            {
                rightHand = FindChildWithNameContaining(PlayerDisplay, "lefthand");
            }
            if (leftHand == null)
            {
                leftHand = FindChildWithNameContaining(PlayerDisplay, "l-upperarm");
            }
            if (rightHand == null)
            {
                rightHand = FindChildWithNameContaining(PlayerDisplay, "r-upperarm");
            }
            checkWallPivot = transform.Find("GameObject");
        }

        Transform FindChildWithNameContaining(Transform parent, string searchString)
        {
            foreach (Transform child in parent)
            {
                if (child.name.ToLower().Contains(searchString))
                {
                    return child;
                }
                Transform found = FindChildWithNameContaining(child, searchString);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }
#endif
    }
}