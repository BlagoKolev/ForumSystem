using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class GlobalConstants
    {
        public const string AlertMessageKey = "AlertMessage";
        public const string AlertMessageFailKey = "AlertMessageFail";
        public class AlertMessage
        {
            public const string PostDeleted = "Your Post was successfully deleted.";
            public const string CommentDeleted = "Your Comment was successfully deleted.";
            public const string AnswerDeleted = "Your Answer was successfully deleted.";
        }

        public class AlertMessageFail
        {
            public const string CannotDeleteAnswer = "Sorry, you can not delete this Answer. Post or answer is missing.";
            public const string CannotDeleteComment = "Sorry, you can not delete this Comment. Post or Comment is missing.";
            public const string ActionFailed = "Something went  wrong, please try again.";
        }
    }
}
