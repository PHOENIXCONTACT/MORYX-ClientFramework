// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework.Serialization
{
    /// <summary>
    /// Represents a identifierd object with the property <see cref="IIdentifiedObject.Id"/>
    /// </summary>
    public interface IIdentifiedObject
    {
        /// <summary>
        /// The identifier of the object
        /// </summary>
        long Id { get; }
    }
}
