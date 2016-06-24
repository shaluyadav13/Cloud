using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

/// <summary>
/// A set of methods 
/// </summary>
public static class Utility
{

    public static String ComputeMD5Hash(this String s)
    {
        MD5 md5 = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(s);
        byte[] hashedBytes = md5.ComputeHash(bytes);
        StringBuilder sb = new StringBuilder("");
        foreach (byte b in hashedBytes)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }


    //emailBegining = "<h1 style=\"color: rgb(7, 71, 49)\">Northwest Cloud</h1><p>Your website has been converted on Northwest Cloud and is available at";

    /// <summary>
    /// Sends and email using the values in AppSettings
    /// </summary>
    /// <param name="emailAddress">The address to send the email to</param>
    /// <param name="emailSubject">The subject line of the email</param>
    /// <param name="emailBody">The content of the email</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SendEmail(string emailAddress, string emailSubject, string emailBody)
    {
        // If the email string is empty, don't try to send an email
        if (!string.IsNullOrEmpty(emailAddress))
        {
            try
            {
                MailAddress to = new MailAddress(emailAddress);
                MailAddress from = new MailAddress(AppSettings.AppEmailAddress);

                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                message.Subject = emailSubject;
                message.Body = emailBody;

                SmtpClient smtp = new SmtpClient(AppSettings.SMTPServerPath);
                smtp.Port = AppSettings.AppEmailPort;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;

                string pass = AppSettings.AppEmailPassword;
                NetworkCredential cred = new NetworkCredential(from.Address, pass);
                smtp.Credentials = cred;
                smtp.Timeout = 100000;
                smtp.Send(message);
                return true;

            }
            catch (Exception ex)
            {
                ApplicationLogger.LogItem(null, "Error sending email using 'Utility.cs' : " + ex.ToString());
                return false;
            }
        }
        // No email address
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the number of bytes free on the disk
    /// </summary>
    /// <returns>Number of bytes free on disk</returns>
    public static long getFreeDiskSpace()
    {
        try
        {
            return new DriveInfo("C").AvailableFreeSpace;
        }
        catch (Exception ex)
        {
            ApplicationLogger.LogItem(null, "Error getting disk space using 'Utility.cs' : " + ex.ToString());
            return -1;
        }
    }

    /// <summary>
    /// Formats bytes to a human readable form such as '10.42 GB'
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string formatBytesToString(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        int order = 0;
        while (bytes >= 1024 && order + 1 < sizes.Length)
        {
            order++;
            bytes /= 1024;
        }
        return String.Format("{0:0.##} {1}", bytes, sizes[order]);
    }

    /// <summary>
    /// Returns a human-readable value for the free space on the disk such as '10.42 GB'
    /// </summary>
    /// <returns></returns>
    public static string getFreeDiskSpaceString()
    {
        return formatBytesToString(getFreeDiskSpace());
    }

    public static string getFreeDiskSpacePercentage()
    {
        try
        {
            return String.Format("{0}%", Math.Round(getFreeDiskSpace() / (double)new DriveInfo("C").TotalSize, 4) * 100);
        }
        catch (Exception ex)
        {
            ApplicationLogger.LogItem(null, "Error getting disk size using 'Utility.cs' : " + ex.ToString());
            return "error";
        }
    }

    /// <summary>
    /// Returns true if the there is more disk space than the set threshold
    /// </summary>
    /// <returns></returns>
    public static bool canUpload()
    {
        return getFreeDiskSpace() > AppSettings.getFreeDiskSpaceThreshold;
    }
}
