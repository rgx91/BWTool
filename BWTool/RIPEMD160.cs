using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BWTool
{
public static class RIPEMD160
{
    /// <summary>
    /// Compute RIPEMD-160 hash of a byte array.
    /// </summary>
    public static byte[] ComputeHash(byte[] data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        var digest = new RipeMD160Digest();
        digest.BlockUpdate(data, 0, data.Length);

        var result = new byte[digest.GetDigestSize()];
        digest.DoFinal(result, 0);
        return result;
    }

    /// <summary>
    /// Compute RIPEMD-160 hash of a ReadOnlySpan&lt;byte&gt;.
    /// </summary>
    public static byte[] ComputeHash(ReadOnlySpan<byte> data)
    {
        var digest = new RipeMD160Digest();
        if (!data.IsEmpty)
        {
            // BlockUpdate takes byte[], so copy the span once
            byte[] buffer = data.ToArray();
            digest.BlockUpdate(buffer, 0, buffer.Length);
        }

        var result = new byte[digest.GetDigestSize()];
        digest.DoFinal(result, 0);
        return result;
    }

    /// <summary>
    /// Compute RIPEMD-160 hash of a stream (sync).
    /// </summary>
    public static byte[] ComputeHash(Stream stream, int bufferSize = 1024 * 64)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        var digest = new RipeMD160Digest();
        byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);

        try
        {
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                digest.BlockUpdate(buffer, 0, read);
            }

            var result = new byte[digest.GetDigestSize()];
            digest.DoFinal(result, 0);
            return result;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// Compute RIPEMD-160 hash of a stream (async).
    /// </summary>
    public static async Task<byte[]> ComputeHashAsync(Stream stream, int bufferSize = 1024 * 64, CancellationToken ct = default)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        var digest = new RipeMD160Digest();
        byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);

        try
        {
            int read;
            while ((read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), ct)) > 0)
            {
                digest.BlockUpdate(buffer, 0, read);
            }

            var result = new byte[digest.GetDigestSize()];
            digest.DoFinal(result, 0);
            return result;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// Hex helpers (uppercase by default). Use ToLowerInvariant() if you prefer lowercase.
    /// </summary>
    public static string ComputeHashHex(byte[] data) => Convert.ToHexString(ComputeHash(data));
    public static string ComputeHashHex(Stream stream, int bufferSize = 1024 * 64) => Convert.ToHexString(ComputeHash(stream, bufferSize));
}
}
