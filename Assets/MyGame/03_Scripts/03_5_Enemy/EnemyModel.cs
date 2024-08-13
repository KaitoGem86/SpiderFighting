using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class EnemyModel : MonoBehaviour{
        public Animator animator;
        public Transform leftHand;
        public Transform rightHand;

        #if UNITY_EDITOR
        [ContextMenu("Init Player Models")]
        public void InitPlayerModels()
        {
            animator = GetComponent<Animator>();
            leftHand = FindChildWithNameContaining(this.transform, "hand_l");
            rightHand = FindChildWithNameContaining(this.transform, "hand_r");
            if (leftHand == null)
            {
                leftHand = FindChildWithNameContaining(this.transform, "lefthand");
            }
            if (rightHand == null)
            {
                rightHand = FindChildWithNameContaining(this.transform, "righthand");
            }
            if (leftHand == null)
            {
                leftHand = FindChildWithNameContaining(this.transform, "l-upperarm");
            }
            if (rightHand == null)
            {
                rightHand = FindChildWithNameContaining(this.transform, "r-upperarm");
            }
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