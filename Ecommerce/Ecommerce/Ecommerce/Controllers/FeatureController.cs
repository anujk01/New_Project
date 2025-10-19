using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly ILogger<FeatureController> _logger;
        private readonly Customer _customer;

        public FeatureController(ILogger<FeatureController> logger, Customer customer)
        {
            _logger = logger;
            _customer = customer;
        }
        [HttpPost]
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

        [HttpPost("find")]
        public int Find(int n)
        {
            // Input: Read a single integer n
            //int n = int.Parse(Console.ReadLine());

            // Initialize the total count of print function calls
            int printCalls = 0;

            // Outer loop (i-loop)
            for (int i = 0; i < n; i++)
            {
                // First inner loop (j-loop)
                for (int j = i; j >= 0; j--)
                {
                    // Second inner loop (k-loop)
                    for (int k = i; k >= i - 1; k--)
                    {
                        // Each time the innermost loop runs, we increment the printCalls
                        printCalls++;
                    }
                }
            }

            // Output: Print the total number of times print() was called
            Console.WriteLine(printCalls);
            return printCalls;
        }
    }
}


