﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class GlobalConstants
    {
        public const string AlertMessageKey = "AlertMessage";
        public class AlertMessage
        {
            public const string PostDeleted = "Your Post was successfully deleted.";
            public const string CommentDeleted = "Your Comment was successfully deleted.";
            public const string AnswerDeleted = "Your Answer was successfully deleted.";
        }
    }
}
