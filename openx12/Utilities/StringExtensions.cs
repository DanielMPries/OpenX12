namespace openx12.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns as stirng as the single item in a collection
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] AsSplitDelimiter(this string s)
        {
            return new[] { s };
        }
    }
}
