using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackSlot : MonoBehaviour
    {
        private GameObject _icon;
        
        public void Initialize(GameObject icon)
        {
            gameObject.SetActive(true);
            _icon = Instantiate(icon, transform.position, Quaternion.identity, transform);
        }

        public void Deinitialize()
        {
            gameObject.SetActive(false);
            Destroy(_icon);
            _icon = null;
        }
    }
}
