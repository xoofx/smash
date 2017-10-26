// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license. 
// See license.txt file in the project root for full license information.
using System;

namespace Smash
{
    /// <summary>
    /// Base interface for writing to a hash.
    /// </summary>
    /// <seealso cref="IHash64"/>
    /// <seealso cref="IHash32"/>
    public interface IHash
    {
        void Write(ulong data);
        void Write(long data);
        void Write(uint data);
        void Write(int data);
        void Write(ushort data);
        void Write(short data);
        void Write(byte data);
        void Write(sbyte data);
        void Write(bool data);
        void Write(string data);
        void Write(float data);
        void Write(double data);
        void Write(char data);
        void Write(IntPtr buffer, ulong length);
        void Write(byte[] buffer, int offset, int count);
    }

    /// <summary>
    /// Interface for computing a 64bit hash.
    /// </summary>
    public interface IHash64 : IHash
    {
        /// <summary>
        /// Resets this hash with the specified seed
        /// </summary>
        /// <param name="seed">The seed to reset this instance</param>
        void Reset(ulong seed);

        /// <summary>
        /// Computes the current 64bit hash
        /// </summary>
        /// <returns>The current 64bit hash</returns>
        ulong Compute();
    }

    /// <summary>
    /// Interface for computing a 32bit hash.
    /// </summary>
    public interface IHash32 : IHash
    {
        /// <summary>
        /// Resets this hash with the specified seed
        /// </summary>
        /// <param name="seed">The seed to reset this instance</param>
        void Reset(uint seed);

        /// <summary>
        /// Computes the current 32bit hash
        /// </summary>
        /// <returns>The current 32bit hash</returns>
        uint Compute();
    }
}