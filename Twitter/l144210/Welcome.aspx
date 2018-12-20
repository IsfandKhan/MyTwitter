<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="l144210.Welcome" %>
<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server"> 
    <title>Welcome to Twitter - Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <li><a href="welcome.aspx"><img src="img/logo/logo.png" width="23" height="15"/>&nbsp Home</a></li>
    <li style="float:right;margin-right:50px;"><a href="#">Language: English</a></li> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ul class="cb-slideshow">
        <li><span>Background1</span></li>
        <li><span>Background2</span></li>
        <li><span>Background3</span></li>
        <li><span>Background4</span></li>
        <li><span>Background5</span></li>
        <li><span>Background6</span></li>
    </ul>

    <div class="card">
        <div class="row">
            <div class="span7 article">
                <h1>
                    Welcome to Twitter.
                </h1>
                
                <p1>Connect with your friends — and other fascinating people. 
                    Get in-the-moment updates on the things that interest you. 
                    And watch events unfold, in real time, from every angle.
                </p1>
                <br /><br /><br /><br /><br /><br /><br />
                <p2>
                    Quote of the Day <br/>
                    "The most beautiful thing in life is, indeed, life itself", Tom Hanks in Cast Away(2000)
                    <br /><br /><br /><br />
                    <i>~ La Vita e Bella</i>  
                </p2>
            </div>
            <div class="span1">
            </div>
            <div class="span3">
                <form id="form2" runat="server">
                    <div class="login-signup front-signin"> 
                        <span style="color:red;padding:0px"><asp:Label ID="WrongLogin" runat="server"></asp:Label></span>
                        <asp:TextBox id="user" runat="server" CssClass="input" placeholder="Phone, email or username" />                           
                        <asp:TextBox id="pass" type="password" placeholder="Password" CssClass="input" style="width:150px" runat="server" /> &nbsp&nbsp&nbsp
                        <asp:Button ID="Login" Text="Login" runat="server" CssClass="login-button" OnClick="btnLogin" />
                    </div>
                    <div class="login-signup front-signup">
                        New to Twitter? <span class="signup-link">Sign Up</span><br /><br />
                        <asp:TextBox id="name" runat="server" CssClass="input" placeholder="Full name" />                            
                        <asp:TextBox id="email" runat="server" CssClass="input" placeholder="Email" />
                        <asp:TextBox id="newpass" runat="server" CssClass="input" type="password" placeholder="Password" />
                        <span style="color:red"><asp:Label ID="WrongSignup" runat="server"></asp:Label></span><br />
                        <asp:Button ID="SignUp" Text="Sign up for Twitter" runat="server" CssClass="signup-button" OnClick="btnSignUP" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div id="footer">
	    <center>(c) Twitter. All rights reserved. Design by <span style="color:#3db6c7">3 Amigos</span></center>
    </div>    
</asp:Content>
