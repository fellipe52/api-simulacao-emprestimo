using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.Dtos;

namespace UseCase.UseCase.Interfaces
{
    public interface ILoanUseCase
    {
        public Task<LoanResponse> CreateLoanAsync(LoanRequest loanRequest);
    }
}