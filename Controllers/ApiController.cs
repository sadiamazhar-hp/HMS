using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V._3._0.App_Data;
using V._3._0.Interfaces;
using V._3._0.Models;
using System.Text.Json;
using Microsoft.Identity.Client;

namespace V._3._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HospitalData db;
        private readonly IPatients patientsData;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ApiController(HospitalData db, IPatients patientsdata, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.patientsData = patientsdata;
            this.webHostEnvironment = webHostEnvironment;
        }
        List<Patients> patients;
        //Get all patients Present in the database
        [HttpGet("AllPatients")]
        public IActionResult AllPatients()
        {
            patients = db.Patients.ToList();
            string Json = JsonSerializer.Serialize(patients);
            return Ok(Json);
        }
        //Get only the Searched Patients 
        [HttpGet("Search")]
        public IActionResult GetPatientsBySearch(string searchinput) 
        {
            if (searchinput == null)
            {
                return RedirectToAction("AllPatirnts", "Api");
            }
            IEnumerable<Patients> patients = patientsData.GetPatientByName(searchinput);
            string Json =JsonSerializer.Serialize(patients);
            return Ok(Json);
        }
        //Delete Patient using ID
        [HttpPost("PatientDelete")]
        public IActionResult OnDelete(int patientID)
        {
            var patient = db.Patients.Find(patientID);
            if(patient != null) {
                db.Patients.Remove(patient);
                db.SaveChanges();
                return Ok("Patient Deleted Succesfully");
            }
            return BadRequest("Your Request Could not be Completed");
        }
        
        //For Adding New Patients
        [HttpPost("NewPatient")]
        public IActionResult NewPatient([FromBody] NewPatientDTO newpatient)
        {
            var Patient = new Patients
            {
                Name = newpatient.Name,
                Roomno = newpatient.Roomno,
                DateOfAdm = newpatient.DateOfAdm,
                Status = newpatient.Status,
                DateOfDis = newpatient.DateOfDis
            };
            if (newpatient == null)
            {
                return BadRequest("Invalid Patient Request");
            }

            patientsData.Add(Patient);
            return Ok("Patient Added Successfully");
        }

    }
}
