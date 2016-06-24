//-----------------Commented out by Lawrence Foley on 03/13/2015, files are now uploaded in UploadMedia.aspx.cs------------//

//using System;
//using System.IO;

///// <summary>
///// Summary description for SilverlightUploadSession
///// </summary>
//public class SilverlightUploadSession
//{
//    public UserAccount Account { get; private set; }
//    public String Username { get; private set; }
//    public String Password { get; private set; }
//    public Upload Upload { get; set; }
//    public String FileName { get; private set; }
//    public long CurrentSize { get; private set; }
//    public String[] Images { get; set; }
//    public DateTime LastKeepAlive { get; set; }

//    private FileStream writer;

//    public SilverlightUploadSession(String username, String password, String fileName)
//    {
//        Account = new UserAccount(username);
//        Username = username;
//        Password = password;
//        FileName = fileName;

//        CurrentSize = 0;
//        LastKeepAlive = DateTime.Now;

//        writer = new FileStream(fileName, FileMode.Create);
//    }

//    public void WriteChunk(byte[] chunk)
//    {
//        writer.Write(chunk, 0, chunk.Length);
//        CurrentSize += chunk.Length;
//    }

//    public void Close()
//    {
//        writer.Close();
//    }

//    public void Delete()
//    {
//        String folder = Path.GetDirectoryName(FileName);
//        File.Delete(FileName);
//        Directory.Delete(folder);
//    }
//}
