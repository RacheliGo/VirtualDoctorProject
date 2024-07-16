using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace textAnalysis.VirtualDoctorApi.Endpoints
{
    [ApiController]
    [Route("[controller]")]
    public class BMI : ControllerBase
    {
        [HttpPost("/api/GetBMI")]
        [SwaggerOperation(Summary = "Get Calculated BMI", Tags = new[] { "BMI" })]
        public decimal[] GetBMI(int height, int weight, int age)
        {
            decimal bmi = textAnalysis.BMIresults.CalculationBMI(height, weight, age).Item1;
            int retingBMI = textAnalysis.BMIresults.CalculationBMI(height, weight, age).Item2;
            decimal[] detail = { bmi, retingBMI };
            return detail;
        }
    }
}