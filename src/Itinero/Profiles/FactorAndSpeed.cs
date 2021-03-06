﻿// Itinero - Routing for .NET
// Copyright (C) 2015 Abelshausen Ben
// 
// This file is part of Itinero.
// 
// Itinero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// Itinero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Itinero. If not, see <http://www.gnu.org/licenses/>.

namespace Itinero.Profiles
{
    /// <summary>
    /// A factor returned by a routing profile to influence routing augmented with the speed.
    /// </summary>
    public struct FactorAndSpeed
    {
        /// <summary>
        /// Gets or sets the actual factor.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Gets or sets the speed (1/m/s).
        /// </summary>
        public float SpeedFactor { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// 0=bidirectional, 1=forward, 2=backward.
        /// 3=bidirectional, 4=forward, 5=backward but without stopping abilities.
        public short Direction { get; set; }

        /// <summary>
        /// Gets or sets the constraint values.
        /// </summary>
        public float[] Constraints { get; set; }
        
        /// <summary>
        /// Returns a non-value.
        /// </summary>
        public static FactorAndSpeed NoFactor { get { return new FactorAndSpeed() { Direction = 0, Value = 0, SpeedFactor = 0, Constraints = null }; } }
    }
}