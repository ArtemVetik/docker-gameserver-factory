using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Server
    {
        [Key]
        public string JoinCode { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public string Port7777 { get; set; }

        [Required]
        public string Port7778 { get; set; }

        [Required]
        public string ContainerId { get; set; }
    }
}
