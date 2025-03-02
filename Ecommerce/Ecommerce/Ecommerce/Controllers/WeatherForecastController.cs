using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Customer _customer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Customer customer)
        {
            _logger = logger;
            _customer = customer;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Math")]
        public string NewMethod()
        {
            string newresult = string.Empty;
            int a = 1, b = 2, c = 3, d = 4;
            double result = a * b * c * d * 252493034;
            newresult = result.ToString();
            return newresult;
        }
        [HttpGet("Sort")]
        public int[] Arraysorting()
        {
            int[] numbers = { 2, 5, 4, 9, 8, 7 };
            Array.Sort(numbers);
            return numbers;

        }
        [HttpPost("twosum")]
        public int[] TwoSum(int[] nums, int target)
        {
            // Create a dictionary to store numbers and their indices
            Dictionary<int, int> numToIndex = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i]; // Calculate the complement

                // Check if complement exists in the dictionary
                if (numToIndex.ContainsKey(complement))
                {
                    return new int[] { numToIndex[complement], i }; // Return indices
                }

                // Store the current number with its index in the dictionary
                if (!numToIndex.ContainsKey(nums[i]))
                {
                    numToIndex[nums[i]] = i;
                }
            }

            // Return an empty array if no solution is found
            return new int[] { };
        }
        [HttpPost("solution")]
        public int Solution(int N)
        {
            int targetSum = _customer.SumOfDigits(N) * 2; // Find the target sum
            int candidate = N + 1;  // Start from the next number

            // Keep checking until we find a number whose digit sum is the target sum
            while (_customer.SumOfDigits(candidate) != targetSum)
            {
                candidate++;
            }

            return candidate;
        }
    }
}


