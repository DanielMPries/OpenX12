namespace openx12.Models
{
    public class FormattingOptions
    {
        public static FormattingOptions DefaultOptions = new FormattingOptions();
        public string SegmentTerminator { get; set; } = "~";

        public string ElementSeparator { get; set; } = "*";

        public string ComponentSeparator { get; set; } = ":";

        public string RepetitionSeparator { get; set; } = "^";

        public string DateFormat { get; set; } = Constants.DateFormatting.CCYYMMDD;

        public string TimeFormat { get; set; } = Constants.TimeFormatting.HHMMSS;

        public FormattingOptions() { }
    }
}
