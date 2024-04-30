namespace Modules.UI.Scripts
{
    public class PlayerStaminaUI : PlayerBaseStatUI
    {
        private void Awake()
        {
            _maxStatAmount = PlayerConfig.MaxStamina;
            _statAmount = _maxStatAmount;
        }
    }
}
