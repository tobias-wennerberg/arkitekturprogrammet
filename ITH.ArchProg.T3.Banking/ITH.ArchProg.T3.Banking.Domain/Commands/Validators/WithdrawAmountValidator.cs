using FluentValidation;
using ITH.ArchProg.T3.Banking.Domain.Entities.BankAccountAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Commands.Validators
{
    public class WithdrawAmountValidator : AbstractValidator<ValidatonContext<WithdrawAmount, BankAccount>>
    {
        internal WithdrawAmountValidator()
        {
            RuleFor(context => context.Command.Amount).LessThanOrEqualTo(BankAccount.MaxWithdrawLimit)
                .WithMessage($"Withdraw must be less than or equal to {BankAccount.MaxWithdrawLimit}");
            RuleFor(context => context.Command.Amount).LessThanOrEqualTo(context => context.Entity.Amount)
                .WithMessage($"Withdraw must be less than or equal to bank account balance");
            RuleFor(context => context.Command.Amount).GreaterThan(0)
                .WithMessage("Withdraw must be greater than 0");
        }
    }
}
