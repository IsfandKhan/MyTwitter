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
    public partial class Followship : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["DataEmail"] == null)
                Response.Redirect("Welcome.aspx");
            //getting name and email
            //String Email = Request.QueryString["Email"];
            //this.HiddenEmail.Value = Email;
            String Email = Session["DataEmail"].ToString();
            myDAL objMyDal = new myDAL();
            DataSet Details = objMyDal.HomeLoader(Email);
            int MaxRows = Details.Tables[0].Rows.Count;
            DataRow drow = Details.Tables[0].Rows[0];
            this.FullName2.Text = drow.ItemArray.GetValue(0).ToString();
            //this.Label2.Text = drow.ItemArray.GetValue(0).ToString();
            int index = Email.IndexOf("@");
            string piece = Email.Substring(0, index);
            this.UserName2.Text = "@" + piece;

            DataSet Details6 = objMyDal.GetCredentials(Email);
            DataRow drow5 = Details6.Tables[0].Rows[0];
            this.ShowGender.Text = drow5.ItemArray.GetValue(0).ToString();
            String Date = drow5.ItemArray.GetValue(2).ToString();
            int ind3 = Date.IndexOf(" ");
            if (ind3 > 0)
            {
                this.ShowDob.Text = Date.Substring(0, ind3);
            }
            this.ShowPhone.Text = drow5.ItemArray.GetValue(1).ToString();
            this.ShowDesc.Text = drow5.ItemArray.GetValue(3).ToString();
            
            //this.Label3.Text = "@" + piece;
            //this.UserName.Text = Email;
            //getting Trends 
            DataSet Trends = objMyDal.TrendGetter();
            MaxRows = Trends.Tables[0].Rows.Count;
            string[] Trending = new string[MaxRows];
            for (int i = 0; i < MaxRows; i++)
            {
                DataRow drow1 = Trends.Tables[0].Rows[i];
                Trending[i] = drow1.ItemArray.GetValue(1).ToString();
            }

            this.rptResults2.DataSource = Trending;
            this.rptResults2.DataBind();

            //LoadingHomeTweets
            //DataSet Tweets = objMyDal.TweetGetter(Email);
            //MaxRows = Tweets.Tables[0].Rows.Count;
            //string[] Tweeting = new string[MaxRows * 3];
            //int x = 0;
            //for (int i = 0; i < MaxRows; i++)
            //{
            //    DataRow drow2 = Tweets.Tables[0].Rows[i];
            //    Tweeting[x] = drow2.ItemArray.GetValue(0).ToString();
            //    x++;
            //    Tweeting[x] = drow2.ItemArray.GetValue(1).ToString();
            //    x++;
            //    Tweeting[x] = drow2.ItemArray.GetValue(2).ToString();
            //    x++;
            //}

            //this.ProfRepeater1.DataSource = Tweeting;
            //this.ProfRepeater1.DataBind();

            //LoadingTweetsOfProfile
            //LoadingHomeTweets
            //TotalNumberOfTweets
            DataSet TotalTweets = objMyDal.TotalTweetsGetter(Email);
            MaxRows = TotalTweets.Tables[0].Rows.Count;
            if (MaxRows == 0)
            {
                this.TotalTweets1.Text = "0";
            }
            else
            {
                DataRow drow3 = TotalTweets.Tables[0].Rows[0];
                this.TotalTweets1.Text = drow3.ItemArray.GetValue(1).ToString();
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
            
            String Check = Request.QueryString["Check"];
            if(Check == "Followers")
            {
                this.TypePressed.Text = "People Following You";
                DataSet Followers = objMyDal.GetFollowers(Email);
                this.Receiver.DataSource = Followers.Tables[0];
                this.Receiver.DataTextField = "Name";
                this.Receiver.DataValueField = "Email";
                this.Receiver.DataBind();
                MaxRows = Followers.Tables[0].Rows.Count;
                string[] Following = new string[MaxRows];
                int ind5;
                for (int i = 0; i < MaxRows; i++)
                {
                    DataRow dro = Followers.Tables[0].Rows[i];
                    Following[i] = dro.ItemArray.GetValue(1).ToString() + "?" + dro.ItemArray.GetValue(0).ToString();
                    ind5 = Following[i].IndexOf("?");
                    if (ind5 < 10)
                    {
                        Following[i] = Following[i] + "0";
                    }
                    Following[i] = Following[i] + ind5.ToString();
                    //Tweeting[i] = drow2.ItemArray.GetValue(1).ToString();
                    // Tweeting[i] = drow2.ItemArray.GetValue(2).ToString();
                }
                this.Followers.DataSource = Following;
                this.Followers.DataBind();
            }
            else if (Check == "Following")
            {
                this.TypePressed.Text = "You are Following";
                DataSet Followers = objMyDal.GetFollowing(Email);
                this.Receiver.DataSource = Followers.Tables[0];
                this.Receiver.DataTextField = "Name";
                this.Receiver.DataValueField = "Email";
                this.Receiver.DataBind();
                MaxRows = Followers.Tables[0].Rows.Count;
                string[] Following = new string[MaxRows];
                int ind5;
                for (int i = 0; i < MaxRows; i++)
                {
                    DataRow dro = Followers.Tables[0].Rows[i];
                    Following[i] = dro.ItemArray.GetValue(1).ToString() + "?" + dro.ItemArray.GetValue(0).ToString();
                    ind5 = Following[i].IndexOf("?");
                    if (ind5 < 10)
                    {
                        Following[i] = Following[i] + "0";
                    }
                    Following[i] = Following[i] + ind5.ToString();
                    //Tweeting[i] = drow2.ItemArray.GetValue(1).ToString();
                    // Tweeting[i] = drow2.ItemArray.GetValue(2).ToString();
                }
                this.Followers.DataSource = Following;
                this.Followers.DataBind();
            }
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
        protected void btnEditProfile(object sender, EventArgs e)
        {
            Response.Redirect("About.aspx");
        }
        //

        protected void SearchPressed(object sender, EventArgs e)
        {
            String Search = SearchBox.Text;
            Response.Redirect("Search.aspx?Search=" + Search);
        }

        protected void BtnSeeFollowers(object sender, EventArgs e)
        {
            Response.Redirect("Followship.aspx?Check=Followers");
        }
        protected void BtnSeeFollowing(object sender, EventArgs e)
        {
            Response.Redirect("Followship.aspx?Check=Following");
        }
        
    }
}