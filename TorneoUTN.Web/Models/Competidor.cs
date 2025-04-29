using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorneoUTN.Web.Models
{
    public class Competidor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La edad debe ser mayor a 0.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "La ciudad de residencia es obligatorio")]
        public string Ciudad { get; set; }

        public int IdDisciplina { get; set; }

        public Disciplina? Disciplina { get; set; }
    }
}