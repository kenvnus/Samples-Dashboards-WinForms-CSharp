using Stimulsoft.Report;
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Export_Dashboard
{
    public partial class FormMain : Form
    {
        public FormMain()
        {           
            InitializeComponent();

            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        private StiReport GetTemplate()
        {
            var report = StiReport.CreateNewDashboard();
            Contract.Requires(File.Exists("Dashboards\\Christmas.mrt"));
            report.Load("Dashboards\\Christmas.mrt");

            return report;
        }

        private void buttonPdf_Click(object sender, EventArgs e)
        {
            var report = GetTemplate();
            
            saveFileDialog.FileName = report.ReportName + ".pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Contract.Requires(!File.Exists(saveFileDialog.FileName));
                var stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                Debug.Assert(stream.Length > 0);
                report.ExportDocument(StiExportFormat.Pdf, stream);
                stream.Close();
            }
            Contract.Ensures(File.Exists(saveFileDialog.FileName), "Failed to export dashboard");
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            var report = GetTemplate();

            saveFileDialog.FileName = report.ReportName + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Contract.Requires(!File.Exists(saveFileDialog.FileName));
                var stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                Debug.Assert(stream.Length > 0);
                report.ExportDocument(StiExportFormat.Excel2007, stream);
                stream.Close();
            }
            Contract.Ensures(File.Exists(saveFileDialog.FileName), "Failed to export dashboard");
        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
            var report = GetTemplate();

            saveFileDialog.FileName = report.ReportName + ".png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Contract.Requires(!File.Exists(saveFileDialog.FileName));
                var stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                Debug.Assert(stream.Length > 0);
                report.ExportDocument(StiExportFormat.ImagePng, stream);
                stream.Close();
            }
            Contract.Ensures(File.Exists(saveFileDialog.FileName), "Failed to export dashboard");
        }
        //This method load the template file and generate a report then using the system IO file stream to save the report in Word 2007 format
        private void buttonWord_Click(object sender, EventArgs e)
        {
            var report = GetTemplate();

            saveFileDialog.FileName = report.ReportName + ".doc";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Contract.Requires(!File.Exists(saveFileDialog.FileName));
                var stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                Contract.Requires(stream.Length > 0);
                Debug.Assert(stream.Length > 0);
                report.ExportDocument(StiExportFormat.Word2007, stream);
                stream.Close();
            }
            Contract.Ensures(File.Exists(saveFileDialog.FileName), "Failed to export dashboard");
            MessageBox.Show("The dasboard is exported to " + saveFileDialog.FileName + " successfully.", "Export Dasboard");
        }
        //This method load the template file and generate a report then using the system IO file stream to save the report in HTML format
        private void buttonHTML_Click(object sender, EventArgs e)
        {
            var report = GetTemplate();

            saveFileDialog.FileName = report.ReportName + ".htm";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Contract.Requires(!File.Exists(saveFileDialog.FileName));
                var stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                Debug.Assert(stream.Length > 0);
                report.ExportDocument(StiExportFormat.Html, stream);
                stream.Close();
            }
            Contract.Ensures(File.Exists(saveFileDialog.FileName), "Failed to export dashboard");
            MessageBox.Show("The dasboard is exported to " + saveFileDialog.FileName + " successfully.", "Export Dasboard");
        }
    }
}
