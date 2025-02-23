using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.Dtos;

namespace Controller.Application.Interfaces
{
    public interface ILoanApplication
    {
        public Task CreateLoan(LoanRequest loanRequest);
    }
}