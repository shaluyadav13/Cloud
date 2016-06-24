//-----------------Commented out by Lawrence Foley on 03/13/2015, the flash media server is no longer used, the files are store on the server directly------------//

//using System;
//using System.IO;
//using System.Net.Sockets;
//using VideoTransfer.Common;

///// <summary>
///// Summary description for FileTransfer
///// </summary>
//public class FileTransfer
//{
//    private Socket socket;
//    private NetworkStream stream;
//    public String VideoID { get; set; }
//    public UserAccount User { get; set; }

//    /// <summary>
//    /// Creates a new FileTransfer object, but does not connect it.
//    /// </summary>
//    public FileTransfer()
//    { 
//        VideoID = "N/A";
//        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//    }

//    /// <summary>
//    /// Connects this FileTransfer object to the file server on the Flash Media Server.
//    /// </summary>
//    public void Connect()
//    {
//        socket.Connect(ServerPath, TcpPort);
//        stream = new NetworkStream(socket);
//    }

//    /// <summary>
//    /// Indicates whether or not this FileTransfer object is connected.
//    /// </summary>
//    /// <returns></returns>
//    public bool IsConnected()
//    {
//        return socket.Connected;
//    }

//    /// <summary>
//    /// Attempts to send a file to the Flash Media Server.
//    /// </summary>
//    /// <param name="filePath">The path to the file that should be transferred.</param>
//    /// <returns>A ResponseType enum containing a message indicating success or failure or other.</returns>
//    public ResponseType SendAddRequest(String filePath)
//    {
//        ApplicationLogger.LogItem(User, "FileTransfer: Preparing to send add file request.", VideoID);
//        // Find the size of the input file.
//        long fileSize = (new FileInfo(filePath)).Length;
//        ApplicationLogger.LogItem(User, "FileTransfer: File size is " + fileSize + ".");

//        // Send an initial request to indicate file name and size.
//        ApplicationLogger.LogItem(User, "FileTransfer: Sending initial request to indicate file name and size.", VideoID);
//        BeginFileTransfer req = new BeginFileTransfer();
//        req.FileName = Path.GetFileName(filePath);
//        req.FileSize = fileSize;

//        req.Serialize(stream);

//        // Wait for a response.
//        ApplicationLogger.LogItem(User, "FileTransfer: Waiting for response from file transfer service.", VideoID);
//        FileResponse response = FileResponse.Deserialize(stream);

//        ApplicationLogger.LogItem(User, response.ResponseType.ToString(), VideoID);

//        // Check the status.
//        if (response.ResponseType != ResponseType.Successful)
//        {
//            ApplicationLogger.LogItem(User, "FileTransfer: Request rejected by file transfer service, aborting.", VideoID);
//            stream.Close();
//            socket.Close();
//            return response.ResponseType;
//        }
//        ApplicationLogger.LogItem(User, "FileTransfer: Response received, file transfer service is waiting.", VideoID);


//        int tenMB = 1024 * 1024 * 10; // 10 MB in bytes.

//        ApplicationLogger.LogItem(User, "FileTransfer: Opening video file.", VideoID);
//        FileStream fs = new FileStream(filePath, FileMode.Open);
//        long pointer = 0;

//        ApplicationLogger.LogItem(User, "FileTransfer: Starting file transfer.", VideoID);
//        // Start sending chunks.
//        while (pointer < fileSize)
//        {
//            // Load the current chunk of the file.
//            FileChunk chunk = new FileChunk();

//            // Read in 10 MB if possible, otherwise read to end.
//            long amountToRead = fileSize - pointer;
//            chunk.IsLastChunk = true;

//            if (amountToRead > tenMB)
//            {
//                chunk.IsLastChunk = false;
//                amountToRead = tenMB;
//            }

//            chunk.File = new byte[amountToRead];

//            fs.Position = pointer;
//            ApplicationLogger.LogItem(User, "FileTransfer: Reading " + amountToRead + " bytes starting at offset " + pointer + ".", VideoID);
//            fs.Read(chunk.File, 0, (int)amountToRead);

