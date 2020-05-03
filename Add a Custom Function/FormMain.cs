using Stimulsoft.Data.Extensions;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Add_a_Custom_Function
{
    public partial class FormMain : Form
    {
        public class MyClass
        {
            public static decimal MySum(object value)
            {
                //Debug.Assert(value != null);
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
            //dictionary variable to hold the template names and its values (the path of the template file.)
            //these variables will be binded to datasource of the drop down list cmbTemplates
            var dict = new Dictionary<string, string>
            {
                {"Select a template",""},
                {"Christmas","Dashboards\\Christmas.mrt"},
                {"Exchange Tenders","Dashboards\\Exchange Tenders.mrt"},
                {"Fast Food Lunch","Dashboards\\Fast Food Lunch.mrt"}
            };
            //binding the dict collection to the datasource for the drop down list
            cmbTemplates.DataSource = new BindingSource(dict, null);
            cmbTemplates.DisplayMember = "Key";
            cmbTemplates.ValueMember = "Value";
            //disable the button "Open dashboard Designer" till users select an item from the drop down list.
            buttonDesigner.Enabled = false;
            AddCustomFunction();
            // Associate the event-handling method with the SelectedIndexChanged event.
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
        // This method is called when the user changes his or her selection.
        // It will enable the button "Open Dashboard Designer" if the selected item is not the first one (prompt "Select an item").
        // otherwise prompt a user to select a template.
        private void cmbTemplates_SelectedIndexChanged(Object sender, System.EventArgs e) 
        {
            if (cmbTemplates.SelectedItem == null || cmbTemplates.SelectedIndex == 0)
            {
                // The first item from the the template drop down list is the prompt "Select an item"
                buttonDesigner.Enabled = false;
                MessageBox.Show("Please select a template from drop down list.", "Edit Dashboard In Designer-FormMain");
                cmbTemplates.Focus();
            }
            else
            {
                //a template is selected, enable the button "Open dashboard Designer"
                buttonDesigner.Enabled = true;
            }               
        }
        //Function will load the template file into the report object and create a new dashboard.       
        private void buttonDesigner_Click(object sender, EventArgs e)
        {
            //instantiate a new dashboard by using the StiReport library
            var report = StiReport.CreateNewDashboard();
            //The first item at position 0 is the prompt "Select an Item"
            if (cmbTemplates.SelectedIndex != 0)
            {
                string key = ((KeyValuePair<String, String>)cmbTemplates.SelectedItem).Key;
                //Retrieve the path of the template file from the template drop down list 
                string value = ((KeyValuePair<String, String>)cmbTemplates.SelectedItem).Value;
                
                //Debug.Assert(File.Exists(value));
                //loading a report template from the file (value is the path of the file)
                report.Load(value);
            }
            else //default template is Christmas.mrt
            {
                //Debug.Assert(Directory.Exists("Dashboards\\Christmas.mrt"));
                report.Load("Dashboards\\Christmas.mrt");               
            }
            // Calls the designer for the report in the Modal window.
            report.Design();
        }
    }
}
