using NUnit.Framework;

namespace Smash.Tests
{
    [TestFixture]
    public class TestxxHash64 : TestHashBase<xxHash.Hash64>
    {
        protected override xxHash.Hash64 Create(ulong seed = 0)
        {
            return new xxHash.Hash64((uint)seed);
        }
    }

    [TestFixture]
    public class TestxxHash32 : TestHashBase<xxHash.Hash32>
    {
        protected override xxHash.Hash32 Create(ulong seed = 0)
        {
            return new xxHash.Hash32((uint)seed);
        }
    }
}