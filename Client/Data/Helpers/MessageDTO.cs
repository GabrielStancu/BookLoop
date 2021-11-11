using Data.Models;
using System;
using Xamarin.Forms;

namespace Data.Helpers
{
    public class MessageDTO
    {
        public string MessageContent { get; set; }
        public BookUser Sender { get; set; }
        public int CurrentUserId { get; set; }
        public int ConversationId { get; set; }
        public ConversationType ConversationType { get; set; }
        public DateTime SendTime { get; set; }
        public bool OtherUserMessage
        {
            get
            {
                return (Sender.Id == CurrentUserId) ? false : true;
            }
        }

        public string ColorMessage
        {
            get
            {
                return (Sender.Id == CurrentUserId) ? "#43aa8b" : "LightGray";
            }
        }

        public string TextColorMessage
        {
            get
            {
                return (Sender.Id == CurrentUserId) ? "White" : "Black";
            }
        }

        public LayoutOptions MessageOrientation
        {
            get
            {
                return (Sender.Id == CurrentUserId) ? LayoutOptions.End : LayoutOptions.Start;
            }
        }
    }
}
