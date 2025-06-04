using Scripts.Items;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Scripts.Character.Inventory
{
    public class InventoryNotification : MonoBehaviour
    {
        private const string PickupMessagePrefix = "Вы подобрали: ";
        private const float FadeDuration = 0.35f;
        private const float VisibleDuration = 1f;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;

        private IInventory _inventory;
        private CancellationTokenSource _cts;

        public void Initialize(IInventory inventory)
        {
            _inventory = inventory;
            _inventory.OnItemPickedUp += ShowNotification;
        }

        private void ShowNotification(ItemData itemData)
        {
            _text.text = $"* {PickupMessagePrefix} {itemData.Name} *";

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            ShowAndFadeAsync(_cts.Token).Forget();
        }

        private async UniTaskVoid ShowAndFadeAsync(CancellationToken token)
        {
            _canvasGroup.alpha = 1f;

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(VisibleDuration), cancellationToken: token);

                float time = 0f;

                while (time < FadeDuration)
                {
                    time += Time.deltaTime;

                    _canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / FadeDuration);
                    
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }

                _canvasGroup.alpha = 0f;
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void OnDestroy()
        {
            if (_inventory == null) return;

            _inventory.OnItemPickedUp -= ShowNotification;
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}