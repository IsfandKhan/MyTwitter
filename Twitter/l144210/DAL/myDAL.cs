using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
//using System.Web.Configuration;

namespace l144210.DAL
{
    public class myDAL
    {
        private static readonly string connString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlCon"].ConnectionString;
        

        public int SignUP(String Name, String Email, String Pass, ref DataTable DT)
        {

            int Found = 0;
            //DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("SigningUp", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@Name"].Value = Name;
                cmd.Parameters["@Email"].Value = Email;
                cmd.Parameters["@Pass"].Value = Pass;

                cmd.ExecuteNonQuery();

                // read output value 
                Found = Convert.ToInt32(cmd.Parameters["@Status"].Value); //convert to output parameter to interger format

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;

        }
        //Signup End

        public int LogIN(String Email, String Pass, ref DataTable DT)
        {

            int Found = 0;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("LoggingIn", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters["@Email"].Value = Email;
                cmd.Parameters["@Pass"].Value = Pass;

                cmd.ExecuteNonQuery();

                // read output value 
                Found = Convert.ToInt32(cmd.Parameters["@Status"].Value); //convert to output parameter to interger format

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }
        //end Login


        public void Deactivating(String Email, ref DataTable DT)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Deactivate", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email;
                
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }
        //end Deactivating
        //HomeLoader
        public DataSet HomeLoader(String Email1) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetName", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50);
                cmd.Parameters["@email"].Value = Email1;


                cmd.ExecuteNonQuery();
                
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        //HomeLoader
        public DataSet GetCredentials(String Email1) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetCredentials", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50);
                cmd.Parameters["@email"].Value = Email1;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public DataSet TrendGetter() //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Select Top 8 count(TweetID),TrendName from Tweets where TrendName is not NULL group by TrendName order by count(TweetID) desc", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.Text; //set type of sqL Command

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return ds; //return the dataset
        }
        //trendgetterEnds

        public DataSet TweetStore(String Email, String TweetContent, String Trend) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            if (Trend==null)
            {
                Trend ="-1";
            }
            try
            {
                cmd = new SqlCommand("TweetStoring", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Trendname", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Content", SqlDbType.VarChar,120);

                
                cmd.Parameters["@Email"].Value = Email;
                cmd.Parameters["@Trendname"].Value = Trend;
                cmd.Parameters["@Content"].Value = TweetContent;

                cmd.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }
        
        //TweetGetter
        public DataSet TweetGetter(String Email) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetTweetsProfile", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command

                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email;
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return ds; //return the dataset
        }
        //end of tweet getter

        //TweetGetter
        public DataSet HomeTweetGetter(String Email) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetTweetsHome", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command

                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email;
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return ds; //return the dataset
        }
        //end of tweet getter


        //TrendTweetGetter
        public DataSet TrendTweetGetter(String TrendName) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetTweetsTrend", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command

                cmd.Parameters.Add("@TrendName", SqlDbType.VarChar, 50);
                cmd.Parameters["@TrendName"].Value = TrendName;
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return ds; //return the dataset
        }
        //end of trend tweet getter


        public void RemainingInfo(String Email, String Gender, String DOB, String Ph, String Notes ,ref DataTable DT)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("AddUserInfo", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Dob", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Ph", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);

                // set parameter values
                cmd.Parameters["@Email"].Value = Email;
                cmd.Parameters["@Gender"].Value = Gender;
                cmd.Parameters["@Dob"].Value = DOB;
                cmd.Parameters["@Ph"].Value = Ph;
                cmd.Parameters["@Notes"].Value = Notes;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }
        //end of RemainingInfo

        public void AboutInfo(String Email, String Password, String DOB, String Ph, String Notes, ref DataTable DT)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("AboutInfo", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Dob", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Ph", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);

                // set parameter values
                cmd.Parameters["@Email"].Value = Email;
                cmd.Parameters["@Password"].Value = Password;
                cmd.Parameters["@Dob"].Value = DOB;
                cmd.Parameters["@Ph"].Value = Ph;
                cmd.Parameters["@Notes"].Value = Notes;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }// End of AboutInfo

        //TotalTweetsGetter
        public DataSet TotalTweetsGetter(String Email1) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("TotalTweets", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50);
                cmd.Parameters["@email"].Value = Email1;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public DataSet SearchPeople(String Search)
        {
            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetSearch", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@search", SqlDbType.VarChar, 50);
                cmd.Parameters["@search"].Value = Search;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public int CheckFollow(String FollowID, String FollowingID,ref DataTable DT)
        {

            int Found = 0;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Iffollowing", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@followemail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@followingemail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@status", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@followemail"].Value = FollowID;
                cmd.Parameters["@followingemail"].Value = FollowingID;
                
                cmd.ExecuteNonQuery();

                // read output value 
                Found = Convert.ToInt32(cmd.Parameters["@status"].Value); //convert to output parameter to interger format

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }

        public void FollowUnfollow(String FollowID, String FollowingID)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("FollowUnfollow", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FollowEmail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@FollowingEmail", SqlDbType.VarChar, 50);
                
                // set parameter values
                cmd.Parameters["@FollowEmail"].Value = FollowID;
                cmd.Parameters["@FollowingEmail"].Value = FollowingID;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }

        public DataSet FriendFollow(String Email)
        {
            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("WhotoFollow", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public DataSet GetFollowers(String Email1) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetFollowers", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email1;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public DataSet GetFollowing(String Email1) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetFollowing", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email1;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }


        public void SendMessage(String ReceiverEmail, String SenderEmail, String MessageContent)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("SendMessage", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReceiverEmail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@SenderEmail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@MessageContent", SqlDbType.VarChar);

                // set parameter values
                cmd.Parameters["@ReceiverEmail"].Value = ReceiverEmail;
                cmd.Parameters["@SenderEmail"].Value = SenderEmail;
                cmd.Parameters["@MessageContent"].Value = MessageContent;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }

        public DataSet FriendMessages(String Email1) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetMessagesFrom", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters["@Email"].Value = Email1;


                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }
        public DataSet ViewConvo(String ReceiverEmail,String SenderEmail) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetMessages", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReceiverEmail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@SenderEmail", SqlDbType.VarChar, 50);
                
                // set parameter values
                cmd.Parameters["@ReceiverEmail"].Value = ReceiverEmail;
                cmd.Parameters["@SenderEmail"].Value = SenderEmail;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }
        
    }
    /* public DataSet getLikes(string tweet , string name ) //to get the values of all the items from table Items and return the Dataset
        {

            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("GetMessages", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReceiverEmail", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@SenderEmail", SqlDbType.VarChar, 50);
                
                // set parameter values
                cmd.Parameters["@ReceiverEmail"].Value = ReceiverEmail;
                cmd.Parameters["@SenderEmail"].Value = SenderEmail;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }
        
    }*/
}