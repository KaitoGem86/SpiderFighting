namespace Core.GamePlay.Player{
    public class _PlayerAnimationKey{
        public const string Idle = "Idle";
        public const string Walking = "Walking";
        public const string Jumping = "JumpingUp";
        public const string Running = "Running";
        public const string FallingDown = "FallingDown";
        public const string Sliding = "Sliding";


        public const string BoolIdle = "IsIdle";
        public const string BoolWalking = "IsWalking";
        public const string BoolJumping = "IsJumpingUp";
        public const string BoolRunning = "IsRunning";
        public const string BoolFallingDown = "IsJumpingDown";
        public const string BoolSliding = "IsSliding";

        public static string GetAnimationKey(ActionEnum action){
            return action switch{
                ActionEnum.Idle => BoolIdle,
                ActionEnum.Moving => BoolWalking,
                ActionEnum.Jumping => BoolJumping,
                ActionEnum.Sprinting => BoolRunning,
                ActionEnum.FallingDown => BoolFallingDown,
                ActionEnum.Sliding => BoolSliding,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        public static string GetAnimationName(ActionEnum action){
            return action switch{
                ActionEnum.Idle => Idle,
                ActionEnum.Moving => Walking,
                ActionEnum.Jumping => Jumping,
                ActionEnum.Sprinting => Running,
                ActionEnum.FallingDown => FallingDown,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }
    }
}