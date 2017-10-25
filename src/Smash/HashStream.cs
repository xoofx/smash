// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license. 
// See license.txt file in the project root for full license information.
using System;
using System.IO;

namespace Smash
{
    /// <summary>
    /// A Stream proxy that allows to compute a hash while reading or writing from a backend <see cref="Stream"/>.
    /// </summary>
    /// <typeparam name="THash"></typeparam>
    public class HashStream<THash> : Stream where THash : IHash
    {
        private readonly Stream _backendStream;

        /// <summary>
        /// Initialize a new instance of <see cref="HashStream{THash}"/>
        /// </summary>
        /// <param name="backendStream">The backend stream that will be used to read/write data to it</param>
        /// <param name="hash">The hash method to use</param>
        public HashStream(Stream backendStream, THash hash)
        {
            Hash = hash;
            _backendStream = backendStream ?? throw new ArgumentNullException(nameof(backendStream));
        }

        /// <summary>
        /// Hash passed to the constructor, accessible on the field directly in order to allow reset.
        /// </summary>
        public THash Hash;

        public override void Flush()
        {
            _backendStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _backendStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _backendStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var length = _backendStream.Read(buffer, offset, count);
            Hash.Write(buffer, offset, length);
            return length;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _backendStream.Write(buffer, offset, count);
            Hash.Write(buffer, offset, count);
        }

        public override bool CanRead => _backendStream.CanRead;

        public override bool CanSeek => _backendStream.CanSeek;

        public override bool CanWrite => _backendStream.CanWrite;

        public override long Length => _backendStream.Length;

        public override long Position
        {
            get => _backendStream.Position;
            set => _backendStream.Position = value;
        }
    }
}