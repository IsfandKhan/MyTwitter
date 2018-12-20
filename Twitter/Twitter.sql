create database twitter
Use twitter

Create Table USERS
(
	UserID [int] NOT NULL Primary Key Identity(1,1),
	Image [image] NULL,
	Name [varchar](50) NOT NULL,
	Password [varchar](50) NOT NULL,
	Phone [varchar](50) NULL,
	DOB [Date] NULL,
	Email [varchar](50) NOT NULL,
	Gender [varchar](50) NULL,
	About [text] NULL,
	Status [varchar](50) NULL  
)

Create Table FOLLOWERS
(
	FollowID [int] NOT NULL Primary Key Identity(1,1),
	UserID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	FollowingID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action
)

Create Table TWEETS
(	
	TweetID [int] NOT NULL Primary Key Identity(1,1),
	UserID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	TrendName [varchar](50) NULL,
	TweetContent [varchar](Max) NOT NULL  
)

Create Table TRENDS
(
	TrendID [int] NOT NULL Primary Key Identity(1,1),
	TrendName [varchar](50) NOT NULL
)

Create Table LIKES
(
	LikeID [int] NOT NULL Primary Key Identity(1,1),
	LikerID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	TweetID [int] NOT NULL Foreign Key References TWEETS(TweetID) On Delete No Action On Update No Action 
)

Create Table REPLIES
(
	ReplyID [int] NOT NULL Primary Key Identity(1,1),
	ReplierID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	TweetID [int] NOT NULL Foreign Key References TWEETS(TweetID) On Delete No Action On Update No Action,
	ReplyContent [varchar](Max) NOT NULL
)  

Create Table RETWEETS
(
	ReTweetID [int] NOT NULL Primary Key Identity(1,1),
	ReTweeterID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	TweetID [int] NOT NULL Foreign Key References TWEETS(TweetID) On Delete No Action On Update No Action
)

Create Table MESSAGES 
(
	MessageID [int] NOT NULL Primary Key Identity(1,1),
	SenderID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	ReceiverID [int] NOT NULL Foreign Key References USERS(UserID) On Delete No Action On Update No Action,
	MessageContent [varchar](Max) NOT NULL
)



-- Procedure for SIGN UP
GO
Create Procedure SigningUp
@Name varchar(50), @Email varchar(50), @Pass varchar(50),  @Status varchar(1) Output
As
Begin
If Exists(Select * From USERS As A Where A.Email=@Email)
	Begin
		Set @Status='0'  -- User is already present
	End	
Else 
	Begin
		Insert Into USERS (Name, Password, Email, Status) Values (@Name, @Pass, @Email, 'Active')
		Set @Status='1'  -- User has been entered
	End
End

--Execution
--Declare @TestStatus varchar(1)
--Execute SigningUp 'Hamza', 'hamza@gmail.com', '123', @Status=@TestStatus Output
--Select @TestStatus As Status

--Procedure for LOGIN
GO
Create Procedure LoggingIn
@Email varchar(50), @Pass varchar(50), @Status varchar(1) Output
As
Begin  
If Exists (Select * From Users Where Email=@Email and Password=@Pass)
	Begin
		Set @Status='1' --User Exists
		If (Select Status From USERS Where Email=@Email) is NULL
		Begin
			Update USERS Set Status='Active' Where Email=@Email
		End
	End
Else
	Begin 
	If Exists ( Select * From Users Where Email=@Email and Password!=@Pass)
		Begin
			Set @Status='2' -- Password Incorrect
		End
	Else If Not Exists( Select * From Users Where Email=@Email )
		Begin
			Set @Status='3' -- Login Does Not Exists
		End
	End
End

-- Execution
--Declare @TestStatus varchar(1)
--Execute LoggingIn 'hamza@gmail.com', '123', @Status=@TestStatus Output

--Select @TestStatus As Status
--Select * From USERS


