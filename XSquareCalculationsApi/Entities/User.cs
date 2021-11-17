using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XSquareCalculationsApi.Entities
{
    [Table("USERS")]
    public class User
    {
        [Column("USER_ID")]
        public int UserId { get; set; }

        [Required]
        [Column("USER_NAME", TypeName = "varchar(35)")]
        public string UserName { get; set; }

        [Required]
        [Column("USER_PASSWORD", TypeName = "text")]
        public string UserPassword { get; set; }

        [Column("PASSWORD_SALT", TypeName = "text")]
        public string PasswordSalt { get; set; }

        [Column("LIKE_NUMBER_SUM", TypeName = "int")]
        public int LikeNumberSum { get; set; }

        [Column("DEL_FLG", TypeName = "char")]
        public string DelFlg { get; set; }

        [Column("CREATED_TIME", TypeName = "datetime")]
        public DateTime CreatedTime { get; set; }

        [Column("UPDATED_TIME", TypeName = "datetime")]
        public DateTime UpdatedTime { get; set; }
    }
}
