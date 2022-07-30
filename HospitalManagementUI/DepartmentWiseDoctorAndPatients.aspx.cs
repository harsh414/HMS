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
    public partial class DepartmentWiseDoctorAndPatients : System.Web.UI.Page
    {
        IDataAccess<Doctor, int> doctorDbAccess;
        IDataAccess<Department, int> deptDbAccess;
        IDataAccess<Patient, int> patientDbAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            deptDbAccess = new DepartmentDataAccess();
            doctorDbAccess = new DoctorDataAccess();
            patientDbAccess = new PatientDataAccess();
            if (!IsPostBack)
            {
                ListItem f_li = new ListItem("Select", "");
                ddlDepartment.Items.Add(f_li);
                foreach (var dept in deptDbAccess.Get())
                {
                    ListItem li = new ListItem($"{dept.dept_name}", dept.id.ToString());
                    ddlDepartment.Items.Add(li);
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            gvDoctors.DataSource = doctorDbAccess.Get();
            gvDoctors.DataBind();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoctorDataAccess dda = new DoctorDataAccess();
            gvDoctors.DataSource = dda.GetDoctorsForADept(Convert.ToInt32(ddlDepartment.SelectedValue));
            gvDoctors.DataBind();
        }

        protected void gvDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cells = gvDoctors.SelectedRow.Cells;
            int dr_id = Convert.ToInt32(cells[0].Text);
            PatientDataAccess pad = new PatientDataAccess();
            gvPatients.DataSource = pad.GetPatientsForADoctor(dr_id);
            gvPatients.DataBind();
        }

        protected void gvPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cells = gvPatients.SelectedRow.Cells;
            int p_id = Convert.ToInt32(cells[0].Text);
            int recommended_by_dr_id = Convert.ToInt32(cells[1].Text);
            Response.Redirect("AssignmentOFPatient.aspx?dr_id=" + recommended_by_dr_id+"&p_id="+p_id);
        }
    }
}