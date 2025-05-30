using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVP.Project.Application.ViewModels
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        
        [DisplayName("Ducument Number")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        
        [StringLength(20, ErrorMessage = "Telefone com tamanho inválido")]
        public string Phone { get; set; }
        
        [StringLength(50)] 
        public string StateInscription { get; set; }
        [StringLength(200)] 
        public string StreetAddress { get; set; }
        [StringLength(20)] 
        public string BuildingNumber { get; set; }
        [StringLength(200)] 
        public string SecondaryAddress { get; set; }
        [StringLength(100)] 
        public string Neighborhood { get; set; }
        [StringLength(20)] 
        public string ZipCode { get; set; }
        [StringLength(100)] 
        public string City { get; set; }
        [StringLength(2)] 
        public string State { get; set; }
        
        [Required(ErrorMessage = "Ativo é um campo obrigatório")]
        public bool Active { get; set; }
    }
}
