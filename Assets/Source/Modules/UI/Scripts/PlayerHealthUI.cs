using Modules.Player.Scripts;

namespace Modules.UI.Scripts
{
    public class PlayerHealthUI : PlayerBaseStatUI
    {
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _maxStatAmount = PlayerConfig.MaxHealth;
            _statAmount = _maxStatAmount;

            _playerHealth = PlayerRoot.GetComponent<PlayerHealth>();
            _playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
        }

        private void PlayerHealth_OnHealthChanged(int hp)
        {
            int barValue = _statAmount - hp;
            UpdateBarView(barValue, false);
        }
    }
}