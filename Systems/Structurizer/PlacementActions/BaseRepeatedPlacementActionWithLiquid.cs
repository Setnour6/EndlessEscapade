﻿namespace EEMod.Systems.Structurizer.PlacementActions
{
    public abstract class
        BaseRepeatedPlacementActionWithLiquid<TPlacementAction> : BaseRepeatedPlacementAction<TPlacementAction>,
            ILiquidData
        where TPlacementAction : IPlacementAction
    {
        public virtual byte LiquidData { get; }

        protected BaseRepeatedPlacementActionWithLiquid(ushort repetitionCount, byte liquidData) : base(repetitionCount)
        {
            LiquidData = liquidData;
        }
    }
}