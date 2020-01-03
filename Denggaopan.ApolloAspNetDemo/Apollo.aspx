<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apollo.aspx.cs" Inherits="Denggaopan.ApolloAspNetDemo.Apollo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>apollo demo</h3>
    <form method="get">
    <p>key:<input type="text" class="input" name="key" value="<%=Key %>" /> value:<span class="red"><%=Value %></span></p>
    <p><input type="submit" class="btn btn-primary" value="OK" /></p>
    </form>
</asp:Content>