-- Procedure For TWEET STORING
GO
Create Procedure TweetStoring
@Email varchar(50), @Trendname varchar(50), @Content varchar(120)
AS
Begin
	Declare @Userid int
	Select @Userid=UserID
	From USERS
	Where Email=@Email
	if(@Content!='')
	Begin
		if(@Trendname='-1')
		begin
			Set @Trendname=NULL
		end
		Insert Into TWEETS (UserID, TrendName, TweetContent) Values (@Userid, @Trendname,@Content)

		If Not Exists (Select * From TRENDS Where TrendName=@Trendname) 
			Begin
				IF (@Trendname is not NULL)
				begin 
					Insert Into TRENDS (TrendName) Values(@Trendname)
				end
			End
	End
End

--drop procedure TweetStoring
-- Execution
--Execute TweetStoring 'waleed@gmail.com', '#GoodLife', 'Just Prayed. #GoodLife'
--Select * From USERS
--Select * From Tweets


-- Procedure for MESSAGE SAVING
GO
Create Procedure MessageControl
@SenderID int, @ReceiverID int, @Content varchar(Max)
As
Begin
	Insert Into MESSAGES(SenderID, ReceiverID, MessageContent) Values(@SenderID, @ReceiverID, @Content)
End

-- Execution
--Execute MessageControl 1,2, 'Hello, how are you?'


-- Procedure For LIKE OR UNLIKE
GO
Create Procedure LikeUnlike
@LikerID int, @TweetID int, @Status varchar(1) Output
As
Begin
If Exists (Select * From LIKES Where LikerID=@LikerID AND TweetID=@TweetID)
	Begin
		Set @Status='0' -- Unliked Now
		Delete From LIKES Where LikerID=@LikerID AND TweetID=@TweetID
	End
Else
	Begin
		Set @Status='1' --Liked Now
		Insert Into LIKES (LikerID,TweetID) Values(@LikerID, @TweetID)
	End
End


-- Execution
--Declare @TestStatus varchar(1)
--Execute LikeUnlike 2,1, @Status=@TestStatus Output
--Select @TestStatus AS Status

go
GO
Create Procedure getTotallikes
 @Tweet varchar(max), @total int Output

As
begin
declare @id int
select @id=TWEETS.TweetID
from TWEETS
where TWEETS.TweetContent=@Tweet

declare @total1 int
select @total=t
from
(
select LIKES.TweetID, count(LIKES.LikeID) as t
from LIKES
group by LIKES.TweetID
) as x
where x.TweetID=@id

set @total =@total1
end
-- Procedure For FOLLOW UNFOLLOW
go
create proc geteachlike
(
@userid int,
@tweetid varchar(max)

)
as
begin

If Exists (Select * From LIKES Where LikerID=@userid AND TweetID=@tweetid)
begin
return 1
end
else
begin
return 0
end
end

--procedure for following unfollowing
GO
Create Procedure FollowUnFollow
@FollowEmail varchar(50), @FollowingEmail varchar(50)
As
Begin
declare @UserID int,@FollowingID int
Set @UserID=(Select UserID From Users Where Email=@FollowEmail)
Set @FollowingID=(Select UserID From Users Where Email=@FollowingEmail)
If Exists (Select * From FOLLOWERS Where UserID=@UserID AND FollowingID=@FollowingID)
	Begin
		Delete From FOLLOWERS Where UserID=@UserID AND FollowingID=@FollowingID
	End
Else
	Begin
		Insert Into FOLLOWERS(UserID,FollowingID) Values(@UserID, @FollowingID)
	End
End

--Exec FollowUnFollow


Select * from FOLLOWERS

--Drop Procedure FollowUnfollow
-- Execution
--Declare @TestStatus varchar(1)
--Execute FollowUnfollow 2,1, @Status=@TestStatus Output
--Select @TestStatus AS Status

-- Procedure for Getting Name
Go
Create Procedure GetName
@Email varchar(50)
As
Begin
Select Name
From USERS
Where Email=@Email
End


GO
Create View AllTrends 
As 
Select *
From Trends

--Procedure for Deactivting
GO
CREATE PROCEDURE Deactivate
@Email varchar(50)
AS
BEGIN
	UPDATE USERS 
	SET Status=NULL
	WHERE Email=@Email
END

-- Execution
--EXEC Deactivate
--@Email='waleed@gmail.com'


