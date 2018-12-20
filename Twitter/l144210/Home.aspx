<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="l144210.Home" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server"> 
    <title>Twitter</title>
    <link rel="stylesheet" type="text/css" href="css\home.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <li class="active"><a href="Home.aspx" data-toggle="tab"><img src="img/logo/home.png" width="23" height="15"/> Home</a></li>
    <li><a onclick="document.getElementById('lightbox').style.display='inline';" data-toggle="tab"><img src="img/logo/messages.png" width="23" height="15"/> Messages</a></li>
    <li style="margin-left:400px;"><img src="img\logo\logo.png" width="24" height="24" style="margin-top:15px;"/></li>
    <li style="float:right;margin-right:50px;"><a href="Welcome.aspx" data-toggle="tab"><img src="img/logo/logout.png" width="23" height="15"/> Logout</a></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="span3">
            <div class="row">
                <div class="cols">
                    <div class="prof-pic-up">
                    </div>
                    <table>
                        <tr>
                            <td>
                                <a class="overlay"></a>
                            </td>
                            <td>
                                <table class="overlay-info">
                                    <tr>
                                        <td><a href="Profile.aspx"><span style="font-size:small;margin-bottom:0;">
                                            <asp:Label ID="FullName" runat="server" Text="UserName"></asp:Label></span></a>
                                        <br />
                                        <span style="color:#A0A0A0;font-size:smaller"><asp:Label ID="UserName" runat="server" Text="@username"></asp:Label></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="prof-pic-down">
                        <h3 class="font-color"><asp:Label ID="TotalTweets" runat="server"></asp:Label> Tweets</h3>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="cols threecols font-color">
                    <h4 >Top Trends</h4>
                    <asp:Repeater ID="rptResults1" runat="server">
                        <ItemTemplate> 
                            <tr>
                                <td><asp:HyperLink ID="TrendLink" runat="server" NavigateUrl='<%#String.Concat("~/Trends.aspx?TrendName=", Container.DataItem.ToString().Substring(1,Container.DataItem.ToString().Length-1))%>' Text="<%# Container.DataItem %>"></asp:HyperLink><br /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <form id="form5" class="tweet-form" runat="server">
            <div class="span8">
                <div class="cols threecols tweets">
                    <div class="tweet-box">
                        <img class="tweet-image" src="img/dp/purple.jpg"/>
                        <asp:TextBox ID="Tweet" runat="server" TextMode="MultiLine" placeholder="Hey BabyDoll!! Whats Goin On?" oninput="checkTweetLength();" CssClass="tweet"/>
                        <asp:ImageButton ID="Tweeting" class="tweet-image" src="img/logo/twitter-tweet-icon.png" runat="server" OnClick="TweetPressed"/>         
                    </div>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate> 
                            <tr>
                                <td>
                                    <div class="all-tweets">
                                        <img class="all-tweet-image" src="img/dp/purple.jpg"/>
                                        <div  class="abbu">
                                            <span><a runat="server" href='<%#String.Concat("~/OthersProfile.aspx?Email=",  Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))+1,int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))-1)  )%>' ><strong><asp:Label ID="Label1" runat="server" Text=" <%# Container.DataItem.ToString().Substring(0,int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))) %>"></asp:Label></strong> </a>
                                            <a runat="server" href='<%#String.Concat("~/OthersProfile.aspx?Email=",  Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))+1,int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))-1)  )%>' style="color:#808080"><small><asp:Label ID="Label2"  runat="server" Text="<%# Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))+1,int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-4,2))-1)  %>"></asp:Label></small></a></span>
                                            <br />
                                            <span><%# Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))+1,(Container.DataItem.ToString().Length-5)-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))) %> </span>
                                            <br /><br />
                                            <a href="#" class="reply-color"><img class="tweet-actions-images" src="img/logo/reply.png"/></a><span class="reply"></span>
                                            <a href="#"><img class="tweet-actions-images" src="img/logo/retweet.png"/></a><span class="reply"></span>
                                            <a href="#"><img src="img/logo/like.png" width="25" height="25" style="margin-top:3px;"/></a><span class="reply"></span>
                                            <br />
                                            <br />
                                        </div>
                                    </div>               
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div id="lightbox" class="lightbox" style="display:none">
                        <table class="lightbox_table">
                            <tr>
                                <td class="lightbox_table_cell" align="center">
                                    <div id="lightbox_content" class="lighbox_style"><br />
                                        <small><a onclick="document.getElementById('lightbox').style.display='none';">Close</a></small>
                                        <center><h3 class="font-color">Send a New Message</h3></center><br />
                                        <asp:DropDownList ID="Receiver" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Message To" Value="0"/>
                                        </asp:DropDownList><br />
                                        <asp:TextBox ID="MessageContent" TextMode="MultiLine" Rows="3" PlaceHolder="Come Home Johnny ..." runat="server"></asp:TextBox><br /><br />
                                        <asp:ImageButton runat="server" src="img/logo/messages.png" OnClick="SendMessage" width="80" height="80"/><br /><br />
                                        <asp:Button runat="server" PostBackUrl="Conversations.aspx" CssClass="login-button" Text="View Your Messages"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        
            <div class="span3">
                <div class="row">
                    <div style="margin-left:20px;margin-top:10px">
                        <asp:TextBox ID="SearchBox" runat="server" CssClass="textbox" PlaceHolder="Search"></asp:TextBox>
                        <asp:ImageButton runat="server" OnClick="SearchPressed" src="\img\logo\search.png" width="18" Height="18" style="margin-bottom:8px;padding:5px; background-color:#FFF"/>
                    </div>
                </div>
                <div class="row">
                    <div class="cols threecols" style="margin-left:20px">
                        <h4  style="margin-left:0px">Who to Follow</h4>
                        <asp:Repeater ID="Repeater3" runat="server">
                            <ItemTemplate> 
                                <tr>
                                    <td>
                                        <img class="all-tweet-image1" src="img/dp/purple.jpg"/>
                                        <div  class="abbu1">
                                            <a href='<%#string.Concat("~/OthersProfile.aspx?Email=", Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))+1,(Container.DataItem.ToString().Length-3)-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))))  %>' runat="server"><strong><asp:Label ID="Label1" runat="server" Text="<%# Container.DataItem.ToString().Substring(0,int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))) %>"></asp:Label></strong> </a>
                                            <br />
                                            <a href='<%#string.Concat("~/OthersProfile.aspx?Email=", Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))+1,(Container.DataItem.ToString().Length-3)-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))))  %>' runat="server" style="color:#A0A0A0;font-size:small"><strong><asp:Label ID="Label2" runat="server" Text="<%# Container.DataItem.ToString().Substring(int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))+1,(Container.DataItem.ToString().Length-3)-int.Parse(Container.DataItem.ToString().Substring(Container.DataItem.ToString().Length-2,2))) %>"></asp:Label></strong> </a>
                                            <br />
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="row">
                    <div class="cols threecols" style="margin-left:20px">
                        <small style="color:#808080">
                            © 2016 Twitter About Help Terms Privacy Cookies Adsinfo Brand Blog Status Apps Jobs Advertise Businesses MediaDevelopers
                        </small>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script>
        function checkTweetLength() {
            var str = document.getElementById('<%=Tweet.ClientID%>').value;
            if (str.length > 120)
            {
                str = str.substring(0, str.length - 1);
                document.getElementById('<%=Tweet.ClientID%>').value = str;
            }
        }
        $(".fancybox").fancybox({
            'href': '#popupID'
        });
    </script>
</asp:Content>
