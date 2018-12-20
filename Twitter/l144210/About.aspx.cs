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
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getting name and email
            String Email = Session["DataEmail"].ToString();
            myDAL objMyDal = new myDAL();
            DataSet Details = objMyDal.HomeLoader(Email);
            int MaxRows = Details.Tables[0].Rows.Count;
            DataRow drow = Details.Tables[0].Rows[0];
            DataSet Details6 = objMyDal.GetCredentials(Email);
            DataRow drow5 = Details6.Tables[0].Rows[0];
            this.ShowGender.Text = drow5.ItemArray.GetValue(0).ToString();
            String Date = drow5.ItemArray.GetValue(2).ToString();
            int ind3 = Date.IndexOf(" ");
            if(ind3 > 0)
            {
                this.ShowDob.Text = Date.Substring(0, ind3);
            }
            this.ShowPhone.Text = drow5.ItemArray.GetValue(1).ToString();
            this.ShowDesc.Text = drow5.ItemArray.GetValue(3).ToString();
            this.FullName2.Text = drow.ItemArray.GetValue(0).ToString();
            this.SettingName.Text = drow.ItemArray.GetValue(0).ToString();
            int index = Email.IndexOf("@");
            string piece = Email.Substring(0, index);
            this.UserName2.Text = "@" + piece;
            //this.UserName4.Text = "@" + piece;
            //getting Trends 
            DataSet Trends = objMyDal.TrendGetter();
            MaxRows = Trends.Tables[0].Rows.Count;
            string[] Trending = new string[MaxRows];
            for (int i = 0; i < MaxRows; i++)
            {
                DataRow drow1 = Trends.Tables[0].Rows[i];
                Trending[i] = drow1.ItemArray.GetValue(1).ToString();
            }
            //this.Label1.Text = Trending[0];
            //this.Label2.Text = Trending[1];
            //this.Label3.Text = Trending[2];
            //this.Label4.Text = Trending[3];
            this.rptResults2.DataSource = Trending;
            this.rptResults2.DataBind();
            DataSet TotalTweets = objMyDal.TotalTweetsGetter(Email);
            MaxRows = TotalTweets.Tables[0].Rows.Count;
            if (MaxRows == 0)
            {
                this.TotalTweets2.Text = "0";
            }
            else
            {
                DataRow drow3 = TotalTweets.Tables[0].Rows[0];
                this.TotalTweets2.Text = drow3.ItemArray.GetValue(1).ToString();
            }
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
        protected void DeactivateAccount(object sender, EventArgs e)
        {
            String Email;
            DataTable DT = new DataTable();
            myDAL objMyDal = new myDAL();
            Email = Session["DataEmail"].ToString();
            
            objMyDal.Deactivating(Email, ref DT);

            Response.Redirect("Welcome.aspx");
        }

        protected void AboutInfoChange(object sender, EventArgs e)
        {
            String Email = Session["DataEmail"].ToString();
            String DOB = ChangeDob.Text;
            String Ph = ChangePhone.Text;
            String Notes = AboutYou.Text;
            String Password = ChangePassword.Text;
            if (DOB.Length == 0)
            {
                DOB = "-1";
            }
            if (Password.Length == 0)
            {
                Password = "-1";
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

            objMyDal.AboutInfo(Email, Password, DOB, Ph, Notes, ref DT);

            Response.Redirect("Profile.aspx");
        }

        protected void SearchPressed(object sender, EventArgs e)
        {
            String Search = SearchBox.Text;
            Response.Redirect("Search.aspx?Search=" + Search);
        }
    }
}