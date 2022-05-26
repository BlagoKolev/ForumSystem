using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface IAnswerService
    {
        Task<int> DeleteAnswer(int answerId);
    }
}
