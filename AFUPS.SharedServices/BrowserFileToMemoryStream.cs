using System;
using System.Diagnostics;
using AFUPS.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace AFUPS.SharedServices
{
    public class BrowserFileToMemoryStream
    {
        private byte[] Combine(List<byte[]> arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        public FileConvertingProcess fileConvertingProcess { get; set; }

        public async Task FileConvertingAsync(IBrowserFile browserFile)
        {
            using var stream = browserFile.OpenReadStream(maxAllowedSize: 2147483648);
            using var ms = new MemoryStream();

            // Caution : Too much buffer size will make application laggy.
            var bufferSize = 25 * 1024;
            byte[] buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(bufferSize);
            int read;

            while ((read = await stream.ReadAsync(buffer, 0, bufferSize)) > 0)
            {
                fileConvertingProcess.UploadedBytes += read;
                await ms.WriteAsync(buffer, 0, read);
            }
            System.Buffers.ArrayPool<byte>.Shared.Return(buffer, clearArray: true);

            fileConvertingProcess.Bytes = ms.ToArray();
        }

    }

}

