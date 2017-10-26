using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Smash.Tests
{
    public abstract class TestHashBase<THash> where THash : IHash
    {
        [TestCase((byte)1)]
        [TestCase((byte)128)]
        [TestCase(byte.MaxValue)]
        [TestCase(byte.MinValue)]
        public void TestByte(byte x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((char)'a')]
        [TestCase((char)257)]
        [TestCase(char.MaxValue)]
        [TestCase(char.MinValue)]
        public void TestChar(char x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((sbyte)0)]
        [TestCase((sbyte)64)]
        [TestCase(sbyte.MaxValue)]
        [TestCase(sbyte.MinValue)]
        public void TestSByte(sbyte x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((ushort)1)]
        [TestCase((ushort)128)]
        [TestCase(ushort.MaxValue)]
        [TestCase(ushort.MinValue)]
        public void Testushort(ushort x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((short)0)]
        [TestCase((short)64)]
        [TestCase(short.MaxValue)]
        [TestCase(short.MinValue)]
        public void Testshort(short x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((uint)1)]
        [TestCase((uint)128)]
        [TestCase(uint.MaxValue)]
        [TestCase(uint.MinValue)]
        public void Testuint(uint x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((int)0)]
        [TestCase((int)64)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void Testint(int x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((ulong)1)]
        [TestCase((ulong)128)]
        [TestCase(ulong.MaxValue)]
        [TestCase(ulong.MinValue)]
        public void Testulong(ulong x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase((long)0)]
        [TestCase((long)64)]
        [TestCase(long.MaxValue)]
        [TestCase(long.MinValue)]
        public void Testlong(long x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestBool(bool x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase(0.0f)]
        [TestCase(1.0f)]
        [TestCase(5.0f)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void TestFloat(float x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase(0.0)]
        [TestCase(1.0)]
        [TestCase(5.0)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void TestDouble(double x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [TestCase("")]
        [TestCase("abcd")]
        [TestCase("123456789")]
        public void TestString(string x)
        {
            var hash = Create();
            hash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(hash), Compute(hash));

            var newHash = Create(1);
            newHash.Write(x);
            // Make sure that 2 consecutive Compute on the same hash are equal
            Assert.AreEqual(Compute(newHash), Compute(newHash));

            // Verify that 2 different seeds gives different results
            var v1 = ComputeAndPrint(hash);
            Assert.AreNotEqual(v1, ComputeAndPrint(newHash));
            Assert.AreNotEqual(0, v1);
        }

        [Test]
        public void TestUnsignedSigned()
        {
            {
                var hash = Create();
                hash.Write((byte)1);

                var hashSigned = Create();
                hashSigned.Write((sbyte)1);

                Assert.AreEqual(ComputeAndPrint(hash), ComputeAndPrint(hashSigned));
            }
            {
                var hash = Create();
                hash.Write((ushort)1);

                var hashSigned = Create();
                hashSigned.Write((short)1);

                Assert.AreEqual(ComputeAndPrint(hash), ComputeAndPrint(hashSigned));
            }
            {
                var hash = Create();
                hash.Write((uint)1);

                var hashSigned = Create();
                hashSigned.Write((int)1);

                Assert.AreEqual(ComputeAndPrint(hash), ComputeAndPrint(hashSigned));
            }
            {
                var hash = Create();
                hash.Write((ulong)1);

                var hashSigned = Create();
                hashSigned.Write((long)1);

                Assert.AreEqual(ComputeAndPrint(hash), ComputeAndPrint(hashSigned));
            }
        }

        /// <summary>
        /// Verifies that computing the hash for all bytes are all unique
        /// </summary>
        [Test]
        public void TestByteUnique()
        {
            var hashes = new HashSet<ulong>();
            byte i = 0;
            while (true)
            {
                var hash = Create();
                hash.Write(i);
                var value = Compute(hash);

                // We make sure that for all bytes, we will never find another same hash
                Assert.True(hashes.Add(value));

                if (i == byte.MaxValue)
                {
                    break;
                }
                i++;
            }
        }

        /// <summary>
        /// Verifies that computing the hash for all ushorts are all unique
        /// </summary>
        [Test]
        public void TestUShortUnique()
        {
            var hashes = new HashSet<ulong>();
            ushort i = 0;
            while (true)
            {
                var hash = Create();
                hash.Write(i);
                var value = Compute(hash);

                // We make sure that for all bytes, we will never find another same hash
                Assert.True(hashes.Add(value));

                if (i == ushort.MaxValue)
                {
                    break;
                }
                i++;
            }
        }

        /// <summary>
        /// Check that different length generates different hash even if values are the same
        /// </summary>
        [Test]
        public void TestLength()
        {
            var hash1 = Create();
            hash1.Write((byte)1);
            var value1 = ComputeAndPrint(hash1);

            var hash2 = Create();
            hash2.Write((short)1);
            var value2 = ComputeAndPrint(hash2);

            var hash3 = Create();
            hash3.Write((int)1);
            var value3 = ComputeAndPrint(hash3);

            var hash4 = Create();
            hash4.Write((long)1);
            var value4 = ComputeAndPrint(hash4);

            Assert.AreNotEqual(value1, value2);
            Assert.AreNotEqual(value1, value3);
            Assert.AreNotEqual(value1, value4);
            Assert.AreNotEqual(value2, value3);
            Assert.AreNotEqual(value2, value4);
            Assert.AreNotEqual(value3, value4);
        }

        /// <summary>
        /// Check that writes of the same value generates different hash
        /// </summary>
        [Test]
        public void TestSameValueDifferentWrites()
        {
            var hash1 = Create();
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            hash1.Write((byte)0xFF);
            var value1 = ComputeAndPrint(hash1);

            var hash2 = Create();
            hash2.Write((ushort)0xFFFF);
            hash2.Write((ushort)0xFFFF);
            hash2.Write((ushort)0xFFFF);
            hash2.Write((ushort)0xFFFF);
            var value2 = ComputeAndPrint(hash2);

            var hash3 = Create();
            hash3.Write((uint)0xFFFFFFFF);
            hash3.Write((uint)0xFFFFFFFF);
            var value3 = ComputeAndPrint(hash3);

            var hash4 = Create();
            hash4.Write((ulong)0xFFFFFFFFFFFFFFFF);
            var value4 = ComputeAndPrint(hash4);

            Assert.AreNotEqual(value1, value2);
            Assert.AreNotEqual(value1, value3);
            Assert.AreNotEqual(value1, value4);
            Assert.AreNotEqual(value2, value3);
            Assert.AreNotEqual(value2, value4);
            Assert.AreNotEqual(value3, value4);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(27)]
        [TestCase(63)]
        [TestCase(64)]
        [TestCase(65)]
        [TestCase(128)]
        [TestCase(129)]
        [TestCase(256)]
        [TestCase(257)]
        [TestCase(258)]
        [TestCase(259)]
        [TestCase(260)]
        [TestCase(1023)]
        [TestCase(1024)]
        [TestCase(1025)]
        public unsafe void TestByteArrayAndStream(int length)
        {
            var array = new byte[length];
            var random = new Random(0);
            random.NextBytes(array);

            var hash = Create();
            hash.Write(array, 0, array.Length);
            var value = ComputeAndPrint(hash);

            var hash1 = Create();
            fixed (void* parray = array)
            {
                hash1.Write(new IntPtr(parray), (ulong)array.Length);
            }
            var value1 = ComputeAndPrint(hash);

            var stream = new MemoryStream();
            var hashStream = new HashStream<THash>(stream, Create());
            hashStream.Write(array, 0, array.Length);
            var valueStreamWrite = ComputeAndPrint(hashStream.Hash);

            // Check that backend stream is fine as well
            var outputArray = stream.ToArray();
            Assert.AreEqual(array, outputArray);

            // Reset stream
            hashStream.Position = 0;
            hashStream.Hash = Create();

            var inputArray = new byte[array.Length];
            var readCount = hashStream.Read(inputArray, 0, array.Length);
            Assert.AreEqual(array.Length, readCount);
            var valueStreamRead = ComputeAndPrint(hashStream.Hash);

            Assert.AreEqual(value, value1);
            Assert.AreEqual(value, valueStreamWrite);
            Assert.AreEqual(value, valueStreamRead);
        }

        protected abstract THash Create(ulong seed = 0);

        private ulong Compute(THash hash)
        {
            return hash is IHash64 ? ((IHash64) hash).Compute() : ((IHash32) hash).Compute();
        }

        private ulong ComputeAndPrint(THash hash)
        {
            if (hash is IHash64)
            {
                var value = ((IHash64) hash).Compute();
                Console.WriteLine($"0x{value:X16}");
                return value;
            }
            else
            {
                var value = ((IHash32) hash).Compute();
                Console.WriteLine($"0x{value:X8}");
                return value;
            }
        }
    }
}