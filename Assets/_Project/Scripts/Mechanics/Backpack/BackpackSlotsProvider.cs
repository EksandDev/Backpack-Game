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
        private Stack<BackpackSlot> _activeSlots;

        public BackpackSlotsProvider(BackpackSlot[] slots, BackpackModel backpackModel)
        {
            _allSlots = slots;
            _activeSlots = new(_allSlots.Length);
            _backpackModel = backpackModel;
        }

        public void Initialize()
        {
            _backpackModel.ItemStored += ActivateSlot;
            _backpackModel.ItemTaken += DeactivateSlot;
        }

        private void ActivateSlot(GameObject icon)
        {
            var disabledSlot = _allSlots.FirstOrDefault(slot => !slot.isActiveAndEnabled);

            if (!disabledSlot)
                throw new ArgumentNullException();
            
            disabledSlot.Initialize(icon);
            _activeSlots.Push(disabledSlot);
        }

        private void DeactivateSlot()
        {
            if (!_activeSlots.Peek())
                throw new ArgumentNullException();

            var activeSlot = _activeSlots.Pop();
            activeSlot.Deinitialize();
        }
    }
}