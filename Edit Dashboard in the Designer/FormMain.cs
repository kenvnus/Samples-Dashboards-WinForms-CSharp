using Stimulsoft.Report;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Edit_Dashboard_in_the_Designer
{
    public partial class FormMain : Form
    {
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
            //Using VS Code Contract for Precondition
            foreach( KeyValuePair<string, string> kvp in dict )
            {               
                Contract.Requires(File.Exists(kvp.Value), "Failed contract: template not exists");
            }

            //binding the dict collection to the datasource for the drop down list
            cmbTemplates.DataSource = new BindingSource(dict, null);
            cmbTemplates.DisplayMember = "Key";
            cmbTemplates.ValueMember = "Value";

            //disable the button "Open dashboard Designer" till users select an item from the drop down list.
            buttonEdit.Enabled = false;
            buttonNew.Enabled = false;
            // Associate the event-handling method with the SelectedIndexChanged event.
            this.cmbTemplates.SelectedIndexChanged += new System.EventHandler(cmbTemplates_SelectedIndexChanged);
            cmbTemplates.Focus();
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        private StiReport GetTemplate()
        {
            var report = new StiReport();
            if (cmbTemplates.Text.Length != 0)
            {
                string key = ((KeyValuePair<String,String>)cmbTemplates.SelectedItem).Key;
                string value = ((KeyValuePair<String, String>)cmbTemplates.SelectedItem).Value;
                Debug.Assert(File.Exists(value));
                //loading a report template from the file (value is the path of the file)            
                report.Load(value);                              
            }
            else
            {
                Debug.Assert(File.Exists("Dashboards\\Christmas.mrt"));
                report.Load("Dashboards\\Christmas.mrt");
            }
            return report;
        }
        // This method is called when the user changes his or her selection.
        // It will enable the button "Open Dashboard Designer" if the selected item is not the first one (prompt "Select an item").
        // otherwise prompt a user to select a template.
        private void cmbTemplates_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // The first item from the the template drop down list is the prompt "Select an item"
            if (cmbTemplates.SelectedItem == null || cmbTemplates.SelectedIndex == 0)
            {
                buttonEdit.Enabled = false;
                buttonNew.Enabled = false;
                MessageBox.Show("Please select a template from drop down list.", "Edit Dashboard In Designer-FormMain");
                cmbTemplates.Focus();
            }
            //a template is selected, enable the button "Edit" and button "New"
            else
            {
                buttonEdit.Enabled = true;
                buttonNew.Enabled = true;
            }               
        }
        //This method generates a new empty dashboard in a design form 
        private void buttonNew_Click(object sender, EventArgs e)
        {
            //instantiate a new dashboard by using the StiReport library
            var report = StiReport.CreateNewDashboard();
            // Calls the designer for the report in the Modal window.
            report.Design();
        }
        //This method calls the function GetTemplate to load the selected template from drop down list into the design form.
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //Load the selected template from the drop down list.
            var report = GetTemplate();
            //Call the designer for the report in the modal window
            report.Design();
        }
    }
}
