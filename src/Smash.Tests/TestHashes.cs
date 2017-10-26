using NUnit.Framework;

namespace Smash.Tests
{
    [TestFixture]
    public class TestxxHash64 : TestHashBase<xxHash.Hash64>
    {
        protected override xxHash.Hash64 Create(ulong seed = 0)
        {
            return xxHash.Create64((uint)seed);
        }

        protected override void Reset(ref xxHash.Hash64 hash, ulong seed = 0)
        {
            hash.Reset(seed);
        }
    }

    [TestFixture]
    public class TestxxHash32 : TestHashBase<xxHash.Hash32>
    {
        protected override xxHash.Hash32 Create(ulong seed = 0)
        {
            return xxHash.Create32((uint)seed);
        }

        protected override void Reset(ref xxHash.Hash32 hash, ulong seed = 0)
        {
            hash.Reset((uint)seed);
        }
    }
}