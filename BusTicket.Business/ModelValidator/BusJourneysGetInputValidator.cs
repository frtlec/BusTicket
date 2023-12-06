using BusTicket.Business.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business.ModelValidator
{
    public class BusJourneysGetInputValidator : AbstractValidator<BusJourneysGetInput>
    {
        public BusJourneysGetInputValidator()
        {

            RuleFor(f => f).Must(f => (f.OriginId == f.DestinationId) == false).WithMessage("Nereden-Nereye eşit olamaz");
            RuleFor(f => f.DestinationId).NotEqual(0);
            RuleFor(f => f.OriginId).NotEqual(0);
            RuleFor(x => x.Date)
          .Must(BeValidDate)
          .WithMessage("Tarih bugünden küçük olamaz!");
        }
        private bool BeValidDate(DateTime date)
        {
            return date.Date >= DateTime.Today;
        }
    }
}
