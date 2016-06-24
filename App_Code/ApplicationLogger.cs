using System;

/// <summary>
/// Summary description for ApplicationLogger
/// </summary>
public class ApplicationLogger
{
    //private const String logLocation = @"c:\NWVideoLogs\AppLog.xml";
    private static bool logEnabled = true;

    public static void LogItem(UserAccount account, String message)
    {
        LogItem(account, message, "N/A");
    }

    /// <summary>
    /// Saves a message into the database log.
    /// </summary>
    /// <param name="account">A UserAccount for the session which generated this message. Pass in null to log it as a system event.</param>
    /// <param name="message"></param>
    public static void LogItem(UserAccount account, String message, String videoID)
    {
        if (logEnabled)
        {
            try
            {
                DBDataContext db = DBDataContext.CreateInstance();
                AppLogItem logItem = new AppLogItem();

                if (account != null)
                {
                    //logItem.AppLogItemID = 1;
                    logItem.AccountType = account.OU.ToString();
                    logItem.Username = account.Username;
                    logItem.FirstName = account.FirstName;
                    logItem.LastName = account.LastName;
                }
                else
                {
                   // logItem.AppLogItemID = 1;
                    logItem.AccountType = "System";
                    logItem.Username = "System";
                    logItem.FirstName = "";
                    logItem.LastName = "";
                }
                logItem.Time = DateTime.Now;
                logItem.Message = message;
                logItem.VideoID = videoID;

                db.AppLogItems.InsertOnSubmit(logItem);
                db.SubmitChanges();

                /*TextReader reader = new StreamReader(logLocation);
                XmlSerializer deserializer = new XmlSerializer(typeof(Log));
                Log log = (Log)deserializer.Deserialize(reader);
                reader.Close();

                LogItem item = new LogItem();
                item.Time = DateTime.Now;
                item.User = account;
                item.Message = message;

                writeItemToLogFile(item, log);
                TextWriter writer = new StreamWriter(logLocation);
                XmlSerializer serializer = new XmlSerializer(typeof(Log));
                serializer.Serialize(writer, log);
                writer.Close();*/

            }
            catch { }
        }
    }

   
    /*private static void writeItemToLogFile(LogItem item, Log log)
    {
        DateTime today = DateTime.Now.Date;
        var todaysLogs = from i in log.Day
                         where i.Date == today
                         select i;

        // Find today's logs.
        LogDay todaysLog;
        if (todaysLogs.Count() > 0)
        {
            todaysLog = todaysLogs.Single();
        }
        else
        {
            todaysLog = new LogDay();
            todaysLog.Date = today;

            // Not sure why this is required, but the Date won't save to the XML file without it.
            // The userLog attributes work just fine and don't have similar fields.
            todaysLog.DateSpecified = true; 
            
            todaysLog.UserLog = new List<LogDayUserLog>();
            log.Day.Add(todaysLog);
        }

        // Find the user's logs.
        var usersLogs = from i in todaysLog.UserLog
                        where i.Username == item.User.Username.ToLower()
                        select i;

        LogDayUserLog userLog;
        if (usersLogs.Count() > 0)
        {
            userLog = usersLogs.First();
        }
        else
        {
            userLog = new LogDayUserLog();
            userLog.Username = item.User.Username.ToLower();
            userLog.CommonName = String.Format("{0}, {1}", 
                                               item.User.LastName, 
                                               item.User.FirstName);
            userLog.AccountType = item.User.OU.ToString();
            userLog.Event = new List<string>();
            todaysLog.UserLog.Add(userLog);
        }

        // Finally log the event.
        userLog.Event.Add(String.Format("{0}   {1}",
                                        item.Time.ToString("hh:mm:ss.fff tt"),
                                        item.Message));
    }*/
}

public class LogItem
{
    public UserAccount User { get; set; }
    public DateTime Time { get; set; }
    public String Message { get; set; }
}