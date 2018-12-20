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
    public partial class Home : System.Web.UI.Page
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
            DataSet Details= objMyDal.HomeLoader(Email);
            int MaxRows =Details.Tables[0].Rows.Count;
            DataRow drow = Details.Tables[0].Rows[0];
            this.FullName.Text = drow.ItemArray.GetValue(0).ToString();
            //this.FullName1.Text = drow.ItemArray.GetValue(0).ToString();
            int index = Email.IndexOf("@");
            string piece = Email.Substring(0, index);
            this.UserName.Text = "@"+piece;
            //this.UserName1.Text = "@"+piece;
            //this.UserName.Text = Email;
            //getting Trends 
            DataSet Trends = objMyDal.TrendGetter();
            MaxRows = Trends.Tables[0].Rows.Count;
            string[] Trending= new string[MaxRows];
            for(int i=0;i<MaxRows;i++)
            {
                DataRow drow1 = Trends.Tables[0].Rows[i];
                Trending[i] = drow1.ItemArray.GetValue(1).ToString();
            }
            
            this.rptResults1.DataSource = Trending;
            this.rptResults1.DataBind();

            //LoadingHomeTweets
            DataSet Tweets = objMyDal.HomeTweetGetter(Email);
            MaxRows = Tweets.Tables[0].Rows.Count;
            string[] Tweeting = new string[MaxRows];
            int ind;
            for (int i = 0; i < MaxRows; i++)
            {
                DataRow drow2 = Tweets.Tables[0].Rows[i];
                Tweeting[i] = drow2.ItemArray.GetValue(0).ToString() + "?" + drow2.ItemArray.GetValue(1).ToString() 
                                + "$"+drow2.ItemArray.GetValue(2).ToString();
                ind = Tweeting[i].IndexOf("?");
                if (ind < 10)
                {
                    Tweeting[i] = Tweeting[i] + "0";
                }
                Tweeting[i] = Tweeting[i] + ind.ToString();
                ind = Tweeting[i].IndexOf("$");
                if (ind < 10)
                {
                    Tweeting[i] = Tweeting[i] + "0";
                }
                Tweeting[i] = Tweeting[i] + ind.ToString();
                //Tweeting[i] = drow2.ItemArray.GetValue(1).ToString();
               // Tweeting[i] = drow2.ItemArray.GetValue(2).ToString();
            }
            string[] Tweeting1 = new string[MaxRows];
            int x = MaxRows - 1;
            for (int i = 0; i < MaxRows; i++)
            {
                Tweeting1[x] = Tweeting[i];
                x--;
            }
            this.Repeater1.DataSource = Tweeting1;
            this.Repeater1.DataBind();
            DataSet TotalTweets = objMyDal.TotalTweetsGetter(Email);
            MaxRows = TotalTweets.Tables[0].Rows.Count;
            if (MaxRows == 0)
            {
                this.TotalTweets.Text = "0";
            }
            else
            {
                DataRow drow3 = TotalTweets.Tables[0].Rows[0];
                this.TotalTweets.Text = drow3.ItemArray.GetValue(1).ToString();
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

            //getting Followers
            DataSet Followers = objMyDal.GetFollowers(Email);
            this.Receiver.DataSource = Followers.Tables[0];
            this.Receiver.DataTextField = "Name";
            this.Receiver.DataValueField = "Email";
            this.Receiver.DataBind();
        }
      //homepageLoader ends

        protected void TweetPressed(object sender, EventArgs e)
        {
            String Email = Session["DataEmail"].ToString();
            String TweetContent = Tweet.Text;
            String Trend=null ;
            if(TweetContent.Contains("#"))
            {
                int index = TweetContent.IndexOf("#");
                int x = 0;
                for (int i = index;i<TweetContent.Length ; i++)
                {
                    if (TweetContent[i] == ' ')
                    {   
                        x = i;
                        x--;
                        break;
                    }
                    x = i;
                }
                Trend = TweetContent.Substring(index,x-index+1);
                if (Trend.Length == 1)
                {
                    Trend = null;
                }
            }
            myDAL objMyDal = new myDAL();
            objMyDal.TweetStore(Email, TweetContent, Trend);
            Response.Redirect("Home.aspx");
        }

        //TweetPressedEnds

        //SearchPressed
        protected void SearchPressed(object sender, EventArgs e)
        {
            String Search = SearchBox.Text;
            Response.Redirect("Search.aspx?Search="+Search);
        }

        //SearchPressed Ends


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
    }
}