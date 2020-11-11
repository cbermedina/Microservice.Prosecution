namespace Microservice.Prosecution.Common.Helpers
{
    using System;
    using System.Text.RegularExpressions;
    /// <summary>
    /// General validations
    /// </summary>
    public class Validations
    {
        /// <summary>
        ///Validate wheter or no number
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }
    }
}
