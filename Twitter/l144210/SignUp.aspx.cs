using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using l144210.DAL;

namespace l144210
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["DataEmail"] == null)
                Response.Redirect("Welcome.aspx");
            String Email = Session["DataEmail"].ToString();
            myDAL objMyDal = new myDAL();
            DataSet Details = objMyDal.HomeLoader(Email);
            int MaxRows = Details.Tables[0].Rows.Count;
            DataRow drow = Details.Tables[0].Rows[0];
            this.AboutName.Text = drow.ItemArray.GetValue(0).ToString();
            this.AboutShowName.Text = drow.ItemArray.GetValue(0).ToString();
            //this.FullName4.Text = drow.ItemArray.GetValue(0).ToString();
            int index = Email.IndexOf("@");
            string piece = Email.Substring(0, index);
            this.AboutUser.Text = "@" + piece;
            DataSet WhoFollow = objMyDal.FriendFollow(Email);
            MaxRows = WhoFollow.Tables[0].Rows.Count;
            string[] Friends = new string[MaxRows];
            int ind1;
            for (int i = 0; i < MaxRows; i++)
            {
                DataRow drow9 = WhoFollow.Tables[0].Rows[i];
                Friends[i] = drow9.ItemArray.GetValue(0).ToString() + "?" + drow9.ItemArray.GetValue(1).ToString();
                ind1 = Friends[i].IndexOf("?");
                if (ind1 < 10)
                {
                    Friends[i] = Friends[i] + "0";
                }
                Friends[i] = Friends[i] + ind1.ToString();
                //Tweeting[i] = drow2.ItemArray.GetValue(1).ToString();
                // Tweeting[i] = drow2.ItemArray.GetValue(2).ToString();
            }
            this.Repeater3.DataSource = Friends;
            this.Repeater3.DataBind();
            DataSet Followers = objMyDal.GetFollowers(Email);
            this.Receiver.DataSource = Followers.Tables[0];
            this.Receiver.DataTextField = "Name";
            this.Receiver.DataValueField = "Email";
            this.Receiver.DataBind();
        }

        //Send Message
        protected void SendMessage(object sender, EventArgs e)
        {
            String ReceiverEmail = Receiver.SelectedValue;
            String SenderEmail = Session["DataEmail"].ToString();
            String messageContent = MessageContent.Text;
            myDAL objMyDal = new myDAL();
            objMyDal.SendMessage(ReceiverEmail, SenderEmail, messageContent);
            Response.Redirect("Conversations.aspx");
        }
        protected void ProceedSignUp(object sender, EventArgs e)
        {
            String Gender = Hijra.Text;
            String DOB = Dob.Text;
            String Ph = Phone.Text;
            String Notes = Desc.Text;
            if (DOB.Length==0)
            {
                DOB = "-1";
            }
            if (Gender.Length == 0)
            {
                Gender = "-1";
            }
            if (Ph.Length == 0)
            {
                Ph = "-1";
            }
            if (Notes.Length == 0)
            {
                Notes = "-1";
            }
            DataTable DT = new DataTable();
            myDAL objMyDal = new myDAL();
            String Email = Session["DataEmail"].ToString();

            objMyDal.RemainingInfo(Email, Gender, DOB, Ph, Notes, ref DT);
            Response.Redirect("Home.aspx");
        }
        protected void SearchPressed(object sender, EventArgs e)
        {
            String Search = SearchBox.Text;
            Response.Redirect("Search.aspx?Search=" + Search);
        }
    }
}