--Procedure for Update of Remaining User Info
GO
Create Procedure AddUserInfo
@Email varchar(50),
@Gender varchar(50),
@Dob varchar(50),
@Ph varchar(50),
@Notes varchar(50)
As
Begin
	If(@Gender='-1')
	Begin
		Set @Gender=NULL
	End
	If(@Dob='-1')
	Begin
		Set @Dob=NULL
	End
	If(@Ph='-1')
	Begin
		Set @Ph=NULL
	End
	If(@Notes='-1')
	Begin
		Set @Notes=NULL
	End
	Update USERS Set Gender=@Gender, DOB=@Dob, Phone=@Ph,  About=@Notes Where Email=@Email 
End

--Drop Procedure AddUserInfo
--Execution
--Execute AddUserInfo 'hamza@gmail.com','-1', '-1','03360101543','-1'
--Select* From USERS


--Procedure for Update of Remaining User Info
GO
Create Procedure AboutInfo
@Email varchar(50),
@Password varchar(50),
@Dob varchar(50),
@Ph varchar(50),
@Notes varchar(50)
As
Begin
	If(@Password='-1')
	Begin
		Select @Password=Password From USERS Where Email=@Email
	End
	If(@Dob='-1')
	Begin
		Select @Dob=DOB From USERS Where Email=@Email
	End
	If(@Ph='-1')
	Begin
		Select @Ph=Phone From USERS Where Email=@Email
	End
	If(@Notes='-1')
	Begin
		Select @Notes=About From USERS Where Email=@Email
	End
	Update USERS Set Password=@Password, DOB=@Dob, Phone=@Ph,  About=@Notes Where Email=@Email 
End

--Drop Procedure AboutInfo
--Execution

--Select* From USERS


--GettingTotalTweets
GO
Create Procedure TotalTweets
@email varchar(50)
as
Begin
	select u.Email,count(t.TweetID) as Total
	from  USERS as u join TWEETS as t on u.UserID=t.UserID
	where u.Email=@email
	group by u.Email
end



--GetRelatedTweets
Go
Create Procedure GetTweetsHome
@Email varchar(50)
AS
Begin
	declare @UserID int

	select @UserID=UserID
	from USERS
	where Email=@Email
	
	Select U.Name,U.Email,TweetContent
	From USERS as U
	Join TWEETS as T ON U.UserID=T.UserID
	WHERE U.UserID=@UserID OR 
	U.UserID IN (
		Select F.UserID
		From (
			Select C.UserID,FollowingID 
			from FOLLOWERS as G Join Users as C On G.UserID=C.UserID 
			WHERE Status is not NULL
			) as F 
		Where FollowingID=@UserID
	)  
End

--Drop Procedure GetTweetsHome
--EXEC GetTweetsHome 'hamza@gmail.com'

--Select * from FOLLOWERS
--Select * from USERS

--GetTweetsProfile
Go
Create Procedure GetTweetsProfile
@Email varchar(50)
AS
Begin
declare @UserID int

select @UserID=UserID
from USERS
where Email=@Email

Select u.Name,u.Email,t.TweetContent
from USERS as u join TWEETS as T on u.UserID=t.UserID
where u.UserID=@UserID 
End
--exec GetTweetsProfile 'asjad@gmail.com'

--GetTweetsTrend
Go
Create Procedure GetTweetsTrend
@Trendname varchar(50)
AS
Begin
Select u.Name,u.Email,t.TweetContent
From TWEETS As T join TRENDS AS Tr On T.TrendName=Tr.TrendName join USERS as u on T.UserID=u.UserID
Where T.TrendName=@Trendname And Status is not null
End

--Exec GetTweetsTrend '#PartyScene' 

--drop procedure GetTweetsTrend
--Select * From Users


-- Procedure for Getting Credentials
Go
Create Procedure GetCredentials
@Email varchar(50)
As
Begin
	Select Gender,Phone,DOB,About
	From USERS
	Where Email=@Email
End

--Drop Procedure GetCredentials
--Procedure for GetSearch
GO
Create Procedure GetSearch
@Search varchar(50)
AS
Begin
	Select Name,Email
	From Users
	Where Name like @Search+'%' AND Status is not NULL AND @Search!=''
End

--Drop Procedure GetSearch


--Exec GetSearch @Search='Ferhan'
--Select * From USERS

