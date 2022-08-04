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
        IDataAccess<Test, int> testDbAccess;
        IDataAccess<Medicine, int> medicineDbAccess;
        IDataAccess<Operation, int> operationDbAccess;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            deptDbAccess = new DepartmentDataAccess();
            doctorDbAccess = new DoctorDataAccess();
            patientDbAccess = new PatientDataAccess();
            testDbAccess = new TestDataAccess();
            medicineDbAccess = new MedicineDataAccess();
            operationDbAccess = new OperationDataAccess();
            if (!IsPostBack)
            {
                ddlTest.Items.Add(new ListItem("Select", ""));
                foreach (var t in testDbAccess.Get())
                {
                    ListItem li = new ListItem(t.test_name, t.id.ToString());
                    ddlTest.Items.Add(li);
                }

                ddlMedicine.Items.Add(new ListItem("Select", ""));
                foreach (var med in medicineDbAccess.Get())
                {
                    ListItem li = new ListItem(med.m_name, med.id.ToString());
                    ddlMedicine.Items.Add(li);
                }

                ddlOperation.Items.Add(new ListItem("Select", ""));
                foreach (var op in operationDbAccess.Get())
                {
                    ListItem li = new ListItem(op.op_name, op.id.ToString());
                    ddlOperation.Items.Add(li);
                }

                //check if patient is already assigned to ipd
                PatientDataAccess pas = new PatientDataAccess();
                int p_id = Convert.ToInt32(Request.QueryString["p_id"]);
                if (pas.isAlreadyAssignedToIPD(p_id))
                {
                    ddlAssign.Enabled = false;
                    ddlAssign.SelectedIndex = 1;
                    Label5.Text = "Already Assigned to IPD";
                    ddlOperation.Enabled = false;
                }
            }

        }

        protected void ddlAssign_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_val = ddlAssign.SelectedValue;
            if(selected_val == "1")
            {
                ddlOperation.Enabled = true;
                int dr_id = Convert.ToInt32(Request.QueryString["dr_id"]);
                int p_id = Convert.ToInt32(Request.QueryString["p_id"]);
                Doctor doc = doctorDbAccess.Get(dr_id);
                Patient pat = patientDbAccess.Get(p_id);
                pat.is_assigned = true;
                
                PatientDataAccess pas = new PatientDataAccess();
                int new_dr_id= pas.AssignDrToDept(doc.dept_id);
                pas.AssignToIPD(pat, new_dr_id);
                int new_nurse_id = pas.AssignNurseToDept(doc.dept_id);
                pas.seenByNurse(pat.id, new_nurse_id);
            }
        }

       

       

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            mqty.Enabled = true;
        }


        protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            testSave();
            medicineSave();

            if(ddlOperation.Enabled)
            OperationSave();
        }

        protected void testSave()
        {
            int patient_id = Convert.ToInt32(Request.QueryString["p_id"]);
            int test_id = Convert.ToInt32(ddlTest.SelectedValue);
            TestDataAccess tda = new TestDataAccess();
            tda.TestToPatient(patient_id, test_id);
        }

        protected void medicineSave()
        {
            int patient_id = Convert.ToInt32(Request.QueryString["p_id"]);
            int medicine_id = Convert.ToInt32(ddlMedicine.SelectedValue);
            int medicine_quantity = Convert.ToInt32(mqty.Text);
            MedicineDataAccess mda = new MedicineDataAccess();
            mda.InsertPatientMedicineMap(patient_id, medicine_id, medicine_quantity);
        }

        protected void OperationSave()
        {
            int patient_id = Convert.ToInt32(Request.QueryString["p_id"]);
            int operation_id = Convert.ToInt32(ddlOperation.SelectedValue);
            OperationDataAccess oda = new OperationDataAccess();
            oda.OperationToPatient(patient_id, operation_id);
        }
    }
}