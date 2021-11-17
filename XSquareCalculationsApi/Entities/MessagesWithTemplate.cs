using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XSquareCalculationsApi.Entities
{
    [Table("MESSAGES_WITH_TEMPLATE")]
    public class MessagesWithTemplate
    {
        [Key]
        [Column("MESSAGE_ID")]
        public int MessageId { get; set; }

        [Column("MESSAGE", TypeName = "varchar(140)")]
        public string Message { get; set; }

        [Column("USER_ID", TypeName = "int")]
        public int UserId { get; set; }

        [Column("TEMPLATE_ID", TypeName = "int")]
        public int TemplateId { get; set; }

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
