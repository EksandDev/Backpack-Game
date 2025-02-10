using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackSlot : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _pointToJump;
        [SerializeField] private float _animationsDuration = 1;
        
        private GameObject _icon;
        private Coroutine _destroyCoroutine;

        public bool IsFull { get; private set; }
        
        public void Initialize(GameObject icon)
        {
            if (_destroyCoroutine != null)
            {
                StopCoroutine(_destroyCoroutine);
                _destroyCoroutine = null;
                DestroyIcon();
            }
            
            IsFull = true;
            _icon = Instantiate(icon, _spawnPoint.position, Quaternion.identity, transform);
            _icon.transform.DOMove(transform.position, _animationsDuration).SetLink(_icon);
        }

        public void Deinitialize()
        {
            IsFull = false;
            _destroyCoroutine = StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            const int jumpsNumber = 1;
            const float shakeStrength = 45;
            var jumpPower = _icon.transform.position.y + 50;
            var sequence = DOTween.Sequence()
                .Append(_icon.transform.DOJump(_pointToJump.position, jumpPower, jumpsNumber, _animationsDuration))
                .Join(_icon.transform.DOShakeRotation(_animationsDuration, shakeStrength))
                .SetLink(_icon);

            yield return sequence.WaitForCompletion();
            
            DestroyIcon();
        }

        private void DestroyIcon()
        {
            if (!_icon)
                return;
            
            Destroy(_icon);
            _icon = null;
        }
    }
}
