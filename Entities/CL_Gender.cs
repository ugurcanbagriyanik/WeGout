using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class CL_Gender
    {
        [Key]
        public long Id { get; set; }

        [StringLength(20, ErrorMessage = "Bu alan 20 karakterden fazla olamaz.")]
        public string Name { get; set; } = "";
    }
}