--Procedure for Iffollowing
GO
Create Procedure Iffollowing
@followemail varchar(50),@followingemail varchar(50), @status varchar(1) output
as
Begin
	If Exists(Select * From Followers 
				Where UserID IN (Select UserID From Users Where Email=@followemail) AND FollowingID IN (Select UserID From Users Where Email=@followingemail))
		Begin
			Set @Status='1'  -- Relation is already present
		End	
	Else 
		Begin
			Set @Status='0'  -- Relation is not present
		End
End

--Declare @TestStatus varchar(1)
--Exec Iffollowing
--@followemail='ferhan@gmail.com',@followingemail='hamza@gmail.com',
--@status=@TestStatus output
--Select @TestStatus
--Drop Procedure Iffollowing


--Procedure for Who To Follow
Go
Create Procedure WhotoFollow
@Email varchar(50)
AS
Declare @UserID int
Set @UserID=(Select UserID From Users Where Email=@Email)

Select Top 5 U.Name,U.Email
From USERS as U
Join	(Select P.UserID
			FROM (Select G.UserID
			From FOLLOWERS as G
			Where G.FollowingID=@UserID) as Q Join FOLLOWERS as P ON Q.UserID=P.FollowingID
			) as F 
ON F.UserID=U.UserID
Where U.UserID!=@UserID AND U.Status is not NULL AND F.UserID NOT IN (Select UserID From FOLLOWERS Where FollowingID=@UserID)
Group By U.Name,U.Email

--End of Procedure Who to Follow

--Get Followers
Go
Create Procedure GetFollowers
@Email varchar(50)
AS
Declare @UserID int
Set @UserID=(Select UserID From USERS Where Email=@Email)

Select U.Email,Name
From Users as U
Join FOLLOWERS as F ON U.UserID=FollowingID
Where F.UserID=@UserID AND U.Status is not Null
Group By U.Email,Name
--Drop Procedure GetFollowers

--Get Followers Ends

--Get Following
Go
Create Procedure GetFollowing
@Email varchar(50)
AS
Declare @UserID int
Set @UserID=(Select UserID From USERS Where Email=@Email)

Select U.Email,Name
From Users as U
Join FOLLOWERS as F ON U.UserID=F.UserID
Where F.FollowingID=@UserID AND U.Status is not Null
Group By U.Email,Name
--Drop Procedure GetFollowers

--Get Following Ends

--Send Message
Go
Create Procedure SendMessage
@SenderEmail varchar(50),@ReceiverEmail varchar(50),@MessageContent varchar(max)
AS
Declare @ReceiverID int,@SenderID int
Set @SenderID=(Select UserID From USERS Where Email=@SenderEmail)
Set @ReceiverID=(Select UserID From USERS Where Email=@ReceiverEmail)

Insert Into MESSAGES(SenderID,ReceiverID,MessageContent) values (@SenderID,@ReceiverID,@MessageContent)
 

--End of SendMessages

--Procedure for MessagesFrom
Go
Create Procedure GetMessagesFrom
@Email varchar(50)
AS
Begin
Declare @UserID int
Set @UserID=(Select UserID From USERS Where Email=@Email)

Select u.Name,u.Email
from MESSAGES as m join USERS as u on m.SenderId=u.userid
where m.ReceiverID=@UserID
union
Select u.Name,u.Email
from MESSAGES as m join USERS as u on m.ReceiverID=u.userid
where m.SenderID=@UserID
end

	
--DROP Procedure GetMessagesFrom
--Select * from MESSAGES
--Exec GetMessagesFrom @Email='hamza@gmail.com'

--End of MessagesFrom

--Procedure to GetMessages
Go
Create Procedure GetMessages
@senderemail varchar(50),@receiveremail varchar(50)
AS
Begin

declare @senderId int
declare @receiverId int

set @senderId=(select UserId
from USERS
where Email=@senderemail)

set @receiverId=(select UserId
from USERS
where Email=@receiveremail)

select u.Name,u.Email,MessageContent
from MESSAGES as m join USERS as u on m.SenderID=u.UserID
where (SenderID=@senderId and ReceiverID=@receiverId) or (SenderID=@receiverId and ReceiverID=@senderId)


End



select * from USERS
insert into USERS (UserID,Name,Password,Phone,DOB,Email,About,s)