using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace proizpract.Extension
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether a string is a palindrome, ignoring case and non-alphanumeric characters.
        /// </summary>
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            string cleaned = Regex.Replace(input, @"[^\p{L}\p{N}]", "").ToLower();
            int left = 0;
            int right = cleaned.Length - 1;

            while (left < right)
            {
                if (cleaned[left] != cleaned[right])
                {
                    return false;
                }
                left++;
                right--;
            }

            return true;
        }

        /// <summary>
        /// Counts the number of words in a string, separating them by spaces and punctuation marks.
        /// </summary>
        public static int WordCount(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            return Regex.Matches(input, @"[\p{L}\p{N}]+").Count;
        }

        /// <summary>
        /// Returns a new string in which the characters are in reverse order.
        /// </summary>
        public static string ReverseString(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
