using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace net.UyghurDev.Text
{
    #region Properties
    /// <summary>
    /// كود ئالماشتۇرۇغۇچ
    /// <para>سارۋان</para>
    /// </summary>
    public class CodeConvert
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Descraption;

        public string Descraption
        {
            get { return _Descraption; }
            set { _Descraption = value; }
        }
        private string _Author;

        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        Dictionary<char, char> Chars;
        Dictionary<string, string> CompoundChars;
        Dictionary<string, string> ReplacmentAfter;
        Dictionary<string, string> ReplacmentBefor;
    #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFileName"></param>
        public CodeConvert(string strFileName)
        {
            Chars = new Dictionary<char, char>();
            CompoundChars = new Dictionary<string, string>();
            ReplacmentAfter = new Dictionary<string, string>();
            ReplacmentBefor = new Dictionary<string, string>();
            Chars.Clear();
            CompoundChars.Clear();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            //System.IO.TextReader tr = new System.IO.StringReader(strFileName); ;


            XmlReader reader = XmlReader.Create(strFileName, settings);
            reader.Read();
           
            while (reader.Read())
            {
                if ((reader.Name == "CodeConvert") && (reader.IsStartElement()))
                {
                    Name = reader.GetAttribute("Name");
                    Descraption = reader.GetAttribute("Descraption");
                    Author = reader.GetAttribute("Author");
                }
                else if ((reader.Name.ToLower() == "code") && (reader.IsStartElement()))
                {
                    if (reader.GetAttribute("isCompound").ToLower() == "true")
                    {
                        CompoundChars.Add(reader.GetAttribute("SourceCode"), reader.GetAttribute("ReplaceCode"));
                    }
                    else if (reader.GetAttribute("isCompound").ToLower() == "false")
                    {
                        bool bln= reader.GetAttribute("SourceCode").StartsWith("\\u") ? true : false;


                        Chars.Add(reader.GetAttribute("SourceCode").StartsWith("\\u") ? 
                            (char)Convert.ToInt32(reader.GetAttribute("SourceCode").Substring(2)) : Convert.ToChar(reader.GetAttribute("SourceCode")),
                            reader.GetAttribute("ReplaceCode").StartsWith("\\u") ? 
                            (char)Convert.ToInt32(reader.GetAttribute("ReplaceCode").Substring(2)) : Convert.ToChar(reader.GetAttribute("ReplaceCode")));

                        //if (reader.GetAttribute("SourceCode").StartsWith("\\u") && reader.GetAttribute("ReplaceCode").StartsWith("\\u"))
                        //{
                        //    Chars.Add((char)Convert.ToInt32(reader.GetAttribute("SourceCode").Substring(2)), (char)Convert.ToInt32(reader.GetAttribute("ReplaceCode").Substring(2)));
                        //}
                        // else if (reader.GetAttribute("SourceCode").StartsWith("\\u"))
                        //{
                        //    Chars.Add((char)Convert.ToInt32(reader.GetAttribute("SourceCode").Substring(2)), Convert.ToChar(reader.GetAttribute("ReplaceCode")));
                        //}
                        //else if (reader.GetAttribute("ReplaceCode").StartsWith("\\u"))
                        //{
                        //    Chars.Add(Convert.ToChar(reader.GetAttribute("SourceCode")), (char)Convert.ToInt32(reader.GetAttribute("ReplaceCode").Substring(2)));

                        //}
                        //else
                        //{
                        //    Chars.Add(Convert.ToChar(reader.GetAttribute("SourceCode")), Convert.ToChar(reader.GetAttribute("ReplaceCode")));
                        //}
                    }
                }
                else if ((reader.Name.ToLower() == "replace") && (reader.IsStartElement()))
                {
                    if (reader.GetAttribute("ReplaceTime") == "Befor")
                    {
                        //string strTem = reader.GetAttribute("Regex");
                        ReplacmentBefor.Add(reader.GetAttribute("Regex"), reader.GetAttribute("Replacement"));
                    }
                    else if (reader.GetAttribute("ReplaceTime") == "After")
                    {
                        ReplacmentAfter.Add(reader.GetAttribute("Regex"), reader.GetAttribute("Replacement"));
                    }
                }
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public string ToConvert(string strSource)
        {

            //Befor
            string strResult = strSource;
            if (ReplacmentBefor !=null )
            {
                foreach (string str in ReplacmentBefor.Keys)
                {
                    if (ReplacmentBefor[str].ToLower().StartsWith("|m"))
                    {
                        if (ReplacmentBefor[str].ToLower().Substring(3) == "reverse")
                        {
                            strResult = Regex.Replace(strResult, str, new MatchEvaluator(Reverse));
                        }
                        else if (ReplacmentBefor[str].ToLower().Substring(3) == "upper")
                        {
                            strResult = Regex.Replace(strResult, str, new MatchEvaluator(Upper));
                        }
                        else if (ReplacmentBefor[str].ToLower().Substring(3) == "lower")
                        {
                            strResult = Regex.Replace(strResult, str, new MatchEvaluator(Lower));
                        }

                    }
                    else
                    {
                        strResult = Regex.Replace(strResult, str, ReplacmentBefor[str]);
                    }
                }
                //strResult = Regex.Replace(strResult, "\u0013\u0013\u0010", "");
            }

            //Main
            //CompoundChars
            foreach (string str in CompoundChars.Keys)
            {
                //if (CompoundChars[str] == "\\r\\n")
                //{
                //    strResult = strResult.Replace(str, "\r\n");
                //}
                //else
                //{
                //}
                    strResult = strResult.Replace(str, CompoundChars[str]);
                
            }
            
            StringBuilder sb = new StringBuilder();
            //Single char
            foreach (char chr in strResult.ToCharArray())
            {
                if (Chars.ContainsKey(chr))
                {
                    sb.Append(Chars[chr]);
                }
                else
                {
                    //sb.Append(chr+"("+((int)chr).ToString()+")");
                    sb.Append(chr);
                }
            }
             string strRet=sb.ToString();

            //After
            foreach (string str in ReplacmentAfter.Keys)
            {
                if(ReplacmentAfter[str].ToLower().StartsWith("|m"))
                {
                    if(ReplacmentAfter[str].ToLower().Substring(3)=="reverse")
                    {
                        strRet = Regex.Replace(strRet, str, new MatchEvaluator(Reverse));
                    }
                    else if (ReplacmentAfter[str].ToLower().Substring(3) == "upper")
                    {
                        strRet = Regex.Replace(strRet, str, new MatchEvaluator(Upper));
                    }
                    else if (ReplacmentAfter[str].ToLower().Substring(3) == "lower")
                    {
                        strRet = Regex.Replace(strRet, str, new MatchEvaluator(Lower));
                    }

                }
                else
                {
                    strRet = Regex.Replace(strRet, str, ReplacmentAfter[str]);
                }
            }
            return strRet;
        }

        /// <summary>
        /// سانلارنىڭ يۈنۈلىشىنى توغرىلاش
        /// </summary>
        /// <param name="m">ئەسلى سانلار</param>
        /// <returns>تۈزۈتىلگەندىن كىيىنكى سانلار</returns>
        static string Reverse(Match m)
        {
            char[] chrArray = m.ToString().ToCharArray();
            Array.Reverse(chrArray);
            string strReversed = new string(chrArray);
            return strReversed;
        }

        /// <summary>
        /// سانلارنىڭ يۈنۈلىشىنى توغرىلاش
        /// </summary>
        /// <param name="m">ئەسلى سانلار</param>
        /// <returns>تۈزۈتىلگەندىن كىيىنكى سانلار</returns>
        static string Upper(Match m)
        {
            return m.ToString().ToUpper();
        }


        /// <summary>
        /// سانلارنىڭ يۈنۈلىشىنى توغرىلاش
        /// </summary>
        /// <param name="m">ئەسلى سانلار</param>
        /// <returns>تۈزۈتىلگەندىن كىيىنكى سانلار</returns>
        static string Lower(Match m)
        {
            return m.ToString().ToLower();
        }
    }
}
