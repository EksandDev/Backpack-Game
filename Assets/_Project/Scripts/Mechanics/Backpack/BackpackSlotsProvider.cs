using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackSlotsProvider
    {
        private BackpackModel _backpackModel;
        private BackpackSlot[] _allSlots;
        private Stack<BackpackSlot> _fullSlots;

        public BackpackSlotsProvider(BackpackSlot[] slots, BackpackModel backpackModel)
        {
            _allSlots = slots;
            _fullSlots = new(_allSlots.Length);
            _backpackModel = backpackModel;
        }

        public void Initialize()
        {
            _backpackModel.ItemStored += ActivateSlot;
            _backpackModel.ItemTaken += DeactivateSlot;
        }

        private void ActivateSlot(GameObject icon)
        {
            var freeSlot = _allSlots.FirstOrDefault(slot => !slot.IsFull);

            if (!freeSlot)
                throw new ArgumentNullException();
            
            freeSlot.Initialize(icon);
            _fullSlots.Push(freeSlot);
        }

        private void DeactivateSlot()
        {
            if (!_fullSlots.Peek())
                throw new ArgumentNullException();

            var activeSlot = _fullSlots.Pop();
            activeSlot.Deinitialize();
        }
    }
}