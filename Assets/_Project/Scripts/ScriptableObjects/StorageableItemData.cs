using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StorageableItemData", menuName = "Data/StorageableItemData")]
    public class StorageableItemData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea(1, 5)] private string _description;
        [SerializeField] private GameObject _icon;
        [SerializeField] private StorageableItem _prefab;

        public string Name => _name;
        public string Description => _description;
        public GameObject Icon => _icon;
        public StorageableItem Prefab => _prefab;
    }
}
