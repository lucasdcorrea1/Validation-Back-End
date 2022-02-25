using System;
using System.ComponentModel.DataAnnotations;

namespace Validation.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        [StringLength(
            10,
            MinimumLength = 3,
            ErrorMessage = "O nome de usuário deve conter entre 3 e 10 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Salário é obrigatório")]
        [Range(0, 999.99, ErrorMessage = "Você ganha muito!")]
        public double Salary { get; set; }

        [EmailInUse]
        [BlockDomain("gmail.com")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "E-mail é obrigatório")]
        public string Email { get; set; }
    }

    public class EmailInUseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (string)value == "hello@lucas.io"
                ? new ValidationResult("Este e-mail já está em uso.")
                : ValidationResult.Success;
        }
    }
    public class BlockDomainAttribute : ValidationAttribute
    {
        public string Domain { get; set; }

        public BlockDomainAttribute(string domain)
        {
            Domain = domain;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ((string)value).Contains(Domain)
                ? new ValidationResult("Domínio inválido.")
                : ValidationResult.Success;
        }
    }
}
