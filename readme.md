# smash [![Build status](https://ci.appveyor.com/api/projects/status/ig5kv8r63bqjsd9a?svg=true)](https://ci.appveyor.com/project/xoofx/smash)   [![NuGet](https://img.shields.io/nuget/v/Smash.svg)](https://www.nuget.org/packages/Smash/)

<img align="right" width="128px" height="128px" src="img/smash.png">

Smash is a lightweight library that provides a collection of non-cryptographic hashes for .NET

```C#
// Computes the hash of the following int32
var hash = xxHash.Create64();
hash.Write(1);
hash.Write(2);
hash.Write(3);
hash.Write(4);
var value = hash.Compute();
```

## Features

- Provides an implementation for the following hash methods:
  - [X] [**`xxHash`**](https://github.com/Cyan4973/xxHash)
  - TODO: If you are looking for another hash method, PR are welcome!
- **Pure .NET implementation**
- **32bits and 64bits hashes** whenever they are provided by the underlying method
- Fast computation using **aggressive inlining**
- **Zero allocation** library, using only structs, no managed objects.
- Allow **progressive hash computation**, the hash state is updated on each `Write` but the hash is computed once `Compute` is called
  - Allows incremental hashes, copy of hashes before appending new data...etc.
- Allows to compute the **hash for structured data** (`Write(int32)`, `Write(short)`...etc.) and not only for a `byte[]`
  - This is useful when computing the hash of some datas without having to serialize them to a `byte[]`
- Provides a generic [`HashStream<THash>`](src/Smash/HashStream.cs) to compute update a hash while reading/writing from another stream.

## Note

The API is **write count and type dependent**, meaning that the following are not generating the same hash, despite their in-memory representation is the same.

```C#
var hash1 = xxHash.Create64();
hash1.Write((byte)0xFF);
hash1.Write((byte)0xFF);

var hash2 = xxHash.Create64();
hash2.Write((ushort)0xFFFF);

// Note that hash1 != hash2 in this case
```

If you want to be binary compatible for any writes, you will have to use just once the methods `Write(byte[], int, int)` or `Write(IntPtr, ulong)`, as you would do with regular hashes functions.

## Binaries

Smash is available as a NuGet package: [![NuGet](https://img.shields.io/nuget/v/Smash.svg)](https://www.nuget.org/packages/Smash/)

Compatible with the following .NET framework profiles:

- `.NET3.5`
- `.NET4.0+`
- `UAP10.0+`
- `NetStandard1.1` running on `.NET4.5+` and `CoreCLR` runtimes

Also [Smash.Signed](https://www.nuget.org/packages/Smash.Signed/) NuGet package provides signed assemblies.

## License

This software is released under the [BSD-Clause 2 license](http://opensource.org/licenses/BSD-2-Clause). 

## Credits

Smash is just an implementation of existing hash methods. All the credits should go to the following libraries.

* [**`xxHash`**](https://github.com/Cyan4973/xxHash)

Adapted logo `Puzzle` by [Andrew Doane](https://thenounproject.com/andydoane/) from the Noun Project

## Author

Alexandre Mutel aka [xoofx](http://xoofx.com).
