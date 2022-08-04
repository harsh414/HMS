using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.DataAccess;
using Application.Entities;


namespace HospitalManagementUI
{
    public partial class DisplayBill : System.Web.UI.Page
    {
        IDataAccess<Patient, int> patientDbAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            patientDbAccess = new PatientDataAccess();
            if (!IsPostBack)
            {
                ListItem f_li = new ListItem("Select", "");
                ddlPatient.Items.Add(f_li);
                foreach (var patient in patientDbAccess.Get())
                {
                    ListItem li = new ListItem($"{patient.id.ToString()}" + "-" + $"{patient.p_name}", patient.id.ToString());
                    ddlPatient.Items.Add(li);
                }

            }
        }


        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int p_id = Convert.ToInt32(ddlPatient.SelectedValue);
            PatientDataAccess pac = new PatientDataAccess();
            
            if (! pac.ifBillWasGenerated(p_id))
            {
                float total_bill = pac.GetPatientTotalBill(p_id);
            }

            

            gvBill.DataSource = pac.PopulateBill(p_id);
            gvBill.DataBind();
        }
    }
}