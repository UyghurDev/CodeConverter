using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RepairFounderFiles
{
    /// <summary>
    /// Founder Filer Format Remove and Number Direction Correct
    /// 
    /// Program By:Sarwan
    /// @ UyghurDev.net
    /// </summary>
    public  class FounderFilter
    {

        /// <summary>
        /// ئوڭشاش
        /// فورمات بەلگىلىرىنى سۈزىۋىتىدۇ، سان، تىنىز بەلگىلىرىنىڭ يۈنۈلىشىنى توغرىلايدۇ
        /// </summary>
        /// <param name="strSource">ئەسلى تېكىست</param>
        /// <returns>ئوڭشالغان تېكىست</returns>
        public string Repair(string strSource)
        {
            return Repair(strSource,true,true,true);
        }

        /// <summary>
        /// ئوڭشاش
        /// </summary>
        /// <param name="strSource">ئەسلى تېكىست</param>
        /// <param name="canRemoveFormat">فورمات بەلگىلرىنى سۈزىۋىتەمدۇ</param>
        /// <param name="canNumberDirection">سانلارنىڭ يۈنۈلىشىنى توغرىلامدۇ</param>
        /// <param name="canReplaceSymble">تىنىش بەلگىلرىنى ئالماشتۇرامدۇ</param>
        /// <returns>ئوڭشالغان تېكىست</returns>
        public string Repair(string strSource,bool canRemoveFormat,bool canNumberDirection,bool canReplaceSymble)
        {
           
            if (canRemoveFormat)
            {
                strSource = removeFormat(strSource);
            }

            if (canNumberDirection)
            {
                strSource = reverseNumber(strSource);
            }

            if (canReplaceSymble)
            {
                strSource = ReplaceSymble(strSource);
            }
            return strSource;
        }

        /// <summary>
        /// فورمات بەلگىلىرنى سۈزۈۋىتىش
        /// </summary>
        /// <param name="strSource">ئەسلى تېكىست</param>
        /// <returns>سۈزۈۋىتىلگەن تېكىست</returns>
        private string removeFormat(string strSource)
        {
            string strTemp=  Regex.Replace(strSource, "〖[^〗]*〗", "");
            return Regex.Replace(strTemp, @"\[[^\]]*\]", "");//[FT4,5NDA]
        }

        /// <summary>
        /// سانلارنىڭ يۈنۈلىشىنى توغرىلاش
        /// </summary>
        /// <param name="strSource">ئەسلى تېكىست</param>
        /// <returns>توغرىلانغان تېكىست</returns>
        private string reverseNumber(string strSource)
        {
            return Regex.Replace(strSource, @"\b\d+\b", new MatchEvaluator(RevNum));
        }

        /// <summary>
        /// تىنىش بەلگىلرىىنى توغرىلامدۇ
        /// </summary>
        /// <param name="strSource">ئەسلى تېكىست</param>
        /// <returns>توغرىلانغان تېكىست</returns>
        private string ReplaceSymble(string strSource)
        {
            string strResult = strSource;
            Dictionary<string, string> dicReplacment = new Dictionary<string, string>();
            dicReplacment.Add("(", ")");
            dicReplacment.Add("»", "«");
            //dicReplacment.Add("〕", "〔");
            

            foreach (string str in dicReplacment.Keys)
            {
                strResult = strResult.Replace(str, "Ʊ").Replace(dicReplacment[str], str).Replace("Ʊ", dicReplacment[str]);
            }
            strResult = strResult.Replace("−", "");
            strResult = strResult.Replace("〓", "  ");//〓
            return strResult;
        }

        /// <summary>
        /// سانلارنىڭ يۈنۈلىشىنى توغرىلاش
        /// </summary>
        /// <param name="m">ئەسلى سانلار</param>
        /// <returns>تۈزۈتىلگەندىن كىيىنكى سانلار</returns>
        static string RevNum(Match m)
        {
            char[] chrArray = m.ToString().ToCharArray();
            Array.Reverse(chrArray);
            string strReversed = new string(chrArray);
            return strReversed;
        }
    }
}
