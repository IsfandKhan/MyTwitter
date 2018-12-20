using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using l144210.DAL;
//using System.Web.Configuration;

namespace l144210
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUP(object sender, EventArgs e)
        {
            String Name = name.Text;
            String Email = email.Text;
            String Pass = newpass.Text;
            DataTable DT = new DataTable();
            myDAL objMyDal = new myDAL();
            Session["DataEmail"] = Email;
            int found;

            found = objMyDal.SignUP(Name, Email, Pass, ref DT);

            if (found == 1)
            {
                Response.Redirect("SignUp.aspx");
                //message.Text = Convert.ToString("Item Not Found");
                // grdItem.DataSource = null;
                // grdItem.DataBind();
            }
            else
            {
                this.WrongSignup.Text = "Email Already Exists";
                //grdItem.DataSource = DT;
                //grdItem.DataBind();
                //message.Text = Convert.ToString("Ghalti Kar Rahy Ho!");

            }
        }
        protected void btnLogin(object sender, EventArgs e)
        {
            String Email = user.Text;
            String Pass = pass.Text;
            DataTable DT = new DataTable();
            myDAL objMyDal = new myDAL();
            Session["DataEmail"] = Email;
            int found;

            found = objMyDal.LogIN(Email, Pass, ref DT);

            if (found == 1)
            {
                Response.Redirect("Home.aspx");
                //message.Text = Convert.ToString("Item Not Found");
                // grdItem.DataSource = null;
                // grdItem.DataBind();
            }
            else
            {
                this.WrongLogin.Text = "Email or Password is Incorrect";
                //grdItem.DataSource = DT;
                //grdItem.DataBind();
                //message.Text = Convert.ToString("Ghalti Kar Rahy Ho!");
            }
        }
    }
}