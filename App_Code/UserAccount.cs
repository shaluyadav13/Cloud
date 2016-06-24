using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cite.DomainAuthentication;
using MySql;
using MySql.Web;

/// <summary>
/// This class extends Cite.DomainAuthentication.DomainAccount and adds a property
/// to determine whether an account has administrative access to the application. It
/// also adds a property to determine whether the account is allowed to log in, by
/// checking organizational units.
/// </summary>
public class UserAccount : DomainAccount
{
    private bool _isAdmin = false;
    private bool _canLogin = false;

    /// <summary>
    /// Looks up a user account but does not perform authentication.
    /// </summary>
    /// <param name="username"></param>
    public UserAccount(String username)
        : base(username)
    {
        lookupPrivileges();
    }

    public UserAccount(String username, String password)
        : base(username, password)
    {
        lookupPrivileges();
    }

    public UserAccount(String username, String password, int timeout)
        : base(username, password, timeout)
    {
        lookupPrivileges();
    }

    private void lookupPrivileges()
    {
        //if (this.Username == "testFaculty")
       // {
       //     _canLogin = true;
        //}
        if (this.Exists)
        {
            lookupAdmin();
           // string tempFac = "testFaculty";
            //Session["username"] = tempFac;
            //if(Session["username"] == "testFaculty");
        

            bool value = this.OU == OrganizationalUnit.FacultyUsers ||
                         this.OU == OrganizationalUnit.StaffUsers ||
                         this._isAdmin;

            if (!value && this.OU == OrganizationalUnit.StudentUsers)
            {
                // If the user is a student, check the student groups to see if the student is allowed.
               // DateTime now = DateTime.Now.Date;
                value = (from i in DBDataContext.CreateInstance().AuthorizedStudents
                         where i.Username.ToLower() == this.Username.ToLower()
                       //  && i.StudentGroup.StartDate <= now && i.StudentGroup.EndDate >= now
                         select i).Count() > 0;
            }
            _canLogin = value;
        }
    }

    private void lookupAdmin()
    {
        try
        {
           DBDataContext db = DBDataContext.CreateInstance();         

            _isAdmin = (from i in db.Admins
             where i.Username.ToLower() == this.Username.ToLower()
             select i).Count() == 1;
        }
        catch (Exception)
        { 
        }
    }
  
    public bool Admin
    {
        get { return _isAdmin; }
    }

    public bool CanLogin
    {
        get { return _canLogin; }
    }
}
