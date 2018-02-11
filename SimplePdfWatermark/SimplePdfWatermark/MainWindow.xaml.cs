using System;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf;
using Microsoft.Win32;
using SimplePdfWatermark.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimplePdfWatermark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _totalPagesCount;

        public int TotalPagesCount
        {
            get { return _totalPagesCount; }
            set
            {
                _totalPagesCount = value;
                OnPropertyChanged(nameof(TotalPagesCount));
            }
        }

        public WatermarkOptions Options { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Options = new WatermarkOptions();
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
            };
            if (openFileDialog.ShowDialog() == true)
            {
                txtFileName.Content = openFileDialog.FileName;
                txtFileName.Opacity = 1;
                dropFilePanel.Opacity = 1;
                txtDropFile.Content = "Selected file";
                fromNum.Text = "0";
                var pdf = new PdfDocument(new PdfReader(openFileDialog.FileName), new PdfWriter("output.pdf"));
                Options.MaxPage = pdf.GetNumberOfPages();
                toNum.Text = Options.MaxPage.ToString();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Options != null)
            {
                Options.MinPage = Int32.Parse(fromNum.Text);
                Options.MaxPage = Int32.Parse(toNum.Text);
                Options.WatermarkText = watermarkText.Text;
                //Options.LayoutMode = whatLayout();
                Options.InputFilePath = txtFileName.Content as string;
                Options.OutputFilePath = "output.txt";

                //Watermarker.ApplyWatermark();
            }
        }

        private void btnAllPages_Click(object sender, RoutedEventArgs e)
        {
            if (Options?.MaxPage != null)
            {
                fromNum.Text = "0";
                toNum.Text = Options.MaxPage.ToString();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion INotifyPropertyChanged
    }
}
