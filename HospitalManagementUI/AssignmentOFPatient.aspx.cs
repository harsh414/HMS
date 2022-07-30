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
    public partial class AssignmentOFPatient : System.Web.UI.Page
    {
        IDataAccess<Doctor, int> doctorDbAccess;
        IDataAccess<Department, int> deptDbAccess;
        IDataAccess<Patient, int> patientDbAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            deptDbAccess = new DepartmentDataAccess();
            doctorDbAccess = new DoctorDataAccess();
            patientDbAccess = new PatientDataAccess();
            
        }

        protected void ddlAssign_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_val = ddlAssign.SelectedValue;
            if(selected_val == "1")
            {
                int dr_id = Convert.ToInt32(Request.QueryString["dr_id"]);
                int p_id = Convert.ToInt32(Request.QueryString["p_id"]);
                Doctor doc = doctorDbAccess.Get(dr_id);
                Patient pat = patientDbAccess.Get(p_id);
                pat.is_assigned = true;
                
                PatientDataAccess pas = new PatientDataAccess();
                int new_dr_id= pas.AssignDrToDept(doc.dept_id);
                pas.AssignToIPD(pat, new_dr_id);
            }
        }
    }
}