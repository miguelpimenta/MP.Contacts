using System;
using System.IO;
using System.Reflection;

namespace MP.Contacts.Utils
{
    internal sealed class Log2Txt
    {
        public string LogsDir { get; set; }

        #region Singleton

        private static Log2Txt instance = null;
        private static object lockThis = new object();

        private Log2Txt()
        {
            LogsDir = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(LogsDir))
            {
                Directory.CreateDirectory("Logs");
            }
#if DEBUG

#endif
        }

        public static Log2Txt Instance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                    {
                        instance = new Log2Txt();
                    }

                    return instance;
                }
            }
        }

        #endregion Singleton

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="errorMessage"> Error msg.</param>
        public void ErrorLog(string errorMessage)
        {
            StreamWriter sw = null;
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            try
            {
                sw = new StreamWriter(LogsDir + date + "-ErrorLog.txt", true);
                sw.WriteLine(DateTime.Now + ": " + Environment.NewLine + errorMessage);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                sw = new StreamWriter(Path.GetTempPath() + date + "-ErrorLog_" + Assembly.GetEntryAssembly().GetName().Name + ".txt", true);
                sw.WriteLine(DateTime.Now + ": " + Environment.NewLine + ex + Environment.NewLine + errorMessage);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="errorCode"> Error code.</param>
        /// <param name="errorMessage"> Error msg.</param>
        /// <param name="exception"> Exception info.</param>
        public void ErrorLog(string errorCode, string errorMessage, Exception exception)
        {
            StreamWriter sw = null;
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            try
            {
                sw = new StreamWriter(LogsDir + date + "-ErrorLog.txt", true);
                sw.WriteLine(DateTime.Now + ": " + Environment.NewLine + "|" + errorCode + "| " + Environment.NewLine + errorMessage + Environment.NewLine + "Exception Type: " + exception.GetType() + Environment.NewLine + "Exception Message: " + exception.Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                sw = new StreamWriter(Path.GetTempPath() + date + "-ErrorLog_" + Assembly.GetEntryAssembly().GetName().Name + ".txt", true);
                sw.WriteLine(DateTime.Now + ": " + Environment.NewLine + ex + Environment.NewLine + errorMessage);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Info log
        /// </summary>
        /// <param name="message"> Info msg.</param>
        public void InfoLog(string message)
        {
            StreamWriter sw = null;
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            try
            {
                sw = new StreamWriter(LogsDir + date + "-InfoLog.txt", true);
                sw.WriteLine(DateTime.Now + ": " + Environment.NewLine + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                sw = new StreamWriter(Path.GetTempPath() + date + "-InfoLog_" + Assembly.GetEntryAssembly().GetName().Name + ".txt", true);
                sw.WriteLine(DateTime.Now + ": " + Environment.NewLine + ex + Environment.NewLine + message);
                sw.Flush();
                sw.Close();
            }
        }
    }
}