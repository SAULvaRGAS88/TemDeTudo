using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace TemDeTudo.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O Nome deve ter entre 3 e 60 caracteres")]
        [Display(Name = "Nome do Vendedor")]
        public string Name { get; set; }

        [EmailAddress (ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Salário Base")]
        [Range(1400, 50000, ErrorMessage = "Salário Inválido")]
        public Decimal Salary { get; set; }

        public int DepatmentId { get; set; }

        public Depatment Depatment { get; set; }

        public List<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
    }


}
