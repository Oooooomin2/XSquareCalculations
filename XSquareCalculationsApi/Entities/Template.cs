using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XSquareCalculationsApi.Entities
{
    [Table("TEMPLATES")]
    public class Template
    {
        [Key]
        [Column("TEMPLATE_ID",TypeName = "integer")]
        public int TemplateId { get; set; }

        [Column("TEMPLATE_NAME", TypeName = "varchar(45)")]
        public string TemplateName { get; set; }

        [Column("TEMPLATE_BLOB", TypeName = "mediumblob")]
        public byte[] TemplateBlob { get; set; }

        [Column("THUMBNAIL", TypeName = "text")]
        public string? ThumbNail { get; set; }

        [Column("LIKE_COUNT", TypeName = "int")]
        public int LikeCount { get; set; }

        [Column("DOWNLOAD_COUNT", TypeName = "int")]
        public int DownloadCount { get; set; }

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
