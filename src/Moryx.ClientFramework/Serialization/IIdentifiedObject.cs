// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Moryx.ClientFramework.Serialization
{
    /// <summary>
    /// Represents a identified object with the property <see cref="IIdentifiedObject.Id"/>
    /// </summary>
    public interface IIdentifiedObject
    {
        /// <summary>
        /// The identifier of the object
        /// </summary>
        long Id { get; }
    }
}
