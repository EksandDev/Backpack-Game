using System.Collections.Generic;
using System.Linq;

namespace BackpackGame.Core
{
    public class CustomStack<T>
    {
        private int _stackLimit;
        private List<T> _list = new();

        public CustomStack(int stackLimit)
        {
            _stackLimit = stackLimit;
        }

        public IReadOnlyList<T> List => _list;
        
        public bool TryTake(out T item)
        {
            if (_list.Count == 0)
            {
                item = default;
                return false;
            }
            
            item = _list.Last();
            _list.RemoveAt(_list.Count - 1);
            return true;
        }

        public bool TryPush(T item)
        {
            if (_list.Count() >= _stackLimit)
                return false;
            
            _list.Add(item);
            return true;
        }

        public void Remove(int index)
        {
            _list.RemoveAt(index);
        }

        public void ReplaceItem(T newItem, int replaceableItemIndex)
        {
            _list[replaceableItemIndex] = newItem;
        }

        public void SwapItemsBetween(int firstItemIndex, int secondItemIndex)
        {
            var firstItem = _list[firstItemIndex];
            var secondItem = _list[secondItemIndex];
            _list[firstItemIndex] = secondItem;
            _list[secondItemIndex] = firstItem;
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}
