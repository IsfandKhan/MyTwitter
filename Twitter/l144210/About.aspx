<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="l144210.About" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server"> 
    <title>About</title>
    <link rel="stylesheet" type="text/css" href="css\about.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <li><a href="Home.aspx" data-toggle="tab"><img src="img/logo/home.png" width="23" height="15"/> Home</a></li>
    <li><a onclick="document.getElementById('lightbox').style.display='inline';"  data-toggle="tab"><img src="img/logo/messages.png" width="23" height="15"/> Messages</a></li>
    <li style="margin-left:400px;"><img src="img\logo\logo.png" width="24" height="24" style="margin-top:15px;"/></li>
    <li style="float:right;margin-right:50px;"><a href="Welcome.aspx" data-toggle="tab"><img src="img/logo/logout.png" width="23" height="15"/> Logout</a></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form10" runat="server">
        <div class="cover">
            <div class="prof-pic-up">
            </div>
            <table>
                <tr>
                    <td>
                        <a class="overlay"></a>
                    </td>
                    <td>
                        <div class="overlay-info">
                            &nbsp
                            <span style="color:#808080;font-size:12px">TWEETS</span>
                            <br />
                            &nbsp&nbsp&nbsp&nbsp&nbsp  
                            <asp:Label ID="TotalTweets2" runat="server" Text="Label"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="profile-body">
            <div class="row">
                <div class="span3">
                    <div style="margin-left:-20px; margin-top:40px" class="cols threecols">
                        <b><a href="Profile.aspx" style="color:#000;font-size:x-large"><span><asp:Label ID="FullName2" runat="server" Text="UserName"></asp:Label></span></a></b>
                        <br /><a href="Profile.aspx" style="color:#A0A0A0;font-size:16px"><span><asp:Label ID="UserName2" runat="server" Text="@username"></asp:Label></span></a><br /><br />
                        <table class="table-data">
                            <tr>
                                <td><span><b>Gender </b></span></td>
                                <td><span><asp:Label ID="ShowGender" runat="server"></asp:Label></span></td>
                            </tr>
                            <tr>
                                <td><span><b>Phone  </b></span></td>
                                <td><span><asp:Label ID="ShowPhone" runat="server"></asp:Label></span></td>
                            </tr>
                            <tr>
                                <td><span><b>BirthDay </b></span></td>
                                <td><span><asp:Label ID="ShowDob" runat="server"></asp:Label></span></td>
                            </tr>
                            <tr>
                                <td><span><b>Description </b></span></td>
                                <td><span><asp:Label ID="ShowDesc" runat="server"></asp:Label></span></td>
                            </tr>
                        </table>                        
                    </div>
                </div>
                
                <div class="span8 cols threecols">
                    <center>
                        <h4>About <asp:Label ID="SettingName" runat="server" Text="UserName"></asp:Label></h4>
                        
                            Password<br />
                            <asp:TextBox ID="ChangePassword" runat="server" type="Password"></asp:TextBox><br />
                            Phone Number<br />
                            <asp:TextBox ID="ChangePhone" onchange="lengthContactNumber()" oninput="checkContactNumber();" runat="server"></asp:TextBox><br />
                            Date of Birth<br />
                            <asp:TextBox ID="ChangeDob" oninput="verifyDate();" onchange="checkDate();" runat="server"></asp:TextBox><br />
                            Notes<br />
                            <asp:TextBox ID="AboutYou" TextMode="MultiLine" Height="50" runat="server"></asp:TextBox><br />
                            <asp:Button ID="SaveSettings" runat="server" CssClass="save" Text="Save Settings" OnClick="AboutInfoChange"/>
                            <asp:Button ID="Deactivate" runat="server" CssClass="deactivate" OnClick="DeactivateAccount" Text="Deactivate Account"/>
                        
                    </center>
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
                        <div class="cols threecols">
                            <h4 >Top Trends</h4>
                            <asp:Repeater ID="rptResults2" runat="server">
                                <ItemTemplate> 
                                    <tr>
                                        <td><asp:HyperLink ID="TrendLink7" runat="server" NavigateUrl='<%#String.Concat("~/Trends.aspx?TrendName=", Container.DataItem.ToString().Substring(1,Container.DataItem.ToString().Length-1))%>' Text="<%# Container.DataItem %>"></asp:HyperLink><br /></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row">
                        <div class="cols threecols">
                            <small style="color:#808080">
                                © 2016 Twitter About Help Terms Privacy Cookies Adsinfo Brand Blog Status Apps Jobs Advertise Businesses MediaDevelopers
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
</asp:Content>
