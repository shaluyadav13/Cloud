using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;
using System.Diagnostics;

public partial class EditStudentGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    
    {
        try
        {
            // Redirect to the login page if the user is not signed in.
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);
            UserAccount account = (UserAccount)Session["account"];
            //sNumber.InnerHtml = account.Username;

            // Make sure the user is an admin.
            if (!((UserAccount)Session["account"]).Admin)
            {
                if (((UserAccount)Session["account"]).OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StudentUsers))
                    Response.Redirect("~/MyMedia.aspx", true);
            }
            savedSuccesfully.Visible = false;
            errorLabel.Text = "";
            studentErrorLabel.Text = "";

            if (!IsPostBack)
            {
                getGroupInfo();
                fillStudentListBox();
                
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void getGroupInfo()
   { 
        try
        {
            int id;

            if (int.TryParse(Request.QueryString["id"], out id))
            {
                DBDataContext db = DBDataContext.CreateInstance();
                //var numberOfRows = (from i in db.StudentGroups
                //                    where i.GroupID == id
                //                    select i).Count();
                //if
                var group = (from i in db.StudentGroups
                             where i.GroupID == id
                             select i);
                if (group.Count() == 0)
                {
                    groupNameTextBox.Text = "New Group";
                    descriptionTextBox.Text = "New Group created on " + DateTime.Now.ToShortDateString();
                    //startDateTextBox.Text = DateTime.Now.Date.ToString();
                    //endDateTextBox.Text = DateTime.Now.Date.AddDays(1).ToString();

                    //showing both date and time instead of date
                    startDateTextBox.Text = DateTime.Now.ToShortDateString();
                    endDateTextBox.Text = DateTime.Now.AddDays(1).ToShortDateString();

                    //Btn_GroupVisibility.Text = group.Single().ShowGroup ? "Stop Sharing" : "Share Videos";

                    if (((UserAccount)Session["account"]).Admin)
                    {
                        facultyTextBox.Text = "Not Assigned";
                    }
                    else if ((((UserAccount)Session["account"]).OU.Equals(OrganizationalUnit.StaffUsers)) || (((UserAccount)Session["account"]).OU.Equals(OrganizationalUnit.FacultyUsers)))
                    {
                        facultyTextBox.Text = ((UserAccount)Session["account"]).Username;
                        facultyTextBox.Enabled = false;

                    }
                  //  pnlStudents.Visible = false;
                    //g.FacultyOwner = "Not Assigned";
                }

                else
                {
                    groupNameTextBox.Text = group.Single().GroupName;
                    descriptionTextBox.Text = group.Single().Description;
                    startDateTextBox.Text = group.Single().StartDate.ToShortDateString();
                    endDateTextBox.Text = group.Single().EndDate.ToShortDateString();
                    chk_GroupVisibility.SelectedValue = group.Single().ShowGroup ? "allMembers" : "ownerOnly";
                    if ((((UserAccount)Session["account"]).OU.Equals(OrganizationalUnit.StaffUsers)) || (((UserAccount)Session["account"]).OU.Equals(OrganizationalUnit.FacultyUsers)))
                    {
                        facultyTextBox.Enabled = false;
                    }
                    facultyTextBox.Text = group.Single().FacultyOwner;
                    pnlStudents.Visible = true;

                    //Btn_GroupVisibility.Text = group.Single().ShowGroup ? "Stop Sharing" : "Share";
                }
            }
            else
            {
                Response.Redirect("StudentGroups.aspx",false);
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    private void fillStudentListBox()
    {
        try
        {
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                studentsListBox.Items.Clear();
                DBDataContext db = DBDataContext.CreateInstance();
                var group = (from i in db.StudentGroups
                             where i.GroupID == id
                             select i);
                if (group.Count() > 0)
                {

                    IList<AuthorizedStudent> mygroups = group.Single().AuthorizedStudents;
                    
                  // var mygroups1 = mygroups.OrderBy(c => c.Username).ToList();
                    
                    foreach (var student in mygroups)
                    {
                        UserAccount account = new UserAccount(student.Username);
                        String display = String.Format("{0} {1}, {2}",
                                                       account.Username,
                                                       account.LastName,
                                                       account.FirstName);
                        studentsListBox.Items.Add(new ListItem(display, account.Username.ToLower()));
                    }

                    if (group.Single().AuthorizedStudents.Count() == 0)
                    {
                        studentsListBox.Visible = false;
                        studentLabel.Visible = true;
                        deleteStudentButton.Visible = false;
                    }
                    else
                    {
                        studentsListBox.Visible = true;
                        studentLabel.Visible = false;
                        deleteStudentButton.Visible = true;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            studentErrorLabel.Text = ex.Message;
        }
    }

    protected void saveInfoButton_Click(object sender, EventArgs e)
    {
        try
        {
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                DBDataContext db = DBDataContext.CreateInstance();
                var group = (from i in db.StudentGroups
                             where i.GroupID == id
                             select i);
                //this condition updates the student group details.
                if (group.Count() > 0)
                {
                    saveOrEdit(group.Single());
                }
                // This condition checks if the group is being created for the first time, this gets invoked on saving the new group
                else
                {
                    StudentGroup g = new StudentGroup();

                    saveOrEdit(g);
                    g.ShowGroup = false;
                    db.StudentGroups.InsertOnSubmit(g);

                }
                db.SubmitChanges();
                savedSuccesfully.Visible = true;
                //pnlcreateOrEdit.Visible = false;
                //lblAddOrRemove.Text = "Student group is added. Now you can add or remove students from this student group";

                //lblAddOrRemove.Visible = true;
                //pnlStudents.Visible = true;

            }
            string currentCssDisplayValue = pnlStudents.Attributes.CssStyle["display"];
            pnlStudents.Attributes.CssStyle.Remove("display");
            pnlStudents.Attributes.CssStyle.Add("display", currentCssDisplayValue);
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    private void saveOrEdit(StudentGroup sg)
    {
        try
        {
            sg.GroupName = groupNameTextBox.Text;
            sg.Description = descriptionTextBox.Text;

            DateTime startDate = DateTime.Parse(startDateTextBox.Text);
            DateTime endDate = DateTime.Parse(endDateTextBox.Text);

            if (startDate >= endDate)
            {
                throw new ApplicationException("Start date must come before end date.");
            }
            sg.StartDate = startDate;
            sg.EndDate = endDate;

            sg.ShowGroup = Convert.ToBoolean(Session["ShowGroup"]);

            if (facultyTextBox.Text.Trim() == "")
            {
                throw new ApplicationException("This group must have a faculty or staff member assigned to it.");
            }

            UserAccount facultyAccount = new UserAccount(facultyTextBox.Text);
            if (!facultyAccount.Exists)
            {
                throw new ApplicationException("The account specified as the faculty owner does not exist.");
            }
            if (facultyAccount.OU != Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers &&
                facultyAccount.OU != Cite.DomainAuthentication.OrganizationalUnit.StaffUsers)
            {
                throw new ApplicationException("The faculty owner must be a faculty or staff account.");
            }

            sg.FacultyOwner = facultyTextBox.Text;
        }
        catch (ApplicationException ex)
        {
            errorLabel.Text = ex.Message;
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cancelInfoButton_Click(object sender, EventArgs e)
    {
        //getGroupInfo();
        Response.Redirect("StudentGroups.aspx", true);
    }
    protected void submitStudentButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (newStudentTextBox.Text.Trim() != "")
            {
                String stu = newStudentTextBox.Text.ToLower();
                UserAccount a = new UserAccount(stu);
                if (a.Exists)
                {
                    if (a.OU == Cite.DomainAuthentication.OrganizationalUnit.StudentUsers || a.OU==Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers||a.OU==Cite.DomainAuthentication.OrganizationalUnit.StaffUsers)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        DBDataContext db = DBDataContext.CreateInstance();
                        var stuGroup = (from i in db.StudentGroups
                                        where i.GroupID == id
                                        select i);
                        bool exists = false;
                        if (stuGroup.Count() > 0)
                        {
                            exists = (from i in stuGroup.Single().AuthorizedStudents
                                      where i.Username == stu
                                      select i).Count() == 1;
                            if (!exists)
                            {
                                AuthorizedStudent student = new AuthorizedStudent();
                                student.Username = stu;
                                student.StudentGroup = stuGroup.Single();
                                db.AuthorizedStudents.InsertOnSubmit(student);
                                db.SubmitChanges();
                                fillStudentListBox();
                            }
                        }
                        else
                        {
                            var maxGroupid = (from s in db.StudentGroups select s.GroupID).Max();
                            exists = (from i in
                                          (from i in db.StudentGroups where i.GroupID == maxGroupid select i).Single().AuthorizedStudents
                                      where i.Username == stu
                                      select i).Count() == 1;

                            if (!exists)
                            {
                                AuthorizedStudent student = new AuthorizedStudent();
                                student.Username = stu;
                                student.StudentGroup = (from i in db.StudentGroups where i.GroupID == maxGroupid select i).Single();
                                db.AuthorizedStudents.InsertOnSubmit(student);
                                db.SubmitChanges();
                                fillStudentListBox();
                            }
                        }
                    }
                    else
                    {
                        throw new ApplicationException("The account is not a student (or)faculty(or)staff  account.");
                    }
                }
                else
                {
                    throw new ApplicationException("The provided username does not exist in the NWMSU domain.");
                }
            }
            newStudentTextBox.Text = "";
            pnlStudents.Attributes.CssStyle.Remove("display");
            pnlStudents.Attributes.CssStyle.Add("display", "block");
        }

        catch (ApplicationException ex)
        {
            studentErrorLabel.Text = ex.Message;
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void deleteStudentButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (studentsListBox.SelectedIndex >= 0)
            {
                String username = studentsListBox.SelectedValue;
                DBDataContext db = DBDataContext.CreateInstance();
                int id = int.Parse(Request.QueryString["id"]);
                var stuGroup = (from i in db.StudentGroups
                                where i.GroupID == id
                                select i).Single();

                var stu = (from i in stuGroup.AuthorizedStudents
                           where i.Username == username
                           select i).Single();
                db.AuthorizedStudents.DeleteOnSubmit(stu);
                db.SubmitChanges();
                fillStudentListBox();
            }
            pnlStudents.Attributes.CssStyle.Remove("display");
            pnlStudents.Attributes.CssStyle.Add("display", "block");
        }
        catch (Exception ex)
        {
            studentErrorLabel.Text = ex.Message;
            throw ex;
        }
    }
    protected void uploadButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (studentFileUpload.HasFile)
            {
                // Create a stream around the file. Use a MemoryStream so we don't have to save the
                // file to the hard disk.
                StreamReader reader = new StreamReader(new MemoryStream(studentFileUpload.FileBytes));

                // Get a database connection.
                DBDataContext db = DBDataContext.CreateInstance();

                // Get the selected StudentGroup.
                int id = int.Parse(Request.QueryString["id"]);
                var stuGroup = (from i in db.StudentGroups
                                where i.GroupID == id
                                select i).Single();

                String[] lines = reader.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                //----- Commented out on 4/9/2015 by Lawrence Foley as this is no longer used ----//

                //if (lines[0] == "T")
                //{
                //    // This is an enrollment API file.
                //    foreach (String s in lines.Skip(1))
                //    {
                //        String[] line = s.Split('\t');
                //        // We want username.
                //        String username = line[3].ToLower();

                //        // Make sure this student isn't already a member of the student group.
                //        if ((from i in stuGroup.AuthorizedStudents
                //             where i.Username.ToLower() == username
                //             select i).Count() == 0)
                //        {
                //            // Create an AuthorizedStudent, add it to the group and give it the username,
                //            // and insert it into the database.
                //            AuthorizedStudent student = new AuthorizedStudent();
                //            student.GroupID = id;
                //            student.Username = username;
                //            db.AuthorizedStudents.InsertOnSubmit(student);
                //        }
                //    }
                //}
                //else
                //{

                // Is the input file a csv
                if (Path.GetExtension(studentFileUpload.FileName) == ".csv" || lines[0].Contains(','))
                {
                    foreach (String line in lines)
                    {
                        String[] parts = line.ToLower().Split(',');
                        foreach(String part in parts)
                        {
                            // do csv stuff here
                        }
                    }
                }
                // The input file is assumed to be a list of s numbers or emails
                else
                {
                    foreach (String line in lines)
                    {
                        if (line.Trim() != "") // Ignore empty lines.
                        {
                            // Convert to lower case and remove email ending
                            String stu = line.ToLower();
                            stu = stu.Replace("@mail.nwmissouri.edu", "");
                            Debug.WriteLine(stu);

                            // Make sure this student isn't already a member of the student group.
                            if ((from i in stuGroup.AuthorizedStudents
                                 where i.Username.ToLower() == stu
                                 select i).Count() == 0)
                            {
                                // Create an AuthorizedStudent, add it to the group and give it the username,
                                // and insert it into the database.
                                AuthorizedStudent student = new AuthorizedStudent();
                                student.GroupID = id;
                                student.Username = stu;
                                db.AuthorizedStudents.InsertOnSubmit(student);
                            }
                        }
                    }
                }
                db.SubmitChanges();
                fillStudentListBox();

            }
            pnlStudents.Attributes.CssStyle.Remove("display");
            pnlStudents.Attributes.CssStyle.Add("display", "block");
        }
        catch (Exception ex)
        {
            studentErrorLabel.Text = ex.Message;
            throw ex;
        }
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (Btn_GroupVisibility.Text == "Share")
    //    {
    //        Btn_GroupVisibility.Text = "Stop Sharing";
    //        Session["ShowGroup"] = true;
    //    }

    //    else if (Btn_GroupVisibility.Text == "Stop Sharing")
    //    {
    //        Btn_GroupVisibility.Text = "Share";
    //        Session["ShowGroup"] = false;
    //    }
    //}


    protected void chk_GroupVisibility_CheckedChanged(object sender, EventArgs e)
    {
        Session["ShowGroup"] = chk_GroupVisibility.SelectedValue == "allMembers" ?  true : false;
    }
}