//            // Send the request.
//            ApplicationLogger.LogItem(User, "FileTransfer: Sending file chunk.", VideoID);
//            chunk.Serialize(stream);

//            // Wait for a response.
//            ApplicationLogger.LogItem(User, "FileTransfer: Waiting for response from file transfer service.", VideoID);
//            FileResponse res = FileResponse.Deserialize(stream);

//            // If the response is bad, close the connection and return.
//            if (res.ResponseType != ResponseType.Successful)
//            {
//                ApplicationLogger.LogItem(User, "FileTransfer: Error from file transfer service, aborting.", VideoID);
//                fs.Close();
//                stream.Close();
//                socket.Close();
//                return res.ResponseType;
//            }

//            ApplicationLogger.LogItem(User, "FileTransfer: Successful response received.", VideoID);

//            // If we just sent the last chunk, clean up.
//            if (res.ResponseType == ResponseType.Successful && chunk.IsLastChunk)
//            {
//                ApplicationLogger.LogItem(User, "FileTransfer: File transfer completed, closing connection.", VideoID);
//                fs.Close();
//                stream.Close();
//                socket.Close();
//                return res.ResponseType;
//            }
//            // If we didn't send the last chunk, prepare for the next one.
//            else
//            {
//                ApplicationLogger.LogItem(User, "FileTransfer: Preparing to send next file chunk.", VideoID);
//                // Increment the file pointer to the next chunk.
//                pointer += tenMB;
//            }
//        }

//        ApplicationLogger.LogItem(User, "FileTransfer: An unknown error occurred.", VideoID);
//        return ResponseType.UnknownError;
//    }

//    /// <summary>
//    /// Sends a request to delete a video.
//    /// </summary>
//    /// <param name="videoID">The VideoID of the file to delete.</param>
//    /// <returns></returns>
//    public FileResponse SendDeleteRequest(String videoID)
//    {
//        ApplicationLogger.LogItem(User, "FileTransfer: Sending delete file request.", videoID);
//        DeleteFile req = new DeleteFile();
//        req.FileName = videoID;
//        req.Serialize(stream); 

//        FileResponse res = FileResponse.Deserialize(stream);
//        if (res.ResponseType == ResponseType.Successful)
//        {
//            ApplicationLogger.LogItem(User, "FileTransfer: File deleted, closing connection.", videoID);
//        }
//        else
//        {
//            ApplicationLogger.LogItem(User, "FileTransfer: Failed to delete file, closing connection.", videoID);
//        }
//        stream.Close();
//        socket.Close();
//        return res;
//    }

//    /// <summary>
//    /// Indicates whether the file transfer service on the Flash Media Server
//    /// is running.
//    /// </summary>
//    /// <returns></returns>
//    public static bool IsFileTransferServiceUp()
//    {
//        try
//        {
//            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            server.Connect(ServerPath, TcpPort);

//            NetworkStream stream = new NetworkStream(server);

//            Ping req = new Ping();

//            req.Serialize(stream);
//            FileResponse res = FileResponse.Deserialize(stream);

//            stream.Close();
//            server.Close();

//            return (res.ResponseType == ResponseType.Pingback);
//        }
//        catch (Exception)
//        {
//            return false;
//        }
//    }

//    /// <summary>
//    /// Contacts the Flash Media Server to find how much disk space it has remaining.
//    /// Note the disk space is intentionally under-reported by 5 GB.
//    /// </summary>
//    /// <returns>The number of bytes of free space, or -1 if the FMS could not be contacted.</returns>
//    public static long GetFileServerFreeSpace()
//    {
//        try
//        {

//            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            server.Connect(ServerPath, TcpPort);

//            NetworkStream stream = new NetworkStream(server);

//            Ping req = new Ping();

//            req.Serialize(stream);
//            FileResponse res = FileResponse.Deserialize(stream);

//            stream.Close();
//            server.Close();

//            return res.FreeDiskSpace;
//        }
//        catch (Exception)
//        {
//            return -1;
//        }
//    }

//    private static String ServerPath
//    {
//        get { return AppSettings.FlashMediaServerPath; }
//    }

//    private static int TcpPort
//    {
//        get { return AppSettings.FileTransferPort; }
//    }
//}
