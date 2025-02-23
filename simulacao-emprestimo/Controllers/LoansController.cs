using Controller.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UseCase.Dtos;

namespace video_authenticator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILogger<LoansController> _logger;

        private readonly ILoanApplication _loanApplication;

        public LoansController(ILogger<LoansController> logger, ILoanApplication loanApplication)
        {
            _logger = logger;
            _loanApplication = loanApplication;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("simulate")]
        public async Task<IActionResult> CreateLoanSimulate(LoanRequest loanRequest, CancellationToken cancellationToken)
        {
            await _loanApplication.CreateLoan(loanRequest);

            return Ok();
        }
    }
}