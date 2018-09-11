using FluentValidation;
using ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Commands.Validators
{
    public class DepositAmountValidator : AbstractValidator<DepositAmount>
    {
        internal DepositAmountValidator()
        {
            RuleFor(depositAmount => depositAmount.Amount).LessThanOrEqualTo(BankAccount.MaxDepositLimit)
                .WithMessage($"Deposit must be less than or equal to {BankAccount.MaxDepositLimit}");
            RuleFor(depositAmount => depositAmount.Amount).GreaterThan(0)
                .WithMessage("Deposit must be greater than 0");
        }
    }
}
