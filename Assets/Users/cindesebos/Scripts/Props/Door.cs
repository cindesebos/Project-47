using UnityEngine;
using DG.Tweening;

namespace Scripts.Props
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform _leftPanel;
        [SerializeField] private Transform _rightPanel;

        [SerializeField] private Vector3 _leftClosedPosition;
        [SerializeField] private Vector3 _leftOpenPosition;

        [SerializeField] private Vector3 _rightClosedPosition;
        [SerializeField] private Vector3 _rightOpenPosition;

        [SerializeField] private float _animationTime = 1f;
        [SerializeField] private Ease _ease = Ease.OutCubic;

        public void Open()
        {
            _leftPanel.DOLocalMove(_leftOpenPosition, _animationTime).SetEase(_ease);
            _rightPanel.DOLocalMove(_rightOpenPosition, _animationTime).SetEase(_ease);
        }

        public void Close()
        {
            _leftPanel.DOLocalMove(_leftClosedPosition, _animationTime).SetEase(_ease);
            _rightPanel.DOLocalMove(_rightClosedPosition, _animationTime).SetEase(_ease);
        }
    }
}
