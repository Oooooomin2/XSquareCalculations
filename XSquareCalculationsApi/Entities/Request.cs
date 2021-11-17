using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XSquareCalculationsApi.Entities
{
    [Table("REQUESTS")]
    public class Request
    {
        [Key]
        [Column("REQUEST_ID")]
        public int RequestId { get; set; }

        [Column("REQUEST_CONTENT", TypeName = "varchar(400)")]
        public string RequestContent { get; set; }

        [Column("USER_ID", TypeName = "int")]
        public int UserId { get; set; }

        [Required]
        [Column("DEL_FLG", TypeName = "char")]
        public string DelFlg { get; set; }

        [Required]
        [Column("CREATED_TIME", TypeName = "Datetime")]
        public DateTime CreatedTime { get; set; }

        [Column("UPDATED_TIME", TypeName = "Datetime")]
        public DateTime UpdatedTime { get; set; }
    }
}
