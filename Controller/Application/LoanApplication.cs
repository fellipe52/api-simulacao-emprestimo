using Controller.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.Dtos;
using UseCase.UseCase.Interfaces;

namespace Controller.Application
{
    public class LoanApplication : ILoanApplication
    {
        private readonly ILoanUseCase _loanUseCase;
        public LoanApplication(ILoanUseCase loanUseCase)
        {
            _loanUseCase = loanUseCase;
        }

        public async Task CreateLoan(LoanRequest loanUseCase)
        {
            await _loanUseCase.CreateLoanAsync(loanUseCase);
        }
    }
}