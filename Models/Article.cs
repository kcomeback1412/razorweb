using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS58_Razor09EF.Models
{
	[Table("Articles")]
	public class Article
	{
		[Key]
		public int ID { get; set; }
		[StringLength(255)]
		[Required]
		public required string Title { get; set; }

		[DataType(DataType.Date)]
		[Required]
		public DateTime PublishDate { get; set; }

		[Column(TypeName = "ntext")]
		public string Content { set; get; }
	}

}
