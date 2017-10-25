// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license. 
// See license.txt file in the project root for full license information.
/*
*  xxHash - Fast Hash algorithm
*  Copyright (C) 2012-2016, Yann Collet
*
*  BSD 2-Clause License (http://www.opensource.org/licenses/bsd-license.php)
*
*  Redistribution and use in source and binary forms, with or without
*  modification, are permitted provided that the following conditions are
*  met:
*
*  * Redistributions of source code must retain the above copyright
*  notice, this list of conditions and the following disclaimer.
*  * Redistributions in binary form must reproduce the above
*  copyright notice, this list of conditions and the following disclaimer
*  in the documentation and/or other materials provided with the
*  distribution.
*
*  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
*  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
*  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
*  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
*  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
*  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
*  LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
*  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
*  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
*  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
*  OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*
*  You can contact the author at :
*  - xxHash homepage: http://www.xxhash.com
*  - xxHash source repository : https://github.com/Cyan4973/xxHash
*/

using System;
using System.Runtime.CompilerServices;

// ReSharper disable InconsistentNaming
using U64 = System.UInt64;
using U32 = System.UInt32;

namespace Smash
{
    /// <summary>
    /// The xxHash method. Use <see cref="Create64"/>.
    /// </summary>
    public static class xxHash
    {
        [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
        public static Hash64 Create64(ulong seed)
        {
            return new Hash64(seed);
        }

        [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
        public static Hash32 Create32(uint seed)
        {
            return new Hash32(seed);
        }

        /// <summary>
        /// xxHash for computing a 64bit hash.
        /// </summary>
        public struct Hash64 : IHash64
        {
            private const U64 PRIME64_1 = 11400714785074694791UL;
            private const U64 PRIME64_2 = 14029467366897019727UL;
            private const U64 PRIME64_3 = 1609587929392839161UL;
            private const U64 PRIME64_4 = 9650029242287828579UL;
            private const U64 PRIME64_5 = 2870177450012600261UL;

            private U64 _v1;
            private U64 _v2;
            private U64 _v3;
            private U64 _v4;

            private ulong _writeCount;
            private ulong _length;

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public Hash64(ulong seed)
            {
                // NOTE: Duplicated with Reset method, keep in sync!
                _v1 = seed + PRIME64_1 + PRIME64_2;
                _v2 = seed + PRIME64_2;
                _v3 = seed + 0;
                _v4 = seed - PRIME64_1;
                _writeCount = 0;
                _length = 0;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(ulong input)
            {
                WriteInternal(input);
                _length += 8;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(long input)
            {
                Write((ulong) input);
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(uint input)
            {
                WriteInternal(input * PRIME64_1);
                _length += 4;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(int input)
            {
                WriteInternal((uint) input * PRIME64_1);
                _length += 4;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(ushort input)
            {
                WriteInternal(input * PRIME64_1);
                _length += 2;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(short input)
            {
                WriteInternal((ushort)input * PRIME64_1);
                _length += 2;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(byte input)
            {
                WriteInternal(input * PRIME64_1);
                _length += 1;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(sbyte input)
            {
                WriteInternal((byte)input * PRIME64_1);
                _length += 1;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(bool input)
            {
                Write(input ? (byte)1 : (byte)0);
            }

            public unsafe void Write(IntPtr buffer, ulong length)
            {
                if (buffer == IntPtr.Zero) throw new ArgumentNullException(nameof(buffer));
                var pBuffer = (byte*)(void*)buffer;

                const ulong FetchSize = sizeof(ulong) * 4;
                while (length >= FetchSize)
                {
                    var pulong = (ulong*)pBuffer;
                    Write(pulong[0]);
                    Write(pulong[1]);
                    Write(pulong[2]);
                    Write(pulong[3]);
                    pBuffer += FetchSize;
                    length -= FetchSize;
                }

                while (length >= 8)
                {
                    Write(*(ulong*)pBuffer);
                    pBuffer += 8;
                    length -= 8;
                }

                while (length > 0)
                {
                    Write(*(byte*)pBuffer);
                    pBuffer += 1;
                    length -= 1;
                }
            }

            public unsafe void Write(byte[] buffer, int offset, int count)
            {
                if (buffer == null) throw new ArgumentNullException(nameof(buffer));
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be < 0");
                if (count < 0)
                    throw new ArgumentOutOfRangeException(nameof(count), "count cannot be < 0");
                if (buffer.Length - offset < count)
                    throw new ArgumentException("Invalid offset + count > length");

                fixed (void* pByte = &buffer[offset])
                {
                    Write(new IntPtr(pByte), (ulong)count);
                }
            }

            public void Reset(ulong seed)
            {
                // NOTE: Duplicated with ctor, keep in sync!
                _v1 = seed + PRIME64_1 + PRIME64_2;
                _v2 = seed + PRIME64_2;
                _v3 = seed + 0;
                _v4 = seed - PRIME64_1;
                _writeCount = 0;
                _length = 0;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public ulong Compute()
            {
                // This part is slightly different from xxHash to use a generic algorithm
                // depending on the number of writes
                // Might not give the exact same result as we are feeding all states v1,v2,v3,v4
                // while xxHash calculates the hash differently for input buffer < 64/32/8/4 bytes
                var h64 = XXH_rotl64(_v1, 1);
                if (_writeCount >= 2)
                {
                    h64 += XXH_rotl64(_v2, 7);
                    if (_writeCount >= 3)
                    {
                        h64 += XXH_rotl64(_v3, 12);
                        if (_writeCount >= 4)
                        {
                            h64 += XXH_rotl64(_v4, 18);
                        }
                    }
                }

                h64 = XXH64_mergeRound(h64, _v1);
                if (_writeCount >= 2)
                {
                    h64 = XXH64_mergeRound(h64, _v2);
                    if (_writeCount >= 3)
                    {
                        h64 = XXH64_mergeRound(h64, _v3);
                        if (_writeCount >= 4)
                        {
                            h64 = XXH64_mergeRound(h64, _v4);
                        }
                    }
                }

                h64 += _length;

                h64 ^= h64 >> 33;
                h64 *= PRIME64_2;
                h64 ^= h64 >> 29;
                h64 *= PRIME64_3;
                h64 ^= h64 >> 32;

                return h64;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private void WriteInternal(ulong input)
            {
                switch (_writeCount & 3)
                {
                    case 0:
                        _v1 = XXH64_round(_v1, input);
                        break;
                    case 1:
                        _v2 = XXH64_round(_v2, input);
                        break;
                    case 2:
                        _v3 = XXH64_round(_v3, input);
                        break;
                    case 4:
                        _v4 = XXH64_round(_v4, input);
                        break;
                }
                _writeCount++;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private static U64 XXH_rotl64(U64 x, int r)
            {
                return (x << r) | (x >> (64 - r));
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private static U64 XXH64_round(U64 acc, U64 input)
            {
                acc += input * PRIME64_2;
                acc = XXH_rotl64(acc, 31);
                acc *= PRIME64_1;
                return acc;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private static U64 XXH64_mergeRound(U64 acc, U64 val)
            {
                val = XXH64_round(0, val);
                acc ^= val;
                acc = acc * PRIME64_1 + PRIME64_4;
                return acc;
            }
        }

        /// <summary>
        /// xxHash for computing a 32bit hash.
        /// </summary>
        public struct Hash32 : IHash32
        {
            private const U32 PRIME32_1 = 2654435761U;
            private const U32 PRIME32_2 = 2246822519U;
            private const U32 PRIME32_3 = 3266489917U;
            private const U32 PRIME32_4 = 668265263U;
            private const U32 PRIME32_5 = 374761393U;

            private U32 _v1;
            private U32 _v2;
            private U32 _v3;
            private U32 _v4;

            private ulong _writeCount;
            private ulong _length;

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public Hash32(uint seed)
            {
                // NOTE: Duplicated with Reset method, keep in sync!
                _v1 = seed + PRIME32_1 + PRIME32_2;
                _v2 = seed + PRIME32_2;
                _v3 = seed + 0;
                _v4 = seed - PRIME32_1;
                _writeCount = 0;
                _length = 0;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(ulong input)
            {
                WriteInternal((uint)(input & 0xFFFFFFFF));
                WriteInternal((uint)(input >> 32));
                _length += 8;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(long input)
            {
                Write((ulong)input);
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(uint input)
            {
                WriteInternal(input * PRIME32_1);
                _length += 4;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(int input)
            {
                WriteInternal((uint)input * PRIME32_1);
                _length += 4;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(ushort input)
            {
                WriteInternal(input * PRIME32_1);
                _length += 2;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(short input)
            {
                WriteInternal((ushort)input * PRIME32_1);
                _length += 2;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(byte input)
            {
                WriteInternal(input * PRIME32_1);
                _length += 1;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(sbyte input)
            {
                WriteInternal((byte)input * PRIME32_1);
                _length += 1;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public void Write(bool input)
            {
                Write(input ? (byte)1 : (byte)0);
            }

            public unsafe void Write(IntPtr buffer, ulong length)
            {
                if (buffer == IntPtr.Zero) throw new ArgumentNullException(nameof(buffer));
                var pBuffer = (byte*)(void*)buffer;

                const ulong FetchSize = sizeof(uint) * 4;
                while (length >= FetchSize)
                {
                    var puint = (uint*)pBuffer;
                    Write(puint[0]);
                    Write(puint[1]);
                    Write(puint[2]);
                    Write(puint[3]);
                    pBuffer += FetchSize;
                    length -= FetchSize;
                }

                while (length >= 4)
                {
                    Write(*(uint*)pBuffer);
                    pBuffer += 4;
                    length -= 4;
                }

                while (length > 0)
                {
                    Write(*(byte*)pBuffer);
                    pBuffer += 1;
                    length -= 1;
                }
            }

            public unsafe void Write(byte[] buffer, int offset, int count)
            {
                if (buffer == null) throw new ArgumentNullException(nameof(buffer));
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be < 0");
                if (count < 0)
                    throw new ArgumentOutOfRangeException(nameof(count), "count cannot be < 0");
                if (buffer.Length - offset < count)
                    throw new ArgumentException("Invalid offset + count > length");

                fixed (void* pByte = &buffer[offset])
                {
                    Write(new IntPtr(pByte), (ulong)count);
                }
            }

            public void Reset(uint seed)
            {
                // NOTE: Duplicated with ctor, keep in sync!
                _v1 = seed + PRIME32_1 + PRIME32_2;
                _v2 = seed + PRIME32_2;
                _v3 = seed + 0;
                _v4 = seed - PRIME32_1;
                _writeCount = 0;
                _length = 0;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            public uint Compute()
            {
                // This part is slightly different from xxHash to use a generic algorithm
                // depending on the number of writes
                // Might not give the exact same result as we are feeding all states v1,v2,v3,v4
                // while xxHash calculates the hash differently for input buffer < 64/32/8/4 bytes
                Write(_length);

                var h32 = XXH_rotl32(_v1, 1);
                if (_writeCount >= 2)
                {
                    h32 += XXH_rotl32(_v2, 7);
                    if (_writeCount >= 3)
                    {
                        h32 += XXH_rotl32(_v3, 12);
                        if (_writeCount >= 4)
                        {
                            h32 += XXH_rotl32(_v4, 18);
                        }
                    }
                }

                h32 ^= h32 >> 15;
                h32 *= PRIME32_2;
                h32 ^= h32 >> 13;
                h32 *= PRIME32_3;
                h32 ^= h32 >> 16;

                return h32;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private void WriteInternal(uint input)
            {
                switch (_writeCount & 3)
                {
                    case 0:
                        _v1 = XXH32_round(_v1, input);
                        break;
                    case 1:
                        _v2 = XXH32_round(_v2, input);
                        break;
                    case 2:
                        _v3 = XXH32_round(_v3, input);
                        break;
                    case 4:
                        _v4 = XXH32_round(_v4, input);
                        break;
                }
                _writeCount++;
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private static U32 XXH_rotl32(U32 x, int r)
            {
                return (x << r) | (x >> (32 - r));
            }

            [MethodImpl((MethodImplOptions)0x100)] // Aggressive inlining for all .NETs
            private static U32 XXH32_round(U32 acc, U32 input)
            {
                acc += input * PRIME32_2;
                acc = XXH_rotl32(acc, 13);
                acc *= PRIME32_1;
                return acc;
            }
        }
    }
}