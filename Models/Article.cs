using System.ComponentModel;
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
		[Display(Name = "Tiêu đề")]
		public required string Title { get; set; }

		[DataType(DataType.Date)]
		[Required]
		[DisplayName("Ngày đăng")]
		public DateTime PublishDate { get; set; }

		[Column(TypeName = "ntext")]
		[DisplayName("Nội dung")]
		public string Content { set; get; }
	}

}
