<%@ Page Title="GridView ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GridViewODS.aspx.cs" Inherits="WebApp.SamplePages.GridViewODS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>GridView using ODS</h1>
    <div class="row">
        <div class="offset-1">
            <asp:GridView ID="AlbumList" runat="server"
                AutoGenerateColumns="False"
                DataSourceID="AlbumListODS" AllowPaging="True"
                 CssClass="table table-striped" GridLines="Horizontal" BorderStyle="None">

                <Columns>
                    <asp:BoundField DataField="AlbumId" HeaderText="Id" 
                        SortExpression="AlbumId"></asp:BoundField>
                    <asp:BoundField DataField="Title" HeaderText="Title" 
                        SortExpression="Title"></asp:BoundField>
                    <asp:BoundField DataField="ArtistId" HeaderText="Artist" 
                        SortExpression="ArtistId"></asp:BoundField>
                    <asp:BoundField DataField="ReleaseYear" HeaderText="Year" 
                        SortExpression="ReleaseYear"></asp:BoundField>
                    <asp:BoundField DataField="ReleaseLabel" HeaderText="Label" 
                        SortExpression="ReleaseLabel"></asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
                OldValuesParameterFormatString="original_{0}" 
                SelectMethod="Album_List" 
                TypeName="ChinookSystem.BLL.AlbumController">
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
