using Ecommerce.Controllers;
using Ecommerce.Interface;

namespace Ecommerce.Data
{
    public class Customer
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Customer> _logger;
        public Customer(IConfiguration configuration, ILogger<Customer> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public int SumOfDigits(int num)
        {
            int sum = 0;
            while (num > 0)
            {
                sum += num % 10;  // Extract last digit and add to sum
                num /= 10;        // Remove last digit
            }
            return sum;
        }
    }
    
}
