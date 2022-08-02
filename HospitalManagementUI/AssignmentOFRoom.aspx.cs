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
    public partial class AssignmentOFRoom : System.Web.UI.Page
    {
        IDataAccess<Room, int> roomDbAccess;
        RoomDataAccess rac= new RoomDataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            roomDbAccess = new RoomDataAccess();
            gvRoom.DataSource = roomDbAccess.Get();
            gvRoom.DataBind();
            if (rac.isPatientAllotedRoom(Convert.ToInt32(Request.QueryString["p_id"])))
            {
                //make grid disabled
                gvRoom.Enabled = false;
            }

        }

        protected void gvRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //gvRoom_SelectedIndexChanged(sender, e);
            var cells = gvRoom.SelectedRow.Cells;
            int r_id = Convert.ToInt32(cells[0].Text);
            int patient_id = Convert.ToInt32(Request.QueryString["p_id"]);
            RoomDataAccess rda = new RoomDataAccess();
            rda.RoomToPatient(patient_id, r_id);
        }
    }
}