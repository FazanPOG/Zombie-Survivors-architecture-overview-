using Modules.Configs.Scripts;
using Modules.Player.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.UI.Scripts
{
    public abstract class PlayerBaseStatUI : MonoBehaviour
    {
        [SerializeField] private Image _statBar;
        [SerializeField] private TextMeshProUGUI _statAmountText;

        protected int _maxStatAmount;
        protected int _statAmount;

        private PlayerConfig _playerConfig;
        private PlayerRoot _playerRoot;
        private float _statBarFillingTime;

        protected PlayerConfig PlayerConfig => _playerConfig;
        protected PlayerRoot PlayerRoot => _playerRoot;
        protected Image StatBar => _statBar;
        protected TextMeshProUGUI StatAmountText => _statAmountText;

        [Inject]
        private void Construct(PlayerRoot playerRoot, PlayerConfig playerConfig)
        {
            _playerRoot = playerRoot;
            _playerConfig = playerConfig;

            _statBarFillingTime = _playerConfig.StatBarFillingTime;
            _statBar.fillAmount = 1f;
        }

        private void Start() => UpdateStatText();

        public void UpdateBarView(int value, bool isEncreaseValue) 
        {
            int oldValue = _statAmount;

            if(isEncreaseValue)
                _statAmount += value;
            else
                _statAmount -= value;

            if (_statAmount < 0)
                _statAmount = 0;

            if(_statAmount > _maxStatAmount)
                _statAmount = _maxStatAmount;

            StartCoroutine(SmoothFillImage(oldValue, _statAmount));

            UpdateStatText();
        }

        private void UpdateStatText() 
        {
            _statAmountText.text = $"{_statAmount}/{_maxStatAmount}";
        }

        private IEnumerator SmoothFillImage(int oldValue, int targetValue) 
        {
            float elapsedTime = 0f;

            float currentFillAmount = _statBar.fillAmount;
            float targetFillAmount = (currentFillAmount * targetValue) / oldValue;

            while (elapsedTime < _statBarFillingTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / _statBarFillingTime;
                _statBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, t);
                yield return null;
            }

            _statBar.fillAmount = targetFillAmount;
        }
    }
}
