﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EEMod.Systems.Structurizer.PlacementActions
{
    public abstract class BaseRepeatedPlacementAction<TPlacementAction> : IRepeatedPlacementAction<TPlacementAction>
        where TPlacementAction : IPlacementAction
    {
        public abstract ushort Flag { get; }

        public virtual ushort EntryData { get; }

        public virtual byte StyleData { get; }

        public virtual byte AlternateStyleData { get; }

        protected ushort InternalRepetitionCount { get; set; }

        public virtual ushort RepetitionCount
        {
            get => InternalRepetitionCount;

            set => InternalRepetitionCount = value;
        }

        public abstract TPlacementAction PlacementAction { get; }

        protected BaseRepeatedPlacementAction(ushort repetitionCount)
        {
            InternalRepetitionCount = repetitionCount;
        }

        public abstract void Place(ref int i, ref int j, Structure structure,
            ref List<(Point, ushort, ushort, ushort)> deferredMultitiles);
    }
}