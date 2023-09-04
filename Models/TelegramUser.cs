using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TelegramBot_OpenAI.Data.Enums;

namespace TelegramBot_OpenAI.Models
{
    public class TelegramUser
    {
        [Key]
        public int IdUser { get; init; }
        public long TelegramId { get; init; }
        public string? UserName { get; set; }
        public string? Bio { get; set; }
        public bool IsReg { get; set; } = false;
        public DateTime? RegestrationDate { get; set; }
        public DateTime? LastActionDate { get; set; }
        public UserAction UserAction { get; set; } = UserAction.None;
        [ForeignKey(nameof(InvitedUser))]
        public int? IdInvited { get; set; } // ID user, who invited (not TelegramId)
        public TelegramUser? InvitedUser { get; set; }
        public int? CountReferals { get; set; } = 0;
    }
}
