// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;

namespace Moryx.ClientFramework.Serialization
{
    /// <summary>
    /// Compares two instances of <see cref="IIdentifiedObject"/>
    /// Will only compare by the property <see cref="IIdentifiedObject.Id"/>
    /// </summary>
    public class IdEqualityComparer : IEqualityComparer<IIdentifiedObject>
    {

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns> true if the specified objects are equal; otherwise, false. </returns>
        public bool Equals(IIdentifiedObject x, IIdentifiedObject y)
        {
            if (ReferenceEquals(x, y))
                return true;

            return x.Id == y.Id;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns> A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(IIdentifiedObject obj)
        {
            return (int)obj.Id;
        }
    }
}
