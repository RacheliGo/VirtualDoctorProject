using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace VitraulDoctor.Api.Endpoints
{
    [ApiController]
    [Route("[controller]")]
    public class Diagnosis : ControllerBase
    {
        [HttpPost("/api/DiagnosisDisease")]
        [SwaggerOperation(Summary = "Get Diagnosis To Disease", Tags = new[] { "DiagnosisDisease" })]
        public IActionResult DiagnosisDisease()
        {
            textAnalysis.Program.Main();
            string findDisease = textAnalysis.DiscoverDisease.FindDisease("C:\\Users\\רחלי גורנשטיין\\Desktop\\מחשב ישן\\לימודים שנה ב\\פרויקט\\Disease.xlsx");

            var jsonObject = new { disease = findDisease };
            string jsonResponse = JsonConvert.SerializeObject(jsonObject);
            return Ok(jsonResponse);
        }
    }
}