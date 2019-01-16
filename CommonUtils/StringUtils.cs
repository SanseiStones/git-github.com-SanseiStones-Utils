using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CommonUtils
{
	//字符串工具类 测试
    public static class StringUtils
    {
        private static readonly IProgramLog log = ProgramLogManager.GetLogger(typeof(StringUtils));
        /// <summary>
        /// HtmlEncode中要去掉的 引号(含 单双) 比如 ' %27  " %22
        /// </summary>
        private static readonly string[] mHtmlQuoteStrArr = new string[]
        {
            "'",
            "%27",
            "\"",
            "%22"
        };
        /// <summary>
        /// 价格显示的默认精度 默认3位
        /// </summary>
        public static readonly int DefaultPricePrecision = 3;
        /// <summary>
        /// 字符
        /// </summary>
        public static char[] HexDigits = new char[]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
        };
        /// <summary>
        /// 奇偶校验位数组(随意后的固定数组值 不能再变化)
        /// </summary>
        private static int[] mParityBitFlagArry = new int[]
        {
            7,
            9,
            1,
            5,
            8,
            4,
            2,
            1,
            6,
            3,
            7,
            9,
            7,
            5,
            8,
            4,
            2,
            3,
            6,
            4,
            3,
            1,
            3,
            5,
            7,
            9,
            2,
            6,
            8,
            4
        };
        /// <summary>
        /// Method to make sure that user's inputs are not malicious
        /// </summary>
        /// <param name="text">User's Input</param>
        /// <param name="maxLength">Maximum length of input</param>
        /// <returns>The cleaned up version of the input</returns>
        public static string InputText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            text = text.Trim();
            if (text.Length > maxLength)
            {
                text = text.Substring(0, maxLength);
            }
            text = Regex.Replace(text, "[\\s]{2,}", " ");
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
            text = text.Replace("'", "''");
            return text;
        }
        /// <summary>
        /// Method to check whether input has other characters than numbers
        /// </summary>
        public static string CleanNonWord(string text)
        {
            return Regex.Replace(text, "\\W", "");
        }
        /// <summary>
        /// 重复某字符
        /// </summary>
        /// <param name="c"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RepeatChar(char c, int count)
        {
            string text = "";
            for (int i = 0; i < count; i++)
            {
                text += c.ToString();
            }
            return text;
        }
        /// <summary>
        /// 重复某字符
        /// </summary>
        /// <param name="c"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RepeatChar(string c, int count)
        {
            string text = "";
            for (int i = 0; i < count; i++)
            {
                text += c;
            }
            return text;
        }
        /// <summary>
        /// 填充字符
        /// </summary>
        /// <param name="iLen"></param>
        /// <param name="strTemp"></param>
        /// <param name="fillChar"></param>
        /// <param name="bAtBefore"></param>
        /// <returns></returns>
        public static string FillStr(int iLen, string strTemp, char fillChar, bool bAtBefore)
        {
            string text = "";
            int num = strTemp.Length;
            if (num >= iLen)
            {
                return strTemp;
            }
            num = iLen - num;
            for (int i = 0; i < num; i++)
            {
                text += fillChar.ToString();
            }
            if (bAtBefore)
            {
                return text + strTemp;
            }
            return strTemp + text;
        }
        /// <summary>
        /// 返回非空字符串
        /// </summary>
        /// <param name="canNullStr"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string NotNullStr(object canNullStr, string defaultStr)
        {
            if (canNullStr != null && !(canNullStr is DBNull))
            {
                return Convert.ToString(canNullStr);
            }
            if (string.IsNullOrEmpty(defaultStr))
            {
                return "";
            }
            return defaultStr;
        }
        /// <summary>
        /// 返回非空字符串
        /// </summary>
        /// <param name="canNullStr"></param>
        /// <returns></returns>
        public static string NotNullStr(object canNullStr)
        {
            return StringUtils.NotNullStr(canNullStr, "");
        }
        /// <summary>
        /// 是否相同的字符串(str0 或 str1 可以为null 比较)
        /// </summary>
        /// <param name="str0"></param>
        /// <param name="str1"></param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool IsEqualStr(string str0, string str1, bool ignoreCase)
        {
            if (str0 == null && str1 != null)
            {
                return false;
            }
            if (str0 == null && str1 == null)
            {
                return true;
            }
            if (str0 != null && str1 == null)
            {
                return false;
            }
            if (ignoreCase)
            {
                return str0.Equals(str1, StringComparison.CurrentCultureIgnoreCase);
            }
            return str0.Equals(str1);
        }
        /// <summary>
        /// 替换字符串（不跑例外）
        /// </summary>
        /// <param name="srcStr"></param>
        /// <param name="strOld"></param>
        /// <param name="strNew"></param>
        /// <returns></returns>
        public static string ReplaceStr(string srcStr, string strOld, string strNew)
        {
            if (string.IsNullOrEmpty(srcStr) || string.IsNullOrEmpty(strOld))
            {
                return srcStr;
            }
            return srcStr.Replace(strOld, strNew);
        }
        /// <summary>
        /// 字符串字节长度(汉字2字节) str.Length中 单个汉字长度为1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StrByteLength(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return bytes.Length;
        }
        /// <summary>
        /// 限制字符串长度(超长，默认会加...)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        public static string LimitStrLen(string str, int maxLen)
        {
            return StringUtils.LimitStrLen(str, maxLen, true);
        }
        /// <summary>
        /// 限制字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLen"></param>
        /// <param name="isAddDots">是否加省略号</param>
        /// <returns></returns>
        public static string LimitStrLen(string str, int maxLen, bool isAddDots)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (maxLen <= 0)
            {
                return str;
            }
            if (str.Length <= maxLen)
            {
                return str;
            }
            if (isAddDots)
            {
                return str.Substring(0, maxLen) + "...";
            }
            return str.Substring(0, maxLen);
        }
        /// <summary>
        /// 某字符str是否在限制的字符串limitStrs内(比如 111,222,333,444  检查333 是否在这个范围之内)
        /// 默认 ,分割， 忽略大小写
        /// </summary>
        /// <param name="limitStrs"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInLimitStr(string limitStrs, string str)
        {
            return StringUtils.IsInLimitStr(limitStrs, str, ',', true);
        }
        /// <summary>
        /// 某字符str是否在限制的字符串limitStrs内(比如 111,222,333,444  检查333 是否在这个范围之内)
        /// </summary>
        /// <param name="limitStrs"></param>
        /// <param name="str"></param>
        /// <param name="splitChar"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsInLimitStr(string limitStrs, string str, char splitChar, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(limitStrs) || string.IsNullOrEmpty(str))
            {
                return false;
            }
            string text = splitChar.ToString() + limitStrs + splitChar.ToString();
            if (ignoreCase)
            {
                return text.IndexOf(splitChar.ToString() + str + splitChar.ToString(), StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
            return text.IndexOf(splitChar.ToString() + str + splitChar.ToString()) >= 0;
        }
        /// <summary>
        /// 某字符str是否在限制的字符串limitStrs内开头(比如 111,222,333,444  检查333abc 是否在这个范围之内某串的开头)
        /// 默认 ,分割， 忽略大小写
        /// </summary>
        /// <param name="limitStrs"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInLimitStartWithStr(string limitStrs, string str)
        {
            return StringUtils.IsInLimitStartWithStr(limitStrs, str, ',', true);
        }
        /// <summary>
        /// 某字符str是否在限制的字符串limitStrs内开头(比如 111,222,333,444  检查333abc 是否在这个范围之内某串的开头)
        /// </summary>
        /// <param name="limitStrs"></param>
        /// <param name="str"></param>
        /// <param name="splitChar"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsInLimitStartWithStr(string limitStrs, string str, char splitChar, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(limitStrs) || string.IsNullOrEmpty(str))
            {
                return false;
            }
            string[] array = limitStrs.Split(new char[]
            {
                splitChar
            });
            if (array == null || array.Length == 0)
            {
                return false;
            }
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string value = array2[i];
                if (ignoreCase)
                {
                    if (str.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
                    {
                        bool result = true;
                        return result;
                    }
                }
                else
                {
                    if (str.StartsWith(value))
                    {
                        bool result = true;
                        return result;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 处理XML的转义符号 要替换 XML中的转义符号
        ///                小于号 &lt;
        ///                大于号 &gt;
        ///                " "
        ///                 &amp;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToXMLStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("&", "&amp;");
        }
        /// <summary>
        /// 转换成整型 不抛例外
        ///
        /// int的最小值和最大值  -2^31 (-2,147,483,648) 到 2^31-1 (2,147,483,647)
        /// </summary>
        /// <param name="objInt"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(object objInt, int defaultValue)
        {
            if (objInt == null || objInt is DBNull)
            {
                return defaultValue;
            }
            int result;
            if (!int.TryParse(objInt.ToString(), out result))
            {
                try
                {
                    int result2 = Convert.ToInt32(objInt);
                    return result2;
                }
                catch
                {
                    int result2 = defaultValue;
                    return result2;
                }              
            }
            return result;
        }
        /// <summary>
        /// 转换成整型 不抛例外
        /// int的最小值和最大值  -2^31 (-2,147,483,648) 到 2^31-1 (2,147,483,647)
        /// </summary>
        /// <param name="objInt"></param>
        /// <returns></returns>
        public static int ToInt(object objInt)
        {
            return StringUtils.ToInt(objInt, 0);
        }
        /// <summary>
        /// 转换成长整型 不抛例外
        /// long的最小值和最大值  -2^63 (-9,223,372,036,854,775,808) 到 2^63-1 (9,223,372,036,854,775,807)
        /// </summary>
        /// <param name="objLong"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(object objLong, long defaultValue)
        {
            if (objLong == null || objLong is DBNull)
            {
                return defaultValue;
            }
            long result;
            if (!long.TryParse(objLong.ToString(), out result))
            {
                try
                {
                    long result2 = Convert.ToInt64(objLong);
                    return result2;
                }
                catch
                {
                    long result2 = defaultValue;
                    return result2;
                }
            }
            return result;
        }
        /// <summary>
        /// 转换成长整型 不抛例外
        /// long的最小值和最大值  -2^63 (-9,223,372,036,854,775,808) 到 2^63-1 (9,223,372,036,854,775,807)
        /// </summary>
        /// <param name="objLong"></param>
        /// <returns></returns>
        public static long ToLong(object objLong)
        {
            return StringUtils.ToLong(objLong, 0L);
        }
        /// <summary>
        /// 转换成bool 不抛例外
        /// objBool 可以为 1,y/Y,t/T,true/True =true,0,n/N,f/F,false/False=false
        /// </summary>
        /// <param name="objBool"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBoolean(object objBool, bool defaultValue)
        {
            if (objBool == null || objBool is DBNull)
            {
                return defaultValue;
            }
            string value = objBool.ToString();
            if ("1".Equals(value) || "true".Equals(value, StringComparison.CurrentCultureIgnoreCase) || "y".Equals(value, StringComparison.CurrentCultureIgnoreCase) || "t".Equals(value, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if ("0".Equals(value) || "false".Equals(value, StringComparison.CurrentCultureIgnoreCase) || "n".Equals(value, StringComparison.CurrentCultureIgnoreCase) || "f".Equals(value, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            bool result;
            if (!bool.TryParse(value, out result))
            {
                try
                {
                    bool result2 = Convert.ToBoolean(objBool);
                    return result2;
                }
                catch
                {
                    bool result2 = defaultValue;
                    return result2;
                }             
            }
            return result;
        }
        /// <summary>
        /// 转换成bool 不抛例外
        /// objBool 可以为 1,y/Y,t/T,true/True =true,0,n/N,f/F,false/False=false
        /// </summary>
        /// <param name="objBool"></param>
        /// <returns></returns>
        public static bool ToBoolean(object objBool)
        {
            return StringUtils.ToBoolean(objBool, false);
        }
        /// <summary>
        /// 转换成decimal 不抛例外
        /// decimal的最小值 和 最大值 -79228162514264337593543950335 和 79228162514264337593543950335
        /// </summary>
        /// <param name="objDecimal"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object objDecimal, decimal defaultValue)
        {
            if (objDecimal == null || objDecimal is DBNull)
            {
                return defaultValue;
            }
            decimal result;
            if (!decimal.TryParse(objDecimal.ToString(), out result))
            {
                try
                {
                    decimal result2 = Convert.ToDecimal(objDecimal);
                    return result2;
                }
                catch
                {
                    decimal result2 = defaultValue;
                    return result2;
                }
            }
            return result;
        }
        /// <summary>
        /// 转换成decimal 不抛例外
        /// decimal的最小值 和 最大值 -79228162514264337593543950335 和 79228162514264337593543950335
        /// </summary>
        /// <param name="objdecimal"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object objdecimal)
        {
            return StringUtils.ToDecimal(objdecimal, 0m);
        }
        /// <summary>
        /// 转换成float 不抛例外
        /// float的最小 和 最大值  -3.40282e+038f; 3.40282e+038f;
        /// </summary>
        /// <param name="objFloat"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(object objFloat, float defaultValue)
        {
            if (objFloat == null || objFloat is DBNull)
            {
                return defaultValue;
            }
            float result;
            if (!float.TryParse(objFloat.ToString(), out result))
            {
                try
                {
                    float result2 = Convert.ToSingle(objFloat);
                    return result2;
                }
                catch
                {
                    float result2 = defaultValue;
                    return result2;
                }
              
            }
            return result;
        }
        /// <summary>
        /// 转换成float 不抛例外
        /// float的最小 和 最大值  -3.40282e+038f; 3.40282e+038f;
        /// </summary>
        /// <param name="objFloat"></param>
        /// <returns></returns>
        public static float ToFloat(object objFloat)
        {
            return StringUtils.ToFloat(objFloat, 0f);
        }
        /// <summary>
        /// 转换成double 不抛例外
        /// double的最小 和 最大值  -1.79769e+308; 1.79769e+308
        /// </summary>
        /// <param name="objDouble"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(object objDouble, double defaultValue)
        {
            if (objDouble == null || objDouble is DBNull)
            {
                return defaultValue;
            }
            double result;
            if (!double.TryParse(objDouble.ToString(), out result))
            {
                try
                {
                    double result2 = Convert.ToDouble(objDouble);
                    return result2;
                }
                catch
                {
                    double result2 = defaultValue;
                    return result2;
                }
            }
            return result;
        }
        /// <summary>
        /// 转换成double 不抛例外
        /// double的最小 和 最大值  -1.79769e+308; 1.79769e+308
        /// </summary>
        /// <param name="objDouble"></param>
        /// <returns></returns>
        public static double ToDouble(object objDouble)
        {
            return StringUtils.ToDouble(objDouble, 0.0);
        }
        /// <summary>
        /// 转换成价格显示字符串 不抛例外
        /// </summary>
        /// <param name="objPrice"></param>
        /// <param name="pricePrecision">小数位精度 不能小于2</param>
        /// <param name="isShowPreCh">是否显示￥</param>
        /// <returns></returns>
        public static string ToPriceStr(object objPrice, int pricePrecision, bool isShowPreCh)
        {
            if (objPrice == null || objPrice is DBNull)
            {
                return "";
            }
            decimal num = StringUtils.ToDecimal(objPrice);
            if (pricePrecision < 2)
            {
                pricePrecision = 2;
            }
            if (isShowPreCh)
            {
                return "￥" + num.ToString("F" + pricePrecision.ToString());
            }
            return num.ToString("F" + pricePrecision.ToString());
        }
        /// <summary>
        /// 转换成价格显示字符串（默认显示3位小数） 不抛例外
        /// </summary>
        /// <param name="objPrice"></param>
        /// <param name="isShowPreCh">是否显示￥</param>
        /// <returns></returns>
        public static string ToPriceStr(object objPrice, bool isShowPreCh)
        {
            return StringUtils.ToPriceStr(objPrice, StringUtils.DefaultPricePrecision, isShowPreCh);
        }
        /// <summary>
        /// 转换成价格显示字符串（默认显示3位小数，并显示￥） 不抛例外
        /// </summary>
        /// <param name="objPrice"></param>
        /// <returns></returns>
        public static string ToPriceStr(object objPrice)
        {
            return StringUtils.ToPriceStr(objPrice, StringUtils.DefaultPricePrecision, true);
        }
        /// <summary>
        /// 转换成金额 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <param name="amtPrecision">小数位精度 不能小于2</param>
        /// <param name="ceilingOrFloor">true=ceiling(天花板) false=floor(地板) Math的Ceiling(返回大于或等于指定数字的最小整数 进一法) 或则 Floor(返回小于或等于指定数字的最大整数 无条件舍去小数) 比如 5.6 Ceiling=6 Floor=5</param>
        /// <returns></returns>
        public static decimal ToAmt(object objAmt, int amtPrecision, bool ceilingOrFloor)
        {
            if (objAmt == null || objAmt is DBNull)
            {
                return 0m;
            }
            decimal d = StringUtils.ToDecimal(objAmt);
            if (amtPrecision < 2)
            {
                amtPrecision = 2;
            }
            double x = 10.0;
            double num = Math.Pow(x, (double)amtPrecision);
            decimal d2 = (decimal)num;
            decimal d3 = d * d2;
            long value;
            if (ceilingOrFloor)
            {
                value = (long)Math.Ceiling(d3);
            }
            else
            {
                value = (long)Math.Floor(d3);
            }
            return decimal.Parse((1.0m * value / d2).ToString("F" + amtPrecision.ToString()));
        }
        /// <summary>
        /// 转换成金额（默认显示2位小数） 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <param name="ceilingOrFloor">true=ceiling(天花板) false=floor(地板)  Math的Ceiling(返回大于或等于指定数字的最小整数 进一法) 或则 Floor(返回小于或等于指定数字的最大整数 无条件舍去小数) 比如 5.6 Ceiling=6 Floor=5</param>
        /// <returns></returns>
        public static decimal ToAmt(object objAmt, bool ceilingOrFloor)
        {
            return StringUtils.ToAmt(objAmt, 2, ceilingOrFloor);
        }
        /// <summary>
        /// 转换成金额（默认显示2位小数，采用Ceiling 进一法） 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <returns></returns>
        public static decimal ToAmt(object objAmt)
        {
            return StringUtils.ToAmt(objAmt, 2, true);
        }
        /// <summary>
        /// 转换成金额显示字符串 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <param name="amtPrecision">小数位精度 不能小于2 默认为2</param>
        /// <param name="ceilingOrFloor">true=ceiling(天花板) false=floor(地板)  Math的Ceiling(返回大于或等于指定数字的最小整数 进一法) 或则 Floor(返回小于或等于指定数字的最大整数 无条件舍去小数) 比如 5.6 Ceiling=6 Floor=5</param>
        /// <param name="isShowPreCh">是否显示￥</param>
        /// <returns></returns>
        public static string ToAmtStr(object objAmt, int amtPrecision, bool ceilingOrFloor, bool isShowPreCh)
        {
            decimal num = StringUtils.ToAmt(objAmt, amtPrecision, ceilingOrFloor);
            if (isShowPreCh)
            {
                return "￥" + num.ToString("F" + amtPrecision.ToString());
            }
            return num.ToString("F" + amtPrecision.ToString());
        }
        /// <summary>
        /// 转换成金额显示字符串（默认显示2位小数） 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <param name="ceilingOrFloor">true=ceiling(天花板) false=floor(地板)  Math的Ceiling(返回大于或等于指定数字的最小整数 进一法) 或则 Floor(返回小于或等于指定数字的最大整数 无条件舍去小数) 比如 5.6 Ceiling=6 Floor=5</param>
        /// <param name="isShowPreCh">是否显示￥</param>
        /// <returns></returns>
        public static string ToAmtStr(object objAmt, bool ceilingOrFloor, bool isShowPreCh)
        {
            return StringUtils.ToAmtStr(objAmt, 2, ceilingOrFloor, isShowPreCh);
        }
        /// <summary>
        /// 转换成金额显示字符串（默认显示2位小数 ，采用Ceiling 进一法） 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <param name="isShowPreCh">是否显示￥</param>
        /// <returns></returns>
        public static string ToAmtStr(object objAmt, bool isShowPreCh)
        {
            return StringUtils.ToAmtStr(objAmt, 2, true, isShowPreCh);
        }
        /// <summary>
        /// 转换成金额显示字符串（默认显示2位小数，采用Ceiling 进一法，并显示￥） 不抛例外
        /// </summary>
        /// <param name="objAmt"></param>
        /// <returns></returns>
        public static string ToAmtStr(object objAmt)
        {
            return StringUtils.ToAmtStr(objAmt, true);
        }
        /// <summary>
        /// 将金额转换到分(即金额*100 只取金额小数2位数 到整数)
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public static int ToMoneyFen(decimal amt)
        {
            return (int)(amt * 100m);
        }
        /// <summary>
        /// 将整型分转金额元(即 整型分/100.0)
        /// </summary>
        /// <param name="amtFen"></param>
        /// <returns></returns>
        public static decimal ToMoneyYuanByFen(int amtFen)
        {
            return amtFen * 1.0m / 100.0m;
        }
        /// <summary>
        /// 生成where 部分的SQL语句
        /// </summary>
        /// <param name="whereSQL">传进来的where SQL语句</param>
        /// <param name="addSQL">要添加的SQL语句</param>
        /// <param name="opSQL">连接操作符 一般为or、and等</param>
        /// <returns></returns>
        public static string AddToWhereSQL(ref string whereSQL, string addSQL, string opSQL)
        {
            if (string.IsNullOrEmpty(whereSQL))
            {
                whereSQL = " (" + addSQL + ") ";
            }
            else
            {
                whereSQL = string.Concat(new string[]
                {
                    whereSQL,
                    " ",
                    opSQL,
                    " (",
                    addSQL,
                    ") "
                });
            }
            return whereSQL;
        }
        /// <summary>
        /// 格式化金额
        /// </summary>
        /// <param name="amt"></param>
        /// <param name="thousandSep"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string FormatCurrency(object amt, bool thousandSep, int precision)
        {
            if (amt == null)
            {
                return "0";
            }
            string result;
            try
            {
                NumberFormatInfo numberFormat = new CultureInfo("zh-CN").NumberFormat;
                numberFormat.CurrencyDecimalDigits = precision;
                if (thousandSep)
                {
                    int[] array = new int[2];
                    array[0] = 3;
                    int[] currencyGroupSizes = array;
                    numberFormat.CurrencyGroupSizes = currencyGroupSizes;
                    result = decimal.Parse(amt.ToString()).ToString("C", numberFormat);
                }
                else
                {
                    int[] array2 = new int[1];
                    int[] currencyGroupSizes2 = array2;
                    numberFormat.CurrencyGroupSizes = currencyGroupSizes2;
                    result = decimal.Parse(amt.ToString()).ToString("F" + precision.ToString(), numberFormat);
                }
            }
            catch
            {
                result = "0";
            }
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="price"></param>
        /// <param name="thousandSep"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string FormatCurrency1(object price, bool thousandSep, int precision)
        {
            if (price == null)
            {
                return "0";
            }
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalDigits = precision;
            numberFormatInfo.NumberDecimalSeparator = ".";
            numberFormatInfo.CurrencySymbol = "";
            if (thousandSep)
            {
                numberFormatInfo.NumberGroupSeparator = ",";
                numberFormatInfo.NumberGroupSizes = new int[]
                {
                    3
                };
            }
            string result;
            try
            {
                result = Convert.ToDecimal(price).ToString("c", numberFormatInfo);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 格式化金额(默认2位小数)
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public static string FormatCurrency(object amt)
        {
            return StringUtils.FormatCurrency(amt, true, 2);
        }
        /// <summary>
        /// 格式化金额(默认显示千分位)
        /// </summary>
        /// <param name="amt"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string FormatCurrency(object amt, int precision)
        {
            return StringUtils.FormatCurrency(amt, true, precision);
        }
        /// <summary>
        /// 格式化金额(默认2位小数)
        /// </summary>
        /// <param name="amt"></param>
        /// <param name="thousandSep"></param>
        /// <returns></returns>
        public static string FormatCurrency(object amt, bool thousandSep)
        {
            return StringUtils.FormatCurrency(amt, thousandSep, 2);
        }
        /// <summary>
        /// 格式化平台虚拟资金金额(金额默认有4位小数，如果最后一位位0 则显示 系统默认的3位小数，否则显示4位)
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public static string FormatAmtStr(decimal amt)
        {
            string text = amt.ToString("F4");
            if (text.EndsWith("0"))
            {
                return amt.ToString("F3");
            }
            return amt.ToString("F4");
        }
        /// <summary>
        /// 格式化平台虚拟资金金额(金额默认有4位小数，如果最后一位位0 则显示 系统默认的3位小数，否则显示4位)
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public static string FormatAmtStr(object amt)
        {
            return StringUtils.FormatAmtStr(StringUtils.ToDecimal(amt));
        }
        /// <summary>
        /// 格式化平台虚拟资金金额(金额默认有4位小数，如果最后一位位0 则显示 系统默认的3位小数，否则显示4位)(带￥)
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public static string FormatAmtChStr(decimal amt)
        {
            return "￥" + StringUtils.FormatAmtStr(amt);
        }
        /// <summary>
        /// 将金额格式化成汉字大写金额
        /// </summary>
        /// <param name="amt">金额</param>
        /// <returns></returns>
        public static string FormatAmtToChStr(decimal amt)
        {
            string[] array = new string[]
            {
                "分",
                "角",
                "元",
                "拾",
                "佰",
                "仟",
                "万",
                "拾",
                "佰",
                "仟",
                "亿",
                "拾",
                "佰",
                "仟",
                "兆",
                "拾",
                "佰",
                "仟"
            };
            string[] array2 = new string[]
            {
                "零",
                "壹",
                "贰",
                "叁",
                "肆",
                "伍",
                "陆",
                "柒",
                "捌",
                "玖"
            };
            string text = "";
            bool flag = false;
            string text2 = amt.ToString("F2");
            if (text2.IndexOf(".") != -1)
            {
                text2 = text2.Remove(text2.IndexOf("."), 1);
                flag = true;
            }
            for (int i = text2.Length; i > 0; i--)
            {
                int num = (int)Convert.ToInt16(text2[text2.Length - i].ToString());
                text += array2[num];
                if (flag)
                {
                    text += array[i - 1];
                }
                else
                {
                    text += array[i + 1];
                }
            }
            return text;
        }
        /// <summary>
        /// 判断输入内容是否为正确的Email格式 
        /// </summary>
        /// <param name="strEmail">用户输入的Email地址</param>
        /// <returns>true/false</returns>
        public static bool IsEmailFormat(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }
        /// <summary>
        /// 判断输入内容是否为正确的手机格式
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>true/false</returns>
        public static bool IsMobileFormat(string mobile)
        {
            return Regex.IsMatch(mobile, "(^[1][3-9]\\d{9}$)");
        }
        /// <summary>
        /// 判断数据合法性
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="nullYN">是否不能为空值(true不能为空 flase可以为空)</param>
        /// <param name="strCharList">允许出现的字符串</param>
        /// <returns>true/false</returns>
        public static bool CheckString(string str, bool nullYN, string strCharList)
        {
            if (nullYN && (str == null || str == ""))
            {
                return false;
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (strCharList.IndexOf(str.Substring(i, 1)) < 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断输入内容是否为正确的数字
        /// </summary>
        /// <param name="strInputNumber">用户输入的内容</param>
        /// <param name="boolNull">是否不能为空值(true不能为空 flase可以为空)</param>
        /// <returns>true/false</returns>
        public static bool IsNumber(string strInputNumber, bool boolNull)
        {
            return strInputNumber.LastIndexOf('-') <= 0 && StringUtils.CheckString(strInputNumber, boolNull, "-1234567890");
        }
        /// <summary>
        /// 判断输入的内容是否为正浮点数
        /// </summary>
        /// <param name="strInputNumber"></param>
        /// <param name="boolNull"></param>
        /// <returns></returns>
        public static bool IsFloat(string strInputNumber, bool boolNull)
        {
            if (strInputNumber.IndexOf("-") >= 0)
            {
                return false;
            }
            string[] array = strInputNumber.Split(new char[]
            {
                '.'
            });
            if (array.Length > 2)
            {
                return false;
            }
            if (array.Length == 2)
            {
                return StringUtils.IsNumber(array[0], boolNull) && StringUtils.IsNumber(array[1], boolNull);
            }
            return StringUtils.IsNumber(array[0], boolNull);
        }
        /// <summary>
        /// 替换SQL中的'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToDBStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Replace("'", "''");
        }
        /// <summary>
        ///  替换SQL中的'
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string ToDBStr(object str, string defaultStr)
        {
            if (str != null && !(str is DBNull))
            {
                return StringUtils.ToDBStr(str.ToString());
            }
            if (string.IsNullOrEmpty(defaultStr))
            {
                return "";
            }
            return StringUtils.ToDBStr(defaultStr);
        }
        /// <summary>
        ///  替换SQL中的'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToDBStr(object str)
        {
            return StringUtils.ToDBStr(str, "");
        }
        /// <summary>
        /// 替换SQL中的' 并增加前后''
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string QuotedToDBStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "''";
            }
            return "'" + str.Replace("'", "''") + "'";
        }
        /// <summary>
        /// 替换SQL中的' 并增加前后''
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string QuotedToDBStr(object str, string defaultStr)
        {
            if (str == null || str is DBNull)
            {
                return StringUtils.QuotedToDBStr(defaultStr);
            }
            return StringUtils.QuotedToDBStr(str.ToString());
        }
        /// <summary>
        /// 替换SQL中的' 并增加前后''
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string QuotedToDBStr(object str)
        {
            return StringUtils.QuotedToDBStr(str, "");
        }
        /// <summary>
        /// 将bool转换为0 1给数据库
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string BoolToDBStr(bool flag)
        {
            if (flag)
            {
                return "1";
            }
            return "0";
        }
        /// <summary>
        /// 将bool转换为0 1给数据库
        /// </summary>
        /// <param name="objFlag"></param>
        /// <returns></returns>
        public static string BoolToDBStr(object objFlag)
        {
            return StringUtils.BoolToDBStr(StringUtils.ToBoolean(objFlag));
        }
        /// <summary>
        /// 将bool转换为0 1
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int BoolTo01(bool flag)
        {
            if (flag)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 将bool转换为0 1
        /// </summary>
        /// <param name="objFlag"></param>
        /// <returns></returns>
        public static int BoolTo01(object objFlag)
        {
            return StringUtils.BoolTo01(StringUtils.ToBoolean(objFlag));
        }
        /// <summary>
        /// 将bool转换为是和否
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string BoolToYesNo(bool flag)
        {
            if (flag)
            {
                return "是";
            }
            return "否";
        }
        /// <summary>
        /// 将bool转换为是和否
        /// </summary>
        /// <param name="objFlag"></param>
        /// <returns></returns>
        public static string BoolToYesNo(object objFlag)
        {
            return StringUtils.BoolToYesNo(StringUtils.ToBoolean(objFlag));
        }
        /// <summary>
        /// 把IP处理成标准的形式0.0.0.0
        /// </summary>
        /// <param name="inputIP"></param>
        /// <returns></returns>
        public static string SimpleIP(string inputIP)
        {
            if (string.IsNullOrEmpty(inputIP))
            {
                return "";
            }
            string text = "";
            string[] array = inputIP.Split(new char[]
            {
                '.'
            });
            for (int i = 0; i < array.Length; i++)
            {
                int num = StringUtils.ToInt(array[i], 0);
                if (i > 0)
                {
                    text += ".";
                }
                text += num.ToString();
            }
            return text;
        }
        /// <summary>
        /// 格式化IP:把IP转化成000.000.000.000的格式
        /// </summary>
        /// <param name="inputIP"></param>
        /// <returns></returns>
        public static string FormatIP(string inputIP)
        {
            if (string.IsNullOrEmpty(inputIP))
            {
                return "";
            }
            if ("::1".Equals(inputIP))
            {
                return "127.000.000.001";
            }
            string text = "";
            string[] array = inputIP.Split(new char[]
            {
                '.'
            });
            for (int i = 0; i < array.Length; i++)
            {
                string str = StringUtils.FillStr(3, array[i], '0', true);
                if (i > 0)
                {
                    text += ".";
                }
                text += str;
            }
            return text;
        }
        /// <summary>
        /// 判断是否是有效的IP
        /// </summary>
        /// <param name="inputIP"></param>
        /// <returns></returns>
        public static bool VaildIP(string inputIP)
        {
            if (string.IsNullOrEmpty(inputIP))
            {
                return false;
            }
            string[] array = inputIP.Split(new char[]
            {
                '.'
            });
            if (array.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    int num = StringUtils.ToInt(array[i], -1);
                    if (num < 0 || num > 255)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否在限制的IP范围之内(支持*号的判断)
        /// </summary>
        /// <param name="limitIPs"></param>
        /// <param name="fromIP"></param>
        /// <returns></returns>
        public static bool IsInLimitIP(string limitIPs, string fromIP)
        {
            if (string.IsNullOrEmpty(limitIPs) || string.IsNullOrEmpty(fromIP))
            {
                return false;
            }
            fromIP = StringUtils.SimpleIP(fromIP);
            string[] array = limitIPs.TrimEnd(new char[]
            {
                ','
            }).Split(new char[]
            {
                ','
            });
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                if (text.IndexOf('*') > 0)
                {
                    string text2 = text.Replace(".*.", ".");
                    text2 = text2.Replace(".*", "");
                    text2 = text2.Replace("*", "");
                    text2 = StringUtils.SimpleIP(text2);
                    if (fromIP.IndexOf(text2) >= 0)
                    {
                        bool result = true;
                        return result;
                    }
                }
                else
                {
                    if (fromIP.Equals(StringUtils.SimpleIP(text)))
                    {
                        bool result = true;
                        return result;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 比较两个ip是否相同(考虑格式化问题)
        /// </summary>
        /// <param name="ip1"></param>
        /// <param name="ip2"></param>
        /// <returns></returns>
        public static bool IsEqualIP(string ip1, string ip2)
        {
            if (string.IsNullOrEmpty(ip1) || string.IsNullOrEmpty(ip1))
            {
                return false;
            }
            if (ip1.Equals(ip2))
            {
                return true;
            }
            string text = StringUtils.FormatIP(ip1);
            string value = StringUtils.FormatIP(ip2);
            return text.Equals(value);
        }
        /// <summary>
        /// 检查是否是合理的 SQL  不正常的将记录日志
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="validStartSelect">是否验证以select开始</param>
        /// <returns></returns>
        public static bool IsValidSQL(string sql, bool validStartSelect)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return false;
            }
            string text = StringUtils.StandardSQL(sql);
            if (text.StartsWith("xp_") || text.IndexOf(" xp_") > 0 || text.StartsWith("sp_") || text.IndexOf(" sp_") > 0)
            {
                StringUtils.log.Warn("危险的SQL,sql=" + sql);
                return false;
            }
            string[] array = new string[]
            {
                "delete ",
                "drop ",
                "create ",
                "insert ",
                "truncate ",
                "update ",
                "exec ",
                "--"
            };
            for (int i = 0; i < array.Length; i++)
            {
                if (text.IndexOf(array[i]) >= 0)
                {
                    StringUtils.log.Warn("危险的SQL,sql=" + sql);
                    return false;
                }
            }
            if (validStartSelect && (text.IndexOf("select ") < 0 || text.IndexOf("select ") > 0))
            {
                StringUtils.log.Warn("查询SQL必须以select开始,sql=" + sql);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 转化为单词的语句 以单个空格为分隔符  by xdh 2006-07-07
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string StandardSQL(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return "";
            }
            string[] array = sql.Split(new char[]
            {
                ' ',
                '\r',
                '\n'
            });
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Trim().Length > 0)
                {
                    stringBuilder.Append(array[i].Trim().ToLower() + " ");
                }
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return "";
            }
            char[] array = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int num = (int)bytes[i];
                array[i * 2] = StringUtils.HexDigits[num >> 4];
                array[i * 2 + 1] = StringUtils.HexDigits[num & 15];
            }
            return new string(array);
        }
        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="byteLen"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes, int byteLen)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return "";
            }
            int num = bytes.Length;
            if (byteLen < num)
            {
                num = byteLen;
            }
            char[] array = new char[num * 2];
            for (int i = 0; i < num; i++)
            {
                int num2 = (int)bytes[i];
                array[i * 2] = StringUtils.HexDigits[num2 >> 4];
                array[i * 2 + 1] = StringUtils.HexDigits[num2 & 15];
            }
            return new string(array);
        }
        /// <summary>
        /// 将二进制字符串转换为字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByte(string hexString)
        {
            byte[] result;
            try
            {
                if (string.IsNullOrEmpty(hexString))
                {
                    result = null;
                }
                else
                {
                    int num = hexString.Length / 2;
                    byte[] array = new byte[num];
                    for (int i = 0; i < num; i++)
                    {
                        array[i] = (byte)Convert.ToInt32(hexString.Substring(i * 2, 2), 16);
                    }
                    result = array;
                }
            }
            catch (Exception ex)
            {
                StringUtils.log.Debug(ex);
                result = null;
            }
            return result;
        }
        /// <summary>
        /// 获取个性参数值字典(注意，不确认key的大小写)
        /// </summary>
        /// <param name="profileStr">个性参数串 格式如 key1=value1;key2=value2...</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetProfileDictionary(string profileStr)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(profileStr))
            {
                return dictionary;
            }
            string[] array = profileStr.Split(new char[]
            {
                ';'
            });
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                string[] array3 = text.Split(new char[]
                {
                    '='
                });
                if (array3.Length == 2)
                {
                    dictionary[array3[0]] = array3[1];
                }
            }
            return dictionary;
        }
        /// <summary>
        /// 获取个性参数值字典(注意，不确认key的大小写)
        /// </summary>
        /// <param name="dict">个性参数值字典</param>
        /// <returns></returns>
        public static string GetProfileStr(IDictionary<string, string> dict)
        {
            if (dict == null)
            {
                return "";
            }
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            foreach (KeyValuePair<string, string> current in dict)
            {
                if (!string.IsNullOrEmpty(current.Value))
                {
                    if (num > 0)
                    {
                        stringBuilder.Append(";");
                    }
                    stringBuilder.Append(current.Key + "=" + current.Value.Replace("=", "＝").Replace(";", "；"));
                    num++;
                }
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 合并个性参数串(返回合并后串，将secondProfileStr合并到1里，以firstProfileStr优先（即firstProfileStr中优先） )
        /// </summary>
        /// <param name="firstProfileStr">个性参数串1 格式如 key1=value1;key2=value2...</param>
        /// <param name="secondProfileStr">个性参数串2 格式如 key1=value1;key2=value2...</param>
        /// <returns></returns>
        public static string MergeProfileStr(string firstProfileStr, string secondProfileStr)
        {
            if (string.IsNullOrEmpty(secondProfileStr))
            {
                return firstProfileStr;
            }
            if (string.IsNullOrEmpty(firstProfileStr))
            {
                return secondProfileStr;
            }
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            IDictionary<string, string> profileDictionary = StringUtils.GetProfileDictionary(firstProfileStr);
            IDictionary<string, string> profileDictionary2 = StringUtils.GetProfileDictionary(secondProfileStr);
            foreach (KeyValuePair<string, string> current in profileDictionary2)
            {
                dictionary[current.Key] = current.Value;
            }
            foreach (KeyValuePair<string, string> current2 in profileDictionary)
            {
                if (dictionary.ContainsKey(current2.Key.ToLower()))
                {
                    dictionary.Remove(current2.Key.ToLower());
                }
                else
                {
                    if (dictionary.ContainsKey(current2.Key))
                    {
                        dictionary.Remove(current2.Key);
                    }
                    else
                    {
                        if (dictionary.ContainsKey(current2.Key.ToUpper()))
                        {
                            dictionary.Remove(current2.Key.ToUpper());
                        }
                    }
                }
                dictionary[current2.Key] = current2.Value;
            }
            return StringUtils.GetProfileStr(dictionary);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="dictProfile">个性参数字典</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetProfileValue(IDictionary<string, string> dictProfile, string key, string defaultValue)
        {
            if (dictProfile == null || string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }
            if (dictProfile.ContainsKey(key.ToLower()))
            {
                return dictProfile[key.ToLower()];
            }
            if (dictProfile.ContainsKey(key))
            {
                return dictProfile[key];
            }
            if (dictProfile.ContainsKey(key.ToUpper()))
            {
                return dictProfile[key.ToUpper()];
            }
            return defaultValue;
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="dictProfile">个性参数字典</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetProfileValue(IDictionary<string, string> dictProfile, string key, bool defaultValue)
        {
            return StringUtils.ToBoolean(StringUtils.GetProfileValue(dictProfile, key, defaultValue.ToString()), defaultValue);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="dictProfile">个性参数字典</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetProfileValue(IDictionary<string, string> dictProfile, string key, int defaultValue)
        {
            return StringUtils.ToInt(StringUtils.GetProfileValue(dictProfile, key, defaultValue.ToString()), defaultValue);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="dictProfile">个性参数字典</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetProfileValue(IDictionary<string, string> dictProfile, string key, decimal defaultValue)
        {
            return StringUtils.ToDecimal(StringUtils.GetProfileValue(dictProfile, key, defaultValue.ToString()), defaultValue);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="profileStr">个性参数串 格式如 key1=value1;key2=value2...</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetProfileValue(string profileStr, string key, string defaultValue)
        {
            IDictionary<string, string> profileDictionary = StringUtils.GetProfileDictionary(profileStr);
            if (profileDictionary == null || profileDictionary.Count == 0)
            {
                return defaultValue;
            }
            return StringUtils.GetProfileValue(profileDictionary, key, defaultValue);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="profileStr">个性参数串 格式如 key1=value1;key2=value2...</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetProfileValue(string profileStr, string key, bool defaultValue)
        {
            return StringUtils.ToBoolean(StringUtils.GetProfileValue(profileStr, key, defaultValue.ToString()), defaultValue);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="profileStr">个性参数串 格式如 key1=value1;key2=value2...</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetProfileValue(string profileStr, string key, int defaultValue)
        {
            return StringUtils.ToInt(StringUtils.GetProfileValue(profileStr, key, defaultValue.ToString()), defaultValue);
        }
        /// <summary>
        /// 获取个性参数值(忽略key的大小写)
        /// </summary>
        /// <param name="profileStr">个性参数串 格式如 key1=value1;key2=value2...</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetProfileValue(string profileStr, string key, decimal defaultValue)
        {
            return StringUtils.ToDecimal(StringUtils.GetProfileValue(profileStr, key, defaultValue.ToString()), defaultValue);
        }
        /// <summary>
        /// 设置个性参数字典值
        /// </summary>
        /// <param name="dictProfile">个性参数字典</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void PutProfileValue(IDictionary<string, string> dictProfile, string key, string value)
        {
            if (dictProfile == null || string.IsNullOrEmpty(key))
            {
                return;
            }
            if (dictProfile.ContainsKey(key.ToLower()))
            {
                dictProfile.Remove(key.ToLower());
            }
            else
            {
                if (dictProfile.ContainsKey(key))
                {
                    dictProfile.Remove(key);
                }
                else
                {
                    if (dictProfile.ContainsKey(key.ToUpper()))
                    {
                        dictProfile.Remove(key.ToUpper());
                    }
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            dictProfile[key] = value;
        }
        /// <summary>
        /// 设置个性参数值，并返回添加后的参数串(忽略key的大小写)
        /// </summary>
        /// <param name="profileStr">个性参数串 格式如 key1=value1;key2=value2...</param>
        /// <param name="key">忽略key的大小写</param>
        /// <param name="value"></param>
        /// <returns>返回添加后的参数串</returns>
        public static string PutProfileValue(string profileStr, string key, string value)
        {
            IDictionary<string, string> dictionary = StringUtils.GetProfileDictionary(profileStr);
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>();
            }
            StringUtils.PutProfileValue(dictionary, key, value);
            return StringUtils.GetProfileStr(dictionary);
        }
        /// <summary>
        /// 获取数字的单个字母0-9 A-Z(10-35)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static char GetNumChar(int num)
        {
            if (num < 0)
            {
                return '0';
            }
            if (num >= 0 && num <= 9)
            {
                return num.ToString()[0];
            }
            if (num >= 10 && num <= 35)
            {
                int num2 = num - 10 + 65;
                return (char)num2;
            }
            return '0';
        }
        /// <summary>
        /// 获取字母的单个数字0-9 A-Z(10-35)
        /// </summary>
        /// <param name="numChar"></param>
        /// <returns></returns>
        public static int GetCharNum(char numChar)
        {
            if (numChar < '\0')
            {
                return 0;
            }
            if (numChar >= '\0' && numChar <= '\t')
            {
                return (int)numChar;
            }
            if (numChar >= '0' && numChar <= '9')
            {
                return (int)(numChar - '0');
            }
            if (numChar >= 'A' && numChar <= 'Z')
            {
                return (int)(numChar - 'A' + '\n');
            }
            return 0;
        }
        /// <summary>
        /// URL串参数值的Encode 因为C#的Request[*]自动对接受的参数UrlDecode了，所以不再提供UrlDecode
        ///
        /// http://www.happycloudy.com/2010/05/24/c-sharp%E7%9A%84request-querystring%E5%87%BD%E6%95%B0%E4%BD%BF%E7%94%A8%E6%B3%A8%E6%84%8F%E4%B8%80%E5%88%99/
        /// http://www.vvcha.cn/c.aspx?id=12441
        ///
        /// C#的Request[] 已经自动对接受的参数解码了的
        ///
        /// 这是因为C#当中的request.querystring函数，已经对参数进行了自动解码，此时对获取的参数再进行urldecode，就会将已经解码的“+”号过滤掉
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpUtility.JavaScriptStringEncode(str);
        }
        /// <summary>
        /// URL串参数值的Decode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 将字符串中的html编码，防止label显示成html内容（支持空串）
        /// 支持空串，等价HttpUtility.HtmlEncode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpUtility.HtmlEncode(str);
        }
        /// <summary>
        /// 将字符串中的html编码，防止label显示成html内容（支持空串） 注意 同时 去掉 去掉引号(含 单双) 比如 ' %27  " %22  等
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HtmlEncode(HttpRequest request, string key)
        {
            return StringUtils.HtmlEncode(request, key, true, "", 0);
        }
        /// <summary>
        /// 将字符串中的html编码，防止label显示成html内容（支持空串） 注意 同时 去掉 去掉引号(含 单双) 比如 ' %27  " %22  等
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">如果请求值为空 给默认值</param>
        /// <returns></returns>
        public static string HtmlEncode(HttpRequest request, string key, string defaultValue)
        {
            return StringUtils.HtmlEncode(request, key, true, defaultValue, 0);
        }
        /// <summary>
        /// 将字符串中的html编码，防止label显示成html内容（支持空串） 注意 同时 去掉 去掉引号(含 单双) 比如 ' %27  " %22  等
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="maxLen">限制长度 大于等于0 限制</param>
        /// <returns></returns>
        public static string HtmlEncode(HttpRequest request, string key, int maxLen)
        {
            return StringUtils.HtmlEncode(request, key, true, "", maxLen);
        }
        /// <summary>
        /// 将字符串中的html编码，防止label显示成html内容（支持空串）
        /// 支持空串，等价HttpUtility.HtmlEncode
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="isRemoveQuoteChar">是否去掉引号(含 单双) 比如 ' %27  " %22  默认去掉</param>
        /// <param name="defaultValue">如果请求值为空 给默认值</param>
        /// <param name="maxLen">限制长度 大于等于0 限制</param>
        /// <returns></returns>
        public static string HtmlEncode(HttpRequest request, string key, bool isRemoveQuoteChar, string defaultValue, int maxLen)
        {
            if (request == null || string.IsNullOrEmpty(key))
            {
                return "";
            }
            string text = request[key];
            if (string.IsNullOrEmpty(text))
            {
                return defaultValue;
            }
            if (isRemoveQuoteChar)
            {
                string[] array = StringUtils.mHtmlQuoteStrArr;
                for (int i = 0; i < array.Length; i++)
                {
                    string oldValue = array[i];
                    text = text.Replace(oldValue, "");
                }
            }
            text = HttpUtility.HtmlEncode(text);
            if (maxLen > 0)
            {
                text = StringUtils.LimitStrLen(text, maxLen);
            }
            return text;
        }
        /// <summary>
        /// 将字符串中的html编码还原（支持空串）
        /// 支持空串，等价HttpUtility.HtmlDecode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpUtility.HtmlDecode(str);
        }
        /// <summary>
        /// 获取汉字串全拼音
        /// </summary>
        /// <param name="strHZ">汉字串 非汉字则原字母返回</param>
        /// <returns></returns>
        public static string GetHzFullPinYin(string strHZ)
        {
            if (string.IsNullOrEmpty(strHZ))
            {
                return "";
            }
            return Chinese2PinYinUtils.Convert(strHZ);
        }
        /// <summary> 
        /// 获取汉字串的首字母串
        /// </summary> 
        /// <param name="strHZ">汉字串</param> 
        /// <returns></returns> 
        public static string GetHzSimplePinYin(string strHZ)
        {
            if (string.IsNullOrEmpty(strHZ))
            {
                return "";
            }
            return Chinese2PinYinUtils.GetFirstPinYin(strHZ);
        }
        /// <summary>
        /// 获取文件长度描述
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFileLengthStr(long length)
        {
            if (length <= 0L)
            {
                return "";
            }
            if (length < 1024L)
            {
                return "1K";
            }
            if (length < 1024000L)
            {
                return (1.0 * (double)length / 1024.0).ToString("F2").Replace(".00", "") + "K";
            }
            if (length < 1024000000L)
            {
                return (1.0 * (double)length / 1024000.0).ToString("F2").Replace(".00", "") + "M";
            }
            return (1.0 * (double)length / 1024000000.0).ToString("F2").Replace(".00", "") + "G";
        }
        /// <summary>
        /// 获取耗时中文说明
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static string GetTimeSpanNote(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan timeSpan = dtEnd - dtStart;
            string text = "耗时：";
            if (timeSpan.Hours > 0)
            {
                text = text + timeSpan.Hours + "小时";
            }
            if (timeSpan.Minutes > 0)
            {
                text = text + timeSpan.Minutes + "分";
            }
            if (timeSpan.Seconds > 0)
            {
                text = text + timeSpan.Seconds + "秒";
            }
            if (timeSpan.Milliseconds > 0)
            {
                text = text + timeSpan.Milliseconds + "毫秒";
            }
            else
            {
                text += "1毫秒以下";
            }
            return text;
        }
        /// <summary>
        /// 获取某字符串 前显示明文几位后明文几位 中间*号隐藏的串(比如 显示用户手机号 中间几位为* 等 )
        /// </summary>
        /// <param name="str">原串</param>
        /// <param name="beforeShowLen">前面明文显示几位</param>
        /// <param name="afterShowLen">后面明文显示几位</param>
        /// <returns></returns>
        public static string GetMiddleHideStr(string str, int beforeShowLen, int afterShowLen)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (beforeShowLen + afterShowLen >= str.Length)
            {
                return str;
            }
            int num = str.Length - beforeShowLen - afterShowLen;
            string str2 = str.Substring(0, beforeShowLen);
            string str3 = str.Substring(beforeShowLen + num, afterShowLen);
            return str2 + StringUtils.RepeatChar('*', num) + str3;
        }
        /// <summary>
        ///             查找之前和之后特征文本的中间字符串
        /// </summary>
        /// <param name="preText">之前特征文本</param>
        /// <param name="afterText">之后特征文本</param>
        /// <param name="srcText">源文本</param>
        /// <returns></returns>
        public static string FindBetweenStr(string preText, string afterText, string srcText)
        {
            if (string.IsNullOrEmpty(srcText))
            {
                return "";
            }
            int num = srcText.IndexOf(preText) + preText.Length;
            if (num < 0 || num < preText.Length)
            {
                return "";
            }
            string text = srcText.Substring(num);
            int num2 = text.IndexOf(afterText);
            if (num2 <= -1)
            {
                return "";
            }
            return text.Substring(0, num2);
        }
        /// <summary>
        /// 返回去除HTML标记后的内容
        /// </summary>
        /// <param name="htmlStr"></param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string htmlStr)
        {
            if (string.IsNullOrEmpty(htmlStr))
            {
                return "";
            }
            htmlStr = Regex.Replace(htmlStr, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "-->", "", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "<!--.*", "", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
            htmlStr = Regex.Replace(htmlStr, "&#(\\d+);", "", RegexOptions.IgnoreCase);
            htmlStr.Replace("<", "");
            htmlStr.Replace(">", "");
            htmlStr.Replace("\r\n", "");
            return htmlStr;
        }
        /// <summary>
        /// 获取类名
        /// </summary>
        /// <param name="typeFullName">类型全名 格式 如 Hynet.Demo.Contract.IWcfDemoService,Hynet.Demo.Contract </param>
        /// <returns></returns>
        public static string GetClassName(string typeFullName)
        {
            if (string.IsNullOrEmpty(typeFullName))
            {
                return "";
            }
            string[] array = typeFullName.Split(new char[]
            {
                ','
            });
            if (array != null && array.Length > 0)
            {
                return array[0];
            }
            return "";
        }
        /// <summary>
        /// 获取程序集名
        /// </summary>
        /// <param name="typeFullName">类型全名 格式 如 Hynet.Demo.Contract.IWcfDemoService,Hynet.Demo.Contract </param>
        /// <returns></returns>
        public static string GetAssemblyName(string typeFullName)
        {
            if (string.IsNullOrEmpty(typeFullName))
            {
                return "";
            }
            string[] array = typeFullName.Split(new char[]
            {
                ','
            });
            if (array != null && array.Length > 1)
            {
                return array[1];
            }
            return "";
        }
        /// <summary>
        /// 获取程序集名 不含版本等信息
        /// </summary>
        /// <param name="type">类型 type.Assembly.FullName 格式 如 Hynet.Demo.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null </param>
        /// <returns></returns>
        public static string GetAssemblyName(Type type)
        {
            string[] array = type.Assembly.FullName.Split(new char[]
            {
                ','
            });
            if (array != null)
            {
                return array[0];
            }
            return "";
        }
        /// <summary>
        /// 返回最简JSON串
        ///
        /// return "{\"value\":\"" + value + "\",\"text\":\"" + text + "\"}";
        ///
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string GetComJSONStr(string value, string text)
        {
            return string.Concat(new string[]
            {
                "{\"value\":\"",
                value,
                "\",\"text\":\"",
                text,
                "\"}"
            });
        }
        /// <summary>
        /// 获取例外的实际内部错误原因
        /// Exception 有 InnerException 和 GetBaseException();
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionRealMessage(Exception ex)
        {
            if (ex == null)
            {
                return "";
            }
            Exception innerException = ex.InnerException;
            if (innerException != null)
            {
                return innerException.Message;
            }
            Exception baseException = ex.GetBaseException();
            if (baseException != null)
            {
                return baseException.Message;
            }
            return ex.Message;
        }
        /// <summary>
        /// 是否有效的Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string url)
        {
            return Regex.IsMatch(url, "^(https|http)://[a-zA-Z0-9.\\-_/?&%]+$");
        }
        /// <summary>
        /// 产生原始数字串（不带最后一位奇偶位）的 奇偶校验位数字(0-9)
        /// Parity 为 奇偶校验性
        /// </summary>
        /// <param name="orgNumberStr">5-30位长度的数字串</param>
        /// <returns>产生0-9 的校验位数字 </returns>
        public static int GenNumberStrParityBitNum(string orgNumberStr)
        {
            int length = orgNumberStr.Length;
            if (length < 5 || length > 30)
            {
                return 0;
            }
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < orgNumberStr.Length; i++)
            {
                char c = orgNumberStr[i];
                num2 += (int)char.GetNumericValue(c) * ((num + 1 != length) ? StringUtils.mParityBitFlagArry[num] : 1);
                num++;
            }
            num2 %= 10;
            return (num2 == 0) ? 0 : (10 - num2);
        }
        /// <summary>
        /// 校验含奇偶校验位(最后一位)的数字串（长度5-30以内）是否有效奇偶校验性
        /// Parity 为 奇偶校验性
        /// </summary>
        /// <param name="fullNumberStr">含奇偶校验位(最后一位)的数字串</param>
        /// <returns></returns>
        public static bool VerifyNumberStrIsValidParity(string fullNumberStr)
        {
            if (string.IsNullOrEmpty(fullNumberStr))
            {
                return false;
            }
            string orgNumberStr = fullNumberStr.Substring(0, fullNumberStr.Length - 1);
            int num = (int)char.GetNumericValue(fullNumberStr[fullNumberStr.Length - 1]);
            int num2 = StringUtils.GenNumberStrParityBitNum(orgNumberStr);
            return num2 == num;
        }
        /// <summary>
        /// 产生含奇偶校验位(最后一位)的新数字串
        /// Parity 为 奇偶校验性
        /// </summary>
        /// <param name="orgNumberStr">不带奇偶校验位的原始数字串</param>
        /// <returns>在orgNumberStr的最后一位加上 新产生的奇偶校验位数字</returns>
        public static string GenNumberStrParityBitNewStr(string orgNumberStr)
        {
            return orgNumberStr + StringUtils.GenNumberStrParityBitNum(orgNumberStr).ToString();
        }
        /// <summary>
        /// 获取字符串字典的[Key1=Value1;Key2=Value2] 等 格式内容串（一般用于日志输出显示）
        /// </summary>
        /// <param name="dict"></param>
        public static string GetStringDictKeyValueStrForDebug(IDictionary<string, string> dict)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (dict == null)
            {
                stringBuilder.Append("[null]");
                return stringBuilder.ToString();
            }
            if (dict.Count == 0)
            {
                stringBuilder.Append("[count=0]");
                return stringBuilder.ToString();
            }
            stringBuilder.Append("[");
            int num = 0;
            foreach (KeyValuePair<string, string> current in dict)
            {
                if (num > 0)
                {
                    stringBuilder.Append(";");
                }
                stringBuilder.Append(current.Key + "=" + current.Value);
                num++;
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 获取对象串字典的Key1=Value1;Key2=Value2 等 格式内容串（一般用于日志输出显示）
        /// </summary>
        /// <param name="dict"></param>
        public static string GetObjDictKeyValueStrForDebug(IDictionary<string, object> dict)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (dict == null)
            {
                stringBuilder.Append("[null]");
                return stringBuilder.ToString();
            }
            if (dict.Count == 0)
            {
                stringBuilder.Append("[count=0]");
                return stringBuilder.ToString();
            }
            stringBuilder.Append("[");
            int num = 0;
            foreach (KeyValuePair<string, object> current in dict)
            {
                if (current.Value == null)
                {
                    StringUtils.log.WarnFormat("GetObjDictKeyValueStrForDebug key={0} 的值为null", current.Key);
                }
                else
                {
                    if (num > 0)
                    {
                        stringBuilder.Append(";");
                    }
                    stringBuilder.Append(current.Key + "=" + current.Value.ToString());
                    num++;
                }
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 获取对象串字典的Key1=Value1;Key2=Value2 等 格式内容串（一般用于日志输出显示）
        /// </summary>
        /// <param name="dict"></param>
        public static string GetObjDictKeyValueStrForDebug<K, V>(IDictionary<K, V> dict)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (dict == null)
            {
                stringBuilder.Append("[null]");
                return stringBuilder.ToString();
            }
            if (dict.Count == 0)
            {
                stringBuilder.Append("[count=0]");
                return stringBuilder.ToString();
            }
            stringBuilder.Append("[");
            int num = 0;
            foreach (KeyValuePair<K, V> current in dict)
            {
                if (current.Value == null)
                {
                    StringUtils.log.WarnFormat("GetObjDictKeyValueStrForDebug key={0} 的值为null", current.Key);
                }
                else
                {
                    if (num > 0)
                    {
                        stringBuilder.Append(";");
                    }
                    StringBuilder arg_BD_0 = stringBuilder;
                    object arg_B8_0 = current.Key;
                    object arg_B8_1 = "=";
                    V value = current.Value;
                    arg_BD_0.Append(StringUtils.NotNullStr(arg_B8_0) + arg_B8_1 + value.ToString());
                    num++;
                }
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}
