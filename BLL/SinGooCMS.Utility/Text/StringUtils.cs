using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace SinGooCMS.Utility
{
    public static class StringUtils
    {
        internal const string CarriageReturnLineFeed = "\r\n";
        internal const string Empty = "";
        internal const char CarriageReturn = '\r';
        internal const char LineFeed = '\n';
        internal const char Tab = '\t';
        internal const int LCMAP_SIMPLIFIED_CHINESE = 0x2000000;
        internal const int LCMAP_TRADITIONAL_CHINESE = 0x4000000;
        internal const int LOCALE_SYSTEM_DEFAULT = 0x800;

        #region GetChineseSpell��ȡ����ƴ���ĵ�һ����ĸ
        /// <summary>
        /// ��ȡ����ƴ���ĵ�һ����ĸ
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        public static string[] GetChineseSpell(string[] strText)
        {
            int len = strText.Length;
            string[] myStr = null;
            for (int i = 0; i < len; i++)
            {
                myStr[i] = getSpell(strText[i]);
            }
            return myStr;
        }
        /// <summary>
        /// ��ȡ����ƴ��
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        public static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }
        #endregion

        #region ��ȡ�ַ���
        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Cut(string SoucreStr, int len)
        {
            return Cut(SoucreStr, len, string.Empty);
        }

        /// <summary>
        /// ��ȡ�ַ���������β�������Ҫ������
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="appendCode"></param>
        /// <returns></returns>
        public static string Cut(string SoucreStr, int len, string appendString)
        {
            if (SoucreStr.Length >= len)
                return SoucreStr.Substring(0, len) + appendString;

            return SoucreStr;
        }

        /// <summary>
        /// ����SQL�ַ�
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Filter(this string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.IgnoreCase).Success)
            {
                output = output.Replace(sInput, "''");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// <summary>
        /// �����ַ�����ʵ����, 1�����ֳ���Ϊ2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        #endregion

        #region �����ַ�

        /// <summary>
        /// ������нű�
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string InputTexts(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''"); //�滻SQL��ע��ĵ�����
            //text = StringToHtml(text);
            text = RemoveHtml(text); //���html��ǩ
            return text.Trim(); //ȥ�����ÿո�
        }

        /// <summary>
        /// ȥ��HTML���
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string StringToHtml(string Htmlstring)
        {
            //ɾ���ű�   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //ɾ��HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "<br/>", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");

            return Htmlstring;
        }

        public static string RemoveHtml(string s)
        {
            return RemoveHtmlInternal(s, null);
        }

        public static string RemoveHtml(string s, IList<string> removeTags)
        {
            if (removeTags == null)
                throw new ArgumentNullException("removeTags");

            return RemoveHtmlInternal(s, removeTags);
        }

        private static string RemoveHtmlInternal(string s, IList<string> removeTags)
        {
            List<string> removeTagsUpper = null;

            if (removeTags != null)
            {
                removeTagsUpper = new List<string>(removeTags.Count);

                foreach (string tag in removeTags)
                {
                    removeTagsUpper.Add(tag.ToUpperInvariant());
                }
            }

            Regex anyTag = new Regex(@"<[/]{0,1}\s*(?<tag>\w*)\s*(?<attr>.*?=['""].*?[""'])*?\s*[/]{0,1}>", RegexOptions.Compiled);

            return anyTag.Replace(s, delegate(Match match)
            {
                string tag = match.Groups["tag"].Value.ToUpperInvariant();

                if (removeTagsUpper == null)
                    return string.Empty;
                else if (removeTagsUpper.Contains(tag))
                    return string.Empty;
                else
                    return match.Value;
            });
        }

        /// <summary>
        /// �Ƿ����������
        /// </summary>
        /// <param name="arrSource"></param>
        /// <param name="strExpression"></param>
        /// <returns></returns>
        public static bool Contain(string[] arrSource, string strExpression)
        {
            foreach (string item in arrSource)
            {
                if (item.Equals(strExpression))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// ѹ��HTML
        /// </summary>
        /// <returns></returns>
        public static string Compress(string strHTML)
        {
            strHTML = Regex.Replace(strHTML, @">\s+\r", ">");
            strHTML = Regex.Replace(strHTML, @">\s+\n", ">");
            strHTML = Regex.Replace(strHTML, @">\s+<", "><");

            return strHTML;
        }

        /// <summary>
        /// ����һ���µ��ļ���
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            return System.DateTime.Now.ToString("yyyyMMddhhffff") + StringUtils.GetRandomNumber(3, true);
        }

        /// <summary>
        /// �滻sql����е����������
        /// </summary>
        public static string ChkSQL(string str)
        {
            return (str == null) ? "" : str.Replace("'", "''");
        }
        /// <summary>
        /// ��HTML�л�ȡ�ı�,����br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string HTML)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"</?(?!br|/?p|img)[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return regEx.Replace(HTML, "");
        }
        /// <summary>
        /// ȥ����һ���ַ�
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string CutFirstChr(string strSource)
        {
            if (strSource.Length > 1)
                return strSource.Substring(1, strSource.Length - 1);

            return strSource;
        }
        /// <summary>
        /// ȥ�����һ���ַ�
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string CutLastChr(string strSource)
        {
            if (strSource.Length > 1)
                return strSource.Substring(0, strSource.Length - 1);

            return strSource;
        }
        public static string AppendStr(string strSource, char chr)
        {
            return AppendStr(strSource, true, true, chr.ToString());
        }
        public static string AppendStr(string strSource, string str)
        {
            return AppendStr(strSource, true, true, str);
        }
        /// <summary>
        /// ���ַ�������ǰ��׺
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="containStart"></param>
        /// <param name="containEnd"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AppendStr(string strSource, bool containStart, bool containEnd, string str)
        {
            string strResult = strSource;

            if (containStart && !strSource.StartsWith(str))
                strResult = str + strResult;
            if (containEnd && !strSource.EndsWith(str))
                strResult = strResult + str;

            return strResult;
        }

        #endregion

        #region ����ָ�

        /// <summary>
        /// ����������ʽģʽ�����λ�ò�������ַ�����
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] Split(string pattern, string input)
        {
            Regex regex = new Regex(pattern);
            return regex.Split(input);
        }

        #endregion

        #region ���������
        /// <summary>
        /// ����һ�������
        /// </summary>
        /// <returns></returns>
        public static string GetRandomNumber()
        {
            string randomNumber = "";
            randomNumber = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            Random rdm = new Random();
            randomNumber = randomNumber + rdm.Next(10000, 100000 - 1).ToString();
            rdm = null;
            return randomNumber;
        }

        /// <summary>
        /// ����һ�������
        /// </summary>
        /// <param name="length"></param>
        /// <param name="isSleep"></param>
        /// <returns></returns>
        public static string GetRandomNumber(int length, bool isSleep)
        {
            if (isSleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
        public static string GetGUID()
        {
            return System.Guid.NewGuid().ToString();
        }
        #endregion

        #region ���ط���ǰ׺
        /// <summary>
        /// ���ط���ǰ׺
        /// </summary>
        /// <param name="intDept"></param>
        /// <returns></returns>
        public static string GetCatePrefix(int intDept, bool boolIsEnd)
        {
            char ch = '\x00a0';
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < intDept; i++)
            {
                builder.Append(ch);
            }
            if (boolIsEnd)
                builder.Append("��");
            else
                builder.Append("��");

            return builder.ToString();
        }
        #endregion

        #region ȥ���ո�
        /// <summary>
        /// �Ƿ�����հ��ַ�
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsWhiteSpace(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// ȥ���ո�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(string strInput)
        {
            bool changed = false;
            char[] output = strInput.ToCharArray();
            int cursor = 0;
            for (int i = 0, size = output.Length; i < size; i++)
            {
                char c = output[i];
                if (Char.IsWhiteSpace(c))
                {
                    changed = true;
                    continue;
                }

                output[cursor] = c;
                cursor++;
            }

            return changed ? new string(output, 0, cursor) : strInput;
        }
        #endregion

        #region ���ķ���ת��

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);
        /// <summary>
        /// ת�ɼ���
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string ToSimplified(string strSource)
        {
            string lpDestStr = new string(' ', strSource.Length);
            LCMapString(0x800, 0x2000000, strSource, strSource.Length, lpDestStr, strSource.Length);

            //��չ��ת��
            lpDestStr = ConvertST.ToSimplified(lpDestStr);

            return lpDestStr;
        }
        /// <summary>
        /// ת�ɷ���
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string ToTraditional(string strSource)
        {
            string lpDestStr = new string(' ', strSource.Length);
            LCMapString(0x800, 0x4000000, strSource, strSource.Length, lpDestStr, strSource.Length);

            //��չ��ת��
            lpDestStr = ConvertST.ToTraditional(lpDestStr);

            return lpDestStr;
        }

        #endregion

        #region ���������ַ�������������һ�������һ�����������ֶ����Ǻ��滻
        public static string GetAnonymous(string strSource)
        {
            return GetAnonymous(strSource, "*");
        }
        public static string GetAnonymous(string strSource, string strReplaceStr)
        {
            if (string.IsNullOrEmpty(strSource))
                return strReplaceStr;
            else if (strSource.Length == 1)
                return strReplaceStr;
            else if (strSource.Length == 2)
                return strSource.Replace(strSource.Substring(1, 1), strReplaceStr);
            else if (strSource.Length == 3)
                return strSource.Replace(strSource.Substring(1, 1), strReplaceStr);
            else
            {
                //���м���ַ��滻
                StringBuilder builder = new StringBuilder();
                char[] arr = strSource.ToCharArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i >= arr.Length / 2 - 1 && i <= arr.Length - arr.Length / 2)
                        builder.Append(strReplaceStr);
                    else
                        builder.Append(arr[i].ToString());
                }
                return builder.ToString();
            }
        }
        #endregion

        #region ��ȡ��ֵ���ַ����ļ���ֵ
        public static string GetSeparatorValue(string strSource)
        {
            return GetSeparatorValue(strSource, 1);
        }
        /// <summary>
        /// strSource ���� ��Ʒa|1,��Ʒb|2
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string GetSeparatorValue(string strSource, int intIndex)
        {
            if (!string.IsNullOrEmpty(strSource))
            {
                intIndex = intIndex.Equals(1) ? 1 : 0;
                string[] arrSource = strSource.Split(',');
                StringBuilder builder = new StringBuilder();

                foreach (string item in arrSource)
                {
                    if (!string.IsNullOrEmpty(item) && item.IndexOf('|') != -1)
                        builder.Append(item.Split('|')[intIndex] + ",");
                }
                string strTemp = builder.ToString();
                if (strTemp.EndsWith(","))
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);

                return strTemp;
            }

            return string.Empty;
        }

        #endregion

        #region ���ɱ��
        /// <summary>
        /// ���ɱ��
        /// </summary>
        /// <param name="SNFormat"></param>
        /// <returns></returns>
        public static string GenerateSN(string SNFormat)
        {
            return SNFormat.Replace("${year}", System.DateTime.Now.ToString("yyyy")).Replace("${month}", System.DateTime.Now.ToString("MM"))
                .Replace("${day}", System.DateTime.Now.ToString("dd")).Replace("${hour}", System.DateTime.Now.ToString("HH"))
                .Replace("${minute}", System.DateTime.Now.ToString("mm")).Replace("${second}", System.DateTime.Now.ToString("ss"))
                .Replace("${millisecond}", System.DateTime.Now.ToString("ffff")).Replace("${rnd}", StringUtils.GetRandomNumber(3, true));
        }
        #endregion
    }
}