using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Feedback.RazorPages.Models;
public class FeedbackModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Código Feedback")]
    public int IdFeedback { get; set; }

    [Required(ErrorMessage = "O nome do cliente é obrigatório."), MaxLength(100), MinLength(3)]
    [Display(Name = "Nome Cliente")]
    public string? NomeCliente { get; set; }

    [Required(ErrorMessage = "O e-mail do cliente é obrigatório."), MaxLength(100), MinLength(3)]
    [Display(Name = "E-mail Cliente")]
    public string? EmailCliente { get; set; }

    [Required(ErrorMessage = "A data do feedback é obrigatória.")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
    [Display(Name = "Data Feedback")]
    public DateTime? DataFeedback { get; set; }

    [Required(ErrorMessage = "O comentário é obrigatório.")]
    [MaxLength(500), MinLength(5)]
    [Display(Name = "Comentário")]
    public string? Comentario { get; set; }

    [Required(ErrorMessage = "A avaliação é obrigatória.")]
    [Range(1, 5, ErrorMessage = "A avaliação deve ser entre 1 e 5.")]
    [Display(Name = "Avaliação")]
    public int? Avaliacao { get; set; }


}