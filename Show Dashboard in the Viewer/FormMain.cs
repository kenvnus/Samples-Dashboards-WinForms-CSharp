﻿using Stimulsoft.Dashboard.Viewer;
using Stimulsoft.Report;
using System;
using System.Windows.Forms;

namespace Show_Dashboard_in_the_Viewer
{
    public partial class FormMain : Form
    {
        private StiDashboardViewerControl ViewerControl { get; set; }

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
            report.Load("Dashboards\\Christmas.mrt");

            return report;
        }

        private void buttonDialog_Click(object sender, EventArgs e)
        {
            var report = GetTemplate();
            report.Show();
        }

        private void buttonPanel_Click(object sender, EventArgs e)
        {
            if (panelViewer.Controls.Count == 0)
            {
                ViewerControl = new StiDashboardViewerControl();
                ViewerControl.Dock = DockStyle.Fill;

                panelViewer.Controls.Add(ViewerControl);
            }

            ViewerControl.Report = GetTemplate();
        }
    }
}
