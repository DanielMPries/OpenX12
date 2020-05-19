using System.Threading.Tasks;

namespace openx12.Utilities
{
    /// <summary>Implements functionality of a buffered stream reader</summary>
    public static class SimpleBufferedReader
    {
        private static byte[] _buffer = new byte[4096];

        /// <summary>Sets the buffer size</summary>
        /// <remarks>Recommended sizes are 4KB (4096 bytes) and 8KB (8192 bytes). Default is 4 Kilobytes.</remarks>
        public static void SetBufferSize(uint size)
        {
            _buffer = new byte[size];
        }

        /// <summary>Gets the size of the memory buffer</summary>
        public static int GetBufferSize()
        {
            return _buffer.Length;
        }

        /// <summary>Reads an entire file from a given path</summary>
        /// <param name="path">Path of the file</param>
        /// <returns>String representation of the file contents</returns>
        public static string Read(string path)
        {
            using var fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            return Read(fs);
        }

        /// <summary>Reads a given stream</summary>
        /// <param name="stream">Stream to be read</param>
        /// <returns>String representation of the stream contents</returns>
        public static string Read(System.IO.Stream stream)
        {
            var sb = new System.Text.StringBuilder();

            int bytesRead;
            while ((bytesRead = stream.Read(_buffer, 0, _buffer.Length)) > 0)
            {
                sb.Append(System.Text.Encoding.UTF8.GetString(_buffer, 0, bytesRead));
            }

            return sb.ToString();
        }

        public static async Task<string> ReadAsync(string path)
        {
            using var fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            return await ReadAsync(fs).ConfigureAwait(false);
        }

        public static async Task<string> ReadAsync(System.IO.Stream stream)
        {
            var sb = new System.Text.StringBuilder();
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(_buffer, 0, _buffer.Length).ConfigureAwait(false)) > 0)
            {
                sb.Append(System.Text.Encoding.UTF8.GetString(_buffer, 0, bytesRead));
            }
            return sb.ToString();
        }
    }
}