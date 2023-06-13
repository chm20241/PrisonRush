using FluentValidation;
using Spg.GammaShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Application.Validators
{
    public class NewProductDtoValidator : AbstractValidator<ProductDTO>
    {
        public NewProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .Length(3,20)
                .WithMessage("Bitte zwischen 3 und 20!")
                .WithErrorCode("9000");
        }
        }
}
