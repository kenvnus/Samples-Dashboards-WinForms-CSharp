using Stimulsoft.Data.Extensions;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Add_a_Custom_Function
{
    public partial class FormMain : Form
    {
        public class MyClass
        {
            public static decimal MySum(object value)
            {
                if (!ListExt.IsList(value))
                    return Stimulsoft.Base.Helpers.StiValueHelper.TryToDecimal(value);

                return Stimulsoft.Data.Functions.Funcs.SkipNulls(ListExt.ToList(value))
                    .TryCastToDecimal()
                    .Sum();
            }
        }

        public FormMain()
        {
            InitializeComponent();

            var dict = new Dictionary<string, string>
            {
                {"Select a template",""},
                {"Christmas","Dashboards\\Christmas.mrt"},
                {"Exchange Tenders","Dashboards\\Exchange Tenders.mrt"},
                {"Fast Food Lunch","Dashboards\\Fast Food Lunch.mrt"}
            };
            cmbTemplates.DataSource = new BindingSource(dict, null);
            cmbTemplates.DisplayMember = "Key";
            cmbTemplates.ValueMember = "Value";
            buttonDesigner.Enabled = false;
            AddCustomFunction();
            this.cmbTemplates.SelectedIndexChanged += new System.EventHandler(cmbTemplates_SelectedIndexChanged);
            cmbTemplates.Focus();

            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
           
        }

        private void AddCustomFunction()
        {
            StiFunctions.AddFunction(
                "My Category", "MySum", "description",
                typeof(MyClass),
                typeof(decimal),
                "Calculates a sum of the specified set of values.",
                new[] { typeof(object) },
                new[] { "values" },
                new[] { "A set of values" }).UseFullPath = false;
        }

        private void cmbTemplates_SelectedIndexChanged(Object sender, System.EventArgs e) 
        {
            //Cursor = Cursors.WaitCursor;
            //Cursor = Cursors.Default;
            buttonDesigner.Enabled = true;
        }

        private void buttonDesigner_Click(object sender, EventArgs e)
        {
            var report = StiReport.CreateNewDashboard();
            if (cmbTemplates.Text.Length == 0)
            {
                string key = ((KeyValuePair<String, String>)cmbTemplates.SelectedItem).Key;
                string value = ((KeyValuePair<String, String>)cmbTemplates.SelectedItem).Value;
                report.Load(value);
            }
            else
            {
                report.Load("Dashboards\\Christmas.mrt");               
            }
            report.Design();
        }

        //private void buttonDesigner_Click(object sender, EventArgs e)
        //{
        //    var report = StiReport.CreateNewDashboard();
        //    report.Load("Dashboards\\Christmas.mrt");

        //    report.Design();
        //}
    }
}
