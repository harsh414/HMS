using Application.DataAccess;
using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagementUI
{
    public partial class PatientsInIpdandRoomAssignment : System.Web.UI.Page
    {
        PatientDataAccess patientDbAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                patientDbAccess = new PatientDataAccess();
                gvIPDpatients.DataSource = patientDbAccess.GetPatientsInIpd();
                gvIPDpatients.DataBind();
            }
        }

        protected void gvIPDpatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cells = gvIPDpatients.SelectedRow.Cells;
            int p_id = Convert.ToInt32(cells[0].Text);
            Response.Redirect("AssignmentOFRoom.aspx?p_id=" + p_id);
        }
    }
}