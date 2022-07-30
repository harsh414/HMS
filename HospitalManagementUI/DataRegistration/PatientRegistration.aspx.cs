using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.DataAccess;
using Application.Entities;

namespace HospitalManagementUI.DataRegistration
{
    public partial class PatientRegistration : System.Web.UI.Page
    {
        IDataAccess<Patient, int> patientDbAccess;
        IDataAccess<Department, int> deptDbAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            deptDbAccess = new DepartmentDataAccess();
            patientDbAccess = new PatientDataAccess();
            if (!IsPostBack)
            {
                ListItem f_li = new ListItem("Select", "");
                ddlDepartment.Items.Add(f_li);
                foreach (var dept in deptDbAccess.Get())
                {
                    ListItem li = new ListItem($"{dept.id.ToString()}" + "-" + $"{dept.dept_name}", dept.id.ToString());
                    ddlDepartment.Items.Add(li);
                }

                patientDbAccess.Get();
                LoadData();
            }
        }

        private void LoadData()
        {
            gvPatients.DataSource = patientDbAccess.Get();
            gvPatients.DataBind();
        }

        protected void txtgender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                Patient p = new Patient()
                {
                    p_name = txtpname.Text,
                    age = Convert.ToInt32(txtage.Text),
                    gender = txtgender.Text,
                    address = (txtaddress.Text),
                    phone = (txtphone.Text),
                    dept_id= Convert.ToInt32(ddlDepartment.SelectedItem.Value)
            };
                patientDbAccess.Create(p);
                LoadData();
                //lblstatus.Text = "New Patient is Created Successfully...";

            }
            catch (Exception ex)
            {
                //lblstatus.Text = ex.Message;
            }
        }

        protected void gvPatients_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvPatients_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvPatients_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvPatients_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvPatients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int p_id = Convert.ToInt32(ddlDe.SelectedItem.Value);
           
        }
    }
}