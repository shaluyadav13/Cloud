using System;

/// <summary>
/// Summary description for DBDataContext
/// </summary>
public partial class DBDataContext
{
    private static String connectionString;

    /// <summary>
    /// Factory pattern method. Do not call the default constructor for the DBDataContext, use this
    /// static method to create one instead. This method will automatically set the correct connection
    /// based on whether this is the test version or the production version.
    /// </summary>
    /// <returns></returns>
    public static DBDataContext CreateInstance()
    {
        //if (connectionString == null)
        //    connectionString = AppSettings.DatabaseConnectionString;
       
        return new DBDataContext();
    }
}
