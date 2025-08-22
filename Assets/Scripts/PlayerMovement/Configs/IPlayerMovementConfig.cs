    namespace PlayerMovement.Configs
    {
        public interface IPlayerMovementConfig
        {
            float JumpForce { get; }
            float Speed { get; }
            float StartRotation { get; }
            float JumpCooldown { get; }
        }
    }
