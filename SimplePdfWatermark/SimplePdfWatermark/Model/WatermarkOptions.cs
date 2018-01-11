namespace SimplePdfWatermark.Model
{
    public class WatermarkOptions
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public int MinPage { get; set; }
        public int MaxPage { get; set; }
        public LayoutMode LayoutMode { get; set; }
        public string WatermarkText { get; set; }
    }
}