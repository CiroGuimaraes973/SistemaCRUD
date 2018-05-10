using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace sistemaCRUD.models
{
    public class Funcionario
    {
        public int id { get; set; }
        [Required]
        [StringLength(70,MinimumLength =5)]
        public string name { get; set; }

        [Required,EmailAddress,StringLength(70,MinimumLength =5)]
        public string email { get; set; }
        [Required]
        public decimal salario { get; set; }
        [Required]
        public string sexo { get; set; }
        [Required]
        public string tipoContrato { get; set; }
        public DateTime dataCadastro { get; set; }
        public DateTime dataAtualizacao { get; set; }

    }
}
