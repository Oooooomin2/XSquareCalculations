using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XSquareCalculationsApi.Entities
{
    [Table("ATHENTICATES")]
    public class Authenticate
    {
        [Key]
        [Column("USER_ID",TypeName = "integer")]
        public int UserId { get; set; }
        
        [Key]
        [Column("ID_TOKEN", TypeName = "varchar(32)")]
        public string IdToken { get; set; }

        [Column("EXPIRED_DATETIME", TypeName = "datetime")]
        public DateTime ExpiredDateTime { get; set; }

        [Column("CREATED_TIME", TypeName = "datetime")]
        public DateTime CreatedTime { get; set; }
    }
}
