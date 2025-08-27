using System.Runtime.CompilerServices;

namespace NiCE_Home_Assignment.Services
{
    public class MatchUtteranceService
    {
        static HashSet<string> BuildWordSet(string text)
        {
            var tokens = text.ToLowerInvariant().Select(ch => char.IsLetterOrDigit(ch) ? ch : ' ').ToArray();
            return new string(tokens).Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        public bool IsMatching(string utterance, string taskKey)
        {
            // Build a dictionary of words from the taskKey
            var taskKeyWords = BuildWordSet(taskKey);

            // Build a dictionary of words from the utterance
            var utteranceWords = BuildWordSet(utterance);

            // Check if all words in the taskKey are present in the utterance
            foreach (var word in taskKeyWords)
            {
                if (!utteranceWords.Contains(word))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
