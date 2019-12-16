namespace openx12.Models.DataElementIndex
{
    public class DataIndex
    {
        /// <summary>
        /// The Transaction Position number
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// The Transaction Loop Name
        /// </summary>
        /// <value></value>
        public string Loop { get; set; }

        /// <summary>
        /// The Interation Index of the Loop
        /// </summary>
        /// <value></value>
        public int LoopIteration { get; set; }

        public System.Guid Id { get; set; }
        public System.Guid ParentId { get; set; }
    }
}
