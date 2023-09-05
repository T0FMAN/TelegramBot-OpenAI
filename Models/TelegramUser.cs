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
        public DateTime? RegestrationDate { get; init; }
        public DateTime? LastActionDate { get; set; }
        public UserAction UserAction { get; set; } = UserAction.None;
        [ForeignKey(nameof(InvitedUser))]
        public int? IdInvited { get; init; } // ID user, who invited (not TelegramId)
        public TelegramUser? InvitedUser { get; set; }
        public int? CountReferals { get; set; } = 0;

        public TelegramUser() { }

        /// <summary>
        /// For first launch bot (pre reg)
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="userName"></param>
        /// <param name="bio"></param>
        /// <param name="isReg"></param>
        /// <param name="regestrationDate"></param>
        /// <param name="lastActionDate"></param>
        /// <param name="userAction"></param>
        /// <param name="idInvited"></param>
        /// <param name="invitedUser"></param>
        /// <param name="countReferals"></param>
        public TelegramUser(long telegramId,
                            string? userName,
                            string? bio,
                            DateTime dateTime,
                            UserAction userAction)
        {
            TelegramId = telegramId;
            UserName = userName;
            Bio = bio;
            RegestrationDate = dateTime;
            LastActionDate = dateTime;
            UserAction = userAction;
        }
    }
}
